using System.Threading;
using System.Threading.Tasks;

namespace ChiefOfStaffCivilWar
{
	interface TeamController
	{
		Task SendMessage(string message, CancellationToken cancellationToken);
		Task SendMessage(string format, CancellationToken cancellationToken, params object[] args);
		Task<int> GetMenuOption(int max, CancellationToken cancellationToken);
	}
}
