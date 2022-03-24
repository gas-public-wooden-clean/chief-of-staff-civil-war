using System;
using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	class War
	{
		public War(Random random, NameGenerator namer, int numTheaters)
		{
			TeamTheater transMississippiUnion = new TeamTheater(random, 2, 1, 2, new string[] { "Win the game." });
			transMississippiUnion.AddGeneral(new General(random, namer, 1));

			TeamTheater transMississippiConfederacy = new TeamTheater(random, 2, 2, 2, new string[] { "Win the game." });
			for (int i = 0; i < 3; i++)
			{
				transMississippiConfederacy.AddGeneral(new General(random, namer, 1));
			}

			IEnumerable<string> transMississippiLocations = new string[]
			{
				"St. Louis, MO", "Rolle, MO", "Springfield, MO", "Fayetteville, AR", "Little Rock, AR"
			};
			IEnumerable<int> transMississippiDistances = new int[]
			{
				3, 4, 4, 6
			};
			List<Theater> theaters = new List<Theater>
			{
				new Theater("Trans-Mississippi", transMississippiLocations, transMississippiDistances, transMississippiUnion, transMississippiConfederacy)
			};
			if (numTheaters > 1)
			{
			}
			if (numTheaters > 2)
			{
			}
			if (numTheaters > 3)
			{
			}
			if (numTheaters > 4)
			{
			}
			if (numTheaters > 5)
			{
			}
			if (numTheaters > 6)
			{
			}
			Theaters = theaters;

			Year = 1861;
			Month = 4;
		}

		public IReadOnlyList<Theater> Theaters
		{
			get;
		}

		public int Year
		{
			get;
			private set;
		}

		public int Month
		{
			get;
			private set;
		}

		public string MonthName
		{
			get
			{
				switch (Month)
				{
					case 1:
						return "January";
					case 2:
						return "February";
					case 3:
						return "March";
					case 4:
						return "April";
					case 5:
						return "May";
					case 6:
						return "June";
					case 7:
						return "July";
					case 8:
						return "August";
					case 9:
						return "September";
					case 10:
						return "October";
					case 11:
						return "November";
					default:
						return "December";
				}
			}
		}

		/// <summary>
		/// Turns remaining after the current turn.
		/// </summary>
		public int TurnsRemaining => 12 * (1865 - Year) - Month;

		public bool IsGameOver => Year >= 1865;

		public void AdvanceTime()
		{
			Month += 1;
			if (Month > 12)
			{
				Month = 1;
				Year += 1;
			}
		}
	}
}
