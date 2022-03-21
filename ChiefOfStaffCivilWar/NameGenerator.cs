using System;
using System.Collections.Generic;

namespace ChiefOfStaffCivilWar
{
	class NameGenerator
	{
		public NameGenerator(Random random)
		{
			_random = random;

			_firstNames = new WeightedOutcomes<string>();
			_firstNames.Add("Hackysack", 1);
			_firstNames.Add("Julius", 1);
			_firstNames.Add("Rambo", 1);
			_firstNames.Add("Jackson", 1);
			_firstNames.Add("Billiam", 1);
			_firstNames.Add("Rick", 1);
			_firstNames.Add("Dick", 1);
			_firstNames.Add("Roger", 1);
			_firstNames.Add("Abraham", 1);
			_firstNames.Add("Jefferson", 1);
			_firstNames.Add("Winfield", 1);
			_firstNames.Add("Losefield", 0.5);
			_firstNames.Add("Irvin", 1);
			_firstNames.Add("George", 1);
			_firstNames.Add("Henry", 1);
			_firstNames.Add("Ulysses", 1);
			_firstNames.Add("William", 1);
			_firstNames.Add("Ambrose", 1);
			_firstNames.Add("Joseph", 1);
			_firstNames.Add("Philip", 1);
			_firstNames.Add("John", 1);
			_firstNames.Add("Joshua", 1);
			_firstNames.Add("Benjamin", 1);
			_firstNames.Add("Franklin", 1);
			_firstNames.Add("James", 1);
			_firstNames.Add("Francis", 1);
			_firstNames.Add("Robert", 1);
			_firstNames.Add("Thomas", 1);
			_firstNames.Add("Pierre", 1);
			_firstNames.Add("Nathan", 1);
			_firstNames.Add("Braxton", 1);
			_firstNames.Add("Jubal", 1);
			_firstNames.Add("Lasalle", 1);
			_firstNames.Add("Orville", 1);
			_firstNames.Add("Pleasant", 1);
			_firstNames.Add("Peyton", 1);
			_firstNames.Add("Josiah", 1);
			_firstNames.Add("Stonewall", 1);
			_firstNames.Add("Firewall", 0.5);
			_firstNames.Add("Gouverneur", 1);
			_firstNames.Add("Jermain", 1);
			_firstNames.Add("Bushrod", 1);
			_firstNames.Add("Vestal", 1);
			_firstNames.Add("Thaddeus", 1);
			_firstNames.Add("Absalom", 1);
			_firstNames.Add("Rooster", 1);
			_firstNames.Add("Sylvannus", 1);
			_firstNames.Add("Jedediah", 1);
			_firstNames.Add("Jebediah", 1);
			_firstNames.Add("Moxley", 1);
			_firstNames.Add("Weenus", 1);
			_firstNames.Add("Dingus", 1);

			_lastNames = new WeightedOutcomes<string>();
			_lastNames.Add("McHankypank", 1);
			_lastNames.Add("Starr", 1);
			_lastNames.Add("Stank", 1);
			_lastNames.Add("Snow", 1);
			_lastNames.Add("Beezle", 1);
			_lastNames.Add("Lincoln", 1);
			_lastNames.Add("Davis", 1);
			_lastNames.Add("McDowell", 1);
			_lastNames.Add("McClellan", 1);
			_lastNames.Add("Halleck", 1);
			_lastNames.Add("Grant", 1);
			_lastNames.Add("Sherman", 1);
			_lastNames.Add("Burnside", 1);
			_lastNames.Add("Hooker", 1);
			_lastNames.Add("Hancock", 1);
			_lastNames.Add("Handcock", 1);
			_lastNames.Add("Kearny", 1);
			_lastNames.Add("Sheridan", 1);
			_lastNames.Add("Sedgwick", 1);
			_lastNames.Add("Buford", 1);
			_lastNames.Add("Chamberlain", 1);
			_lastNames.Add("Chamberpot", 1);
			_lastNames.Add("Armstrong", 1);
			_lastNames.Add("Armstronk", 1);
			_lastNames.Add("Legstrong", 1);
			_lastNames.Add("Butler", 1);
			_lastNames.Add("McPherson", 1);
			_lastNames.Add("Barlow", 1);
			_lastNames.Add("Johnson", 1);
			_lastNames.Add("Lee", 1);
			_lastNames.Add("Jackson", 1);
			_lastNames.Add("Longstreet", 1);
			_lastNames.Add("Shortstreet", 1);
			_lastNames.Add("Hill", 1);
			_lastNames.Add("Valley", 1);
			_lastNames.Add("Stuart", 1);
			_lastNames.Add("Beauregard", 1);
			_lastNames.Add("Hood", 1);
			_lastNames.Add("Gordon", 1);
			_lastNames.Add("Forrest", 1);
			_lastNames.Add("Bragg", 1);
			_lastNames.Add("Early", 1);
			_lastNames.Add("Late", 1);
			_lastNames.Add("Dangleberries", 1);
			_lastNames.Add("Bananas", 0.5);
			_lastNames.Add("Pickett", 1);
			_lastNames.Add("Corbell", 1);
			_lastNames.Add("Bumpus", 1);
			_lastNames.Add("Unthank", 1);
			_lastNames.Add("Farquhar", 1);
			_lastNames.Add("Warren", 1);
			_lastNames.Add("Buckner", 1);
			_lastNames.Add("Loguen", 1);
			_lastNames.Add("Coffin", 1);
			_lastNames.Add("Baird", 1);
			_lastNames.Add("Cogburn", 1);
			_lastNames.Add("Cadwallader", 1);
			_lastNames.Add("Hotchkiss", 1);
			_lastNames.Add("Sorrel", 1);
			_lastNames.Add("Trumbull", 1);
			_lastNames.Add("Sartorius", 1);

			_middleInitials = new WeightedOutcomes<char>();
			_middleInitials.Add('A', 0.8);
			_middleInitials.Add('B', 1);
			_middleInitials.Add('C', 1);
			_middleInitials.Add('D', 1);
			_middleInitials.Add('E', 0.8);
			_middleInitials.Add('F', 1);
			_middleInitials.Add('G', 1);
			_middleInitials.Add('H', 1);
			_middleInitials.Add('I', 0.8);
			_middleInitials.Add('J', 2);
			_middleInitials.Add('K', 1);
			_middleInitials.Add('L', 1);
			_middleInitials.Add('M', 1);
			_middleInitials.Add('N', 1);
			_middleInitials.Add('O', 0.8);
			_middleInitials.Add('P', 1);
			_middleInitials.Add('Q', 0.5);
			_middleInitials.Add('R', 1.5);
			_middleInitials.Add('S', 2);
			_middleInitials.Add('T', 2);
			_middleInitials.Add('U', 0.8);
			_middleInitials.Add('V', 0.2);
			_middleInitials.Add('W', 1);
			_middleInitials.Add('X', 0.2);
			_middleInitials.Add('Y', 0.2);
			_middleInitials.Add('Z', 0.2);

			_suffixes = new WeightedOutcomes<string>();
			_suffixes.Add("Jr.", 2);
			_suffixes.Add("Sr.", 2);
			_suffixes.Add("II", 1);
			_suffixes.Add("III", 0.8);
			_suffixes.Add("IV", 0.5);
			_suffixes.Add("M.D.", 0.5);

			_used = new HashSet<Name>();
		}

		readonly Random _random;
		readonly WeightedOutcomes<string> _firstNames;
		readonly WeightedOutcomes<char> _middleInitials;
		readonly WeightedOutcomes<string> _lastNames;
		readonly WeightedOutcomes<string> _suffixes;
		readonly ICollection<Name> _used;

		public Name GetName()
		{
			Name name;
			do
			{
				string given = _firstNames.Get(_random);
				char middleInitial;
				if (_random.Next(6) == 0)
				{
					middleInitial = _middleInitials.Get(_random);
				}
				else
				{
					middleInitial = '\0';
				}
				string sur = _lastNames.Get(_random);
				if (_random.Next(8) == 0)
				{
					sur += "-";
					sur += _lastNames.Get(_random);
				}
				string suffix;
				if (_random.Next(8) == 0)
				{
					suffix = _suffixes.Get(_random);
				}
				else
				{
					suffix = null;
				}
				name = new Name(given, middleInitial, sur, suffix);
			} while (_used.Contains(name));
			_used.Add(name);
			return name;
		}
	}
}
