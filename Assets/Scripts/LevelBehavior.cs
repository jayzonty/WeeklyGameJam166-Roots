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

        public Tilemap backgroundTilemap;
        public Tilemap foregroundTilemap;

        public GameState gameState;
        public GameOverPanelBehavior gameOverPanel;

        public LevelData Data
        {
            get;
            set;
        }

        public Vector3 TreeStartingPoint
        {
            get
            {
                return backgroundTilemap.CellToWorld(new Vector3Int(Data.Width / 2 - 1, Data.Height - 1, 0)) + backgroundTilemap.tileAnchor;
            }
        }

        private void Awake()
        {
            // Put initialization of the level data here for now.
            Data = new LevelData();
        }

        private void OnEnable()
        {
            Data.OnLevelDataChanged += LevelData_OnLevelDataChanged;

            gameState.onGameOver.AddListener(HandleGameOver);
        }

        private void LevelData_OnLevelDataChanged(List<Vector3Int> changedCellsPositions)
        {
            RefreshTileMap(changedCellsPositions);
        }

        private void OnDisable()
        {
            Data.OnLevelDataChanged -= LevelData_OnLevelDataChanged;

            gameState.onGameOver.RemoveListener(HandleGameOver);
        }

        private void Start()
        {
            RefreshTileMap();

            gameOverPanel.gameObject.SetActive(false);
        }

        public void RefreshTileMap()
        {
            // TODO: Implement
            for (int x = 0; x < Data.Width; ++x)
            {
                for (int y = 0; y < Data.Height; ++y)
                {
                    Cell foregroundCell = Data.GetForegroundCellAt(x, y);
                    if (foregroundCell != null)
                    {
                        if (!foregroundCell.IsHidden)
                        {
                            TileMapping tileMapping = tileMappings.FirstOrDefault((t) => t.cellType == foregroundCell.Type);
                            if (tileMapping != null)
                            {
                                foregroundTilemap.SetTile(new Vector3Int(x, y, 0), tileMapping.tile);
                            }
                        }
                        else
                        {
                            foregroundTilemap.SetTile(new Vector3Int(x, y, 0), null);
                        }
                    }

                    Cell backgroundCell = Data.GetBackgroundCellAt(x, y);
                    if (backgroundCell != null)
                    {
                        TileMapping tileMapping = tileMappings.FirstOrDefault((t) => t.cellType == backgroundCell.Type);
                        if (tileMapping != null)
                        {
                            backgroundTilemap.SetTile(new Vector3Int(x, y, 0), tileMapping.tile);
                        }
                    }
                }
            }
        }

        public void RefreshTileMap(List<Vector3Int> changedCellsPositions)
        {
            foreach (Vector3Int cellPos in changedCellsPositions)
            {
                int x = cellPos.x;
                int y = cellPos.y;

                Cell foregroundCell = Data.GetForegroundCellAt(x, y);
                if (foregroundCell != null)
                {
                    if (!foregroundCell.IsHidden)
                    {
                        TileMapping tileMapping = tileMappings.FirstOrDefault((t) => t.cellType == foregroundCell.Type);
                        if (tileMapping != null)
                        {
                            foregroundTilemap.SetTile(new Vector3Int(x, y, 0), tileMapping.tile);
                        }
                    }
                    else
                    {
                        foregroundTilemap.SetTile(new Vector3Int(x, y, 0), null);
                    }
                }

                Cell backgroundCell = Data.GetBackgroundCellAt(x, y);
                if (backgroundCell != null)
                {
                    TileMapping tileMapping = tileMappings.FirstOrDefault((t) => t.cellType == backgroundCell.Type);
                    if (tileMapping != null)
                    {
                        backgroundTilemap.SetTile(new Vector3Int(x, y, 0), tileMapping.tile);
                    }
                }
            }
        }

        private void HandleGameOver()
        {
            gameOverPanel.gameObject.SetActive(true);
            gameOverPanel.RefreshText();
        }
    }
}
