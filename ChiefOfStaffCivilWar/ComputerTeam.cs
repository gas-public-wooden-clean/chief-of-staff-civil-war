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

		public Task SendMessage(string format, CancellationToken cancellationToken, params object[] args)
		{
			return Task.CompletedTask;
		}

		public Task SendMessage(string message, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
