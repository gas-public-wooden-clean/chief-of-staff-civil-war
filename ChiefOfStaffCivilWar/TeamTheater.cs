using System;
using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	class TeamTheater
	{
		public TeamTheater(Random random, int supply, int recruit, int leadershipCost, IEnumerable<string> missions)
		{
			SupplyIncome = supply;
			RecruitIncome = recruit;
			LeadershipCost = leadershipCost;
			_missionDeck = new List<string>(new Deck<string>(random, missions));
			_activeMissions = new List<string>();
			_generals = new List<General>();
			Regiments = new List<Regiment>();
		}

		readonly IList<string> _missionDeck;
		readonly IList<string> _activeMissions;
		readonly IList<General> _generals;

		public string TheaterCard { get; set; }

		public int SupplyIncome { get; }

		public int Supplies { get; }

		public int RecruitIncome { get; }

		public int Recruits { get; }

		public int LeadershipCost { get; }

		public IEnumerable<string> ActiveMissions => _activeMissions;

		public IEnumerable<General> Generals => _generals;

		public ICollection<Regiment> Regiments { get; }

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
