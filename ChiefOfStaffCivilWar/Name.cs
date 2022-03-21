namespace ChiefOfStaffCivilWar
{
	class Name
	{
		public Name(string given, char middleInitial, string sur, string suffix)
		{
			_given = given;
			_middleInitial = middleInitial;
			_sur = sur;
			_suffix = suffix;
		}

		readonly string _given;
		readonly char _middleInitial;
		readonly string _sur;
		readonly string _suffix;

		public override bool Equals(object obj)
		{
			if (obj is null || obj.GetType() != GetType())
			{
				return false;
			}

			Name other = (Name)obj;
			return other._given == _given && other._sur == _sur;
		}

		public override int GetHashCode()
		{
			return _given.GetHashCode() ^ _sur.GetHashCode();
		}

		public override string ToString()
		{
			string name = _given;
			if (_middleInitial != '\0')
			{
				name += " ";
				name += _middleInitial;
				name += ".";
			}
			name += " ";
			name += _sur;
			if (_suffix is not null)
			{
				name += " ";
				name += _suffix;
			}
			return name;
		}
	}
}
