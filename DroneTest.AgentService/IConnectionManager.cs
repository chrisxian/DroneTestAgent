using System.Threading.Tasks;

namespace DroneTest.AgentService
{
    public interface IConnectionManager
    {
        bool IsConnected { get; }

        /// <summary>
        /// Try to connect to configured master server.
        /// </summary>
        /// <remarks>
        /// connection will be ignored if already connected.
        /// max 10 times of retry will be performed.
        /// </remarks>
        /// <returns></returns>
        Task TryConnect();
    }
}
