using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChiefOfStaffCivilWar
{
	class Regiment
	{
		static Regiment()
		{
			_originCounts = new Dictionary<string, int>();

			_unionOrigins = new WeightedOutcomes<string>();
			_unionOrigins.Add("New York", 2);
			_unionOrigins.Add("Pennsylvania", 2);
			_unionOrigins.Add("New Jersey", 0.5);
			_unionOrigins.Add("Connecticut", 0.5);
			_unionOrigins.Add("Maine", 0.5);
			_unionOrigins.Add("Delaware", 0.5);
			_unionOrigins.Add("New Hampshire", 1.0 / 3);
			_unionOrigins.Add("Rhode Island", 1.0 / 3);
			_unionOrigins.Add("Maryland", 1.0 / 3);
			_unionOrigins.Add("Ohio", 2.0 / 8);
			_unionOrigins.Add("Illinois", 1.0 / 8);
			_unionOrigins.Add("Michigan", 1.0 / 8);
			_unionOrigins.Add("Indiana", 1.0 / 8);
			_unionOrigins.Add("Wisconsin", 1.0 / 8);
			_unionOrigins.Add("West Virginia", 1.0 / 8);
			_unionOrigins.Add("Missouri", 3.0 / 64);
			_unionOrigins.Add("Iowa", 2.0 / 64);
			_unionOrigins.Add("Kansas", 2.0 / 64);
			_unionOrigins.Add("Minnesota", 1.0 / 64);
			_unionOrigins.Add("California", 3.0 / 512);
			_unionOrigins.Add("Colorado", 2.0 / 512);
			_unionOrigins.Add("Oregon", 1.0 / 512);
			_unionOrigins.Add("Nevada", 1.0 / 512);
			_unionOrigins.Add("New Mexico", 1.0 / 512);

			_confederateOrigins = new WeightedOutcomes<string>();
			_confederateOrigins.Add("Virginia", 2);
			_confederateOrigins.Add("North Carolina", 1);
			_confederateOrigins.Add("South Carolina", 1);
			_confederateOrigins.Add("Georgia", 1);
			_confederateOrigins.Add("Alabama", 1);
			_confederateOrigins.Add("Florida", 1);
			_confederateOrigins.Add("Tennessee", 2.0 / 8);
			_confederateOrigins.Add("Kentucky", 2.0 / 8);
			_confederateOrigins.Add("Mississippi", 2.0 / 8);
			_confederateOrigins.Add("Louisiana", 1.0 / 8);
			_confederateOrigins.Add("Texas", 1.0 / 64 + 5.0 / 512);
			_confederateOrigins.Add("Arizona", 2.0 / 512);
			_confederateOrigins.Add("Indian Territory", 1.0 / 512);
		}

		static readonly IDictionary<string, int> _originCounts;
		static readonly WeightedOutcomes<string> _unionOrigins;
		static readonly WeightedOutcomes<string> _confederateOrigins;

		/// <param name="level">The regiment's starting experience level as an integer between 1 and 4 inclusive.</param>
		public Regiment(Random random, bool isUnion, int level)
		{
			if (isUnion)
			{
				_origin = _unionOrigins.Get(random);
			}
			else
			{
				_origin = _confederateOrigins.Get(random);
			}

			if (!_originCounts.ContainsKey(_origin))
			{
				_originCounts[_origin] = 0;
			}
			_originNumber = ++_originCounts[_origin];

			Level = level;
		}

		readonly string _origin;
		readonly int _originNumber;
		int _level;

		public int Level
		{
			get => _level;
			set
			{
				if (value is < 1 or > 4)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, "Level must be between 1 and 4 inclusive.");
				}
				_level = value;
			}
		}

		public override string ToString()
		{
			StringBuilder retval = new StringBuilder();

			// The ordinal numeral.
			_ = retval.Append(_originNumber.ToString(CultureInfo.CurrentCulture));
			if (_originNumber is <= 10 or >= 20)
			{
				switch (_originNumber % 10)
				{
					case 1:
						_ = retval.Append("st");
						break;
					case 2:
						_ = retval.Append("nd");
						break;
					case 3:
						_ = retval.Append("rd");
						break;
					default:
						_ = retval.Append("th");
						break;
				}
			}
			else
			{
				_ = retval.Append("th");
			}

			_ = retval.Append(" ");

			_ = retval.Append(_origin);

			_ = retval.Append(" (");
			switch (Level)
			{
				case 1:
					_ = retval.Append("Volunteer");
					break;
				case 2:
					_ = retval.Append("Militia");
					break;
				case 3:
					_ = retval.Append("Veteran");
					break;
				default:
					_ = retval.Append("Elite");
					break;
			}
			_ = retval.Append(")");

			return retval.ToString();
		}
	}
}
