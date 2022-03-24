using System;
using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	class General : IComparable<General>
	{
		public General(Random random, NameGenerator namer, int stars)
		{
			Name = namer.GetName();
			Politics = random.Next(8);
			Offense = random.Next(8);
			Defense = random.Next(8);
			Command = random.Next(8);
			Speed = random.Next(8);
			Efficiency = random.Next(8);
			Stars = stars;
			Seniority = random.NextDouble();

			IEnumerable<int> stats = new int[]
			{
				Politics,
				Offense,
				Defense,
				Command,
				Speed,
				Efficiency
			};

			// Add one random trait to a general for each stat number that is a duplicate of one that was previously determined for this general.
			int numTraits = 0;
			for (int i = 0; i < 8; i++)
			{
				int occurrences = 0;
				foreach (int stat in stats)
				{
					if (stat == i)
					{
						occurrences += 1;
					}
				}
				numTraits += occurrences - 1;
			}
		}

		public Name Name
		{
			get;
		}

		public int Politics
		{
			get;
		}

		public int Offense
		{
			get;
		}

		public int Defense
		{
			get;
		}

		public int Command
		{
			get;
		}

		public int Speed
		{
			get;
		}

		public int Efficiency
		{
			get;
		}

		public int Stars
		{
			get;
			set;
		}

		public double Seniority
		{
			get;
		}

		public int CompareTo(General other)
		{
			if (Stars > other.Stars)
			{
				return 1;
			}
			else if (Stars < other.Stars)
			{
				return -1;
			}
			else if (Seniority > other.Seniority)
			{
				return 1;
			}
			else if (Seniority < other.Seniority)
			{
				return -1;
			}
			else
			{
				return 0;
			}
		}
	}
}
