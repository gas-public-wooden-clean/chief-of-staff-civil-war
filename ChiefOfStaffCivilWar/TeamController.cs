using System.Threading;
using System.Threading.Tasks;

namespace ChiefOfStaffCivilWar
{
	interface TeamController
	{
		void SendMessage(string message, CancellationToken cancellationToken);
		Task<int> GetMenuOption(int max, CancellationToken cancellationToken);
	}
}
