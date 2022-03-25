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
				{ 1861, new string[]
					{
						"Lincoln's Call for Volunteers",
						"Generals recalled from Duty Out West",
						"Ericsson's Folly",
						"Ellsworth's Death",
						"Enlistments Expire",
						"St. Louis Massacre",
						"On to Richmond!",
						"State Militias Federalized",
						"Confiscation Act",
						"First Income Tax",
						"Division Army Organization",
						"Washington D.C. Fortifications",
						"Van Wyck Report",
						"Trent Affair",
						"Martial Law in Maryland"
					}
				},
				{ 1862, new string[] {"Stuff happens."} },
				{ 1863, new string[] {"Stuff happens."} },
				{ 1864, new string[] {"Stuff happens."} },
				{ 1865, new string[] {"Stuff happens."} }
			};
			IEnumerable<string> theater = new string[]
			{
				"Naval/Riverine Transport",
				"Recruits!",
				"Rapid Mobilization",
				"Political Pressure",
				"Transfer Materials",
				"Force March",
				"Transfer Troops",
				"Political Appointment",
				"Poor Morale",
				"Forage",
				"Drill",
				"Taxes",
				"Construct Depot"
			};

			return new Team(random, true, history, theater);
		}

		public static Team SetupConfederacy(Random random)
		{
			Dictionary<int, IEnumerable<string>> history = new Dictionary<int, IEnumerable<string>>()
			{
				{ 1861, new string[] {"Stuff"} },
				{ 1862, new string[] {"Stuff"} },
				{ 1863, new string[] {"Stuff"} },
				{ 1864, new string[] {"Stuff"} },
				{ 1865, new string[] {"Stuff"} }
			};
			IEnumerable<string> theater = new string[]
			{
			};

			return new Team(random, false, history, theater);
		}

		Team(Random random, bool isUnion, IReadOnlyDictionary<int, IEnumerable<string>> historyByYear, IEnumerable<string> theaterDeck)
		{
			IsUnion = isUnion;

			_theaterHand = new List<string>(new Deck<string>(random, theaterDeck));
			_theaterDiscard = new List<string>();

			// Shuffle history decks.
			// TODO: At the end of each year, undrawn cards are shuffled into the next year's deck.
			Dictionary<int, IList<string>> historyDecks = new Dictionary<int, IList<string>>();
			foreach (KeyValuePair<int, IEnumerable<string>> year in historyByYear)
			{
				IList<string> deckYear = new List<string>(new Deck<string>(random, year.Value));
				historyDecks.Add(year.Key, deckYear);
			}
			_historyByYear = historyDecks;
		}

		readonly IReadOnlyDictionary<int, IList<string>> _historyByYear;
		List<string> _theaterHand;
		List<string> _theaterDiscard;

		public bool IsUnion { get; }

		public ICollection<string> TheaterDiscard => _theaterDiscard;

		public IReadOnlyList<string> TheaterHand => _theaterHand;

		public void PlayTheater(int theaterIndex, Theater target)
		{
			string toPlay = _theaterHand[theaterIndex];
			_theaterHand.RemoveAt(theaterIndex);

			if (_theaterHand.Count is 0)
			{
				List<string> temp = _theaterDiscard;
				_theaterDiscard = _theaterHand;
				_theaterHand = temp;
			}

			TeamTheater targetTeam = target.GetSide(IsUnion);
			targetTeam.TheaterCard = toPlay;
		}

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
