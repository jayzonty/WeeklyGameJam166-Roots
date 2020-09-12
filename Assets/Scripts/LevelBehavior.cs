using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WGJRoots
{
    public class LevelBehavior : MonoBehaviour
    {
        public Tilemap tileMap;

        public LevelData LevelData
        {
            get;
            set;
        }

        private void Start()
        {
            // Put initialization of the level data here for now.
            LevelData = new LevelData();

            RefreshTileMap();
        }

        public void RefreshTileMap()
        {
            // TODO: Implement
        }
    }
}
