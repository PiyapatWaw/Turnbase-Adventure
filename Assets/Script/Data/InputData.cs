using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    public struct InputData
    {
        public InputData(EActionType action, Vector2Int value)
        {
            Action = action;
            Value = value;
        }

        public EActionType Action;
        public Vector2Int Value;
    }
}


