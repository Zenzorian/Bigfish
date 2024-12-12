using Scripts.UI.Services;
using Zenject;
using System;
using Scripts.Logic;
using Scripts.Services;
using Scripts.UI.Markers;
using ScriptableObjects;

namespace Scripts.UI.Logic
{
    public class BallSpawnButtons : IInitializable, IDisposable
    {
        private readonly IGameFactoryService _gameFactory;
        private readonly IUIElementsManagementService _uiClickManagement;
        private readonly ISaveLoadPlayerDataService _saveLoadPlayerDataService;


        private SelectColorButtons _selectColorButtons;
        private PlayerData _playerData;

        public BallSpawnButtons( IGameFactoryService gameFactory, IUIElementsManagementService uiClickManagement, ISaveLoadPlayerDataService saveLoadPlayerDataService)
        {
            _gameFactory = gameFactory;
            _uiClickManagement = uiClickManagement;
            _saveLoadPlayerDataService = saveLoadPlayerDataService;
        }
        public void Initialize()
        {
            _selectColorButtons = _uiClickManagement.GetSelectColorButtons();
            _playerData = _saveLoadPlayerDataService.PlayerData;            

            AddListeners();
        }

        private void AddListeners()
        {
            _selectColorButtons.greenButton.onClick.AddListener(GreenButtonClicked);
            _selectColorButtons.yellowButton.onClick.AddListener(YellowButtonClicked);
            _selectColorButtons.redButton.onClick.AddListener(RedButtonClicked);
        }

        private void GreenButtonClicked()
        {            
            if (_playerData.balance <= 0) return;
            _gameFactory.SpawnPlayerBall(BallType.Green);
        }

        private void YellowButtonClicked()
        {
            if (_playerData.balance <= 0) return;
            _gameFactory.SpawnPlayerBall(BallType.Yellow);
        }

        private void RedButtonClicked()
        {
            if (_playerData.balance <= 0) return;
            _gameFactory.SpawnPlayerBall(BallType.Red);
        }
        
        public void Dispose()
        {
            _selectColorButtons.greenButton.onClick.RemoveListener(GreenButtonClicked);
            _selectColorButtons.yellowButton.onClick.RemoveListener(YellowButtonClicked);
            _selectColorButtons.redButton.onClick.RemoveListener(RedButtonClicked);
        }
    }
}
