using System.Collections.Generic;
using CHROMAVOID.Core;
using CHROMAVOID.ScriptableObjects;
using CHROMAVOID.Tiles;
using UnityEngine;

namespace CHROMAVOID.Grid
{
    [DisallowMultipleComponent]
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridSettings _settings;
        [SerializeField] private Transform _tileRoot;
        [SerializeField] private bool _autoRegisterTilesOnAwake = true;
        [SerializeField] private bool _drawGizmos = true;

        private readonly List<Tile> _tiles = new List<Tile>();
        private readonly Dictionary<Vector2Int, Tile> _tilesByPosition = new Dictionary<Vector2Int, Tile>();

        public IReadOnlyList<Tile> Tiles => _tiles;
        public int TotalTileCount => _tiles.Count;
        public int BlackTileCount { get; private set; }
        public float BlackTileRatio => TotalTileCount == 0 ? 0f : (float)BlackTileCount / TotalTileCount;
        public float TileSize => _settings != null ? _settings.tileSize : 2f;

        private void Awake()
        {
            if (_autoRegisterTilesOnAwake)
            {
                RegisterTilesFromScene();
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

        [ContextMenu("Grid/Register Tiles From Scene")]
        public void RegisterTilesFromScene()
        {
            _tiles.Clear();
            _tilesByPosition.Clear();

            Tile[] foundTiles = _tileRoot != null
                ? _tileRoot.GetComponentsInChildren<Tile>(true)
                : FindObjectsByType<Tile>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (Tile tile in foundTiles)
            {
                RegisterTile(tile);
            }

            RecalculateBlackTiles();
            Debug.Log($"[Grid] Registered {_tiles.Count} tiles.", this);
        }

        public void RegisterTile(Tile tile)
        {
            if (tile == null || _tiles.Contains(tile))
            {
                return;
            }

            Vector2Int position = WorldToGrid(tile.transform.position);
            tile.SetGridPosition(position);
            _tiles.Add(tile);
            _tilesByPosition[position] = tile;
        }

        public Vector2Int WorldToGrid(Vector3 worldPosition)
        {
            float size = Mathf.Max(0.1f, TileSize);
            return new Vector2Int(Mathf.RoundToInt(worldPosition.x / size), Mathf.RoundToInt(worldPosition.z / size));
        }

        public Tile GetTileAtGrid(Vector2Int gridPosition)
        {
            _tilesByPosition.TryGetValue(gridPosition, out Tile tile);
            return tile;
        }

        public Tile GetTileUnderWorldPosition(Vector3 worldPosition)
        {
            Tile logicalTile = GetTileAtGrid(WorldToGrid(worldPosition));
            if (logicalTile != null)
            {
                return logicalTile;
            }

            float probeHeight = _settings != null ? _settings.tileProbeHeight : 30f;
            LayerMask mask = _settings != null ? _settings.tileLayerMask : ~0;
            Vector3 origin = worldPosition + Vector3.up * probeHeight;
            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, probeHeight * 2f, mask, QueryTriggerInteraction.Collide))
            {
                return hit.collider.GetComponentInParent<Tile>();
            }

            return null;
        }

        public List<Tile> GetTilesInRadius(Tile centerTile, float radius)
        {
            List<Tile> result = new List<Tile>();
            if (centerTile == null)
            {
                return result;
            }

            float radiusSqr = radius * radius;
            Vector3 center = centerTile.transform.position;
            foreach (Tile tile in _tiles)
            {
                if (tile == null || tile.IsBlack)
                {
                    continue;
                }

                Vector3 delta = tile.transform.position - center;
                delta.y = 0f;
                if (delta.sqrMagnitude <= radiusSqr)
                {
                    result.Add(tile);
                }
            }

            return result;
        }

        public List<Tile> GetColoredTiles(bool excludeThreatened)
        {
            List<Tile> result = new List<Tile>();
            foreach (Tile tile in _tiles)
            {
                if (tile == null || !tile.IsColored)
                {
                    continue;
                }

                if (excludeThreatened && HasThreatNearby(tile))
                {
                    continue;
                }

                result.Add(tile);
            }

            return result;
        }

        public bool HasThreatNearby(Tile tile)
        {
            if (tile == null)
            {
                return false;
            }

            foreach (Tile other in _tiles)
            {
                if (other == null || !other.HasActiveThreat)
                {
                    continue;
                }

                int dx = Mathf.Abs(other.GridPosition.x - tile.GridPosition.x);
                int dy = Mathf.Abs(other.GridPosition.y - tile.GridPosition.y);
                if (dx <= 1 && dy <= 1)
                {
                    return true;
                }
            }

            return false;
        }

        private void HandleTileStateChanged(Tile tile, TileState previousState, TileState newState)
        {
            if (previousState == TileState.Black || newState == TileState.Black)
            {
                RecalculateBlackTiles();
            }
        }

        private void RecalculateBlackTiles()
        {
            BlackTileCount = 0;
            foreach (Tile tile in _tiles)
            {
                if (tile != null && tile.IsBlack)
                {
                    BlackTileCount++;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (!_drawGizmos)
            {
                return;
            }

            Tile[] sceneTiles = _tileRoot != null
                ? _tileRoot.GetComponentsInChildren<Tile>()
                : FindObjectsByType<Tile>(FindObjectsSortMode.None);

            foreach (Tile tile in sceneTiles)
            {
                if (tile == null)
                {
                    continue;
                }

                Gizmos.color = tile.IsBlack ? GetBlackGizmo() : tile.IsFading ? GetFadingGizmo() : GetColoredGizmo();
                Gizmos.DrawWireCube(tile.transform.position + Vector3.up * 0.05f, new Vector3(TileSize, 0.05f, TileSize));
            }
        }

        private Color GetColoredGizmo() => _settings != null ? _settings.coloredGizmo : new Color(0f, 0.85f, 1f, 0.35f);
        private Color GetFadingGizmo() => _settings != null ? _settings.fadingGizmo : new Color(1f, 0.75f, 0.1f, 0.45f);
        private Color GetBlackGizmo() => _settings != null ? _settings.blackGizmo : new Color(0f, 0f, 0f, 0.65f);
    }
}
