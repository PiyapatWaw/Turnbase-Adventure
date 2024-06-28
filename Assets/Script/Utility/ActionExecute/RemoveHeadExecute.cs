using System.Threading.Tasks;
using Game.Interface;
using UnityEngine;

namespace Game.Utility
{
    public class RemoveHeadExecute : IActionExecute<IWorldObject>
    {
        public async Task<EExecuteResult> Execute(GameManager gameManager,IWorldObject parameter)
        {
            var party = gameManager.HeroParty;
            var spawner = gameManager.Spawner;
            var target = party.Head;
            party.Remove(target);
            var newDirection = party.GetWalkableDirection();
            gameManager.InputManager.ResetLastInput(newDirection);
            spawner.Return(target);
            gameManager.World.GetTileFromCoordinate(target.coordinate).worldObject = null;
            
            if(party.IsPartyEmpty())
                return EExecuteResult.Over;
            else
            {
                var direction = target.coordinate - party.Head.coordinate;
                await ActionExecuteManager.Execute(EActionType.Move,direction);
                return EExecuteResult.None;
            }
                
        }
    }
}