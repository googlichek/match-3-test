using System;
using UnityEngine;

namespace Game.Scripts.Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/Data/Item")]
    public class ItemData : ScriptableObject
    {
        [SerializeField]
        private ItemType _type = default;

        [SerializeField, Range(0, 10), Space]
        private float _movementDuration = 1f;

        public ItemType Type => _type;
        public float MovementDuration => _movementDuration;
    }
}