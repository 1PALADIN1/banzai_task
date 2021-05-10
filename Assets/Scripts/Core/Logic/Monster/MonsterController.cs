using System.Collections;
using System.Collections.Generic;
using Core.Data.Monster;
using Core.Logic.Scene;
using Core.Views;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core.Logic.Monster
{
    public class MonsterController : MonoBehaviour
    {
        private const float FindPlayerInterval = 0.5f;
        
        [SerializeField] private MonsterSpawner _monsterSpawner;
        [SerializeField] private MonsterType[] _spawnMonsters;

        private readonly HashSet<MonsterView> _activeMonsterViews = new HashSet<MonsterView>();

        private ISceneController _sceneController;
        private PlayerView _player;

        [Inject]
        public void Init(ISceneController sceneController)
        {
            _sceneController = sceneController;

            _sceneController.GameStarted += OnGameStarted;
            _sceneController.GameFinished += OnGameFinished;
        }
        
        private void OnGameStarted()
        {
            StartCoroutine(TryFindPlayer());
        }
        
        private void OnGameFinished()
        {
            foreach (var monsterView in _activeMonsterViews)
                monsterView.StopMoving();
        }

        private void OnPlayerFound(PlayerView playerView)
        {
            _player = playerView;
            
            while (TryCreateMonster())
            {
            }
        }
        
        private bool TryCreateMonster()
        {
            if (_spawnMonsters.Length == 0)
            {
            	Debug.LogError("Spawn monster types are not specified!", this);
            	return false;
            }
            
            var spawnPosition = _sceneController.GetRandomPointOutOfScene();
            if (!_monsterSpawner.TrySpawnMonster(GetRandomMonster(), spawnPosition, out var monsterView))
                return false;
            
            monsterView.SetMoveTarget(_player.transform);
            _activeMonsterViews.Add(monsterView);
            monsterView.Destroyed += OnMonsterDestroyed;
            return true;
        }

        private MonsterType GetRandomMonster()
        {
            var randIndex = Random.Range(0, _spawnMonsters.Length);
            return _spawnMonsters[randIndex];
        }

        private void OnMonsterDestroyed(MonsterView monsterView)
        {
            monsterView.Destroyed -= OnMonsterDestroyed;
            _activeMonsterViews.Remove(monsterView);
            
            if (!_sceneController.IsGameFinished)
                TryCreateMonster();
        }

        private IEnumerator TryFindPlayer()
        {
            PlayerView player = null;
            while (player == null)
            {
                player = FindObjectOfType<PlayerView>();
                yield return new WaitForSeconds(FindPlayerInterval);
            }
            
            OnPlayerFound(player);
        }

        private void OnDestroy()
        {
            _sceneController.GameStarted -= OnGameStarted;
            _sceneController.GameFinished -= OnGameFinished;
            
            foreach (var monsterView in _activeMonsterViews)
                monsterView.Destroyed -= OnMonsterDestroyed;
			
            _activeMonsterViews.Clear();
        }
        
        
        //TODO: debug
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                if (!_sceneController.IsGameStarted)
                    _sceneController.StartGame();
            }
        }
    }
}