using System;
using System.Collections;
using System.Collections.Generic;
using CHROMAVOID.Core;
using CHROMAVOID.Enemies;
using CHROMAVOID.Grid;
using CHROMAVOID.ScriptableObjects;
using CHROMAVOID.Tiles;
using UnityEngine;

namespace CHROMAVOID.Spawning
{
    [DisallowMultipleComponent]
    public class SpawnManager : MonoBehaviour
    {
        [Serializable]
        public class EnemySpawnWeight
        {
            public EnemyDefinition definition;
            [Min(0f)] public float baseWeight = 1f;
            [Min(0f)] public float weightPerWave = 0f;
        }

        [Header("References")]
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private Transform _player;
        [SerializeField] private EnemyContainer _enemyPrefab;
        [SerializeField] private Transform _enemyRoot;

        [Header("Spawn Distances")]
        [SerializeField] private float _minPlayerDistance = 6f;
        [SerializeField] private float _maxPlayerDistance = 24f;

        [Header("Difficulty")]
        [SerializeField] private float _baseSpawnInterval = 4f;
        [SerializeField] private float _minSpawnInterval = 1.1f;
        [SerializeField] private float _intervalReductionPerWave = 0.25f;
        [SerializeField] private int _baseMaxActiveEnemies = 2;
        [SerializeField] private int _maxActiveIncreaseEveryWaves = 2;
        [SerializeField] private List<EnemySpawnWeight> _enemyWeights = new List<EnemySpawnWeight>();

        [Header("Debug")]
        [SerializeField] private bool _autoSpawn;
        [SerializeField] private bool _drawGizmos = true;
        [SerializeField] private int _maxConsecutiveFailedSpawnAttempts = 12;

        private readonly List<EnemyContainer> _activeEnemies = new List<EnemyContainer>();
        private Coroutine _spawnRoutine;
        private int _currentWave = 1;
        private int _spawnedThisWave;
        private int _targetSpawnsThisWave = 6;
        private int _consecutiveFailedSpawnAttempts;

        public int ActiveEnemyCount => _activeEnemies.Count;
        public int SpawnedThisWave => _spawnedThisWave;
        public int TargetSpawnsThisWave => _targetSpawnsThisWave;
        public bool HasReachedWaveSpawnTarget => _spawnedThisWave >= _targetSpawnsThisWave;
        public bool HasActiveEnemies => _activeEnemies.Count > 0;

        private void Awake()
        {
            if (_gridManager == null)
            {
                _gridManager = FindFirstObjectByType<GridManager>();
            }
        }

        private void OnEnable()
        {
            GameEvents.EnemyKilled += HandleEnemyRemoved;
            GameEvents.EnemyExpired += HandleEnemyRemoved;
        }

        private void OnDisable()
        {
            GameEvents.EnemyKilled -= HandleEnemyRemoved;
            GameEvents.EnemyExpired -= HandleEnemyRemoved;
        }

        private void Start()
        {
            if (_autoSpawn)
            {
                StartSpawning(_currentWave, _targetSpawnsThisWave);
            }
        }

        public void StartSpawning(int waveIndex, int targetSpawnCount)
        {
            _currentWave = Mathf.Max(1, waveIndex);
            _targetSpawnsThisWave = Mathf.Max(1, targetSpawnCount);
            _spawnedThisWave = 0;
            _consecutiveFailedSpawnAttempts = 0;

            if (_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
            }

            _spawnRoutine = StartCoroutine(SpawnLoop());
        }

        public void StopSpawning()
        {
            if (_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
                _spawnRoutine = null;
            }
        }

        [ContextMenu("Debug/Spawn One")]
        public void DebugSpawnOne()
        {
            TrySpawnOne();
        }

        private IEnumerator SpawnLoop()
        {
            while (_spawnedThisWave < _targetSpawnsThisWave)
            {
                if (TrySpawnOne())
                {
                    _consecutiveFailedSpawnAttempts = 0;
                }
                else
                {
                    _consecutiveFailedSpawnAttempts++;
                    if (_consecutiveFailedSpawnAttempts >= _maxConsecutiveFailedSpawnAttempts)
                    {
                        Debug.LogWarning("[Spawn] Could not find valid spawn tiles. Ending current spawn budget.", this);
                        _spawnedThisWave = _targetSpawnsThisWave;
                        break;
                    }
                }

                yield return new WaitForSeconds(GetSpawnInterval());
            }

            _spawnRoutine = null;
        }

        private bool TrySpawnOne()
        {
            CleanupActiveEnemies();

            if (_gridManager == null)
            {
                Debug.LogWarning("[Spawn] Missing GridManager.", this);
                return false;
            }

            if (_activeEnemies.Count >= GetMaxActiveEnemies())
            {
                return false;
            }

            Tile tile = PickSpawnTile();
            EnemyDefinition definition = PickEnemyDefinition();
            if (tile == null || definition == null)
            {
                return false;
            }

            EnemyContainer enemy = CreateEnemyInstance(definition);
            enemy.Initialize(definition, tile, _gridManager);
            _activeEnemies.Add(enemy);
            _spawnedThisWave++;
            return true;
        }

