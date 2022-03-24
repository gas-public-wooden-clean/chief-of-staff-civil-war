using System;
using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	class TeamTheater
	{
		public TeamTheater(Random random, int supply, int recruit, int leadershipCost, IEnumerable<string> missions)
		{
			Supply = supply;
			Recruit = recruit;
			LeadershipCost = leadershipCost;
			_missionDeck = new List<string>(new Deck<string>(random, missions));
			_activeMissions = new List<string>();
			_generals = new List<General>();
		}

		readonly IList<string> _missionDeck;
		readonly IList<string> _activeMissions;
		readonly IList<General> _generals;

		public string TheaterCard { get; set; }

		public int Supply { get; }

		public int Recruit { get; }

		public int LeadershipCost { get; }

		public IEnumerable<string> ActiveMissions => _activeMissions;

		public IEnumerable<General> Generals => _generals;

		public string DrawMission()
		{
			if (_missionDeck.Count < 1)
			{
				return null;
			}

			string retval = _missionDeck[_missionDeck.Count - 1];
			_missionDeck.RemoveAt(_missionDeck.Count - 1);
			_activeMissions.Add(retval);
			return retval;
		}

		public void AddGeneral(General general)
		{
			_generals.Add(general);
		}
	}
}
