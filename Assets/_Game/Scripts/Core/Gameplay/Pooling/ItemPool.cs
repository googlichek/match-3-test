using UnityEngine;

namespace Game.Scripts.Core
{
    public class ItemPool : BasePool<Item>
    {
        [SerializeField]
        private Item _itemPrefab = default;

        [SerializeField, Space]
        private Transform _root = default;

        [SerializeField, Range(0, 100), Space]
        private int _maxCapacity = 10;

        void Awake()
        {
            InitPool(_itemPrefab, _root, 10, _maxCapacity);
        }

        void OnDestroy()
        {
            DisposePool();
        }
    }
}