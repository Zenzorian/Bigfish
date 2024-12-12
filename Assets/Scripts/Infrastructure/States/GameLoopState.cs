using Scripts.UI.Services;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly DiContainer _diContainer;       

        public GameLoopState(GameStateMachine stateMachine)
        {          
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            Debug.Log("Game Loop State");            
        }
       
    }
}