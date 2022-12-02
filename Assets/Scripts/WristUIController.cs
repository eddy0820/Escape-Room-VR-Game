using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class WristUIController : MonoBehaviour
{
    public static WristUIController Instance {get; private set; }
    [SerializeField] GameObject wristCanvas;
    [SerializeField] GameObject timeStopScreen;
    [SerializeField] GameObject timeSlowScreen;
    [SerializeField] GameObject timeReverseScreen;
    [SerializeField] GameObject timeSlowInfoScreen;
    [SerializeField] GameObject timeReverseInfoScreen;
    public TextMeshProUGUI timeSlowTimeLeftText;
    public TextMeshProUGUI timeSlowCooldownText;
    public TextMeshProUGUI timeReverseCooldownText;
    [SerializeField] GameObject rightHandController;
    [SerializeField] GameObject rightHandControllerInteractor;
    [SerializeField] InputActionProperty rightHandSelectAction;
    XRRayInteractor rightHandRayInteractor;
    LineRenderer rightHandLineRenderer;
    XRInteractorLineVisual rightHandInteractorLineVisual;


    private void Awake()
    {
        Instance = this;

        wristCanvas.SetActive(false);

        timeSlowTimeLeftText.text = "Time Left: " + Mathf.Round(TimeManager.Instance.SlowDownTimer);
        timeSlowCooldownText.text = "Cooldown: " + 0;
        timeReverseCooldownText.text = "Cooldown: " + 0;

        rightHandRayInteractor = rightHandController.GetComponent<XRRayInteractor>();
        rightHandInteractorLineVisual = rightHandController.GetComponent<XRInteractorLineVisual>();
        rightHandLineRenderer = rightHandController.GetComponent<LineRenderer>();

        DisableRightHandRay();
    }

    public void ToggleUI()
    {
        if(wristCanvas.activeInHierarchy)
        {
            wristCanvas.SetActive(false);
        }
        else
        {
            wristCanvas.SetActive(true);
        }
    }

    public void SwitchTimeManipulationModeAndScreen(Vector2 input)
    {
        if(!TimeManager.Instance.StoppingTime && !TimeManager.Instance.SlowingDownTime && !TimeManager.Instance.ReversingTime)
        {
            if(Mathf.Abs(input.x) > 0.5)
            {
                if(input.x > 0)
                {
                    if(wristCanvas.activeInHierarchy)
                        SwitchTimeManipulationModeAndScreenRight();
                }
                else
                {
                    if(wristCanvas.activeInHierarchy)
                        SwitchTimeManipulationModeAndScreenLeft();
                }
            }
        } 
    }

    private void SwitchTimeManipulationModeAndScreenLeft()
    {
        switch(TimeManager.Instance.currentTimeManipulationMode)
        {
            case TimeManipulationModes.Slow:
                TimeManager.Instance.currentTimeManipulationMode = TimeManipulationModes.Stop;
                DisableAllScreens();
                timeStopScreen.SetActive(true);
                break;
            case TimeManipulationModes.Reverse:
                TimeManager.Instance.currentTimeManipulationMode = TimeManipulationModes.Slow;
                DisableAllScreens();
                timeSlowScreen.SetActive(true);
                timeSlowInfoScreen.SetActive(true);
                DisableRightHandRay();
                rightHandControllerInteractor.SetActive(true);
                rightHandSelectAction.action.Enable();
                break;
            default:
                break;
        }
    }


    private void SwitchTimeManipulationModeAndScreenRight()
    {
        switch(TimeManager.Instance.currentTimeManipulationMode)
        {
            case TimeManipulationModes.Stop:
                TimeManager.Instance.currentTimeManipulationMode = TimeManipulationModes.Slow;
                DisableAllScreens();
                timeSlowScreen.SetActive(true);
                timeSlowInfoScreen.SetActive(true);
                break;
            case TimeManipulationModes.Slow:
                TimeManager.Instance.currentTimeManipulationMode = TimeManipulationModes.Reverse;
                DisableAllScreens();
                timeReverseScreen.SetActive(true);
                timeReverseInfoScreen.SetActive(true);
                EnableRightHandRay();
                rightHandSelectAction.action.Disable();
                rightHandControllerInteractor.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void DisableAllScreens()
    {
        timeStopScreen.SetActive(false);
        timeSlowScreen.SetActive(false);
        timeReverseScreen.SetActive(false);
        timeSlowInfoScreen.SetActive(false);
        timeReverseInfoScreen.SetActive(false);
    }

    private void DisableRightHandRay()
    {
        rightHandRayInteractor.enabled = false;
        rightHandInteractorLineVisual.enabled = false;
        rightHandLineRenderer.enabled = false;
    }

    private void EnableRightHandRay()
    {
        rightHandRayInteractor.enabled = true;
        rightHandInteractorLineVisual.enabled = true;
        rightHandLineRenderer.enabled = true;
    }
}
