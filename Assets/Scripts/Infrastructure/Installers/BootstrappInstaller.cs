using Scripts.Infrastructure.States;
using Scripts.Logic;
using Scripts.Services;
using Scripts.Services.StaticData;
using Scripts.UI.Logic;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _curtainPrefab;
        [SerializeField] private GameObject _multiplicationFieldsCanvasPrefab;
        
        private ISceneLoaderService _sceneLoaderService;
        private ICoroutineRunner _coroutineRunner;

        public override void InstallBindings()
        {
            Debug.Log("Game Started");

            RegisterServices();

            CreateGameStateMachine();
        }

        private void CreateGameStateMachine()
        {           
            _sceneLoaderService.ShowLoadingCurtain();

            Container
               .Bind<GameStateMachine>()  
               .AsSingle()
               .NonLazy();          
        }
        private void RegisterServices()
        {
            BindCoroutineRunner();

            BindSceneLoaderService();

            BindConfigDataService();

            BindSaveLoadPlayerDataService();

            BindPlayerBallPoolService();

            BindGameFactoryService();  
        }
        
        private void BindCoroutineRunner()
        {
            var coroutineRunnerObject = new GameObject("Coroutine Runner");

            GameObject.DontDestroyOnLoad(coroutineRunnerObject);

            _coroutineRunner = coroutineRunnerObject.AddComponent<CoroutineRunner>();

            Container
               .Bind<ICoroutineRunner>()
               .FromInstance(_coroutineRunner)
               .AsSingle()
               .NonLazy();
        }

        private void BindSceneLoaderService()
        {
            var loaderCurtain = Instantiate(_curtainPrefab);

            GameObject.DontDestroyOnLoad(loaderCurtain);

            _sceneLoaderService = new SceneLoaderService(_coroutineRunner, loaderCurtain);

            Container
               .Bind<ISceneLoaderService>()
               .FromInstance(_sceneLoaderService)
               .AsSingle()
               .NonLazy();
        }

        private void BindConfigDataService()
        {
            Container
                .BindInterfacesAndSelfTo<ConfigDataService>()               
                .AsSingle()
                .NonLazy();   
        }

        private void BindSaveLoadPlayerDataService()
        {
            Container
               .BindInterfacesAndSelfTo<SaveLoadPlayerDataService>()
               .AsSingle()
               .NonLazy();
        }

        private void BindPlayerBallPoolService()
        {
            Container
               .BindInterfacesAndSelfTo<PlayerBallPoolService>()
               .AsSingle()
               .NonLazy();
        }

        private void BindGameFactoryService()
        {
            var multiplicationFieldView = Container.
                InstantiatePrefabForComponent<MultiplicationFieldView>(_multiplicationFieldsCanvasPrefab);
            
            Container
                .Bind<MultiplicationFieldView>()
                .FromInstance(multiplicationFieldView)
                .AsSingle();
            
            Container
              .BindInterfacesAndSelfTo<GameFactoryService>()
              .AsSingle()
              .NonLazy();
        }

        
    }
}