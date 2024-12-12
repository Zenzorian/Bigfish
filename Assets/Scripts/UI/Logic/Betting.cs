using System;
using Scripts.UI.Markers;
using Scripts.UI.Services;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI.Logic
{
    public class Betting : IInitializable, IDisposable
    {
        private readonly IUIElementsManagementService _uiClickManagement;

        public float Bet { get; private set;}

        private BettingElements _betElements;
        private InputField _inputField;
       
        public Betting(IUIElementsManagementService uiClickManagement)
        {
            _uiClickManagement = uiClickManagement;           
        }

        public void Initialize()
        {    
            _betElements = _uiClickManagement.GetBettingElements();
            _inputField = _betElements.inputField;

            Bet = 0;

            AddListeners();

            UpdateBet();
        }

        private void AddListeners()
        {
            _betElements.plussButton.onClick.AddListener(Plus);
            _betElements.minusButton.onClick.AddListener(Minus);

            _inputField.onEndEdit.AddListener(UpdateBet);
        }
        private void Plus()
        {           
            Bet += 0.5f;
            UpdateBet();
        }

        private void Minus()
        {            
            Bet -= 0.5f;
            UpdateBet();
        }
        private void UpdateBet(string value = null)
        {
            if (Bet > 100) Bet = 100;
            
            if (Bet <= 0) Bet = 0;
            
            if (value != null)
            {
                float bet = 0;
                float.TryParse(value, out bet);
                Bet = bet;
            }   
            _inputField.text = Bet.ToString();
        }

        public void Dispose()
        {
            _betElements.plussButton.onClick.RemoveListener(Plus);
            _betElements.minusButton.onClick.RemoveListener(Minus);

            _inputField.onEndEdit.RemoveListener(UpdateBet);
        }
    }
}
