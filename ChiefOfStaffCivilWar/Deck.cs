using System;
using System.Collections;
using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	public class Deck<T> : IEnumerable<T>
	{
		readonly IList<T> _deck;

		public Deck(Random generator, IEnumerable<T> items)
		{
			_deck = new List<T>(items);
			// Use the Fisher-Yates shuffle algorithm to randomize the order of the items.
			for (int i = _deck.Count - 1; i > 0; i--)
			{
				int j = generator.Next(i + 1);
				T temp = _deck[j];
				_deck[j] = _deck[i];
				_deck[i] = temp;
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _deck.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
