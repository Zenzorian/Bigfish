
using ScriptableObjects;
using UnityEngine;

namespace Scripts.Services
{
    public class SaveLoadPlayerDataService : ISaveLoadPlayerDataService
    {
        private const string PLAYER_DATA_PATH = "PlayerData";

        private PlayerData _playerData;
                       
        public PlayerData PlayerData
        {
            get { return _playerData; }
            set { _playerData = value; }
        }
        public void Load()
        {
            _playerData = Resources.Load<PlayerData>(PLAYER_DATA_PATH);

            if (_playerData == null)
            {
                _playerData = new PlayerData(3000);  
            }
        }                
    }
}
