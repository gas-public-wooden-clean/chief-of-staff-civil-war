using System;
using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	class War
	{
		public War(Random random, NameGenerator namer, int numTheaters)
		{
			WeightedOutcomes<string> eastUnionOrigins = new WeightedOutcomes<string>();
			eastUnionOrigins.Add("New York", 2);
			eastUnionOrigins.Add("Pennsylvania", 2);
			eastUnionOrigins.Add("New Jersey", 0.5);
			eastUnionOrigins.Add("Connecticut", 0.5);
			eastUnionOrigins.Add("Maine", 0.5);
			eastUnionOrigins.Add("Delaware", 0.5);
			eastUnionOrigins.Add("New Hampshire", 1.0 / 3);
			eastUnionOrigins.Add("Rhode Island", 1.0 / 3);
			eastUnionOrigins.Add("Maryland", 1.0 / 3);
			eastUnionOrigins.Add("Ohio", 2.0 / 8);
			eastUnionOrigins.Add("Illinois", 1.0 / 8);
			eastUnionOrigins.Add("Michigan", 1.0 / 8);
			eastUnionOrigins.Add("Indiana", 1.0 / 8);
			eastUnionOrigins.Add("Wisconsin", 1.0 / 8);
			eastUnionOrigins.Add("West Virginia", 1.0 / 8);
			eastUnionOrigins.Add("Missouri", 3.0 / 64);
			eastUnionOrigins.Add("Iowa", 2.0 / 64);
			eastUnionOrigins.Add("Kansas", 2.0 / 64);
			eastUnionOrigins.Add("Minnesota", 1.0 / 64);

			WeightedOutcomes<string> westUnionOrigins = new WeightedOutcomes<string>();
			westUnionOrigins.Add("California", 3);
			westUnionOrigins.Add("Colorado", 2);
			westUnionOrigins.Add("Oregon", 1);
			westUnionOrigins.Add("Nevada", 1);
			westUnionOrigins.Add("New Mexico", 1);

			WeightedOutcomes<string> confederateOrigins = new WeightedOutcomes<string>();
			confederateOrigins.Add("Virginia", 2);
			confederateOrigins.Add("North Carolina", 1);
			confederateOrigins.Add("South Carolina", 1);
			confederateOrigins.Add("Georgia", 1);
			confederateOrigins.Add("Alabama", 1);
			confederateOrigins.Add("Florida", 1);
			confederateOrigins.Add("Tennessee", 2.0 / 8);
			confederateOrigins.Add("Kentucky", 2.0 / 8);
			confederateOrigins.Add("Mississippi", 2.0 / 8);
			confederateOrigins.Add("Louisiana", 1.0 / 8);
			confederateOrigins.Add("Texas", 1.0 / 64 + 5.0 / 512);
			confederateOrigins.Add("Arizona", 2.0 / 512);
			confederateOrigins.Add("Indian Territory", 1.0 / 512);

			TeamTheater transMississippiUnion = new TeamTheater(random, 2, 1, 2, new string[] { "Win the game." }, eastUnionOrigins);
			// Add the veterans first so they get a lower number than the volunteers.
			transMississippiUnion.AddRegiment(3);
			for (int i = 0; i < 3; i++)
			{
				transMississippiUnion.AddRegiment(1);
			}
			transMississippiUnion.AddGeneral(new General(random, namer, 1));

			TeamTheater transMississippiConfederacy = new TeamTheater(random, 2, 2, 2, new string[] { "Win the game." }, confederateOrigins);
			// 4 militia regiments and 4 volunteer regiments.
			for (int level = 2; level >= 1; level--)
			{
				for (int i = 0; i < 4; i++)
				{
					transMississippiConfederacy.AddRegiment(level);
				}
			}
			for (int i = 0; i < 3; i++)
			{
				transMississippiConfederacy.AddGeneral(new General(random, namer, 1));
			}

			IEnumerable<string> transMississippiLocations = new string[]
			{
				"St. Louis, MO", "Rolle, MO", "Springfield, MO", "Fayetteville, AR", "Little Rock, AR"
			};
			IEnumerable<int> transMississippiDistances = new int[]
			{
				3, 4, 4, 6
			};
			List<Theater> theaters = new List<Theater>
			{
				new Theater("Trans-Mississippi", transMississippiLocations, transMississippiDistances, transMississippiUnion, transMississippiConfederacy)
			};
			if (numTheaters > 1)
			{
			}
			if (numTheaters > 2)
			{
			}
			if (numTheaters > 3)
			{
			}
			if (numTheaters > 4)
			{
			}
			if (numTheaters > 5)
			{
			}
			if (numTheaters > 6)
			{
			}
			Theaters = theaters;

			Year = 1861;
			Month = 4;
		}

		public IReadOnlyList<Theater> Theaters
		{
			get;
		}

		public int Year
		{
			get;
			private set;
		}

		public int Month
		{
			get;
			private set;
		}

		public string MonthName
		{
			get
			{
				switch (Month)
				{
					case 1:
						return "January";
					case 2:
						return "February";
					case 3:
						return "March";
					case 4:
						return "April";
					case 5:
						return "May";
					case 6:
						return "June";
					case 7:
						return "July";
					case 8:
						return "August";
					case 9:
						return "September";
					case 10:
						return "October";
					case 11:
						return "November";
					default:
						return "December";
				}
			}
		}

		/// <summary>
		/// Turns remaining after the current turn.
		/// </summary>
		public int TurnsRemaining => 12 * (1865 - Year) - Month;

		public bool IsGameOver => Year >= 1865;

		public void AdvanceTime()
		{
			Month += 1;
			if (Month > 12)
			{
				Month = 1;
				Year += 1;
			}
		}
	}
}
