using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace ChiefOfStaffCivilWar
{
	class HumanPlayer : TeamController
	{
		HumanPlayer() { }

		static readonly HumanPlayer _instance = new HumanPlayer();

		public static HumanPlayer GetInstance()
		{
			return _instance;
		}

		public static int GetInt32(int min, int max)
		{
			int selection;
			while (!TryGetInt32(out selection) ||
				selection < min || selection > max)
			{
				Console.Out.Write("Invalid option. Try again: ");
			}

			return selection;
		}

		static bool TryGetInt32(out int entry)
		{
			string input = Console.ReadLine();
			return int.TryParse(input, NumberStyles.None, CultureInfo.CurrentCulture, out entry);
		}

		public Task SendMessage(string message, CancellationToken cancellationToken)
		{
			Console.Out.Write(message.Replace("\n", Environment.NewLine));
			return Task.CompletedTask;
		}

		public Task SendMessage(string format, CancellationToken cancellationToken, params object[] args)
		{
			return SendMessage(string.Format(CultureInfo.CurrentCulture, format, args), cancellationToken);
		}

		public Task<int> GetMenuOption(int max, CancellationToken cancellationToken)
		{
			return Task.FromResult(GetInt32(1, max));
		}
	}
}
