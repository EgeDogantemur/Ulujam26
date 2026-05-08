using CHROMAVOID.Core;
using CHROMAVOID.Enemies;
using UnityEngine;

namespace CHROMAVOID.Core
{
    [DisallowMultipleComponent]
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private int _waveCompleteScore = 250;
        [SerializeField] private int _survivalScorePerSecond = 1;

        private float _survivalAccumulator;
        private bool _gameOver;

        public int Score { get; private set; }

        private void OnEnable()
        {
            GameEvents.EnemyKilled += HandleEnemyKilled;
            GameEvents.WaveCompleted += HandleWaveCompleted;
            GameEvents.GameOver += HandleGameOver;
        }

        private void OnDisable()
        {
            GameEvents.EnemyKilled -= HandleEnemyKilled;
            GameEvents.WaveCompleted -= HandleWaveCompleted;
            GameEvents.GameOver -= HandleGameOver;
        }

        private void Update()
        {
            if (_gameOver || _survivalScorePerSecond <= 0)
            {
                return;
            }

            _survivalAccumulator += Time.deltaTime;
            if (_survivalAccumulator >= 1f)
            {
                int seconds = Mathf.FloorToInt(_survivalAccumulator);
                _survivalAccumulator -= seconds;
                AddScore(seconds * _survivalScorePerSecond);
            }
        }

        private void HandleEnemyKilled(EnemyContainer enemy)
        {
            if (enemy == null || enemy.Definition == null)
            {
                AddScore(50);
                return;
            }

            AddScore(enemy.Definition.killScore + enemy.LastRescuedTileCount * enemy.Definition.rescuedTileScore);
        }

        private void HandleWaveCompleted(int waveIndex)
        {
            AddScore(_waveCompleteScore * Mathf.Max(1, waveIndex));
        }

        private void HandleGameOver()
        {
            _gameOver = true;
        }

        private void AddScore(int amount)
        {
            Score += Mathf.Max(0, amount);
            GameEvents.RaiseScoreChanged(Score);
        }
    }
}
