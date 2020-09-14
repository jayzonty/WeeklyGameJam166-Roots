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

        private int selectedRootBranchIndex = 0;
        private List<RootBranchCell> rootBranchTipCells = new List<RootBranchCell>();

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
        }

        private void Update()
        {
            if (gameState.IsGameOver)
            {
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mouseWorldPosition.z = 0.0f;

                Vector3Int clickedCellPosition = levelTileMap.WorldToCell(mouseWorldPosition);
                
                List<Vector3Int> clickableCellLocations = GetClickableCellLocations();
                if (clickableCellLocations.Contains(clickedCellPosition))
                {
                    Cell clickedCell = levelBehavior.Data.GetForegroundCellAt(clickedCellPosition.x, clickedCellPosition.y);
                    if (clickedCell != null)
                    {
                        if (gameState.BranchPoints >= clickedCell.BranchCost)
                        {
                            if (clickedCell.Type == Cell.CellType.Nutrient)
                            {
                                NutrientCell nutrientCell = clickedCell as NutrientCell;

                                gameState.NutrientPoints += nutrientCell.NutrientValue;
                                gameState.BranchPoints += nutrientCell.BranchPointValue;
                            }

                            gameState.BranchPoints -= clickedCell.BranchCost;

                            RootBranchCell newRootBranch = new RootBranchCell(clickedCellPosition.x, clickedCellPosition.y, selectedRootBranchIndex);
                            newRootBranch.SetParent(rootBranchTipCells[selectedRootBranchIndex]);
                            newRootBranch.IsHidden = false;
                            rootBranchTipCells[selectedRootBranchIndex].AddChild(newRootBranch);
                            rootBranchTipCells[selectedRootBranchIndex] = newRootBranch;

                            levelBehavior.Data.SetForegroundCellAt(clickedCellPosition.x, clickedCellPosition.y, newRootBranch);

                            levelBehavior.Data.SetForegroundCellHidden(clickedCellPosition.x - 1, clickedCellPosition.y, false);
                            levelBehavior.Data.SetForegroundCellHidden(clickedCellPosition.x + 1, clickedCellPosition.y, false);
                            levelBehavior.Data.SetForegroundCellHidden(clickedCellPosition.x, clickedCellPosition.y - 1, false);
                            levelBehavior.Data.SetForegroundCellHidden(clickedCellPosition.x, clickedCellPosition.y + 1, false);

                            levelBehavior.RefreshTileMap();

                            CenterCameraToSelectedRootBranch();
                        }
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
        }

        private void CenterCameraToSelectedRootBranch()
        {
            Vector3 cameraPos = levelTileMap.CellToWorld(new Vector3Int(rootBranchTipCells[selectedRootBranchIndex].X, rootBranchTipCells[selectedRootBranchIndex].Y, 0)) + levelTileMap.tileAnchor;
            cameraPos.z = -10.0f;
            Camera.main.transform.position = cameraPos;
        }

        private List<Vector3Int> GetClickableCellLocations()
        {
            List<Vector3Int> ret = new List<Vector3Int>();
            foreach (RootBranchCell rootBranchTip in rootBranchTipCells)
            {
                ret.Add(new Vector3Int(rootBranchTip.X - 1, rootBranchTip.Y, 0));
                ret.Add(new Vector3Int(rootBranchTip.X + 1, rootBranchTip.Y, 0));
                ret.Add(new Vector3Int(rootBranchTip.X, rootBranchTip.Y - 1, 0));
                ret.Add(new Vector3Int(rootBranchTip.X, rootBranchTip.Y + 1, 0));
            }
            return ret;
        }
    }
}
