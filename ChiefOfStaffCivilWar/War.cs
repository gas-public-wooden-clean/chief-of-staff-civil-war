using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	class War
	{
		public War(int numTheatres)
		{
			IEnumerable<string> transMississippiLocations = new string[]
			{
				"St. Louis, MO", "Rolle, MO", "Springfield, MO", "Fayetteville, AR", "Little Rock, AR"
			};
			IEnumerable<int> transMississippiDistances = new int[]
			{
				3, 4, 4, 6
			};
			_theatres = new List<Theatre>
			{
				new Theatre(transMississippiLocations, transMississippiDistances, 2, 1, 2, 2, 2, 2)
			};
			if (numTheatres > 1)
			{
			}
			if (numTheatres > 2)
			{
			}
			if (numTheatres > 3)
			{
			}
			if (numTheatres > 4)
			{
			}
			if (numTheatres > 5)
			{
			}
			if (numTheatres > 6)
			{
			}

			_year = 1861;
			_month = 7;
		}

		readonly IList<Theatre> _theatres;
		readonly int _year;
		readonly int _month;
	}
}
