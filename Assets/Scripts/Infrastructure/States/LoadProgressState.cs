using ScriptableObjects;
using Scripts.Services;
using Scripts.Services.StaticData;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string SCENE_NAME = "Main";

        private readonly GameStateMachine _gameStateMachine;
        private readonly IConfigDataService _configDataService;
        private readonly ISaveLoadPlayerDataService _saveLoadService;

        private LoadedData _loadedData;      
              
        public LoadProgressState(GameStateMachine gameStateMachine, IConfigDataService configDataService, ISaveLoadPlayerDataService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _configDataService = configDataService;
            _saveLoadService = saveLoadService;           
        }

        public void Enter()
        {
            Debug.Log("Load Progress State");

            _loadedData = new LoadedData();

            _loadedData.sceneName = SCENE_NAME;

            LoadConfigData(ref _loadedData);
            LoadProgress(ref _loadedData);

            _gameStateMachine.Enter<LoadLevelState, LoadedData>(_loadedData);
        }

        public void Exit()
        {
        }

        private void LoadConfigData(ref LoadedData loadedData)
        {           
            if (_configDataService.Load() == true)
                loadedData.gameConfigData = _configDataService.GetGameConfigData();
            else Debug.LogError("Config data not loaded");
        }

        private void LoadProgress(ref LoadedData loadedData)
        {
            _saveLoadService.Load();

            loadedData.playerData = _saveLoadService.PlayerData;
           
        }

       

    }
    public struct LoadedData
    {
        public string sceneName;

        public GameConfigData gameConfigData;

        public PlayerData playerData;
    }
}