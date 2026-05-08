using CHROMAVOID.Grid;
using CHROMAVOID.ScriptableObjects;
using CHROMAVOID.Tiles;
using UnityEngine;

namespace CHROMAVOID.Core
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private GridSettings _gridSettings;
        [SerializeField] private bool _logGameOver = true;

        public bool IsGameOver { get; private set; }

        private void Awake()
        {
            if (_gridManager == null)
            {
                _gridManager = FindFirstObjectByType<GridManager>();
            }
        }

        private void OnEnable()
        {
            GameEvents.TileStateChanged += HandleTileStateChanged;
        }

        private void OnDisable()
        {
            GameEvents.TileStateChanged -= HandleTileStateChanged;
        }

        [ContextMenu("Debug/Force Game Over")]
        public void ForceGameOver()
        {
            TriggerGameOver("Forced from inspector.");
        }

        private void HandleTileStateChanged(Tile tile, TileState previousState, TileState newState)
        {
            if (newState == TileState.Black)
            {
                CheckBlackTileGameOver();
            }
        }

        private void CheckBlackTileGameOver()
        {
            if (IsGameOver || _gridManager == null)
            {
                return;
            }

            float threshold = _gridSettings != null ? _gridSettings.blackTileGameOverRatio : 0.65f;
            if (_gridManager.BlackTileRatio >= threshold)
            {
                TriggerGameOver($"Black tile ratio reached {_gridManager.BlackTileRatio:P0}.");
            }
        }

        private void TriggerGameOver(string reason)
        {
            if (IsGameOver)
            {
                return;
            }

            IsGameOver = true;
            if (_logGameOver)
            {
                Debug.Log($"[Game] Game Over: {reason}", this);
            }

            GameEvents.RaiseGameOver();
        }
    }
}
