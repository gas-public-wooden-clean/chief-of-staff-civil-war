using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	class Theater
	{
		public Theater(string name, IEnumerable<string> locations, IEnumerable<int> distances, TeamTheater union, TeamTheater confederacy)
		{
			Name = name;
			_locations = new List<string>(locations);
			_distances = new List<int>(distances);
			_union = union;
			_confederacy = confederacy;
		}

		readonly IList<string> _locations;
		readonly IList<int> _distances;
		readonly TeamTheater _union;
		readonly TeamTheater _confederacy;

		public string Name
		{
			get;
		}

		public TeamTheater GetSide(bool isUnion)
		{
			return isUnion ? _union : _confederacy;
		}
	}
}
