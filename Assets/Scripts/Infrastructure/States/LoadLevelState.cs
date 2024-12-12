using Scripts.Logic;
using Scripts.Services;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<LoadedData>
    {
        private readonly GameStateMachine _stateMachine;

        private readonly ISceneLoaderService _sceneLoader;      
        private readonly IGameFactoryService _gameFactory;
         
        public LoadLevelState(GameStateMachine gameStateMachine, ISceneLoaderService sceneLoader, IGameFactoryService gameFactory)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;           
            _gameFactory = gameFactory;
        }

        public void Enter(LoadedData loadedData)
        {
            Debug.Log("Load Level State");
                      
            _sceneLoader.Load(loadedData.sceneName, OnLoaded);
        }

        public void Exit()
        {         
        }

        private void OnLoaded()
        {
            _gameFactory.CreateGameFild();
            _stateMachine.Enter<GameLoopState>();
        }         
    }
}