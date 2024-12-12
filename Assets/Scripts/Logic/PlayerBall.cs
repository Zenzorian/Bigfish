using Scripts.Services;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Logic
{
    public class PlayerBall : MonoBehaviour
    {
        public UnityEvent<float,BallType> OnWin;
        public BallType BallType { get; private set; }
        
        private IPlayerBallPoolService _playerBallPoolService;
        private float _finishHeight;
        private Rigidbody2D _rigidbody;
       

        public void Construct(Rigidbody2D rigidbody, BallType ballType, float finishHeight, IPlayerBallPoolService playerBallPoolService)
        {
            _rigidbody = rigidbody;
            BallType = ballType;
            _playerBallPoolService = playerBallPoolService;
            _finishHeight = finishHeight;
        }

        private void Update()
        {
            if(transform.position.y > _finishHeight)return;

            OnWin?.Invoke(transform.position.x, BallType);
            _playerBallPoolService.ReturnBall(this);
        }
    }
    public enum BallType
    {
        Green,
        Yellow,
        Red
    }
}