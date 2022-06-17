using System;
using Game.Scripts.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Core
{
    public class Board : MonoBehaviour
    {
        [SerializeField, Space]
        private BoardPool _pool = default;

        [SerializeField, Space]
        private MatchHandler _matchHandler = default;

        [SerializeField, Space]
        private MovementStateHandler _movementStateHandler = default;

        private Cell[,] _cells = default;

        private int _itemTypeCount = 0;

        private bool _haveItemsStoppedMoving = false;

        public Cell[,] Cells => _cells;

        void Start()
        {
            _cells = new Cell[Constants.BoardWidth, Constants.BoardHeight];

            _itemTypeCount = Enum.GetNames(typeof(ItemType)).Length;

            CreateBoard();
        }

        void Update()
        {
            var selectedItem = _matchHandler.GetSelected();
            if (selectedItem != null)
            {
                EmptyCell(selectedItem.GridPosition.x, selectedItem.GridPosition.y);
                UpdateBoard();
            }

            var haveItemsStoppedMoving = _movementStateHandler.CheckIfItemsStoppedMoving();
            if (_haveItemsStoppedMoving == haveItemsStoppedMoving)
                return;

            _haveItemsStoppedMoving = haveItemsStoppedMoving;
            if (_haveItemsStoppedMoving)
                ProcessMatches();
                
            UpdateBoard();
        }

        private void CreateBoard()
        {
            var firstCellPosition =
                new Vector2(
                    -Constants.BoardWidth * 0.5f + Constants.UnitLength * 0.5f,
                    -Constants.BoardHeight * 0.5f + Constants.UnitLength * 0.5f);

            for (var i = 0; i < Constants.BoardWidth; i++)
            {
                for (var j = 0; j < Constants.BoardHeight; j++)
                {
                    CreateCell(firstCellPosition, i, j);
                }
            }
        }

        private void UpdateBoard()
        {
            for (var i = 0; i < Constants.BoardWidth; i++)
            {
                for (var j = 0; j < Constants.BoardHeight - 1; j++)
                {
                    var cell = _cells[i, j];
                    if (cell.Item != null)
                    {
                        continue;
                    }

                    var upperCell = _cells[i, j + 1];

                    var iterations = 1;
                    while (upperCell.Item == null &&
                           iterations < Constants.BoardHeight - j - 1)
                    {
                        upperCell = _cells[i, j + iterations + 1];
                        iterations++;
                    }

                    if (upperCell.Item == null)
                    {
                        break;
                    }

                    _cells[i, j].SetItem(upperCell.Item);
                    upperCell.Empty();
                }
            }
        }

        private void EmptyBoard()
        {
            for (var i = 0; i < Constants.BoardWidth; i++)
            {
                for (var j = 0; j < Constants.BoardHeight; j++)
                {
                    var cell = _cells[i, j];

                    if (cell.Item != null)
                    {
                        cell.Empty();
                    }
                }
            }
        }

        private void CreateCell(Vector2 firstCellPosition, int i, int j)
        {
            if (_cells[i, j] != null)
            {
                return;
            }

            var worldPositionX = firstCellPosition.x + i * Constants.UnitLength;
            var worldPositionY = firstCellPosition.y + j * Constants.UnitLength;

            _cells[i, j] = new Cell(i, j, worldPositionX, worldPositionY);

            var item = CreateItem(i, j);
            _cells[i, j].SetItem(item);
        }

        private void EmptyCell(int gridPositionX, int gridPositionY)
        {
            var cell = _cells[gridPositionX, gridPositionY];
            var item = cell.Item;

            _pool.ReleaseItem(item);
            cell.Empty();
        }

        private Item CreateItem(int gridPositionX, int gridPositionY)
        {
            var itemTypeInt = Random.Range(0, _itemTypeCount);
            var itemType = (ItemType) itemTypeInt;

            var iterations = 0;
            var maxIterations = Constants.BoardWidth * Constants.BoardHeight;
            while (!CheckIfAppropriateTypeForItem(gridPositionX, gridPositionY, itemType) &&
                   iterations < maxIterations)
            {
                itemTypeInt = Random.Range(0, _itemTypeCount);
                itemType = (ItemType) itemTypeInt;
                iterations++;
            }

            var item = _pool.GetItem(itemType);
            return item;
        }

        private bool CheckIfAppropriateTypeForItem(int x, int y, ItemType itemType)
        {
            if (x > 1)
            {
                if (_cells[x - 1, y].Item.Type == itemType &&
                    _cells[x - 2, y].Item.Type == itemType)
                {
                    return false;
                }
            }

            if (y > 1)
            {
                if (_cells[x, y - 1].Item.Type == itemType &&
                    _cells[x, y - 2].Item.Type == itemType)
                {
                    return false;
                }
            }

            return true;
        }

        private void ProcessMatches()
        {
            _matchHandler.FindMatches();

            for (var i = 0; i < _matchHandler.Matches.Count; i++)
            {
                var position = _matchHandler.Matches[i].GridPosition;
                EmptyCell(position.x, position.y);
            }
        }
    }
}