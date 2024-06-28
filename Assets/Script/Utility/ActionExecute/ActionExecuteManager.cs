using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Data;
using Game.Interface;

namespace Game.Utility
{
    public static class ActionExecuteManager
    {
        private static Dictionary<EActionType, object> actionExecutesHandler;

        public static void Initailize(params (EActionType action,object handler)[] handlers)
        {
            actionExecutesHandler = new Dictionary<EActionType, object>();
            foreach (var execute in handlers)
            {
                actionExecutesHandler.Add(execute.action,execute.handler);
            }
        }

        public static async Task<EExecuteResult> Execute<TData>(EActionType type,TData data)
        {
            if (actionExecutesHandler.TryGetValue(type, out var handlerObj))
            {
                if (handlerObj is IActionExecute<TData> handler)
                {
                    return await handler.Execute(GameManager.Singleton,data);
                }
            }

            return EExecuteResult.Continue;
        }
    }
}
