using System.Collections.Generic;

namespace Game.Data
{
    public struct GetTileResult
    {
        public bool IsUsable;
        public List<Tile> Tiles;

        public GetTileResult(bool isUsable,List<Tile> tiles)
        {
            this.IsUsable = isUsable;
            this.Tiles = tiles;
        }
    }
}


