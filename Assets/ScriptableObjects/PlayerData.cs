using UnityEngine;

namespace ScriptableObjects
{
    [System.Serializable, CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public PlayerData(float balance)
        {
            this.balance = balance;
        }
        public float balance;
    }
}