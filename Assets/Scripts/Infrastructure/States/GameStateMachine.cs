using Scripts.Services;
using Scripts.Services.StaticData;
using System;
using System.Collections.Generic;
using Zenject;

namespace Scripts.Infrastructure.States
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(DiContainer diContainer)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(LoadProgressState)] = new LoadProgressState
                (
                    this,
                    diContainer.Resolve<IConfigDataService>(),
                    diContainer.Resolve<ISaveLoadPlayerDataService>()
                ),
                [typeof(LoadLevelState)] = new LoadLevelState
                (
                    this,
                    diContainer.Resolve<ISceneLoaderService>(),
                    diContainer.Resolve<IGameFactoryService>()
                ),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };

            Enter<LoadProgressState>();
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
          _states[typeof(TState)] as TState;
    }
}