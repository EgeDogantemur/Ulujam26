using UnityEngine;

namespace CHROMAVOID.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public sealed class HumanoidWeaponHandIK : MonoBehaviour
    {
        [Header("Grip Targets")]
        [SerializeField] private Transform _rightHandGrip;
        [SerializeField] private Transform _leftHandGrip;

        [Header("Right Hand")]
        [Range(0f, 1f)] [SerializeField] private float _rightPositionWeight = 1f;
        [Range(0f, 1f)] [SerializeField] private float _rightRotationWeight;

        [Header("Left Hand")]
        [Range(0f, 1f)] [SerializeField] private float _leftPositionWeight = 1f;
        [Range(0f, 1f)] [SerializeField] private float _leftRotationWeight;

        [Header("Debug")]
        [SerializeField] private bool _warnIfInvalid = true;
        [SerializeField] private bool _drawGripGizmos = true;

        private Animator _animator;
        private bool _hasValidAnimator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            ResolveMissingGripTargets();
            _hasValidAnimator = _animator != null && _animator.isHuman;

            if (!_hasValidAnimator && _warnIfInvalid)
            {
                Debug.LogWarning("[WeaponHandIK] Animator is missing or not Humanoid. Built-in hand IK will not run.", this);
            }
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (!_hasValidAnimator)
            {
                return;
            }

            ResolveMissingGripTargets();

            ApplyHandIK(AvatarIKGoal.RightHand, _rightHandGrip, _rightPositionWeight, _rightRotationWeight);
            ApplyHandIK(AvatarIKGoal.LeftHand, _leftHandGrip, _leftPositionWeight, _leftRotationWeight);
        }

        private void ApplyHandIK(AvatarIKGoal goal, Transform target, float positionWeight, float rotationWeight)
        {
            if (target == null)
            {
                _animator.SetIKPositionWeight(goal, 0f);
                _animator.SetIKRotationWeight(goal, 0f);
                return;
            }

            _animator.SetIKPositionWeight(goal, positionWeight);
            _animator.SetIKRotationWeight(goal, rotationWeight);
            _animator.SetIKPosition(goal, target.position);
            _animator.SetIKRotation(goal, target.rotation);
        }

        [ContextMenu("Find Grip Targets By Name")]
        private void FindGripTargetsByName()
        {
            ResolveMissingGripTargets(true);
        }

        private void ResolveMissingGripTargets(bool force = false)
        {
            Transform[] transforms = FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (Transform candidate in transforms)
            {
                if ((force || _rightHandGrip == null) && candidate.name == "RightHandGrip")
                {
                    _rightHandGrip = candidate;
                }

                if ((force || _leftHandGrip == null) && candidate.name == "LeftHandGrip")
                {
                    _leftHandGrip = candidate;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (!_drawGripGizmos)
            {
                return;
            }

            DrawGrip(_rightHandGrip, Color.cyan);
            DrawGrip(_leftHandGrip, Color.magenta);
        }

        private static void DrawGrip(Transform target, Color color)
        {
            if (target == null)
            {
                return;
            }

            Gizmos.color = color;
            Gizmos.DrawWireSphere(target.position, 0.04f);
            Gizmos.DrawLine(target.position, target.position + target.forward * 0.18f);
        }
    }
}
