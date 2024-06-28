using System.Threading.Tasks;
using Game.Character;
using Game.Interface;
using UnityEngine;

namespace Game.Utility
{
    public class EvolutionExecute: IActionExecute<Monster>
    {
        public async Task<EExecuteResult> Execute(GameManager gameManager,Monster parameter)
        {
            var tile = gameManager.World.GetTileFromCoordinate(parameter.coordinate);
            
            gameManager.Spawner.Return(parameter);
            var newMonster = gameManager.Spawner.SpawnMonster(gameManager.World.MonsterContainer,parameter.Level+1,tile);
            
            return EExecuteResult.None;
        }
    }
}