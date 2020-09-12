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
            public LevelData.Cell.CellType cellType = LevelData.Cell.CellType.Soil;
            public Tile tile;
        }

        public List<TileMapping> tileMappings;

        public Tilemap tileMap;

        public LevelData Data
        {
            get;
            set;
        }

        private void Start()
        {
            // Put initialization of the level data here for now.
            Data = new LevelData();

            RefreshTileMap();
        }

        public void RefreshTileMap()
        {
            // TODO: Implement
            for (uint x = 0; x < Data.Width; ++x)
            {
                for (uint y = 0; y < Data.Height; ++y)
                {
                    LevelData.Cell cell = Data.GetCellAt(x, y);
                    if (cell != null)
                    {
                        TileMapping tileMapping = tileMappings.FirstOrDefault((t) => t.cellType == cell.cellType);
                        if (tileMapping != null)
                        {
                            tileMap.SetTile(new Vector3Int((int)x, (int)y, 0), tileMapping.tile);
                        }
                    }
                }
            }
        }
    }
}
