
using ScriptableObjects;

namespace Scripts.Services
{
    public interface ISaveLoadPlayerDataService
    {
        void Load();
        PlayerData PlayerData { get; set; }

    }
}
