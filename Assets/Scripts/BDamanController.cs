using Mirror;
using UnityEngine;
using NaughtyAttributes;

public class BDamanController : NetworkBehaviour
{
    [SerializeField] private InputReader inputReader = default;
    [SerializeField] [Required] private Transform movementTransform = default;
    [SerializeField] [Required] private Transform rotationTransform = default;

    [ShowNonSerializedField]
    private float _horizontalMoveDirection = 0f;
    [SerializeField]
    private FloatVariable _horizontalMoveSpeed = default;

    [ShowNonSerializedField]
    private float _rotationDirection = 0f;
    [SerializeField]
    private FloatVariable _rotationSpeed = default;

    [Client]
    private void Start()
    {
        Debug.Log(hasAuthority + " A/L " + isLocalPlayer);
        if (hasAuthority)
        {
            inputReader.MoveEvent += HorizontalMove;
            inputReader.LookEvent += Rotate;
        }
    }

    [Client]
    private void OnDestroy()
    {
        if (hasAuthority)
        {
            inputReader.MoveEvent -= HorizontalMove;
            inputReader.LookEvent -= Rotate;
        }
    }

    [Client]
    public void HorizontalMove(float direction)
    {
        _horizontalMoveDirection = direction;
    }

    [Client]
    public void Rotate(float direction)
    {
        _rotationDirection = direction;
    }

    [Client]
    private void FixedUpdate()
    {
        float movement = _horizontalMoveDirection * _horizontalMoveSpeed;
        float rotation = _rotationDirection * _rotationSpeed;

        // Mudar para comando para servidor
        movementTransform.Translate(transform.right * movement * Time.fixedDeltaTime);
        rotationTransform.Rotate(Vector3.up, rotation * Time.fixedDeltaTime);
    }


}
