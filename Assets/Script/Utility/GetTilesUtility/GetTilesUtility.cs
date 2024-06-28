using System.Collections.Generic;
using Game.Data;
using Game.Interface;

namespace Game.Utility
{
    public static class GetTilesUtility
    {
        private static Dictionary<EShape, IGetTiles> tilesFromShapeHandler;

        public static void InitailizeGetTilesFromShape(params (EShape shape,IGetTiles getTiles)[] GetTiels)
        {
            tilesFromShapeHandler = new Dictionary<EShape, IGetTiles>();
            foreach (var getTile in GetTiels)
            {
                tilesFromShapeHandler.Add(getTile.shape,getTile.getTiles);
            }
        }

        public static GetTileResult GetTilesFromShape(EShape shape, Tile tile)
        {
            GetTileResult result = new GetTileResult();
            if (tilesFromShapeHandler.TryGetValue(shape, out var handler))
            {
                return handler.GetTiles(tile);
            }
            
            return result;
        }
    }
}


