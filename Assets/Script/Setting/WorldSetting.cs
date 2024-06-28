using System.Collections;
using System.Collections.Generic;
using Game.World;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Setting
{
    [CreateAssetMenu(fileName = "WorldSetting", menuName = "ScriptableObjects/WorldSetting", order = 1)]
    public class WorldSetting : ScriptableObject
    {
        public Vector2Int worldSize;
        public int tileSize;
        [Range(0,100)]
        public int environmentPercent;
        public List<GameObject> tiles;
        public List<Environment> environment;

        public int EnvironmentTotalSpace => (worldSize.x * worldSize.y) * environmentPercent / 100;

    }
}


