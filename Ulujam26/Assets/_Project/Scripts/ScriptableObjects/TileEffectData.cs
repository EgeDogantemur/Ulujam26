using CHROMAVOID.Tiles;
using UnityEngine;

namespace CHROMAVOID.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TileEffectData_Default", menuName = "CHROMAVOID/Tile Effect Data")]
    public class TileEffectData : ScriptableObject
    {
        [Header("Fallback Materials")]
        public Material coloredMaterial;
        public Material fadingMaterial;
        public Material blackMaterial;

        [Header("Fallback Tint")]
        public Color coloredTint = new Color(0.1f, 0.9f, 1f, 1f);
        public Color fadingTint = new Color(0.9f, 0.65f, 0.25f, 1f);
        public Color blackTint = new Color(0.01f, 0.01f, 0.015f, 1f);

        [Header("Shader Graph Property Hooks")]
        [Tooltip("Optional color/tint property used by the tile shader.")]
        public string colorProperty = "_BaseColor";
        [Tooltip("Optional float property for fading/void intensity.")]
        public string fadeAmountProperty = "_FadeAmount";
        [Tooltip("Optional float property for black/void state intensity.")]
        public string voidAmountProperty = "_VoidAmount";

        public Color GetTint(TileState state)
        {
            switch (state)
            {
                case TileState.Fading:
                    return fadingTint;
                case TileState.Black:
                    return blackTint;
                default:
                    return coloredTint;
            }
        }

        public Material GetMaterial(TileState state)
        {
            switch (state)
            {
                case TileState.Fading:
                    return fadingMaterial;
                case TileState.Black:
                    return blackMaterial;
                default:
                    return coloredMaterial;
            }
        }
    }
}
