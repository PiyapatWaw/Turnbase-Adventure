using System;
using Game.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Data
{
    [Serializable]
    public struct SpawnAbleObject
    {
        public ESpawnable Type;
        public Actor Character;
    }

    [Serializable]
    public struct SpawnChnace
    {
        [Range(0, 100)] public int SpawnAfterEndTurn;
    }
}


