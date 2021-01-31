using Mirror;
using UnityEngine;
using NaughtyAttributes;

public class BDamanController : NetworkBehaviour
{
    [SerializeField] private InputReader inputReader = default;

    [BoxGroup("Movement")] [SyncVar]
    private float horizontalMoveDirection = 0f;
    [BoxGroup("Movement")]
    [SerializeField] private FloatVariable horizontalMoveSpeed = default;

    [BoxGroup("Rotation")]
    [SerializeField] private FloatVariable maxRotationDegrees = default;

    private CharacterController _characterController = default;
    private Vector3 _rightDirection = Vector3.right;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        inputReader.MoveEvent += CmdHorizontalMove;
        inputReader.LookEventPerformed += CmdRotate;
        inputReader.LookEventCanceled += CmdRotateCenter;

        _rightDirection = transform.right;
        enabled = true;
    }

    public override void OnStopAuthority()
    {
        base.OnStopAuthority();
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
        transform.localEulerAngles = rotation;
        
        RpcRotate(rotation);
    }

    [Command]
    public void CmdRotateCenter(float direction)
    {
        Vector3 rotation = Vector3.zero;
        transform.localEulerAngles = rotation;

        RpcRotate(rotation);
    }

    [ClientRpc]
    public void RpcRotate(Vector3 localEulerAngles) => transform.localEulerAngles = localEulerAngles;

    //[ServerCallback]
    private void FixedUpdate()
    {
        float movement = horizontalMoveDirection * horizontalMoveSpeed;
        _characterController.SimpleMove(_rightDirection * movement * Time.fixedDeltaTime);
    }
}
