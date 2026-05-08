using CHROMAVOID.Enemies;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace CHROMAVOID.Weapons
{
    [DisallowMultipleComponent]
    public class SimpleRaycastWeapon : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera _aimCamera;
        [SerializeField] private Transform _muzzlePoint;

        [Header("Raycast")]
        [SerializeField] private float _range = 80f;
        [SerializeField] private LayerMask _hitMask = ~0;
        [SerializeField] private QueryTriggerInteraction _triggerInteraction = QueryTriggerInteraction.Collide;

        [Header("Debug")]
        [SerializeField] private bool _drawDebugRay = true;
        [SerializeField] private float _debugRayDuration = 0.15f;

        private void Awake()
        {
            if (_aimCamera == null)
            {
                _aimCamera = Camera.main;
            }
        }

        private void Update()
        {
            if (WasFirePressed())
            {
                Fire();
            }
        }

        [ContextMenu("Debug/Fire")]
        public void Fire()
        {
            if (_aimCamera == null)
            {
                Debug.LogWarning("[Weapon] Missing aim camera.", this);
                return;
            }

            Ray ray = _aimCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (_drawDebugRay)
            {
                Debug.DrawRay(ray.origin, ray.direction * _range, Color.cyan, _debugRayDuration);
            }

            if (!Physics.Raycast(ray, out RaycastHit hit, _range, _hitMask, _triggerInteraction))
            {
                return;
            }

            EnemyContainer enemy = hit.collider.GetComponentInParent<EnemyContainer>();
            if (enemy != null)
            {
                enemy.Kill();
            }
        }

        private bool WasFirePressed()
        {
#if ENABLE_INPUT_SYSTEM
            return Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
#else
            return Input.GetMouseButtonDown(0);
#endif
        }
    }
}
