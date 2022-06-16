using UnityEngine;

namespace Game.Scripts.Core
{
    public class Cell
    {
        private readonly Vector2Int _gridPosition;

        private readonly Vector2 _worldPosition;

        private Item _item;

        public Vector2Int GridPosition => _gridPosition;

        public Vector2 WorldPosition => _worldPosition;

        public Item Item => _item;

        public Cell(int gridPositionX, int gridPositionY, float worldPositionX, float worldPositionY)
        {
            _gridPosition = new Vector2Int(gridPositionX, gridPositionY);
            _worldPosition = new Vector2(worldPositionX, worldPositionY);
        }

        public void SetItem(Item item)
        {
            _item = item;

            _item.UpdateGridPosition(_gridPosition.x, _gridPosition.y);
            _item.UpdateWorldPosition(_worldPosition.x, _worldPosition.y);
        }

        public void Empty()
        {
            _item = null;
        }
    }
}