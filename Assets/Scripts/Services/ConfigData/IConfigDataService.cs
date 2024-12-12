using ScriptableObjects;

namespace Scripts.Services.StaticData
{
    public interface IConfigDataService
    {
        GameConfigData GetGameConfigData();       
        bool Load();
    }
}