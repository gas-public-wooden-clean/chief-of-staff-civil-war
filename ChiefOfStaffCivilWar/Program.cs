using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ChiefOfStaffCivilWar
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Random random = new Random();
			TeamController union = HumanPlayer.GetInstance();
			TeamController confederacy = new ComputerTeam(random);
			int numTheaters = 7;

			int selection;
			do
			{
				Console.Error.WriteLine("Select an option.");
				Console.Error.WriteLine("1: Begin.");
				Console.Error.WriteLine("2: Set Union player.");
				Console.Error.WriteLine("3: Set Confederacy player.");
				Console.Error.WriteLine("4: Set number of theaters.");
				Console.Error.WriteLine("5: Quit.");
				selection = HumanPlayer.GetInt32(1, 5);

				switch (selection)
				{
					case 1:
						await Play(random, union, confederacy, numTheaters);
						break;
					case 2:
						union = GetTeamController(random);
						break;
					case 3:
						confederacy = GetTeamController(random);
						break;
					case 4:
						Console.Write("How many theaters of war? ");
						numTheaters = HumanPlayer.GetInt32(1, 7);
						break;
					default:
						break;
				}
			} while (selection < 5);
		}

		static async Task Play(Random random, TeamController unionController, TeamController confederacyController, int numTheaters)
		{
			Team union = Team.SetupUnion(random);
			Team confederacy = Team.SetupConfederacy(random);

			NameGenerator namer = new NameGenerator(random);

			CancellationTokenSource canceller = new CancellationTokenSource();

			for (War war = new War(random, namer, numTheaters); !war.IsGameOver; war.AdvanceTime())
			{
				foreach (Theater theater in war.Theaters)
				{
					foreach (Team side in new Team[] { union, confederacy })
					{
						TeamTheater teamTheater = theater.GetSide(side.IsUnion);
						side.TheaterDiscard.Add(teamTheater.TheaterCard);
						teamTheater.TheaterCard = null;
					}
				}

				Task unionTurn = Turn(random, unionController, war, union, canceller.Token);
				Task confederacyTurn = Turn(random, confederacyController, war, confederacy, canceller.Token);

				Task finishedFirst = await Task.WhenAny(unionTurn, confederacyTurn);
				if (unionTurn == finishedFirst)
				{
					await unionController.SendMessage("Waiting for other player...\n", canceller.Token);
					await confederacyTurn;
				}
				else
				{
					await confederacyController.SendMessage("Waiting for other player...\n", canceller.Token);
					await unionTurn;
				}
			}
		}

		static async Task Turn(Random random, TeamController controller, War war, Team team, CancellationToken cancellationToken)
		{
			await controller.SendMessage("{0} {1}: {2} turns remaining (after this one).\n", cancellationToken, war.MonthName, war.Year, war.TurnsRemaining);

			if (war.Month is 1)
			{
				foreach (Theater theater in war.Theaters)
				{
					TeamTheater teamTheater = theater.GetSide(team.IsUnion);
					string mission = teamTheater.DrawMission();
					if (mission is not null)
					{
						await controller.SendMessage("You have a new mission in the {0} theater: {1}\n", cancellationToken, theater.Name, mission);
					}
				}
			}

			string history = team.GetHistory(war.Year);
			if (history is not null)
			{
				await controller.SendMessage("History: {0}.\n", cancellationToken, history);
			}

			IList<bool> playedTheaters = new List<bool>(war.Theaters.Count);
			for (int i = 0; i < war.Theaters.Count; i++)
			{
				playedTheaters.Add(false);
			}

			int selected;
			do
			{
				await controller.SendMessage("What would you like to do?\n", cancellationToken);
				await controller.SendMessage("1. View summary.\n", cancellationToken);
				await controller.SendMessage("2. View theater.\n", cancellationToken);
				await controller.SendMessage("3. Play a theater card.\n", cancellationToken);
				await controller.SendMessage("4. Command armies.\n", cancellationToken);
				await controller.SendMessage("5. End turn.\n", cancellationToken);
				selected = await controller.GetMenuOption(5, cancellationToken);
				switch (selected)
				{
					case 1:
						await ViewSummary(controller, war, team, cancellationToken);
						break;
					case 2:
						await ViewTheater(controller, war, team.IsUnion, cancellationToken);
						break;
					case 3:
						await PlayTheaterCard(controller, war, team, playedTheaters, cancellationToken);
						break;
					case 4:
						break;
					default:
						if (playedTheaters.Contains(false))
						{
							await controller.SendMessage("You haven't played a theater card in every theater. Are you sure?\n", cancellationToken);
							await controller.SendMessage("1. No.\n", cancellationToken);
							await controller.SendMessage("2. Yes.\n", cancellationToken);
							int confirmation = await controller.GetMenuOption(2, cancellationToken);
							if (confirmation is not 2)
							{
								selected = 0;
							}
						}
						break;
				}
			} while (selected != 5);
		}

		static async Task ViewSummary(TeamController controller, War war, Team team, CancellationToken cancellationToken)
		{
			await controller.SendMessage("{0} {1}: {2} turns remaining (after this one).\n", cancellationToken, war.MonthName, war.Year, war.TurnsRemaining);
			await controller.SendMessage("1. View theater hand.\n", cancellationToken);
			await controller.SendMessage("2. View theater discard.\n", cancellationToken);
			await controller.SendMessage("3. Back.\n", cancellationToken);
			int selection = await controller.GetMenuOption(3, cancellationToken);
			switch (selection)
			{
				case 1:
					foreach (string theater in team.TheaterHand)
					{
						await controller.SendMessage(theater + "\n", cancellationToken);
					}
					break;
				case 2:
					foreach (string theater in team.TheaterDiscard)
					{
						await controller.SendMessage(theater + "\n", cancellationToken);
					}
					break;
				default:
					break;
			}
		}

		static async Task ViewTheater(TeamController controller, War war, bool isUnion, CancellationToken cancellationToken)
		{
			await controller.SendMessage("Which theater would you like to view?\n", cancellationToken);
			for (int i = 0; i < war.Theaters.Count; i++)
			{
				await controller.SendMessage("{0}. {1}.\n", cancellationToken, i + 1, war.Theaters[i].Name);
			}

			int selection = await controller.GetMenuOption(war.Theaters.Count, cancellationToken);
			Theater theater = war.Theaters[selection - 1];
			TeamTheater teamTheater = theater.GetSide(isUnion);

			await controller.SendMessage("Monthly Supply: {0}.\n", cancellationToken, teamTheater.SupplyIncome);
			await controller.SendMessage("Monthly Recruits: {0}.\n", cancellationToken, teamTheater.RecruitIncome);
			await controller.SendMessage("Assign General Surcharge: ${0}.\n", cancellationToken, teamTheater.LeadershipCost);
			await controller.SendMessage("Played theater card: {0}.\n", cancellationToken, teamTheater.TheaterCard ?? "None");
			await controller.SendMessage("Missions:\n", cancellationToken);
			foreach (string mission in teamTheater.ActiveMissions)
			{
				await controller.SendMessage(mission + "\n", cancellationToken);
			}
			await controller.SendMessage("Generals:\n", cancellationToken);
			foreach (General general in teamTheater.Generals)
			{
				await controller.SendMessage(general.Name.ToString() + "\n", cancellationToken);
			}
			await controller.SendMessage("Regiments:\n", cancellationToken);
			foreach (Regiment regiment in teamTheater.Regiments)
			{
				await controller.SendMessage(regiment.ToString() + "\n", cancellationToken);
			}
		}

		static async Task PlayTheaterCard(TeamController controller, War war, Team team, IList<bool> playedTheaters, CancellationToken cancellationToken)
		{
			await controller.SendMessage("Which theater would you like to play in?\n", cancellationToken);
			for (int i = 0; i < war.Theaters.Count; i++)
			{
				string format = "{0}. {1}";
				if (playedTheaters[i])
				{
					format += " (Already played)";
				}
				await controller.SendMessage(format + ".\n", cancellationToken, i + 1, war.Theaters[i].Name);
			}
			await controller.SendMessage("{0}. Back.\n", cancellationToken, war.Theaters.Count + 1);

			int theaterNumber = await controller.GetMenuOption(war.Theaters.Count + 1, cancellationToken);
			if (theaterNumber == war.Theaters.Count + 1)
			{
				return;
			}
			if (playedTheaters[theaterNumber - 1])
			{
				await controller.SendMessage("You've already played a theater card there this turn.\n", cancellationToken);
				return;
			}

			await controller.SendMessage("Which card would you like to play?\n", cancellationToken);
			for (int i = 0; i < team.TheaterHand.Count; i++)
			{
				await controller.SendMessage("{0}. {1}.\n", cancellationToken, i + 1, team.TheaterHand[i]);
			}
			await controller.SendMessage("{0}. Back.\n", cancellationToken, team.TheaterHand.Count + 1);
			int cardNumber = await controller.GetMenuOption(team.TheaterHand.Count + 1, cancellationToken);
			if (cardNumber == team.TheaterHand.Count + 1)
			{
				return;
			}
			team.PlayTheater(cardNumber - 1, war.Theaters[theaterNumber - 1]);
			playedTheaters[theaterNumber - 1] = true;
		}

		static TeamController GetTeamController(Random random)
		{
			Console.Error.WriteLine("Select player type.");
			Console.Error.WriteLine("1: Local Human.");
			Console.Error.WriteLine("2: Network Human.");
			Console.Error.WriteLine("3: Computer.");
			switch (HumanPlayer.GetInt32(1, 3))
			{
				case 1:
					return HumanPlayer.GetInstance();
				case 2:
					TcpListener listener = GetNetworkListener();
					return new NetworkPlayer(listener);
				default:
					return new ComputerTeam(random);
			}
		}

		static TcpListener GetNetworkListener()
		{
			Console.Error.WriteLine("Select a port to accept connections on or enter 0 to automatically select an available port.");
			int port = HumanPlayer.GetInt32(0, ushort.MaxValue);
			TcpListener listener = TcpListener.Create(port);
			listener.Start(1);
			if (port == 0)
			{
				Console.Error.WriteLine("Listening on port {0}.", ((IPEndPoint)listener.LocalEndpoint).Port);
			}
			return listener;
		}
	}
}
