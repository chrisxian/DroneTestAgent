using System.Threading.Tasks;

namespace DroneTest.AgentService
{
    public interface IConnectionManager
    {
        bool IsConnected { get; }

        Task TryConnect();
    }
}
