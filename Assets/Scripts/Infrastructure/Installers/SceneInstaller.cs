using Scripts.Services;
using Scripts.UI.Logic;
using Scripts.UI.Services;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private UIElementsManagementService _UIClickManagementService;
        
        public override void InstallBindings()
        {
            BindUIClickManagementService();

            BindBettingService();

            BindBallSpawnButtons();

            BindWinHandlingService();
        }

        private void BindUIClickManagementService()
        {
            Container
                 .BindInterfacesAndSelfTo<UIElementsManagementService>()
                 .FromInstance(_UIClickManagementService)
                 .AsSingle()
                 .NonLazy();
        }

        private void BindBettingService()
        {
            Container
                .BindInterfacesAndSelfTo<Betting>()
                .AsCached()
                .NonLazy();
        }

        private void BindBallSpawnButtons()
        {
            Container
                .BindInterfacesAndSelfTo<BallSpawnButtons>()
                .AsCached()
                .NonLazy();
        }
        private void BindWinHandlingService()
        {
            Container
                .BindInterfacesAndSelfTo<BalanceHandlingService>()
                .AsCached()
                .NonLazy();
        }
    }

}
