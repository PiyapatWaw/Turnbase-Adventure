using System.Linq;
using System.Collections.Generic;
using Game.Character;
using Game.Data;
using Game.Interface;
using Game.Setting;
using Game.UI;
using Game.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Singleton;

        [SerializeField] private WorldSetting _worldSetting;
        [SerializeField] private SpawnSetting _spawnSetting;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private NavigatorUI _navigatorUI;
        [SerializeField] private GamePlayUI _gamePlayUI;
        [SerializeField] private OverUI _overUI;
        

        private WorldData world;
        private ObjectSpawner spawner;
        private Party heroParty;
        private InputManager inputManager;
        private List<Actor> allmonster;
        private bool Isplay;

        public int Turn { get;private set; }
        public int Kill { get;private set; }

        
        public Party HeroParty => heroParty;
        public WorldData World => world;
        public ObjectSpawner Spawner => spawner;
        public InputManager InputManager => inputManager;

        public delegate void OnEndTurn();
        public event OnEndTurn OnEndTurnEvent;

        private void Awake()
        {
            Singleton = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            inputManager = new InputManager(_playerInput);
            var worldCreator = new WorldCreator(_worldSetting);
            world = worldCreator.CreateWorld();
            world.Container.SetParent(this.transform);

            spawner = new ObjectSpawner(_spawnSetting, world);
            heroParty = new Party(world,spawner);

            for (int i = 0; i < _spawnSetting.initialHero; i++)
            {
                spawner.SpawnHero(world.HeroesContainer);
            }

            for (int i = 0; i < _spawnSetting.initialMonster; i++)
            {
                spawner.SpawnMonster(world.MonsterContainer,1);
            }

            var head = spawner.SpawnHero(heroParty.container);
            head.action = EActionType.Over;
            heroParty.Add(head);

            Turn = 1;
            Kill = 0;
            _gamePlayUI.UpdateUIText();
            Play();
        }


        async void Play()
        {
            Isplay = true;

            while (Isplay)
            {
                var input = await inputManager.GetInput();
                await ActionExecuteManager.Execute(input.Action,input.Value);
                if (input.Action == EActionType.Move)
                {
                    _navigatorUI.UpdateNavigator(inputManager.LastInput);
                    EndTurn();
                }
                   
            }
        }
        
        private void EndTurn()
        {
            Turn++;
            int heroChance = Random.Range(0, 100);
            int monsterChance = Random.Range(0, 100);
            if (world.AllTile.Count(c => c.Value.worldObject == null) > 0)
            {
                if (heroChance <= _spawnSetting.HeroChance.SpawnAfterEndTurn)
                {
                    spawner.SpawnHero(world.HeroesContainer);
                }

                if (monsterChance <= _spawnSetting.MonsterChance.SpawnAfterEndTurn)
                {
                    spawner.SpawnMonster(world.MonsterContainer, 1);
                }
            }

            OnEndTurnEvent?.Invoke();
            _gamePlayUI.UpdateUIText();
        }
        

        public void AddKill()
        {
            Kill++;
        }
        
        public void GameOver()
        {
            _navigatorUI.gameObject.SetActive(false);
            _gamePlayUI.gameObject.SetActive(false);
            _overUI.UpdateUIText();
            _overUI.gameObject.SetActive(true);
            Isplay = false;
        }

        public void ReStart()
        {
            SceneManager.LoadScene("Game");
        }
    }
}