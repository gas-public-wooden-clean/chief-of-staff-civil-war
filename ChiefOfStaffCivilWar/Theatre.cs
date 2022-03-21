using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	class Theatre
	{
		public Theatre(IEnumerable<string> locations, IEnumerable<int> distances, int unionSupply, int unionRecruit, int unionLeadershipCost, int confederacySupply, int confederacyRecruit, int confederacyLeadershipCost)
		{
			_locations = new List<string>(locations);
			_distances = new List<int>(distances);
			_unionSupply = unionSupply;
			_unionRecruit = unionRecruit;
			_unionLeadershipCost = unionLeadershipCost;
			_confederacySupply = confederacySupply;
			_confederacyRecruit = confederacyRecruit;
			_confederacyLeadershipCost = confederacyLeadershipCost;
		}

		readonly IList<string> _locations;
		readonly IList<int> _distances;
		readonly int _unionSupply;
		readonly int _unionRecruit;
		readonly int _unionLeadershipCost;
		readonly int _confederacySupply;
		readonly int _confederacyRecruit;
		readonly int _confederacyLeadershipCost;
	}
}
