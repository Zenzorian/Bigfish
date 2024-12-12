using Scripts.Logic;
using UnityEngine;

namespace ScriptableObjects
{
    [System.Serializable,CreateAssetMenu(fileName = "GameConfigData", menuName = "ScriptableObjects/GameConfigData")]
    public class GameConfigData : ScriptableObject
    {
        public GameFildConfig gameFildConfig;       
        public PlayerBallConfig[] playerBallsConfig;
        public MultiplicationFieldConfig multiplicationFieldConfig;
    }
    
    [System.Serializable]
    public class GameFildConfig
    {
        public Vector3 gameFildPosition;
        public Vector3 playerBallSpawnPosition;

        public Sprite fildElementSprite;

        public int rows;
        public float rowSpacing;
        public float pointSpacing;
    }   
    [System.Serializable]
    public class PlayerBallConfig
    {
        public Sprite ballSprite;
        public BallType ballType;
        public Color color; 
    }
    [System.Serializable]
    public class MultiplicationFieldConfig
    {
        public Sprite sprite;
        public Vector2 spacing;
        public float offset;
        
        public WinningCoefficient[] winningCoefficient;
    }
    [System.Serializable]
    public class WinningCoefficient
    {
        public BallType ballType;
        public float coefficient;
    }

}