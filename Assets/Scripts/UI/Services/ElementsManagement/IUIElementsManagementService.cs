using Scripts.UI.Markers;

namespace Scripts.UI.Services
{
    public interface IUIElementsManagementService
    {
        BalanceElements GetBalanceElements();
        BettingElements GetBettingElements();
        SelectColorButtons GetSelectColorButtons();
    }
}