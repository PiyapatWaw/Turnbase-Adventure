using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Interface
{
    public interface IActionExecute<Data>
    {
        public Task<EExecuteResult> Execute(GameManager gameManager,Data parameter);
    }
}


