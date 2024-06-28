using System.Threading.Tasks;
using Game.Interface;
using UnityEngine;


namespace Game.Utility
{
    public class MoveExecute : IActionExecute<Vector2Int>
    {
        public async Task<EExecuteResult> Execute(GameManager gameManager,Vector2Int parameter)
        {
            var party = gameManager.HeroParty;
            var newTileCoordinate = party.Head.coordinate + parameter;
            var next = gameManager.World.GetTileFromCoordinate(newTileCoordinate);
            if (next)
            {
                var result = await BeforeMove(next);
                if (SolveExecuteResult(result))
                {
                    await party.UpdatePartyPosition(next);
                }
            }
            
            return EExecuteResult.Continue;
        }
        
        private async Task<EExecuteResult> BeforeMove(Tile nextTile)
        {
            if (nextTile.worldObject != null)
            {
                return await ActionExecuteManager.Execute(nextTile.worldObject.action,nextTile.worldObject);
            }

            return EExecuteResult.Continue;
        }
        
        private bool SolveExecuteResult(EExecuteResult result)
        {
            if (result == EExecuteResult.Over)
            {
                GameManager.Singleton.GameOver();
                return false;
            }
            else if (result == EExecuteResult.None)
            {
                return false;
            }
            else
                return true;
        }
    }
}

