using CHROMAVOID.Core;
using CHROMAVOID.Grid;
using CHROMAVOID.Player;
using CHROMAVOID.Waves;
using UnityEngine;
using UnityEngine.UI;

namespace CHROMAVOID.UI
{
    [DisallowMultipleComponent]
    public class PrototypeDebugHUD : MonoBehaviour
    {
        [SerializeField] private Text _debugText;
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private WaveManager _waveManager;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private PlayerTileDetector _playerTileDetector;
        [SerializeField] private float _refreshInterval = 0.1f;

        private float _nextRefreshTime;
        private int _score;
        private bool _gameOver;

        private void Awake()
        {
            if (_gridManager == null)
            {
                _gridManager = FindFirstObjectByType<GridManager>();
            }

            if (_waveManager == null)
            {
                _waveManager = FindFirstObjectByType<WaveManager>();
            }

            if (_scoreManager == null)
            {
                _scoreManager = FindFirstObjectByType<ScoreManager>();
            }

            if (_playerTileDetector == null)
            {
                _playerTileDetector = FindFirstObjectByType<PlayerTileDetector>();
            }
        }

        private void OnEnable()
        {
            GameEvents.ScoreChanged += HandleScoreChanged;
            GameEvents.GameOver += HandleGameOver;
        }

        private void OnDisable()
        {
            GameEvents.ScoreChanged -= HandleScoreChanged;
            GameEvents.GameOver -= HandleGameOver;
        }

        private void Update()
        {
            if (_debugText == null || Time.time < _nextRefreshTime)
            {
                return;
            }

            _nextRefreshTime = Time.time + Mathf.Max(0.02f, _refreshInterval);
            string tileState = _playerTileDetector != null && _playerTileDetector.CurrentTile != null
                ? _playerTileDetector.CurrentTile.State.ToString()
                : "None";

            float blackRatio = _gridManager != null ? _gridManager.BlackTileRatio : 0f;
            int wave = _waveManager != null ? _waveManager.CurrentWave : 0;
            _debugText.text = $"CHROMAVOID\nWave: {wave}\nScore: {_score}\nBlack: {blackRatio:P0}\nTile: {tileState}\n{(_gameOver ? "GAME OVER" : string.Empty)}";
        }

        private void HandleScoreChanged(int score)
        {
            _score = score;
        }

        private void HandleGameOver()
        {
            _gameOver = true;
        }
    }
}
