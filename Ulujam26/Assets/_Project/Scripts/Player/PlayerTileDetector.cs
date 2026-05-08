using CHROMAVOID.Core;
using CHROMAVOID.Grid;
using CHROMAVOID.Tiles;
using UnityEngine;

namespace CHROMAVOID.Player
{
    [DisallowMultipleComponent]
    public class PlayerTileDetector : MonoBehaviour
    {
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private Transform _probeTransform;
        [SerializeField] private float _checkInterval = 0.1f;
        [SerializeField] private bool _debugLogs;

        private Tile _currentTile;
        private bool _isOnBlackTile;
        private float _nextCheckTime;

        public Tile CurrentTile => _currentTile;
        public bool IsOnBlackTile => _isOnBlackTile;

        private void Awake()
        {
            if (_gridManager == null)
            {
                _gridManager = FindFirstObjectByType<GridManager>();
            }

            if (_probeTransform == null)
            {
                _probeTransform = transform;
            }
        }

        private void Update()
        {
            if (Time.time < _nextCheckTime)
            {
                return;
            }

            _nextCheckTime = Time.time + Mathf.Max(0.02f, _checkInterval);
            UpdateCurrentTile();
        }

        private void UpdateCurrentTile()
        {
            if (_gridManager == null || _probeTransform == null)
            {
                return;
            }

            Tile tile = _gridManager.GetTileUnderWorldPosition(_probeTransform.position);
            if (tile != _currentTile)
            {
                _currentTile = tile;
            }

            bool nowOnBlack = tile != null && tile.IsBlack;
            if (nowOnBlack == _isOnBlackTile)
            {
                return;
            }

            _isOnBlackTile = nowOnBlack;
            if (_isOnBlackTile)
            {
                if (_debugLogs)
                {
                    Debug.Log("[PlayerTile] Entered black tile.", this);
                }

                GameEvents.RaisePlayerEnteredBlackTile(tile);
            }
            else
            {
                if (_debugLogs)
                {
                    Debug.Log("[PlayerTile] Exited black tile.", this);
                }

                GameEvents.RaisePlayerExitedBlackTile(tile);
            }
        }
    }
}
