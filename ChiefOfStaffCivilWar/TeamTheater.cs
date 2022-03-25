using System;
using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	class TeamTheater
	{
		public TeamTheater(Random random, int supply, int recruit, int leadershipCost, IEnumerable<string> missions, WeightedOutcomes<string> regimentOrigins)
		{
			_random = random;
			SupplyIncome = supply;
			RecruitIncome = recruit;
			LeadershipCost = leadershipCost;
			_missionDeck = new List<string>(new Deck<string>(random, missions));
			_regimentOrigins = regimentOrigins;
			_activeMissions = new List<string>();
			_generals = new List<General>();
			_regiments = new List<Regiment>();
		}

		readonly Random _random;
		readonly IList<string> _missionDeck;
		readonly IList<string> _activeMissions;
		readonly IList<Regiment> _regiments;
		readonly IList<General> _generals;
		readonly WeightedOutcomes<string> _regimentOrigins;

		public string TheaterCard { get; set; }

		public int SupplyIncome { get; }

		public int Supplies { get; }

		public int RecruitIncome { get; }

		public int Recruits { get; }

		public int LeadershipCost { get; }

		public IEnumerable<string> ActiveMissions => _activeMissions;

		public IEnumerable<General> Generals => _generals;

		public IEnumerable<Regiment> Regiments => _regiments;

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

		public void AddRegiment(int level)
		{
			_regiments.Add(new Regiment(_regimentOrigins.Get(_random), level));
		}
	}
}
