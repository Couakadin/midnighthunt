using Data.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Midnighthunt.Runtime
{
    public class PlayerController : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake()
        {
            _inputController = new InputController();

            _camera = Camera.main;

            Cursor.visible = false;

            if (_playerBlackboard.ContainsKey("PlayerSpeedBase"))
                _playerSpeedBase = _playerBlackboard.GetValue<float>("PlayerSpeedBase");
            if (_playerBlackboard.ContainsKey("PlayerSpeedFocus"))
                _playerSpeedFocus = _playerBlackboard.GetValue<float>("PlayerSpeedFocus");
            if (_playerBlackboard.ContainsKey("PlayerRun"))
                _playerRun = _playerBlackboard.GetValue<float>("PlayerRun");

            if (_lanternBlackboard.ContainsKey("LightIntensityBase"))
                _lightIntensityBase = _lanternBlackboard.GetValue<float>("LightIntensityBase");
            if (_lanternBlackboard.ContainsKey("LightIntensityBase"))
                _lightAngleBase = _lanternBlackboard.GetValue<float>("LightAngleBase");
            if (_lanternBlackboard.ContainsKey("LightIntensityFocus"))
                _lightIntensityFocus = _lanternBlackboard.GetValue<float>("LightIntensityFocus");
            if (_lanternBlackboard.ContainsKey("LightIntensityFocus"))
                _lightAngleFocus = _lanternBlackboard.GetValue<float>("LightAngleFocus");                     
            if (_lanternBlackboard.ContainsKey("ClassicLightColor"))
                _classicLightColor = _lanternBlackboard.GetValue<Color>("ClassicLightColor");
            if (_lanternBlackboard.ContainsKey("DangerLightColor"))
                _dangerLightColor = _lanternBlackboard.GetValue<Color>("DangerLightColor");
        }

        private void FixedUpdate()
        {
            Vector3 velocity = _camera.transform.forward * GetPlayerMovement().z + _camera.transform.right * GetPlayerMovement().x;
            if (_lanternBlackboard.ContainsKey("IsInDangerZone"))
                _isInDangerZone = _lanternBlackboard.GetValue<bool>("IsInDangerZone");
            if (_isInDangerZone) 
                _lantern.color = _dangerLightColor;
            else 
                _lantern.color = _classicLightColor;
            if (IsLighting())
            {
                _lantern.spotAngle = _lightAngleFocus;
                _lantern.intensity = _lightIntensityFocus;
                _playerRigidbody.velocity = velocity * _playerSpeedFocus;
            }
            else
            {
                _lantern.spotAngle = _lightAngleBase;
                _lantern.intensity = _lightIntensityBase;        
                if (IsRunning())
                    _playerRigidbody.velocity = velocity * _playerRun;
                else
                    _playerRigidbody.velocity = velocity * _playerSpeedBase;
            }
            _playerRigidbody.velocity = new Vector3(_playerRigidbody.velocity.x, 0f, _playerRigidbody.velocity.z);
        }

        private void OnEnable() => _inputController.Enable();

        private void OnDisable() => _inputController.Disable();

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
                _playerBlackboard.SetValue<bool>("GroundedPlayer", true);
        }

        #endregion

        #region Methods

        public Vector3 GetPlayerMovement() => _inputController.Player.Move.ReadValue<Vector3>();
        
        public Vector3 GetMouseDelta() => _inputController.Player.Look.ReadValue<Vector3>();

        public bool IsLighting() => _inputController.Player.Light.IsPressed();

        public bool IsRunning() => _inputController.Player.Run.IsPressed();

        #endregion

        #region Utils

        #endregion

        #region Privates

        [Title("Components")]
        [SerializeField]
        private Blackboard _playerBlackboard;
        [SerializeField]
        private Blackboard _lanternBlackboard;
        [SerializeField]
        private Rigidbody _playerRigidbody;
        [SerializeField]
        private Light _lantern;

        private InputController _inputController;

        private Camera _camera;

        private bool _isInDangerZone;
        private Color _classicLightColor;
        private Color _dangerLightColor;

        private float _playerSpeedBase;
        private float _playerSpeedFocus;
        private float _playerRun;

        private float _lightIntensityBase;
        private float _lightAngleBase;
        private float _lightIntensityFocus;
        private float _lightAngleFocus;

        #endregion
    }
}
