using System.Collections;
using System.Collections.Generic;
using CHROMAVOID.Core;
using CHROMAVOID.Grid;
using CHROMAVOID.ScriptableObjects;
using CHROMAVOID.Tiles;
using UnityEngine;

namespace CHROMAVOID.Enemies
{
    [DisallowMultipleComponent]
    public class EnemyContainer : MonoBehaviour
    {
        [SerializeField] private EnemyDefinition _definition;
        [SerializeField] private Renderer _debugRenderer;
        [SerializeField] private float _postSuccessDespawnDelay = 0.25f;
        [SerializeField] private bool _drawGizmos = true;

        private readonly List<Tile> _affectedTiles = new List<Tile>();
        private GridManager _gridManager;
        private Tile _targetTile;
        private bool _initialized;
        private bool _resolved;
        private Coroutine _landingRoutine;

        public EnemyDefinition Definition => _definition;
        public EnemyType EnemyType => _definition != null ? _definition.enemyType : EnemyType.Spot;
        public Tile TargetTile => _targetTile;
        public IReadOnlyList<Tile> AffectedTiles => _affectedTiles;
        public bool IsResolved => _resolved;
        public int LastRescuedTileCount { get; private set; }

        private void Reset()
        {
            _debugRenderer = GetComponentInChildren<Renderer>();
        }

        private void OnEnable()
        {
            GameEvents.TileStateChanged += HandleTileStateChanged;
        }

        private void OnDisable()
        {
            GameEvents.TileStateChanged -= HandleTileStateChanged;
        }

        public void Initialize(EnemyDefinition definition, Tile targetTile, GridManager gridManager)
        {
            _definition = definition;
            _targetTile = targetTile;
            _gridManager = gridManager;
            _resolved = false;
            _initialized = true;
            _affectedTiles.Clear();

            ApplyDebugColor();

            if (_landingRoutine != null)
            {
                StopCoroutine(_landingRoutine);
            }

            if (_targetTile == null)
            {
                Debug.LogWarning("[Enemy] Spawned without target tile. Expiring.", this);
                Expire();
                return;
            }

            float spawnHeight = _definition != null ? _definition.spawnHeight : 5f;
            Vector3 targetPosition = GetLandingPosition();
            transform.position = targetPosition + Vector3.up * spawnHeight;
            _landingRoutine = StartCoroutine(LandOnTarget(targetPosition));
            GameEvents.RaiseEnemySpawned(this);
        }

        [ContextMenu("Debug/Kill Enemy")]
        public void Kill()
        {
            if (_resolved)
            {
                return;
            }

            _resolved = true;
            LastRescuedTileCount = RestoreAffectedTiles();
            GameEvents.RaiseEnemyKilled(this);
            Debug.Log($"[Enemy] Killed {name}. Rescued {LastRescuedTileCount} tile(s).", this);
            Destroy(gameObject);
        }

        public int RestoreAffectedTiles()
        {
            int restored = 0;
            foreach (Tile tile in _affectedTiles)
            {
                if (tile != null && tile.Restore(this))
                {
                    restored++;
                }
            }

            return restored;
        }

        private IEnumerator LandOnTarget(Vector3 targetPosition)
        {
            float duration = _definition != null ? _definition.landingDuration : 1.2f;
            AnimationCurve curve = _definition != null ? _definition.landingCurve : AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
            Vector3 start = transform.position;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / Mathf.Max(0.05f, duration));
                float curvedT = curve != null ? curve.Evaluate(t) : t;
                transform.position = Vector3.Lerp(start, targetPosition, curvedT);
                yield return null;
            }

            transform.position = targetPosition;
            _landingRoutine = null;
            BeginTileThreat();
        }

        private void BeginTileThreat()
        {
            if (!_initialized || _resolved || _targetTile == null)
            {
                return;
            }

            _affectedTiles.Clear();

            switch (EnemyType)
            {
                case EnemyType.Spreader:
                    AddTilesInRadius(_definition != null ? _definition.spreadRadius : 2f);
                    break;
                case EnemyType.Chain:
                    _affectedTiles.Add(_targetTile);
                    break;
                default:
                    _affectedTiles.Add(_targetTile);
                    break;
            }

            float fadingDuration = _definition != null ? _definition.fadingDuration : 6f;
            foreach (Tile tile in _affectedTiles)
            {
                if (tile != null)
                {
                    tile.BeginFading(fadingDuration, this);
                }
            }
        }

        private void AddTilesInRadius(float radius)
        {
            if (_gridManager == null)
            {
                _affectedTiles.Add(_targetTile);
                return;
            }

            List<Tile> tiles = _gridManager.GetTilesInRadius(_targetTile, Mathf.Max(0.1f, radius));
            foreach (Tile tile in tiles)
            {
                if (tile != null && !_affectedTiles.Contains(tile))
                {
                    _affectedTiles.Add(tile);
                }
            }
        }

        private void HandleTileStateChanged(Tile tile, TileState previousState, TileState newState)
        {
            if (_resolved || tile == null || newState != TileState.Black || !_affectedTiles.Contains(tile))
            {
                return;
            }

            if (EnemyType == EnemyType.Chain && tile == _targetTile)
            {
                SpreadChainThreat();
            }

            StartCoroutine(ExpireAfterDelay());
        }

        private void SpreadChainThreat()
        {
            if (_gridManager == null || _targetTile == null)
            {
                return;
            }

            float radius = _definition != null ? _definition.chainRadius : 2.1f;
            float duration = _definition != null ? _definition.chainFadingDuration : 4f;
            List<Tile> chainTiles = _gridManager.GetTilesInRadius(_targetTile, radius);
            foreach (Tile tile in chainTiles)
            {
                if (tile == null || tile == _targetTile || tile.IsBlack)
                {
                    continue;
                }

                if (!_affectedTiles.Contains(tile))
                {
                    _affectedTiles.Add(tile);
                }

                tile.BeginFading(duration, this);
            }
        }

        private IEnumerator ExpireAfterDelay()
        {
            yield return new WaitForSeconds(_postSuccessDespawnDelay);
            Expire();
        }

        private void Expire()
        {
            if (_resolved)
            {
                return;
            }

            _resolved = true;
            GameEvents.RaiseEnemyExpired(this);
            Destroy(gameObject);
        }

        private Vector3 GetLandingPosition()
        {
            return _targetTile != null ? _targetTile.transform.position + Vector3.up * 1.2f : transform.position;
        }

        private void ApplyDebugColor()
        {
            if (_debugRenderer == null)
            {
                _debugRenderer = GetComponentInChildren<Renderer>();
            }

            if (_debugRenderer == null)
            {
                return;
            }

            Color color = EnemyType == EnemyType.Chain ? Color.magenta : EnemyType == EnemyType.Spreader ? Color.red : Color.cyan;
            _debugRenderer.material.color = color;
        }

        private void OnDrawGizmosSelected()
        {
            if (!_drawGizmos || _definition == null)
            {
                return;
            }

            Gizmos.color = EnemyType == EnemyType.Chain ? Color.magenta : EnemyType == EnemyType.Spreader ? Color.red : Color.cyan;
            float radius = EnemyType == EnemyType.Spreader ? _definition.spreadRadius : EnemyType == EnemyType.Chain ? _definition.chainRadius : 0.35f;
            Gizmos.DrawWireSphere(transform.position, Mathf.Max(0.35f, radius));
        }
    }
}
