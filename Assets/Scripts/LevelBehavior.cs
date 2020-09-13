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

        private void Start()
        {
            // Put initialization of the level data here for now.
            Data = new LevelData();

            RefreshTileMap();

            // Move camera to above soil, center of the soil grid
            Vector3 initialCameraPosition = tileMap.CellToWorld(new Vector3Int((int)(Data.Width / 2 - 1), (int)(Data.Height - 1), 0));
            initialCameraPosition.z = -10.0f;
            Camera.main.transform.position = initialCameraPosition;
        }

        public void RefreshTileMap()
        {
            // TODO: Implement
            for (uint x = 0; x < Data.Width; ++x)
            {
                for (uint y = 0; y < Data.Height; ++y)
                {
                    Cell cell = Data.GetCellAt(x, y);
                    if (cell != null)
                    {
                        TileMapping tileMapping = tileMappings.FirstOrDefault((t) => t.cellType == cell.Type);
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
