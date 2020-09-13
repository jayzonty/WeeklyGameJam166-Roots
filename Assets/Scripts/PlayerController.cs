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

        private int selectedRootBranchIndex = 0;
        private List<RootBranchCell> rootBranchTipCells = new List<RootBranchCell>();

        private void Start()
        {
            for (int x = 0; x < levelBehavior.Data.Width; ++x)
            {
                for (int y = 0; y < levelBehavior.Data.Height; ++y)
                {
                    Cell cell = levelBehavior.Data.GetCellAt(x, y);
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
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mouseWorldPosition.z = 0.0f;

                Vector3Int cellPosition = levelTileMap.WorldToCell(mouseWorldPosition);

                RootBranchCell newRootBranch = new RootBranchCell(cellPosition.x, cellPosition.y, selectedRootBranchIndex);
                newRootBranch.SetParent(rootBranchTipCells[selectedRootBranchIndex]);
                rootBranchTipCells[selectedRootBranchIndex].AddChild(newRootBranch);
                rootBranchTipCells[selectedRootBranchIndex] = newRootBranch;

                levelBehavior.Data.SetCellAt(cellPosition.x, cellPosition.y, newRootBranch);
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
    }
}
