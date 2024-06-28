using System.Collections;
using System.Collections.Generic;
using Game.Interface;
using Game.Utility;
using UnityEngine;

namespace Game
{
    public class GameBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Main()
        {
            GetTilesUtility.InitailizeGetTilesFromShape(
                (EShape.Vertical, new VerticalShapeTiles()),
                (EShape.Horizontal, new HorizontalShapeTiles()),
                (EShape.Square, new SquareShapeTiles()),
                (EShape.One, new OneShapeTiles())
            );

            ActionExecuteManager.Initailize(
                (EActionType.Battle, new BattleExecute()),
                (EActionType.Over, new OverExecute()),
                (EActionType.Party, new PartyExecute()),
                (EActionType.RemoveHead, new RemoveHeadExecute()),
                (EActionType.Move, new MoveExecute()),
                (EActionType.Swap, new SwapExecute()),
                (EActionType.Evolution, new EvolutionExecute())
            );
        }
    }
}