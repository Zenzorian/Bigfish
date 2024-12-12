using ModestTree;
using ScriptableObjects;
using Scripts.Logic;
using Scripts.Services.StaticData;
using Scripts.UI.Logic;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Services
{
    public class GameFactoryService : IGameFactoryService
    {  
        public UnityEvent<float, BallType> OnWin
        {
            get { return _onWin; }
            set { _onWin = value; }
        }

        private UnityEvent<float, BallType> _onWin = new UnityEvent<float, BallType>();

        private readonly IConfigDataService _configDataService;
        private readonly IPlayerBallPoolService _playerBallPoolService;

        private MultiplicationFieldView _multiplicationFieldView;
        
        private PlayerBallConfig[] _playerBallsConfig;
        private GameFildConfig _gameFildConfig;

        private Transform _gridParent;

        private Vector2[] _gridCoordinates = new Vector2[200];

        public GameFactoryService
        (
            IConfigDataService configData,
            IPlayerBallPoolService playerBallPoolService, 
            MultiplicationFieldView multiplicationFieldView
        )
        {
            _configDataService = configData;
            _playerBallPoolService = playerBallPoolService;      
            
            _multiplicationFieldView = multiplicationFieldView;
        }

       

        public void CreateGameFild()
        {
            _gameFildConfig = _configDataService.GetGameConfigData().gameFildConfig;
            _playerBallsConfig = _configDataService.GetGameConfigData().playerBallsConfig;

            GridCoordinatesGenerator gridCoordinatesGenerator = new GridCoordinatesGenerator(_gameFildConfig.rows, _gameFildConfig.rowSpacing, _gameFildConfig.pointSpacing);

            _gridCoordinates = gridCoordinatesGenerator.GenerateGrid().ToArray();
                     
            var grid = new GameObject("Grid");
            _gridParent = grid.GetComponent<Transform>() ?? grid.AddComponent<Transform>();

            foreach (var coordinates in _gridCoordinates)
            {
                var gridCircle = new GameObject("Grid Circle");

                var transform = gridCircle.GetComponent<Transform>() ?? gridCircle.AddComponent<Transform>();
                transform.parent = _gridParent;
                transform.position = coordinates;

                var spriteRenderer = gridCircle.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = _gameFildConfig.fildElementSprite;
                spriteRenderer.color = Color.white;

                var collider = gridCircle.AddComponent<CircleCollider2D>();
            }
            _gridParent.position = _gameFildConfig.gameFildPosition;
            
            _multiplicationFieldView.Create();
        }

        public Vector2[] GetGridCoordinates() => _gridCoordinates;

        public void SpawnPlayerBall(BallType ballType)
        {
            foreach (var config in _playerBallsConfig)
            {
                if (ballType == config.ballType)
                {
                    var playerBall = _playerBallPoolService.GetPlayerBall(config);
                    playerBall.transform.parent = _gridParent;
                    playerBall.transform.position = _gameFildConfig.playerBallSpawnPosition
                        + new Vector3(Random.RandomRange(-0.05f,0.05f),0,0);
                    playerBall.GetComponent<PlayerBall>().OnWin = _onWin;
                }
            }
        }    
    }
}