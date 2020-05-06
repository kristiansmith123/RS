using RS.Bots.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RS.Bots.Domain.Interfaces
{
    public interface IClassificationService
    {
        MatchedModelResult MatchModelInImage(string pathToImage, string pathToModel);
    }
}
