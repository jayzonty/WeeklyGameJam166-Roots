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

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mouseWorldPosition.z = 0.0f;

                Vector3Int cellPosition = levelTileMap.WorldToCell(mouseWorldPosition);

                levelBehavior.Data.SetCellAt(cellPosition.x, cellPosition.y, new RootBranchCell(cellPosition.x, cellPosition.y, selectedRootBranchIndex));
            }
        }
    }
}
