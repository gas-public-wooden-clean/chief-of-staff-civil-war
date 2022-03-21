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
			List<Theatre> theatres = new List<Theatre>
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
			Theatres = theatres;

			_year = 1861;
			_month = 7;
		}

		public IReadOnlyList<Theatre> Theatres
		{
			get;
		}

		int _year;
		int _month;

		public bool IsGameOver => _year < 1865 || _month < 4;

		public void AdvanceTime()
		{
			_month += 1;
			if (_month > 12)
			{
				_month = 1;
				_year += 1;
			}
		}
	}
}
