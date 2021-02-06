using Mirror;
using UnityEngine;
using NaughtyAttributes;

namespace BDaman
{
    public class BDamanController : NetworkBehaviour
    {
        [SerializeField] private InputReader inputReader = default;

        [BoxGroup("Movement")]
        [SyncVar]
        private float horizontalMoveDirection = 0f;
        [BoxGroup("Movement")]
        [SerializeField] private FloatVariable horizontalMoveSpeed = default;

        [BoxGroup("Rotation")]
        [SerializeField] private FloatVariable maxRotationDegrees = default;

        private Vector3 _rightDirection = Vector3.right;
        private float _eulerRotationY = 0f;

        private CharacterController _characterController = default;

        [Command]
        public void CmdSetOrientation(Vector3 rightDirection, float eulerRotationY)
        {
            _rightDirection = rightDirection;
            _eulerRotationY = eulerRotationY;

            RpcSetOrientation(rightDirection, eulerRotationY);
        }

        [ClientRpc]
        public void RpcSetOrientation(Vector3 rightDirection, float eulerRotationY)
        {
            _rightDirection = rightDirection;
            _eulerRotationY = eulerRotationY;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public override void OnStartAuthority()
        {
            inputReader.MoveEvent += CmdHorizontalMove;
            inputReader.LookEventPerformed += CmdRotate;
            inputReader.LookEventCanceled += CmdRotateCenter;

            enabled = true;
        }

        public override void OnStopAuthority()
        {
            inputReader.MoveEvent -= CmdHorizontalMove;
            inputReader.LookEventPerformed -= CmdRotate;
            inputReader.LookEventCanceled -= CmdRotateCenter;
        }

        private void OnDestroy()
        {
            if (hasAuthority)
            {
                inputReader.MoveEvent -= CmdHorizontalMove;
                inputReader.LookEventPerformed -= CmdRotate;
                inputReader.LookEventCanceled -= CmdRotateCenter;
            }
        }

        [Command]
        public void CmdHorizontalMove(float direction)
        {
            // Clamp direction input
            direction = Mathf.Clamp(direction, -1f, 1f);

            // Debug View
            horizontalMoveDirection = direction;
        }

        [Command]
        public void CmdRotate(float direction)
        {
            // Clamp direction input
            direction = Mathf.Clamp(direction, -1f, 1f);

            Vector3 rotation = Vector3.up * maxRotationDegrees * direction;
            rotation.y += _eulerRotationY;
            transform.localEulerAngles = rotation;

            RpcRotate(rotation);
        }

        [Command]
        public void CmdRotateCenter(float direction)
        {
            Vector3 rotation = Vector3.zero;
            rotation.y += _eulerRotationY;
            transform.localEulerAngles = rotation;

            RpcRotate(rotation);
        }

        [ClientRpc]
        public void RpcRotate(Vector3 localEulerAngles)
        {
            transform.localEulerAngles = localEulerAngles;
        }

        //[ServerCallback]
        private void FixedUpdate()
        {
            float movement = horizontalMoveDirection * horizontalMoveSpeed;
            _characterController.SimpleMove(_rightDirection * movement * Time.fixedDeltaTime);
        }
    }
}