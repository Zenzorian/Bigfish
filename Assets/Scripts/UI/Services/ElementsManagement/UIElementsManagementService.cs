using Scripts.UI.Markers;
using UnityEngine;

namespace Scripts.UI.Services
{
    [System.Serializable]
    public class UIElementsManagementService : IUIElementsManagementService
    {
        [SerializeField] private BalanceElements _balanceElements;
        [SerializeField] private BettingElements _bettingElements;
        [SerializeField] private SelectColorButtons _selectColorButtons;

        public BalanceElements GetBalanceElements() => _balanceElements;
        public BettingElements GetBettingElements() => _bettingElements;
        public SelectColorButtons GetSelectColorButtons() => _selectColorButtons;

    }
}