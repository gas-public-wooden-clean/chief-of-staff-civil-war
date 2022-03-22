using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace ChiefOfStaffCivilWar
{
	class Program
	{
		static void Main(string[] args)
		{
			Random random = new Random();
			TeamController union = HumanPlayer.GetInstance();
			TeamController confederacy = new ComputerTeam(random);
			int numTheatres = 7;

			int selection;
			do
			{
				Console.Error.WriteLine("Select an option.");
				Console.Error.WriteLine("1: Begin.");
				Console.Error.WriteLine("2: Set Union player.");
				Console.Error.WriteLine("3: Set Confederacy player.");
				Console.Error.WriteLine("4: Set number of theatres.");
				Console.Error.WriteLine("5: Quit.");
				selection = HumanPlayer.GetInt32(1, 5);

				switch (selection)
				{
					case 1:
						Play(random, union, confederacy, numTheatres);
						break;
					case 2:
						union = GetTeamController(random);
						break;
					case 3:
						confederacy = GetTeamController(random);
						break;
					case 4:
						Console.Write("How many theatres of war? ");
						numTheatres = HumanPlayer.GetInt32(1, 7);
						break;
					default:
						break;
				}
			} while (selection < 5);
		}

		static void Play(Random random, TeamController unionController, TeamController confederacyController, int numTheatres)
		{
			Team union = Team.SetupUnion(random);
			Team confederacy = Team.SetupConfederacy(random);

			CancellationTokenSource canceller = new CancellationTokenSource();

			for (War war = new War(numTheatres); !war.IsGameOver; war.AdvanceTime())
			{

			}
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
