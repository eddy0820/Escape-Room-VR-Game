using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WristUIController : MonoBehaviour
{
    public static WristUIController Instance {get; private set; }
    [SerializeField] GameObject wristCanvas;
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
}
