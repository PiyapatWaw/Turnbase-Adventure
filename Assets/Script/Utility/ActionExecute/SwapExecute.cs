using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.Character;
using Game.Data;
using Game.Interface;
using UnityEngine;

namespace Game.Utility
{
    public class SwapExecute: IActionExecute<Vector2Int>
    {
        public async Task<EExecuteResult> Execute(GameManager gameManager,Vector2Int parameter)
        {
            var party = gameManager.HeroParty;

            if (party.Member.Count > 1)
            {
                List<Actor> sort = new List<Actor>();
                List<ActorData> sortData = new List<ActorData>();
                foreach (var member in party.Member)
                {
                    sortData.Add(new ActorData(member.transform.position,member.transform.eulerAngles,member.coordinate,gameManager.World.GetTileFromCoordinate(member.coordinate)));
                }

                if (parameter.x > 0) // Q
                {
                    for (int i = 1; i < party.Member.Count; i++)
                    {
                        sort.Add(party.Member[i]);
                    }

                    sort.Add(party.Member[0]);
                }
                else if (parameter.x < 0) // E
                {
                    sort.Add(party.Tail);
                    
                    for (int i = 0; i < party.Member.Count - 1; i++)
                    {
                        sort.Add(party.Member[i]);
                    }
                }

                for (int i = 0; i < sort.Count; i++)
                {
                    sort[i].transform.position = sortData[i].position;
                    sort[i].transform.eulerAngles = sortData[i].eulerAngles;
                    
                    sort[i].coordinate = sortData[i].coordinate;
                    sortData[i].tile.worldObject = sort[i];
                }

                party.ReplaceMembers(sort);
                
                party.UpdateIdentifier();
            }
            
            return EExecuteResult.None;
        }
    }

}