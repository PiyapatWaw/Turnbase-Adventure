using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.Character;
using Game.Data;
using Game.Interface;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Utility
{
    public class BattleExecute : IActionExecute<IWorldObject>
    {
        private const int DelayBetweenActionsMilliseconds = 300;

        public async Task<EExecuteResult> Execute(GameManager gameManager, IWorldObject parameter)
        {
            var spawner = gameManager.Spawner;
            var world = gameManager.World;
            var party = gameManager.HeroParty;
            var input = gameManager.InputManager;

            if (!(parameter is Monster monster))
                return EExecuteResult.Continue;

            spawner.SpawnWorldCanvas(ESpawnable.HPBar,monster);
            
            var battleArea = GetBattleArea(world, party, monster);
            var battleParty = GetBattleParty(party, battleArea);

            await AttackSequence(monster,battleParty,battleArea);

            RemoveDefeated(world,party, spawner,input, battleParty);

            spawner.Return(monster.HpBar);
            monster.HpBar = null;

            var moveDirection = new Vector2Int();
            
            if (monster.HP <= 0)
            {
                moveDirection = monster.coordinate - battleParty.First().coordinate;
                var tile = world.GetTileFromCoordinate(monster.coordinate);
                tile.worldObject = null;
                spawner.Return(monster);
                gameManager.AddKill();
            }
            
            if (party.IsPartyEmpty())
                return EExecuteResult.Over;
            else
            {
                if (battleParty[0].HP <= 0)
                {
                    moveDirection =
                        battleParty[0].coordinate -
                        party.Member[0].coordinate; // death hero position - new head position
                    //Debug.LogErrorFormat("{0} - {1} = {2}",battleParty[0].coordinate,party.Member[0].coordinate,moveDirection);
                }


                if (moveDirection != new Vector2Int())
                {
                    await ActionExecuteManager.Execute(EActionType.Move, moveDirection);
                    return EExecuteResult.None;
                }
                    
                else
                    return EExecuteResult.None;
            }

        }

        private List<Tile> GetBattleArea(WorldData world, Party party, Monster monster)
        {
            var neighborTiles = world.GetTileFromCoordinate(monster.coordinate).neighbor;
            var battleArea = new List<Tile>
            {
                world.GetTileFromCoordinate(party.Head.coordinate)
            };
            battleArea.AddRange(neighborTiles[ENeighborType.Plus].Where(tile => tile.worldObject == null));
            battleArea.AddRange(neighborTiles[ENeighborType.Cross].Where(tile => tile.worldObject == null));
            return battleArea;
        }
        
        private List<Actor> GetBattleParty(Party party,List<Tile> battleArea)
        {
            var battleParty = party.GetBattleParty(battleArea.Count);
            return battleParty;
        }

        private async Task AttackSequence(Monster monster,List<Actor> battleParty,List<Tile> battleArea)
        {
            for (int i = 0; i < battleParty.Count; i++)
            {
                battleParty[i].transform.position = battleArea[i].characterPosition;
                battleParty[i].transform.LookAt(monster.transform);
            }
            
            await monster.AttackEnemy(battleParty.First());
            await Task.Delay(DelayBetweenActionsMilliseconds);

            foreach (var member in battleParty.Where(member => member.HP > 0))
            {
                if (monster.HP > 0)
                {
                    await member.AttackEnemy(monster);
                    await Task.Delay(DelayBetweenActionsMilliseconds);
                }
                else
                {
                    break;
                }
            }

            await Task.Delay(DelayBetweenActionsMilliseconds);
        }

        private void RemoveDefeated(WorldData world,Party party, ObjectSpawner spawner,InputManager input, List<Actor> battleParty)
        {
            for (int i = 1;  i<party.Member.Count; i++)
            {
                party.Member[i].transform.position = world.GetTileFromCoordinate(party.Member[i].coordinate).characterPosition;
                party.Member[i].transform.LookAt(party.Member[i-1].transform);
            }
            foreach (var member in battleParty)
            {
                var tile = world.GetTileFromCoordinate(member.coordinate);
                if (member.HP <= 0)
                {
                    party.Remove(member);
                    var newDirection = party.GetWalkableDirection();
                    input.ResetLastInput(newDirection);
                    tile.worldObject = null;
                    spawner.Return(member);
                }
            }
        }
    }
}
