using System;
using System.Globalization;

namespace ChiefOfStaffCivilWar
{
	class HumanPlayer : TeamController
	{
		private HumanPlayer() { }

		static readonly HumanPlayer _instance = new HumanPlayer();

		public static HumanPlayer GetInstance()
		{
			return _instance;
		}

		public static int GetInt32(int max)
		{
			int selection;
			while (!TryGetInt32(out selection) ||
				selection < 1 || selection > max)
			{
				Console.Out.Write("Invalid option. Try again: ");
			}

			return selection;
		}

		private static bool TryGetInt32(out int entry)
		{
			string input = Console.ReadLine();
			return int.TryParse(input, NumberStyles.None, CultureInfo.CurrentCulture, out entry);
		}
	}
}
