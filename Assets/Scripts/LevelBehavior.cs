using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace WGJRoots
{
    public class LevelBehavior : MonoBehaviour
    {
        [System.Serializable]
        public class TileMapping
        {
            public Cell.CellType cellType = Cell.CellType.Soil;
            public Tile tile;
        }

        public List<TileMapping> tileMappings;

        public Tilemap tileMap;

        public LevelData Data
        {
            get;
            set;
        }

        private void Awake()
        {
            // Put initialization of the level data here for now.
            Data = new LevelData();
        }

        private void OnEnable()
        {
            Data.OnLevelDataChanged += LevelData_OnLevelDataChanged;
        }

        private void LevelData_OnLevelDataChanged(List<Vector3Int> changedCellsPositions)
        {
            RefreshTileMap();
        }

        private void OnDisable()
        {
            Data.OnLevelDataChanged -= LevelData_OnLevelDataChanged;
        }

        private void Start()
        {
            RefreshTileMap();
        }

        public void RefreshTileMap()
        {
            // TODO: Implement
            for (int x = 0; x < Data.Width; ++x)
            {
                for (int y = 0; y < Data.Height; ++y)
                {
                    Cell cell = Data.GetCellAt(x, y);
                    if (cell != null)
                    {
                        TileMapping tileMapping = tileMappings.FirstOrDefault((t) => t.cellType == cell.Type);
                        if (tileMapping != null)
                        {
                            tileMap.SetTile(new Vector3Int(x, y, 0), tileMapping.tile);
                        }
                    }
                }
            }
        }
    }
}