        private Tile PickSpawnTile()
        {
            List<Tile> candidates = _gridManager.GetColoredTiles(true);
            for (int i = candidates.Count - 1; i >= 0; i--)
            {
                Tile tile = candidates[i];
                if (!IsInPlayerSpawnBand(tile) || IsNearActiveEnemyTarget(tile))
                {
                    candidates.RemoveAt(i);
                }
            }

            if (candidates.Count == 0)
            {
                return null;
            }

            return candidates[UnityEngine.Random.Range(0, candidates.Count)];
        }

        private bool IsInPlayerSpawnBand(Tile tile)
        {
            if (tile == null || _player == null)
            {
                return true;
            }

            Vector3 delta = tile.transform.position - _player.position;
            delta.y = 0f;
            float distance = delta.magnitude;
            return distance >= _minPlayerDistance && distance <= _maxPlayerDistance;
        }

        private bool IsNearActiveEnemyTarget(Tile tile)
        {
            if (tile == null)
            {
                return true;
            }

            CleanupActiveEnemies();
            foreach (EnemyContainer enemy in _activeEnemies)
            {
                Tile target = enemy != null ? enemy.TargetTile : null;
                if (target == null)
                {
                    continue;
                }

                int dx = Mathf.Abs(target.GridPosition.x - tile.GridPosition.x);
                int dy = Mathf.Abs(target.GridPosition.y - tile.GridPosition.y);
                if (dx <= 1 && dy <= 1)
                {
                    return true;
                }
            }

            return false;
        }

        private EnemyDefinition PickEnemyDefinition()
        {
            float totalWeight = 0f;
            for (int i = 0; i < _enemyWeights.Count; i++)
            {
                if (_enemyWeights[i].definition == null)
                {
                    continue;
                }

                totalWeight += GetWeight(_enemyWeights[i]);
            }

            if (totalWeight <= 0f)
            {
                return _enemyWeights.Count > 0 ? _enemyWeights[0].definition : null;
            }

            float roll = UnityEngine.Random.Range(0f, totalWeight);
            for (int i = 0; i < _enemyWeights.Count; i++)
            {
                float weight = GetWeight(_enemyWeights[i]);
                if (weight <= 0f)
                {
                    continue;
                }

                roll -= weight;
                if (roll <= 0f)
                {
                    return _enemyWeights[i].definition;
                }
            }

            return _enemyWeights[_enemyWeights.Count - 1].definition;
        }

        private float GetWeight(EnemySpawnWeight weight)
        {
            return Mathf.Max(0f, weight.baseWeight + weight.weightPerWave * Mathf.Max(0, _currentWave - 1));
        }

        private EnemyContainer CreateEnemyInstance(EnemyDefinition definition)
        {
            if (_enemyPrefab != null)
            {
                return Instantiate(_enemyPrefab, _enemyRoot);
            }

            GameObject placeholder = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            placeholder.name = $"Enemy_Fanus_{definition.enemyType}_Placeholder";
            placeholder.transform.SetParent(_enemyRoot);
            placeholder.transform.localScale = Vector3.one * 1.2f;
            return placeholder.AddComponent<EnemyContainer>();
        }

        private float GetSpawnInterval()
        {
            return Mathf.Max(_minSpawnInterval, _baseSpawnInterval - (_currentWave - 1) * _intervalReductionPerWave);
        }

        private int GetMaxActiveEnemies()
        {
            int bonus = _maxActiveIncreaseEveryWaves <= 0 ? 0 : (_currentWave - 1) / _maxActiveIncreaseEveryWaves;
            return Mathf.Max(1, _baseMaxActiveEnemies + bonus);
        }

        private void HandleEnemyRemoved(EnemyContainer enemy)
        {
            if (enemy != null)
            {
                _activeEnemies.Remove(enemy);
            }
        }

        private void CleanupActiveEnemies()
        {
            for (int i = _activeEnemies.Count - 1; i >= 0; i--)
            {
                if (_activeEnemies[i] == null || _activeEnemies[i].IsResolved)
                {
                    _activeEnemies.RemoveAt(i);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!_drawGizmos || _player == null)
            {
                return;
            }

            Gizmos.color = new Color(1f, 0.2f, 0.1f, 0.25f);
            Gizmos.DrawWireSphere(_player.position, _minPlayerDistance);
            Gizmos.color = new Color(0.1f, 0.7f, 1f, 0.25f);
            Gizmos.DrawWireSphere(_player.position, _maxPlayerDistance);
        }
    }
}
