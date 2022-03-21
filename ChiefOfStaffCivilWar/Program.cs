using System;
using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	class Program
	{
		static void Main(string[] args)
		{
			Random random = new Random();
			TeamController union = HumanPlayer.GetInstance();
			TeamController confederacy = new ComputerTeam();
			int numTheatres = 7;

			int selection;
			do
			{
				Console.Error.WriteLine("Select an option.");
				Console.Error.WriteLine("1: Begin.");
				Console.Error.WriteLine("2: Set Union player.");
				Console.Error.WriteLine("3: Set Confederacy player.");
				Console.Error.WriteLine("4: Set number of theatres.");
				selection = HumanPlayer.GetInt32(5);

				switch (selection)
				{
					case 1:
						Play(random, union, confederacy, numTheatres);
						break;
					case 2:
						union = GetTeamController();
						break;
					case 3:
						confederacy = GetTeamController();
						break;
					case 4:
						Console.Write("How many theatres of war? ");
						numTheatres = HumanPlayer.GetInt32(7);
						break;
					default:
						// Quit.
						break;
				}
			} while (selection < 5);
		}

		static void Play(Random random, TeamController unionController, TeamController confederacyController, int numTheatres)
		{
			Team union = Team.SetupUnion(random);
			Team confederacy = Team.SetupConfederacy(random);

			for (War war = new War(numTheatres); !war.IsGameOver; war.AdvanceTime())
			{

			}
		}

		static TeamController GetTeamController()
		{
			Console.Error.WriteLine("Select player type.");
			Console.Error.WriteLine("1: Local Human.");
			Console.Error.WriteLine("2: Network Human.");
			Console.Error.WriteLine("3: Computer.");
			switch (HumanPlayer.GetInt32(3))
			{
				case 1:
					return HumanPlayer.GetInstance();
				case 2:
					return HumanPlayer.GetInstance();
				default:
					return new ComputerTeam();
			}
		}
	}
}
