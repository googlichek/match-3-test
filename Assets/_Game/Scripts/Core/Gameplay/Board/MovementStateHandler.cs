using UnityEngine;

namespace Game.Scripts.Core
{
    public class MovementStateHandler : MonoBehaviour
    {
        [SerializeField]
        private Board _board = default;

        public bool CheckIfItemsStoppedMoving()
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

                    if (!cell.Item.IsInPosition)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}