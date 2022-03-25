using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChiefOfStaffCivilWar
{
	class Regiment
	{
		static readonly IDictionary<string, int> _originCounts = new Dictionary<string, int>();

		/// <param name="origin">The regiment's origin.</param>
		/// <param name="level">The regiment's starting experience level as an integer between 1 and 4 inclusive.</param>
		public Regiment(string origin, int level)
		{
			_origin = origin;

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

			_ = retval.Append(' ');

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
			_ = retval.Append(')');

			return retval.ToString();
		}
	}
}
