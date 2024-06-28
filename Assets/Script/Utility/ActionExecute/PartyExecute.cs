using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Character;
using Game.Interface;
using UnityEngine;

namespace Game.Utility
{
    public class PartyExecute : IActionExecute<IWorldObject>
    {
        public async Task<EExecuteResult> Execute(GameManager gameManager,IWorldObject parameter)
        {
            if (parameter is Actor actor)
            {
                actor.gameObject.SetActive(false);
                await Task.Delay(100);
                actor.transform.position = gameManager.HeroParty.Tail.transform.position;
                gameManager.HeroParty.Add(actor);
                actor.gameObject.SetActive(true);
            }

            return EExecuteResult.Continue;
        }
    }
}


