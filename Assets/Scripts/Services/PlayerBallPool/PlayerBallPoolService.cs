using ScriptableObjects;
using Scripts.Logic;
using Scripts.Services.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Services
{
    public class PlayerBallPoolService : IPlayerBallPoolService
    {
        private const int INITIAL_SIZE = 30;
        private const int MAX_POOL_SIZE = 300;

        private readonly IConfigDataService _configDataService;
               
        private List<PlayerBall> _pool = new List<PlayerBall>();
        private PlayerBallConfig[] _playerBallConfig;

        private Transform _parent;

        public PlayerBallPoolService(IConfigDataService configDataService)
        {
            _configDataService = configDataService;           
        }

        public void InitializePool()
        {
            _playerBallConfig = _configDataService.GetGameConfigData().playerBallsConfig;

            _parent = new GameObject($"Player Balls Pool").transform;
                      

            for (int i = 0; i < INITIAL_SIZE / _playerBallConfig.Length; i++)
            {
                foreach (var config in _playerBallConfig)
                {
                    var ball = CreatePlayerBall(config);
                    ball.transform.parent = _parent;
                    ball.gameObject.SetActive(false);
                    _pool.Add(ball);
                }
            }
        }
        private PlayerBall CreatePlayerBall(PlayerBallConfig config)
        {
            var ball = new GameObject($"{config.ballType} Ball");

            var spriteRenderer = ball.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = config.ballSprite;
            spriteRenderer.color = config.color;

            var rigidBody = ball.AddComponent<Rigidbody2D>();
            var collider = ball.AddComponent<CircleCollider2D>();

            var playerBall = ball.AddComponent<PlayerBall>();
            var fildConfig = _configDataService.GetGameConfigData().gameFildConfig;
            var finishHeight = fildConfig.gameFildPosition.y - fildConfig.rowSpacing * fildConfig.rows;
            playerBall.Construct(rigidBody, config.ballType,finishHeight,this);

            return playerBall;
        }

        public PlayerBall GetPlayerBall(PlayerBallConfig config)
        {
            foreach (var playerBall in _pool)
            {
                if (!playerBall.gameObject.activeInHierarchy && playerBall.BallType == config.ballType)
                {                  
                    playerBall.gameObject.SetActive(true);
                    return playerBall;
                }
            }

            if (_pool.Count >= MAX_POOL_SIZE)
            {
                Debug.LogWarning("Max pool size reached, cannot create more boxes!");
                return null;
            }

            var newBall = CreatePlayerBall(config);            
            _pool.Add(newBall);
            return newBall;
        }

        public void ReturnBall(PlayerBall playerBall)
        {           
            playerBall.gameObject.SetActive(false);
            playerBall.transform.parent = _parent;
        }
    }
}