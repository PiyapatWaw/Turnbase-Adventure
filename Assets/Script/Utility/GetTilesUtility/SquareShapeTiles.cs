using System.Collections.Generic;
using System.Linq;
using Game.Data;
using Game.Interface;

namespace Game.Utility
{
    public class SquareShapeTiles : IGetTiles
    {
        public GetTileResult GetTiles(Tile tile)
        {
            var possibleSquares = FindPossibleSquares(tile);
            var square = possibleSquares.FirstOrDefault(w => w.Count == 4);

            if (square != null)
                return new GetTileResult(true, square);
            
            return new GetTileResult(false, new List<Tile>());
        }
        
        private List<List<Tile>> FindPossibleSquares(Tile tile)
        {
            var possibleSquares = new List<List<Tile>>();
            var plusNeighbors = tile.neighbor[ENeighborType.Plus].Where(n => n.worldObject == null).ToList();

            foreach (var neighbor in plusNeighbors)
            {
                possibleSquares.Add(Search(tile, neighbor));
            }

            return possibleSquares;
        }

        private List<Tile> Search(Tile first, Tile second)
        {
            var square = new List<Tile> { first, second };
            var crossNeighbors = first.neighbor[ENeighborType.Cross].Where(n => n.worldObject == null).ToList();

            foreach (var third in crossNeighbors)
            {
                if (second.neighbor[ENeighborType.Plus].Contains(third))
                {
                    //select fourth from first and third plus contain without environment
                    var fourth = third.neighbor[ENeighborType.Plus].Where(w=>w != second).FirstOrDefault(n => first.neighbor[ENeighborType.Plus].Contains(n) && n.worldObject == null);
                    if (fourth != null)
                    {
                        square.Add(third);
                        square.Add(fourth);
                        break;
                    }
                }
            }
            
            return square;
        }
    }
}