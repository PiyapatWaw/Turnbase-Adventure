using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Data
{
    public class WorldData
    {
        public Transform Container { get; private set; }
        public Transform HeroesContainer { get; private set; }
        public Transform MonsterContainer { get; private set; }
        public Dictionary<Vector2Int,Tile> AllTile { get; private set; }

        public WorldData(Transform container,Dictionary<Vector2Int,Tile> allTile)
        {
            Container = container;
            AllTile = allTile;
            HeroesContainer = new GameObject("Heroes").transform;
            MonsterContainer = new GameObject("Monster").transform;
            HeroesContainer.parent = container;
            MonsterContainer.parent = container;
        }

        public Tile GetTileFromCoordinate(Vector2Int coordinate)
        {
            if (AllTile.TryGetValue(coordinate, out var tile))
                return tile;
            Debug.LogError($"coordinate {coordinate} not register on map");
            return null;
        }
        
        public Tile GetEmptyRandomTile()
        {
            var tiles = AllTile.Values.Where(w => w.worldObject == null).ToList();
            if (tiles.Count > 0)
            {
                var tile = tiles[Random.Range(0, tiles.Count)];
                return tile;
            }

            return null;
        }
    }
}


