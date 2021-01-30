using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using BDaman;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IPlayerActions
{
    public UnityAction<float> MoveEvent = delegate { }; 
    public UnityAction<float> LookEvent = delegate { }; 
    public UnityAction        FireEvent = delegate { };

    private GameInput gameInput;

    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new GameInput();
            gameInput.Player.SetCallbacks(this);
        }

        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.started)
            FireEvent.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookEvent.Invoke(context.ReadValue<float>());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent.Invoke(context.ReadValue<float>());
    }

    public void EnableGameplayInput()
    {
        gameInput.Player.Enable();

    }

    public void DisableAllInput()
    {
        gameInput.Player.Disable();
    }
}
