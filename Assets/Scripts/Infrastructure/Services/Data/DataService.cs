using Constants;
using Data;
using Data.Static;
using Infrastructure.Services.Assets;
using Infrastructure.Services.ScoreService;

namespace Infrastructure.Services.Data
{
    public class DataService : IDataService
    {
        public StaticData StaticData { get; set; }
        public string PlayerName { get; set; }

        private readonly IAssetProvider _assetProvider;

        public DataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            LoadStaticData();
        }

        private void LoadStaticData()
        {
            StaticData = _assetProvider.LoadScriptableObject(ConstantsResourcesPath.StaticData) as StaticData;
        }
    }
}