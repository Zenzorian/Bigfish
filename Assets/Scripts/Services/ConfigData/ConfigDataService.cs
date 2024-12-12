using ScriptableObjects;
using UnityEngine;

namespace Scripts.Services.StaticData
{
    public class ConfigDataService : IConfigDataService
    {
        private const string CONFIG_DATA_PATH = "ConfigData/GameConfigData";

        private GameConfigData _gameConfigData;

        public bool Load()
        {           
            _gameConfigData = Resources.Load<GameConfigData>(CONFIG_DATA_PATH);

            Debug.Log(_gameConfigData);

            if (_gameConfigData == null) return false;
            else return true;
        }

        public GameConfigData GetGameConfigData() => _gameConfigData;
    }
}