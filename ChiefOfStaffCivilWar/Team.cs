using System;
using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	class Team
	{
		public static Team SetupUnion(Random random)
		{
			Dictionary<int, IEnumerable<string>> history = new Dictionary<int, IEnumerable<string>>()
			{
				{ 1861, new string[] {"Stuff happens."} },
				{ 1862, new string[] {"Stuff happens."} },
				{ 1863, new string[] {"Stuff happens."} },
				{ 1864, new string[] {"Stuff happens."} },
				{ 1865, new string[] {"Stuff happens."} }
			};
			return new Team(random, history);
		}

		public static Team SetupConfederacy(Random random)
		{
			Dictionary<int, IEnumerable<string>> history = new Dictionary<int, IEnumerable<string>>()
			{
				{ 1861, new string[] {"Stuff happens."} },
				{ 1862, new string[] {"Stuff happens."} },
				{ 1863, new string[] {"Stuff happens."} },
				{ 1864, new string[] {"Stuff happens."} },
				{ 1865, new string[] {"Stuff happens."} }
			};
			return new Team(random, history);
		}

		Team(Random random, IReadOnlyDictionary<int, IEnumerable<string>> historyByYear)
		{
			_activeMissions = new List<string>();

			// Shuffle history decks.
			Dictionary<int, IList<string>> historyDecks = new Dictionary<int, IList<string>>();
			foreach (KeyValuePair<int, IEnumerable<string>> year in historyByYear)
			{
				IList<string> deckYear = new List<string>(new Deck<string>(random, year.Value));
				historyDecks.Add(year.Key, deckYear);
			}
			_historyByYear = historyDecks;
		}

		readonly IReadOnlyDictionary<int, IList<string>> _historyByYear;
		readonly IList<string> _activeMissions;

		public string GetHistory(int year)
		{
			IList<string> history = _historyByYear[year];
			if (history.Count < 1)
			{
				return null;
			}

			string retval = history[history.Count - 1];
			history.RemoveAt(history.Count - 1);
			return retval;
		}
	}
}
