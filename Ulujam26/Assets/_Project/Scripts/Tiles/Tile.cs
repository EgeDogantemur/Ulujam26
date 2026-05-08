using System.Collections;
using CHROMAVOID.Core;
using UnityEngine;

namespace CHROMAVOID.Tiles
{
    [DisallowMultipleComponent]
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Vector2Int _gridPosition;
        [SerializeField] private TileState _state = TileState.Colored;
        [SerializeField] private TileVisualController _visualController;
        [SerializeField] private Collider _tileCollider;
        [SerializeField] private bool _debugStateChanges;

        private Coroutine _fadingRoutine;
        private Object _currentThreatSource;

        public Vector2Int GridPosition => _gridPosition;
        public TileState State => _state;
        public bool IsColored => _state == TileState.Colored;
        public bool IsFading => _state == TileState.Fading;
        public bool IsBlack => _state == TileState.Black;
        public bool HasActiveThreat => _currentThreatSource != null && !IsBlack;

        private void Reset()
        {
            _visualController = GetComponent<TileVisualController>();
            _tileCollider = GetComponent<Collider>();
        }

        private void Awake()
        {
            if (_visualController == null)
            {
                _visualController = GetComponent<TileVisualController>();
            }

            if (_tileCollider == null)
            {
                _tileCollider = GetComponent<Collider>();
            }

            ApplyVisual(0f);
        }

        public void SetGridPosition(Vector2Int gridPosition)
        {
            _gridPosition = gridPosition;
        }

        public void BeginFading(float duration, Object threatSource)
        {
            if (IsBlack)
            {
                return;
            }

            _currentThreatSource = threatSource;
            SetState(TileState.Fading);

            if (_fadingRoutine != null)
            {
                StopCoroutine(_fadingRoutine);
            }

            _fadingRoutine = StartCoroutine(FadingCountdown(duration, threatSource));
        }

        public bool Restore(Object threatSource)
        {
            if (IsBlack)
            {
                return false;
            }

            if (threatSource != null && _currentThreatSource != null && _currentThreatSource != threatSource)
            {
                return false;
            }

            if (_fadingRoutine != null)
            {
                StopCoroutine(_fadingRoutine);
                _fadingRoutine = null;
            }

            _currentThreatSource = null;
            bool wasFading = IsFading;
            SetState(TileState.Colored);
            return wasFading;
        }

        [ContextMenu("Debug/Set Colored")]
        public void DebugSetColored()
        {
            ForceColored();
        }

        [ContextMenu("Debug/Set Fading 5s")]
        public void DebugSetFading()
        {
            BeginFading(5f, this);
        }

        [ContextMenu("Debug/Set Black")]
        public void DebugSetBlack()
        {
            SetBlack();
        }

        public void ForceColored()
        {
            if (_fadingRoutine != null)
            {
                StopCoroutine(_fadingRoutine);
                _fadingRoutine = null;
            }

            _currentThreatSource = null;
            SetState(TileState.Colored);
        }

        public void SetBlack()
        {
            if (_fadingRoutine != null)
            {
                StopCoroutine(_fadingRoutine);
                _fadingRoutine = null;
            }

            _currentThreatSource = null;
            SetState(TileState.Black);
            ApplyVisual(1f);
        }

        private IEnumerator FadingCountdown(float duration, Object threatSource)
        {
            float safeDuration = Mathf.Max(0.05f, duration);
            float elapsed = 0f;

            while (elapsed < safeDuration)
            {
                if (IsBlack || _currentThreatSource != threatSource)
                {
                    _fadingRoutine = null;
                    yield break;
                }

                elapsed += Time.deltaTime;
                ApplyVisual(elapsed / safeDuration);
                yield return null;
            }

            _fadingRoutine = null;
            SetBlack();
        }

        private void SetState(TileState newState)
        {
            if (_state == newState)
            {
                ApplyVisual(newState == TileState.Black ? 1f : 0f);
                return;
            }

            TileState previous = _state;
            _state = newState;
            ApplyVisual(newState == TileState.Black ? 1f : 0f);

            if (_debugStateChanges)
            {
                Debug.Log($"[Tile] {name} {previous} -> {newState}", this);
            }

            GameEvents.RaiseTileStateChanged(this, previous, newState);
        }

        private void ApplyVisual(float normalizedFade)
        {
            if (_visualController != null)
            {
                _visualController.Apply(_state, normalizedFade);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = IsBlack ? Color.black : IsFading ? Color.yellow : Color.cyan;
            Gizmos.DrawWireCube(transform.position + Vector3.up * 0.03f, new Vector3(1.9f, 0.05f, 1.9f));
        }
    }
}
