using System.Collections.Generic;
using ScriptableObjects;
using Scripts.Logic;
using Scripts.Services.StaticData;
using Scripts.UI.Logic;
using Scripts.UI.Markers;
using Scripts.UI.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Services
{
    public class BalanceHandlingService : IWinHandlingService, IInitializable,ILateDisposable
    {
        private readonly IConfigDataService _configDataService;
        private readonly IGameFactoryService _gameFactoryService;
        private readonly ISaveLoadPlayerDataService _saveLoadPlayerDataService;
        private readonly IUIElementsManagementService _uIElementsManagementService;

        private readonly Betting _betting;

        private Text _balanceText;
        private PlayerData _playerData;
        private SelectColorButtons _selectColorButtons;

        public BalanceHandlingService
        (
            IConfigDataService configDataService,
            IGameFactoryService gameFactoryService,
            ISaveLoadPlayerDataService saveLoadPlayerDataService,
            IUIElementsManagementService uIClickManagementService,
            Betting betting
        )
        {
            _configDataService = configDataService;
            _gameFactoryService = gameFactoryService;
            _saveLoadPlayerDataService = saveLoadPlayerDataService;
            _uIElementsManagementService = uIClickManagementService;
            _betting = betting;
        }

        public void Initialize()
        {           
            _gameFactoryService.OnWin.AddListener(SetBallPosition);           
                    
            _balanceText = _uIElementsManagementService.GetBalanceElements().balanceText;
            _playerData = _saveLoadPlayerDataService.PlayerData;

            _selectColorButtons = _uIElementsManagementService.GetSelectColorButtons();

            _selectColorButtons.greenButton.onClick.AddListener(Betted);
            _selectColorButtons.yellowButton.onClick.AddListener(Betted);
            _selectColorButtons.redButton.onClick.AddListener(Betted);
        }

        public void LateDispose()
        {   
            _selectColorButtons.greenButton.onClick.RemoveListener(Betted);
            _selectColorButtons.yellowButton.onClick.RemoveListener(Betted);
            _selectColorButtons.redButton.onClick.RemoveListener(Betted);

            _gameFactoryService.OnWin.RemoveListener(SetBallPosition);
        }
       
        private void Betted()
        {
            if (_playerData.balance <= 0) return;
            _playerData.balance -= _betting.Bet;
            UpdateBalance();
        }
        public void SetBallPosition(float positionX, BallType ballType)
        {
            var gridCoordinates = _gameFactoryService.GetGridCoordinates();
            int cellIndex = _configDataService.GetGameConfigData().gameFildConfig.rows;
           
            for (int i = 0; i <= _configDataService.GetGameConfigData().gameFildConfig.rows; i++)
            {
                if (gridCoordinates[gridCoordinates.Length - i -1].x > positionX)
                {
                    cellIndex--;
                }
                else break;
            }
            CalculateBalance(cellIndex, ballType);
        }
        private void CalculateBalance(int cellIndex, BallType ballType)
        {
            var coefficient = _configDataService.GetGameConfigData()
                .multiplicationFieldConfig.winningCoefficient[(int)ballType].coefficient;

            _playerData.balance += _betting.Bet * (coefficient * cellIndex);

            UpdateBalance();
        }
        private void UpdateBalance()
        {
            _balanceText.text = _playerData.balance.ToString();
        }
    }

    public interface IWinHandlingService
    {   

    }
}