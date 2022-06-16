using Game.Scripts.Data;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class Item : MonoBehaviour
    {
        [SerializeField]
        private ItemData _data = default;

        private Vector2Int _gridPosition = default;

        private Vector2 _worldPosition = default;

        private Vector2 _movementVelocity = default;

        private bool _isSelected = false;

        private bool _isInPosition = false;

        public ItemType Type => _data.Type;

        public Vector2Int GridPosition => _gridPosition;

        public Vector2 WorldPosition => _worldPosition;

        public bool IsSelected => _isSelected;

        public bool IsInPosition => _isInPosition;

        void OnEnable()
        {
            _isSelected = false;
            _isInPosition = false;
        }

        void Update()
        {
            if (_isInPosition)
            {
                return;
            }

            transform.position =
                Vector2.SmoothDamp(
                    transform.position,
                    _worldPosition,
                    ref _movementVelocity,
                    _data.MovementDuration);

            if (_worldPosition.IsEqual(transform.position))
            {
                _isInPosition = true;
            }
        }

        void OnMouseDown()
        {
            _isSelected = true;
        }

        public void UpdateGridPosition(int x, int y)
        {
            _gridPosition = new Vector2Int(x, y);
        }

        public void UpdateWorldPosition(float x, float y)
        {
            _worldPosition = new Vector2(x, y);
            _isInPosition = false;
        }
    }
}