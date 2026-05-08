using System.Collections;
using CHROMAVOID.Core;
using CHROMAVOID.Spawning;
using UnityEngine;

namespace CHROMAVOID.Waves
{
    [DisallowMultipleComponent]
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private SpawnManager _spawnManager;
        [SerializeField] private bool _startOnAwake = true;
        [SerializeField] private float _timeBetweenWaves = 3f;
        [SerializeField] private int _baseSpawnsPerWave = 5;
        [SerializeField] private int _spawnsAddedPerWave = 2;

        private Coroutine _waveRoutine;
        private bool _gameOver;

        public int CurrentWave { get; private set; }

        private void Awake()
        {
            if (_spawnManager == null)
            {
                _spawnManager = FindFirstObjectByType<SpawnManager>();
            }
        }

        private void OnEnable()
        {
            GameEvents.GameOver += HandleGameOver;
        }

        private void OnDisable()
        {
            GameEvents.GameOver -= HandleGameOver;
        }

        private void Start()
        {
            if (_startOnAwake)
            {
                StartWaves();
            }
        }

        [ContextMenu("Debug/Start Waves")]
        public void StartWaves()
        {
            if (_waveRoutine != null)
            {
                StopCoroutine(_waveRoutine);
            }

            _gameOver = false;
            _waveRoutine = StartCoroutine(WaveLoop());
        }

        private IEnumerator WaveLoop()
        {
            CurrentWave = 0;

            while (!_gameOver)
            {
                CurrentWave++;
                int targetSpawns = _baseSpawnsPerWave + Mathf.Max(0, CurrentWave - 1) * _spawnsAddedPerWave;
                GameEvents.RaiseWaveStarted(CurrentWave);

                if (_spawnManager != null)
                {
                    _spawnManager.StartSpawning(CurrentWave, targetSpawns);
                }

                while (!_gameOver && _spawnManager != null && (!_spawnManager.HasReachedWaveSpawnTarget || _spawnManager.HasActiveEnemies))
                {
                    yield return null;
                }

                if (_gameOver)
                {
                    yield break;
                }

                GameEvents.RaiseWaveCompleted(CurrentWave);
                yield return new WaitForSeconds(_timeBetweenWaves);
            }
        }

        private void HandleGameOver()
        {
            _gameOver = true;
            if (_spawnManager != null)
            {
                _spawnManager.StopSpawning();
            }
        }
    }
}
