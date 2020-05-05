using System.Threading.Tasks;

namespace RS.Bots.Domain.Interfaces
{
    public interface IVisionService
    {
        Task AnalyseImageAsync(string pathToImage);
    }
}
