using System.Collections;
using System.Collections.Generic;
using Game.Data;
using Game.World;
using UnityEngine;

namespace Game.Setting
{
    [CreateAssetMenu(fileName = "SpawnSetting", menuName = "ScriptableObjects/SpawnSetting", order = 2)]
    public class SpawnSetting : ScriptableObject
    {
        [Range(1,10)]
        public int initialHero;
        [Range(0,10)]
        public int initialMonster;
        
        public SpawnChnace HeroChance;
        public SpawnChnace MonsterChance;
        
        public List<SpawnAbleObject> AllHero;
        public List<SpawnAbleObject> AllMonster;

        public HPBar HpBar;
        public Identifier Identifier;

        public int TurnToEvolution;
        public int Statuspoint;
    }
}