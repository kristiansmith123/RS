using System.Threading.Tasks;

namespace RS.Bots.Domain.Interfaces
{
    public interface IBot
    {
        Task StartAsync();
        void Stop();
        string GetName();
    }
}
