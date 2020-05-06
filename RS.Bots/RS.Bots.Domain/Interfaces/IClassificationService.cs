using RS.Bots.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RS.Bots.Domain.Interfaces
{
    public interface IClassificationService
    {
        Task<IEnumerable<VisionResult>> AnalyseImageAsync(string pathToImage, string keywords = null);
        bool Detect(string pathToImage, string pathToModel);
    }
}
