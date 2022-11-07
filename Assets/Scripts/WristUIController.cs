using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WristUIController : MonoBehaviour
{
    public static WristUIController Instance {get; private set; }
    [SerializeField] GameObject wristCanvas;
    [SerializeField] GameObject timeStopScreen;
    [SerializeField] GameObject timeSlowScreen;
    [SerializeField] GameObject timeReverseScreen;
    public TextMeshProUGUI timeSlowTimeLeftText;
    public TextMeshProUGUI timeSlowCooldownText;
    public TextMeshProUGUI timeReverseCooldownText;

    private void Awake()
    {
        Instance = this;

        wristCanvas.SetActive(false);

        timeSlowTimeLeftText.text = "Time Left: " + Mathf.Round(TimeManager.Instance.SlowDownTimer);
        timeSlowCooldownText.text = "Cooldown: " + 0;
        timeReverseCooldownText.text = "Cooldown: " + 0;
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
                break;
            case TimeManipulationModes.Slow:
                TimeManager.Instance.currentTimeManipulationMode = TimeManipulationModes.Reverse;
                DisableAllScreens();
                timeReverseScreen.SetActive(true);
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
    }

    
}
