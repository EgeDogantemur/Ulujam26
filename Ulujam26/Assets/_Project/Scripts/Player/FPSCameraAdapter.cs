using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace CHROMAVOID.Player
{
    [DisallowMultipleComponent]
    public class FPSCameraAdapter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _playerBody;
        [SerializeField] private Transform _cameraPivot;
        [SerializeField] private Camera _camera;

        [Header("Look")]
        [SerializeField] private float _mouseSensitivity = 0.12f;
        [SerializeField] private float _gamepadSensitivity = 120f;
        [SerializeField] private float _minPitch = -75f;
        [SerializeField] private float _maxPitch = 75f;
        [SerializeField] private bool _lockCursor = true;

        private float _yaw;
        private float _pitch;

        private void Reset()
        {
            _camera = Camera.main;
            _cameraPivot = transform;
        }

        private void Awake()
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }

            if (_cameraPivot == null)
            {
                _cameraPivot = _camera != null ? _camera.transform : transform;
            }

            if (_playerBody == null)
            {
                _playerBody = transform.root;
            }

            Vector3 euler = _cameraPivot.rotation.eulerAngles;
            _yaw = euler.y;
            _pitch = NormalizePitch(euler.x);
            ApplyCursorState();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                ApplyCursorState();
            }
        }

        private void LateUpdate()
        {
            Vector2 look = ReadLookInput();
            if (look.sqrMagnitude <= 0.0001f)
            {
                return;
            }

            _yaw += look.x;
            _pitch = Mathf.Clamp(_pitch - look.y, _minPitch, _maxPitch);

            if (_playerBody != null)
            {
                _playerBody.rotation = Quaternion.Euler(0f, _yaw, 0f);
            }

            if (_cameraPivot != null)
            {
                _cameraPivot.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
            }
        }

        private Vector2 ReadLookInput()
        {
#if ENABLE_INPUT_SYSTEM
            if (Mouse.current != null)
            {
                return Mouse.current.delta.ReadValue() * _mouseSensitivity;
            }

            if (Gamepad.current != null)
            {
                return Gamepad.current.rightStick.ReadValue() * (_gamepadSensitivity * Time.deltaTime);
            }

            return Vector2.zero;
#else
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * _gamepadSensitivity * Time.deltaTime;
#endif
        }

        private void ApplyCursorState()
        {
            if (!_lockCursor)
            {
                return;
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private static float NormalizePitch(float pitch)
        {
            return pitch > 180f ? pitch - 360f : pitch;
        }
    }
}
