using System.Collections.Generic;
using System.Linq;
using Game.Data;
using Game.Setting;
using Game.Utility;
using Game.World;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game
{
    public class WorldCreator
    {
        private WorldSetting _worldSetting;

        public WorldCreator(WorldSetting worldSetting)
        {
            _worldSetting = worldSetting;
        }

        public WorldData CreateWorld()
        {
            Transform container = new GameObject("World").transform;
            Transform tiles = new GameObject("Tiles").transform;
            Transform environments = new GameObject("Environments").transform;
            tiles.parent = container;
            environments.parent = container;

            var tilesize = _worldSetting.tileSize;

            var allTile = createTiles(tiles, tilesize);
            var environmentList = CreateEnvironment(environments, tilesize);

            SpreadEnvironment(allTile, environmentList);

            var world = new WorldData(container, allTile);

            return world;
        }

        private Dictionary<Vector2Int, Tile> createTiles(Transform parent, int size)
        {
            Dictionary<Vector2Int, Tile> allTile = new Dictionary<Vector2Int, Tile>();
            Dictionary<Vector2Int, Dictionary<ENeighborType, List<Vector2Int>>> allTileNeightbor = new Dictionary<Vector2Int, Dictionary<ENeighborType, List<Vector2Int>>>();

            for (int i = 0; i < _worldSetting.worldSize.x; i++)
            {
                for (int j = 0; j < _worldSetting.worldSize.y; j++)
                {
                    var tile = new GameObject(string.Format("Tile {0}x{1}", i, j), typeof(Tile)).GetComponent<Tile>();
                    tile.visual = Object.Instantiate(GetRandomTile(), tile.transform);
                    var coordinate = new Vector2Int(i, j);
                    
                    var neighborCoordinat = new Dictionary<ENeighborType, List<Vector2Int>>();
                    
                    List<Vector2Int> neighborPlus= new List<Vector2Int>();
                    List<Vector2Int> neighborCross = new List<Vector2Int>();
                    
                    neighborCoordinat.Add(ENeighborType.Plus,neighborPlus);
                    neighborCoordinat.Add(ENeighborType.Cross,neighborCross);
                    
                    if (i + 1 < _worldSetting.worldSize.x)
                        neighborPlus.Add(new Vector2Int(i + 1, j));
                    if (i - 1 > 0)
                        neighborPlus.Add(new Vector2Int(i - 1, j));
                    if (j + 1 < _worldSetting.worldSize.y)
                        neighborPlus.Add(new Vector2Int(i, j + 1));
                    if (j - 1 > 0)
                        neighborPlus.Add(new Vector2Int(i, j - 1));

                    if (i + 1 < _worldSetting.worldSize.x && j + 1 < _worldSetting.worldSize.y)
                        neighborCross.Add(new Vector2Int(i + 1, j+1));
                    if (i - 1 > 0 && j - 1 > 0)
                        neighborCross.Add(new Vector2Int(i - 1, j-1));
                    if (j + 1 < _worldSetting.worldSize.y && i - 1 > 0)
                        neighborCross.Add(new Vector2Int(i - 1, j + 1));
                    if (j - 1 > 0 && i + 1 < _worldSetting.worldSize.x)
                        neighborCross.Add(new Vector2Int(i + 1, j - 1));


                    tile.Initialize(parent, coordinate, size);
                    allTile.Add(coordinate, tile);
                    allTileNeightbor.Add(coordinate, neighborCoordinat);
                }
            }

            foreach (var tiles in allTile)
            {
                if (allTileNeightbor.TryGetValue(tiles.Key, out var neighbors))
                {
                    tiles.Value.neighbor = new Dictionary<ENeighborType, List<Tile>>();
                    tiles.Value.neighbor.Add(ENeighborType.Plus,new List<Tile>());
                    tiles.Value.neighbor.Add(ENeighborType.Cross,new List<Tile>());
                    foreach (var coordinate in neighbors[ENeighborType.Plus])
                    {
                        tiles.Value.neighbor[ENeighborType.Plus].Add(allTile[coordinate]);
                    }
                    foreach (var coordinate in neighbors[ENeighborType.Cross])
                    {
                        tiles.Value.neighbor[ENeighborType.Cross].Add(allTile[coordinate]);
                    }
                }
            }

            return allTile;
        }

        private List<Environment> CreateEnvironment(Transform parent, int size)
        {
            var environmentList = new List<Environment>();
            int environmentSpace = _worldSetting.EnvironmentTotalSpace;
            while (environmentSpace > 0)
            {
                var environment = Object.Instantiate(GetRandomEnvironment(environmentSpace), parent);
                environment.transform.localScale = new Vector3(size, size, size);
                environment.transform.eulerAngles = new Vector3(0, 0, 0);
                environmentList.Add(environment);
                environmentSpace -= environment.shape.ShapeSize();
            }

            return environmentList.OrderByDescending(o=>o.shape.ShapeSize()).ToList();
        }
        
        

        private void SpreadEnvironment(Dictionary<Vector2Int, Tile> allTile, List<Environment> enironments)
        {
            foreach (var enironment in enironments)
            {
                var getResult = new List<GetTileResult>();
                foreach (var tile in allTile.Values.Where(w=>w.worldObject == null))
                {
                    var possible = GetTilesUtility.GetTilesFromShape(enironment.shape, tile);
                    if (possible.IsUsable)
                    {
                        getResult.Add(possible);
                    }
                }

                var selectPossible = getResult[Random.Range(0, getResult.Count)];

                Vector3 totalposition = Vector3.zero;
                
                foreach (var select in selectPossible.Tiles)
                {
                    select.worldObject = enironment;
                    totalposition += select.transform.localPosition;
                }
                
                enironment.transform.localPosition = totalposition / selectPossible.Tiles.Count;
                
            }
        }

        private GameObject GetRandomTile()
        {
            return _worldSetting.tiles[Random.Range(0, _worldSetting.tiles.Count)];
        }

        private Environment GetRandomEnvironment(int remainSpace)
        {
            var list = _worldSetting.environment.Where(w => w.shape.ShapeSize() <= remainSpace).ToList();
            return list[Random.Range(0, list.Count)];
        }
    }
}