using System;
using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	class Team
	{
		public static Team SetupUnion(Random random)
		{
			IEnumerable<string> missions = new string[] { "Win the game." };
			Dictionary<int, IEnumerable<string>> history = new Dictionary<int, IEnumerable<string>>()
			{
				{ 1861, new string[] {"Stuff happens."} },
				{ 1862, new string[] {"Stuff happens."} },
				{ 1863, new string[] {"Stuff happens."} },
				{ 1864, new string[] {"Stuff happens."} },
				{ 1865, new string[] {"Stuff happens."} }
			};
			return new Team(random, missions, history);
		}

		public static Team SetupConfederacy(Random random)
		{
			IEnumerable<string> missions = new string[] { "Win the game." };
			Dictionary<int, IEnumerable<string>> history = new Dictionary<int, IEnumerable<string>>()
			{
				{ 1861, new string[] {"Stuff happens."} },
				{ 1862, new string[] {"Stuff happens."} },
				{ 1863, new string[] {"Stuff happens."} },
				{ 1864, new string[] {"Stuff happens."} },
				{ 1865, new string[] {"Stuff happens."} }
			};
			return new Team(random, missions, history);
		}

		private Team(Random random, IEnumerable<string> missions, IReadOnlyDictionary<int, IEnumerable<string>> historyByYear)
		{
			// Shuffle mission deck.
			_missionDeck = new Deck<string>(random, missions);

			// Shuffle history decks.
			Dictionary<int, Deck<string>> historyDecks = new Dictionary<int, Deck<string>>();
			foreach (KeyValuePair<int, IEnumerable<string>> year in historyByYear)
			{
				Deck<string> deckYear = new Deck<string>(random, year.Value);
				historyDecks.Add(year.Key, deckYear);
			}
			_historyByYear = historyDecks;

			_controller = controller;
		}

		readonly Deck<string> _missionDeck;
		readonly IReadOnlyDictionary<int, Deck<string>> _historyByYear;
	}
}
