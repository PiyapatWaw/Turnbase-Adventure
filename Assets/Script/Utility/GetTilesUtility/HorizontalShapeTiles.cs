using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game.Data;
using Game.Interface;

namespace Game.Utility
{
    public class HorizontalShapeTiles : IGetTiles
    {
        public GetTileResult GetTiles(Tile tile)
        {
            var tiles = new List<Tile>() {tile};
            var usableNeighnor = tile.neighbor[ENeighborType.Plus].Where(w => w.worldObject == null && w.coordinate.x == tile.coordinate.x).ToList();
            if (usableNeighnor.Count > 0)
            {
                tiles.Add(usableNeighnor[Random.Range(0,usableNeighnor.Count)]);
                return new GetTileResult(true,tiles);
            }
            
            return new GetTileResult();
           
        }
    }
}
