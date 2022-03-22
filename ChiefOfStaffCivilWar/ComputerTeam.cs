using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChiefOfStaffCivilWar
{
	class ComputerTeam : TeamController
	{
		public ComputerTeam(Random random) => _random = random;

		readonly Random _random;

		public Task<int> GetMenuOption(int max, CancellationToken cancellationToken)
		{
			return Task.FromResult(_random.Next(max) + 1);
		}

		public void SendMessage(string message, CancellationToken cancellationToken)
		{ }
	}
}
