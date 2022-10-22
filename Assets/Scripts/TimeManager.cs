using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance {get; private set; }
    [SerializeField] float startingTimeScale = 1f;
    [SerializeField] float slowDownTimeScale = 0.5f;

    [Header("Debug")]
    [SerializeField] bool stopTime;
    [SerializeField] bool slowDownTime;
    public bool timeReverse;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = startingTimeScale;
    }

    private void Update()
    {
        if(stopTime)
        {
            Time.timeScale = 0;
        }
        else if(slowDownTime)
        {
            Time.timeScale = slowDownTimeScale;
        }
        else
        {
            Time.timeScale = startingTimeScale;
        }

        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
}
