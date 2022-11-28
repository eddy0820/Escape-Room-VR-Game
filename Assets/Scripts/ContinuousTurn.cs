using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class ContinuousTurn : ContinuousTurnBase
{
    bool turnOn = false;

    [SerializeField] InputActionProperty m_LeftHandTurnAction;

    public InputActionProperty leftHandTurnAction
    {
        get => m_LeftHandTurnAction;
        set => SetInputActionProperty(ref m_LeftHandTurnAction, value);
    }

    [SerializeField] InputActionProperty m_RightHandTurnAction;
 
    public InputActionProperty rightHandTurnAction
    {
        get => m_RightHandTurnAction;
        set => SetInputActionProperty(ref m_RightHandTurnAction, value);
    }

    protected void OnEnable()
    {
        m_LeftHandTurnAction.EnableDirectAction();
        m_RightHandTurnAction.EnableDirectAction();
    }

    protected void OnDisable()
    {
        m_LeftHandTurnAction.DisableDirectAction();
        m_RightHandTurnAction.DisableDirectAction();
    }

    protected override Vector2 ReadInput()
    {
        var leftHandValue = m_LeftHandTurnAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
        var rightHandValue = m_RightHandTurnAction.action?.ReadValue<Vector2>() ?? Vector2.zero;

        if(turnOn)
        {
            return leftHandValue + rightHandValue;
        }
        else
        {
            return Vector2.zero;
        }
    }

    void SetInputActionProperty(ref InputActionProperty property, InputActionProperty value)
    {
        if (Application.isPlaying)
            property.DisableDirectAction();

        property = value;

        if (Application.isPlaying && isActiveAndEnabled)
            property.EnableDirectAction();
    }

    public void TurnFlipOn()
    {
        turnOn = true;
    }

    public void TurnFlipOff()
    {
        turnOn = false;
    }
}
