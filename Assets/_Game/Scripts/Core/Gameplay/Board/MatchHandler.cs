using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class MatchHandler : MonoBehaviour
    {
        [SerializeField]
        private Board _board = default;

        private List<Item> _matches = default;

        public List<Item> Matches => _matches;

        void Awake()
        {
            _matches = new List<Item>();
        }

        public Item GetSelected()
        {
            for (var i = 0; i < Constants.BoardWidth; i++)
            {
                for (var j = 0; j < Constants.BoardHeight; j++)
                {
                    var cell = _board.Cells[i, j];
                    if (cell.Item == null)
                    {
                        continue;
                    }

                    if (cell.Item.IsSelected)
                    {
                        return cell.Item;
                    }
                }
            }

            return null;
        }

        public void FindMatches()
        {
            _matches.Clear();

            for (var i = 0; i < Constants.BoardWidth; i++)
            {
                for (var j = 0; j < Constants.BoardHeight; j++)
                {
                    var item = _board.Cells[i, j].Item;
                    if (item == null)
                    {
                        continue;
                    }

                    if (i > 0 && i < Constants.BoardWidth - 1)
                    {
                        var leftItem = _board.Cells[i - 1, j].Item;
                        var rightItem = _board.Cells[i + 1, j].Item;

                        if (leftItem != null &&
                            rightItem != null)
                        {
                            if (leftItem.Type == item.Type &&
                                rightItem.Type == item.Type)
                            {
                                UpdateMatches(leftItem);
                                UpdateMatches(item);
                                UpdateMatches(rightItem);
                            }
                        }
                    }

                    if (j > 0 && j < Constants.BoardHeight - 1)
                    {
                        var lowerItem = _board.Cells[i, j - 1].Item;
                        var upperItem = _board.Cells[i, j + 1].Item;

                        if (upperItem != null &&
                            lowerItem != null)
                        {
                            if (lowerItem.Type == item.Type &&
                             upperItem.Type == item.Type)
                            {
                                UpdateMatches(lowerItem);
                                UpdateMatches(item);
                                UpdateMatches(upperItem);
                            }
                        }
                    }
                }
            }
        }

        private void UpdateMatches(Item item)
        {
            if (_matches.Contains(item))
            {
                return;
            }

            _matches.Add(item);
        }
    }
}