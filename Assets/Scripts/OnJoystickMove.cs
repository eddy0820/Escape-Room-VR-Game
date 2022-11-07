using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class OnJoystickMove : MonoBehaviour
{
    [Tooltip("Actions to check")]
    public InputAction action = null;

    // When the button is pressed
    public JoystickMoveEvent OnMove = new JoystickMoveEvent();

    private void Awake()
    {
        action.started += Pressed;
    }

    private void OnDestroy()
    {
        action.started -= Pressed;
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    private void Pressed(InputAction.CallbackContext context)
    {   
        OnMove.Invoke(context.ReadValue<Vector2>());
    }

    [System.Serializable]
    public class JoystickMoveEvent: UnityEvent<Vector2>{};
}
