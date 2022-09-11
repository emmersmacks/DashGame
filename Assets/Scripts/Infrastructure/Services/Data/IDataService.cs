using Data;
using Data.Static;
using Infrastructure.Services.ScoreService;

namespace Infrastructure.Services.Data
{
    public interface IDataService : IService
    {
        StaticData StaticData { get; set; }
        string PlayerName { get; set; }
    }
}