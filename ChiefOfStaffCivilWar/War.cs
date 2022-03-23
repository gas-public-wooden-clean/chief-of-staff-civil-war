using System;
using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	class War
	{
		public War(Random random, int numTheaters)
		{
			IEnumerable<string> transMississippiLocations = new string[]
			{
				"St. Louis, MO", "Rolle, MO", "Springfield, MO", "Fayetteville, AR", "Little Rock, AR"
			};
			IEnumerable<int> transMississippiDistances = new int[]
			{
				3, 4, 4, 6
			};
			TeamTheater transMississippiUnion = new TeamTheater(random, 2, 1, 2, new string[] { "Win the game." });
			TeamTheater transMississippiConfederacy = new TeamTheater(random, 2, 2, 2, new string[] { "Win the game." });
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
