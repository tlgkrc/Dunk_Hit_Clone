using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_PlayerData", menuName = "Game/CD_PlayerData", order = 0)]
    public class CD_Player : ScriptableObject
    {
        public PlayerData Data;
    }
}