using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class BoardPool : MonoBehaviour
    {
        [SerializeField]
        private ItemPool _tile00Pool = default;

        [SerializeField]
        private ItemPool _tile01Pool = default;

        [SerializeField]
        private ItemPool _tile02Pool = default;

        [SerializeField]
        private ItemPool _tile03Pool = default;

        public Item GetItem(ItemType type)
        {
            Item item = null;
            switch (type)
            {
                case ItemType.Tile00:

                    item = _tile00Pool.Get();
                    return item;

                case ItemType.Tile01:

                    item = _tile01Pool.Get();
                    return item;

                case ItemType.Tile02:

                    item = _tile02Pool.Get();
                    return item;

                case ItemType.Tile03:

                    item = _tile03Pool.Get();
                    return item;

                default:
                    return null;
            }
        }

        public void ReleaseItem(Item item)
        {
            switch (item.Type)
            {
                case ItemType.Tile00:

                    _tile00Pool.Release(item);
                    break;

                case ItemType.Tile01:

                    _tile01Pool.Release(item);
                    break;

                case ItemType.Tile02:

                    _tile02Pool.Release(item);
                    break;

                case ItemType.Tile03:

                    _tile03Pool.Release(item);
                    break;

                default:
                    break;
            }
        }
    }
}