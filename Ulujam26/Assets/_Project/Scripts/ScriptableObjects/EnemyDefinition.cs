using CHROMAVOID.Enemies;
using UnityEngine;

namespace CHROMAVOID.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyDefinition_Fanus", menuName = "CHROMAVOID/Enemy Definition")]
    public class EnemyDefinition : ScriptableObject
    {
        [Header("Identity")]
        public EnemyType enemyType = EnemyType.Spot;
        public string displayName = "Fanus";

        [Header("Tile Threat")]
        [Min(0.25f)] public float fadingDuration = 6f;
        [Min(0f)] public float spreadRadius = 0f;
        [Min(0f)] public float chainRadius = 2.1f;
        [Min(0.25f)] public float chainFadingDuration = 4f;

        [Header("Spawn Motion")]
        [Min(0f)] public float spawnHeight = 5f;
        [Min(0.1f)] public float landingDuration = 1.2f;
        public AnimationCurve landingCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        [Header("Score")]
        public int killScore = 100;
        public int rescuedTileScore = 25;
    }
}
