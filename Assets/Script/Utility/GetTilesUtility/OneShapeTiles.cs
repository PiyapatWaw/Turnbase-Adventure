using System.Collections.Generic;
using Game.Data;
using Game.Interface;

namespace Game.Utility
{
    public class OneShapeTiles : IGetTiles
    {
        public GetTileResult GetTiles(Tile tile)
        {
            var result = new GetTileResult(true,new List<Tile>(){tile});
            return result;
        }
    }
}


