using UnityEngine;

namespace CHROMAVOID.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GridSettings_Default", menuName = "CHROMAVOID/Grid Settings")]
    public class GridSettings : ScriptableObject
    {
        [Header("Tile Detection")]
        [Min(0.1f)] public float tileSize = 2f;
        [Min(0.1f)] public float tileProbeHeight = 30f;
        public LayerMask tileLayerMask = ~0;

        [Header("Game Over")]
        [Range(0.01f, 1f)] public float blackTileGameOverRatio = 0.65f;

        [Header("Debug")]
        public Color coloredGizmo = new Color(0f, 0.85f, 1f, 0.35f);
        public Color fadingGizmo = new Color(1f, 0.75f, 0.1f, 0.45f);
        public Color blackGizmo = new Color(0f, 0f, 0f, 0.65f);
    }
}
