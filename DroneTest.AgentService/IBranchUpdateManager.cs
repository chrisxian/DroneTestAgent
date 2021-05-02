using System.Threading.Tasks;

namespace DroneTest.AgentService
{
    interface IBranchUpdateManager
    {
        bool CheckBranchUpdateNeeded();
        Task BranchUpdate();
    }
}
