using CHROMAVOID.ScriptableObjects;
using UnityEngine;

namespace CHROMAVOID.Tiles
{
    [DisallowMultipleComponent]
    public class TileVisualController : MonoBehaviour
    {
        [SerializeField] private Renderer _targetRenderer;
        [SerializeField] private TileEffectData _effectData;
        [SerializeField] private bool _useMaterialSwap = true;
        [SerializeField] private bool _usePropertyBlock = true;

        private MaterialPropertyBlock _propertyBlock;

        private void Reset()
        {
            _targetRenderer = GetComponentInChildren<Renderer>();
        }

        private void Awake()
        {
            if (_targetRenderer == null)
            {
                _targetRenderer = GetComponentInChildren<Renderer>();
            }

            _propertyBlock = new MaterialPropertyBlock();
        }

        public void Apply(TileState state, float normalizedFade = 0f)
        {
            if (_targetRenderer == null)
            {
                return;
            }

            if (_effectData != null && _useMaterialSwap)
            {
                Material material = _effectData.GetMaterial(state);
                if (material != null)
                {
                    _targetRenderer.sharedMaterial = material;
                }
            }

            if (!_usePropertyBlock)
            {
                return;
            }

            if (_propertyBlock == null)
            {
                _propertyBlock = new MaterialPropertyBlock();
            }

            _targetRenderer.GetPropertyBlock(_propertyBlock);

            Color tint = _effectData != null ? _effectData.GetTint(state) : GetDefaultTint(state);
            string colorProperty = _effectData != null ? _effectData.colorProperty : "_BaseColor";
            string fadeProperty = _effectData != null ? _effectData.fadeAmountProperty : "_FadeAmount";
            string voidProperty = _effectData != null ? _effectData.voidAmountProperty : "_VoidAmount";

            SetColorIfExists(colorProperty, tint);
            SetFloatIfExists(fadeProperty, state == TileState.Fading ? Mathf.Clamp01(normalizedFade) : 0f);
            SetFloatIfExists(voidProperty, state == TileState.Black ? 1f : 0f);

            _propertyBlock.SetColor("_Color", tint);
            _targetRenderer.SetPropertyBlock(_propertyBlock);
        }

        private void SetColorIfExists(string propertyName, Color value)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                _propertyBlock.SetColor(propertyName, value);
            }
        }

        private void SetFloatIfExists(string propertyName, float value)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                _propertyBlock.SetFloat(propertyName, value);
            }
        }

        private static Color GetDefaultTint(TileState state)
        {
            switch (state)
            {
                case TileState.Fading:
                    return new Color(1f, 0.65f, 0.15f, 1f);
                case TileState.Black:
                    return new Color(0.01f, 0.01f, 0.015f, 1f);
                default:
                    return new Color(0.1f, 0.9f, 1f, 1f);
            }
        }
    }
}
