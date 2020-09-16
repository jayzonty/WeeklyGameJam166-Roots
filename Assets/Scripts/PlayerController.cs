using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WGJRoots
{
    public class PlayerController : MonoBehaviour
    {
        public Tilemap levelTileMap;
        public LevelBehavior levelBehavior;
        public GameState gameState;

        public float defaultOrthoSize = 5.0f;
        public float minOrthoSize = 4.0f;
        public float maxOrthoSize = 8.0f;

        private int selectedRootBranchIndex = 0;
        private List<RootBranchCell> rootBranchTipCells = new List<RootBranchCell>();

        private Vector3 prevMousePosition;

        private void Start()
        {
            for (int x = 0; x < levelBehavior.Data.Width; ++x)
            {
                for (int y = 0; y < levelBehavior.Data.Height; ++y)
                {
                    Cell cell = levelBehavior.Data.GetForegroundCellAt(x, y);
                    if (cell is RootBranchCell)
                    {
                        RootBranchCell rootBranchCell = cell as RootBranchCell;
                        rootBranchTipCells.Add(rootBranchCell);
                    }
                }
            }

            rootBranchTipCells.Sort((a, b) => a.Index.CompareTo(b.Index));

            CenterCameraToSelectedRootBranch();
            Camera.main.orthographicSize = defaultOrthoSize;

            prevMousePosition = Input.mousePosition;
        }

        private void Update()
        {
            if (gameState.IsGameOver)
            {
                return;
            }

            int dx = 0, dy = 0;
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                dy = 1;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                dy = -1;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                dx = -1;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                dx = 1;
            }

            if ((dx != 0) || (dy != 0))
            {
                RootBranchCell currentRootBranchCell = rootBranchTipCells[selectedRootBranchIndex];
                Cell targetCell = levelBehavior.Data.GetForegroundCellAt(currentRootBranchCell.X + dx, currentRootBranchCell.Y + dy);
                if ((targetCell != null) 
                    && targetCell.IsDiggable)
                {
                    if (targetCell.Type == Cell.CellType.Nutrient)
                    {
                        NutrientCell nutrientCell = targetCell as NutrientCell;

                        gameState.NutrientPoints += nutrientCell.NutrientValue;
                        gameState.BranchPoints += nutrientCell.BranchPointValue;
                    }

                    gameState.BranchPoints = Mathf.Max(gameState.BranchPoints - targetCell.BranchCost, 0);

                    RootBranchCell newRootBranch = new RootBranchCell(targetCell.X, targetCell.Y, selectedRootBranchIndex);
                    newRootBranch.SetParent(rootBranchTipCells[selectedRootBranchIndex]);
                    newRootBranch.IsHidden = false;
                    rootBranchTipCells[selectedRootBranchIndex].AddChild(newRootBranch);
                    rootBranchTipCells[selectedRootBranchIndex] = newRootBranch;

                    levelBehavior.Data.SetForegroundCellAt(newRootBranch.X, newRootBranch.Y, newRootBranch);
                    levelBehavior.Data.SetBackgroundCellHidden(newRootBranch.X, newRootBranch.Y, false);

                    levelBehavior.Data.SetCellsHidden(newRootBranch.X - 1, newRootBranch.Y, false);
                    levelBehavior.Data.SetCellsHidden(newRootBranch.X + 1, newRootBranch.Y, false);
                    levelBehavior.Data.SetCellsHidden(newRootBranch.X, newRootBranch.Y - 1, false);
                    levelBehavior.Data.SetCellsHidden(newRootBranch.X, newRootBranch.Y + 1, false);

                    CenterCameraToSelectedRootBranch();

                    if (!HasValidMoves())
                    {
                        gameState.GameOver();
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                selectedRootBranchIndex = ((selectedRootBranchIndex - 1) + rootBranchTipCells.Count) % rootBranchTipCells.Count;
                CenterCameraToSelectedRootBranch();
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                selectedRootBranchIndex = (selectedRootBranchIndex + 1) % rootBranchTipCells.Count;
                CenterCameraToSelectedRootBranch();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                CenterCameraToSelectedRootBranch();
            }
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 prevMouseWorldPos = Camera.main.ScreenToWorldPoint(prevMousePosition);
                Vector3 currentMouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 mousePosDelta = currentMouseWorldPos - prevMouseWorldPos;
                Camera.main.transform.position -= mousePosDelta;
            }

            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - Input.mouseScrollDelta.y, minOrthoSize, maxOrthoSize);

            prevMousePosition = Input.mousePosition;
        }

        private void CenterCameraToSelectedRootBranch()
        {
            Vector3 cameraPos = levelTileMap.CellToWorld(new Vector3Int(rootBranchTipCells[selectedRootBranchIndex].X, rootBranchTipCells[selectedRootBranchIndex].Y, 0)) + levelTileMap.tileAnchor;
            cameraPos.z = -10.0f;
            Camera.main.transform.position = cameraPos;
        }

        private bool HasValidMoves()
        {
            for (int i = 0; i < rootBranchTipCells.Count; ++i)
            {
                Cell cell = rootBranchTipCells[i];

                if (levelBehavior.Data.IsForegroundCellDiggable(cell.X - 1, cell.Y)
                    || levelBehavior.Data.IsForegroundCellDiggable(cell.X + 1, cell.Y)
                    || levelBehavior.Data.IsForegroundCellDiggable(cell.X, cell.Y - 1)
                    || levelBehavior.Data.IsForegroundCellDiggable(cell.X, cell.Y + 1))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
