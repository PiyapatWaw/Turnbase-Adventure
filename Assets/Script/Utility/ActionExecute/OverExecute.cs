using System.Threading.Tasks;
using Game.Interface;
using UnityEngine;

namespace Game.Utility
{
    public class OverExecute : IActionExecute<IWorldObject>
    {
        public async Task<EExecuteResult> Execute(GameManager gameManager,IWorldObject parameter)
        {
            return EExecuteResult.Over;
        }
    }
}