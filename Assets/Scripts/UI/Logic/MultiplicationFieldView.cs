using ScriptableObjects;
using Scripts.Logic;
using Scripts.Services.StaticData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI.Logic
{
    public class MultiplicationFieldView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        
        private IConfigDataService _configDataService;
        private  GameConfigData _gameConfigData ;
        private Font _font;
        
        
        [Inject]
        public void Construct(IConfigDataService configDataService)
        {
            _configDataService = configDataService;
        }

        public void Create()
        {
            _font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            
            _gameConfigData = _configDataService.GetGameConfigData();
            
            _canvas.worldCamera = Camera.main == null? FindAnyObjectByType<Camera>() : Camera.main; ;

            CreateFild(this.gameObject);
            
            var gameFildConfig = _gameConfigData.gameFildConfig;
            var yPosition = gameFildConfig.gameFildPosition.y - gameFildConfig.rowSpacing 
                * gameFildConfig.rows - _gameConfigData.multiplicationFieldConfig.offset;
                
            _canvas.transform.position = new Vector3(0, yPosition , 0);
        }
        private void CreateFild(GameObject parent)
        {
            CreateLayoutGroup(parent);
            
            foreach (var ballConfig in  _gameConfigData.playerBallsConfig)
            {
                for (int i = 0; i <= _gameConfigData.gameFildConfig.rows;  i++)
                {
                    string text = (i * GetCoefficient(ballConfig.ballType)).ToString();
                    CreateCell
                    (
                        parent,
                        _gameConfigData.multiplicationFieldConfig,
                        ballConfig.color,
                        text
                    );
                }
            }
        }

        private float GetCoefficient(BallType ballType)
        {
            foreach (var coeff in _gameConfigData.multiplicationFieldConfig.winningCoefficient)
            {
                if (coeff.ballType == ballType)
                {
                    return coeff.coefficient; 
                }
            }
            return 0;
        }

        private void CreateLayoutGroup(GameObject parent)
        {
            var grid = parent.AddComponent<GridLayoutGroup>();
            grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            grid.constraintCount = _gameConfigData.playerBallsConfig.Length;
            grid.childAlignment = TextAnchor.MiddleCenter;
            var sellWidth = _gameConfigData.gameFildConfig.pointSpacing
                - _gameConfigData.multiplicationFieldConfig.offset;
            grid.cellSize = new Vector2(sellWidth, 1);
            grid.spacing = _gameConfigData.multiplicationFieldConfig.spacing;
        }

        private RectTransform CreateCell(GameObject parent, MultiplicationFieldConfig fieldConfig, Color color, string text)
        {
            var cell = new GameObject("Cell");
            cell.transform.parent = parent.transform;  
            
            var cellRect  = cell.GetComponent<RectTransform>()??cell.AddComponent<RectTransform>();
            
            var image = cell.AddComponent<Image>();
            image.sprite = fieldConfig.sprite;
            image.color = color;
            
            var textObject = new GameObject("Text");
            textObject.transform.parent = cell.transform;
            var textRect  = textObject.AddComponent<RectTransform>();
            textRect.anchorMax = Vector2.one;
            textRect.anchorMin = Vector2.zero;
            textRect.offsetMin = Vector2.zero; 
            textRect.offsetMax = Vector2.zero;
            
            var textElement = textObject.AddComponent<Text>();
            textElement.font = _font;
            textElement.text = text;
            textElement.alignment = TextAnchor.MiddleCenter;
            textElement.color = Color.black;
            textElement.fontSize = 1;
            
            return cellRect;
        }
    }
}