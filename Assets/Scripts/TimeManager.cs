using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance {get; private set; }
    [SerializeField] float startingTimeScale = 1f;
    [SerializeField] float slowDownTimeScale = 0.5f;

    [Space(15)]

    [SerializeField] float timeStopSlowDownStepSpeed = 0.05f;
    [SerializeField] float slowDownTimer = 10f;
    public float SlowDownTimer => slowDownTimer;
    [SerializeField] float slowDownCooldown = 5f;
    public float SlowDownCooldown => slowDownCooldown;

    [Space(15)]

    [SerializeField] float globalRecordTime = 6f;
    public float GlobalRecordTime => globalRecordTime;
    [SerializeField] float reverseTimeCooldown = 5f;
    public float ReverseTimeCooldown => reverseTimeCooldown;

    [Header("Debug")]
    [SerializeField] bool debug;
    [ReadOnly] public TimeManipulationModes currentTimeManipulationMode;
    [ReadOnly, SerializeField] bool stoppingTime;
    public bool StoppingTime => stoppingTime;
    [ReadOnly, SerializeField] bool slowingDownTime;
    public bool SlowingDownTime => slowingDownTime;
    [ReadOnly, SerializeField] bool canSlowTime;
    [ReadOnly, SerializeField] bool reversingTime;
    public bool ReversingTime => reversingTime;
    [ReadOnly, SerializeField] bool canReverseTime;

    Coroutine timestopCoroutine;
    Coroutine timeSlowCoroutine;

    GameObject currentlySelectedTimeReversalObject = null;

    WristUIController wristUI;


    private void Awake()
    {
        Instance = this;
        Time.timeScale = startingTimeScale;
        currentTimeManipulationMode = TimeManipulationModes.Stop;
        canSlowTime = true;
        canReverseTime = true;
        wristUI = FindObjectOfType<WristUIController>();
    }

    private void Update()
    {
        if(debug)
        {
            if(stoppingTime)
            {
                Time.timeScale = 0;
            }
            else if(slowingDownTime)
            {
                Time.timeScale = slowDownTimeScale;
            }
            else
            {
                Time.timeScale = startingTimeScale;
            }
        }
        
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    public void ToggleTimeManipulation()
    {
        switch(currentTimeManipulationMode)
        {
            case TimeManipulationModes.Stop:

                if(stoppingTime)
                {
                    ResetTimeStop();
                }
                else
                {
                    StopTime();
                }
                
                break;

            case TimeManipulationModes.Slow:

                if(slowingDownTime)
                {
                    ResetTimeSlow();
                }
                else
                {
                    SlowTime();
                }

                break;

            case TimeManipulationModes.Reverse:

                if(currentlySelectedTimeReversalObject != null)
                {
                    ReverseTimeTargeted();
                }
                else
                {
                    if(reversingTime)
                    {
                        ResetTimeReverse();
                    }
                    else
                    {
                        ReverseTime();
                    }
                }
                

                break;
        }
    }

    public void StopTime()
    {
        timestopCoroutine = StartCoroutine(DoTimeStop());
        stoppingTime = true;
        wristUI.timeStopBackground.SetActive(true);
    }

    public void ResetTimeStop()
    {
        Time.timeScale = startingTimeScale;
        StopCoroutine(timestopCoroutine);
        stoppingTime = false;
        wristUI.timeStopBackground.SetActive(false);
    }

    IEnumerator DoTimeStop()
    {
        while(true)
        {
            if(Time.timeScale <= 0)
            {
                yield break;
            }

            if(Time.timeScale - timeStopSlowDownStepSpeed < 0)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = Time.timeScale - timeStopSlowDownStepSpeed;
            }
            
            yield return new WaitForEndOfFrame();
        }
    }

    public void SlowTime()
    {
        if(canSlowTime)
        {
            Time.timeScale = slowDownTimeScale;
            slowingDownTime = true;
            timeSlowCoroutine = StartCoroutine(TimeSlowDownTimer());
            canSlowTime = false;
            wristUI.timeSlowBackground.SetActive(true);
        }
    }

    public void ResetTimeSlow()
    {
        Time.timeScale = startingTimeScale;
        slowingDownTime = false;
        StopCoroutine(timeSlowCoroutine);
        WristUIController.Instance.timeSlowTimeLeftText.text = "Time Left: " + Mathf.Round(slowDownTimer);
        StartCoroutine(TimeSlowDownCooldown());
        wristUI.timeSlowBackground.SetActive(false);
    }

    IEnumerator TimeSlowDownTimer()
    {
        float timer = slowDownTimer;
        bool b = true;
        WristUIController.Instance.timeSlowTimeLeftText.text = "Time Left: " + Mathf.Round(timer);

        while(b)
        {
            timer -= Time.unscaledDeltaTime;
            WristUIController.Instance.timeSlowTimeLeftText.text = "Time Left: " + Mathf.Round(timer);

            if(timer <= 0)
            {
                ResetTimeSlow();
                b = false;
            }

            yield return new WaitForEndOfFrame();
        }

        yield break;
    }

    IEnumerator TimeSlowDownCooldown()
    {
        float timer = slowDownCooldown;
        bool b = true;
        WristUIController.Instance.timeSlowCooldownText.text = "Cooldown: " + Mathf.Round(timer);

        while(b)
        {
            timer -= Time.unscaledDeltaTime;
            WristUIController.Instance.timeSlowCooldownText.text = "Cooldown: " + Mathf.Round(timer);

            if(timer <= 0)
            {
                canSlowTime = true;
                b = false;
            }

            yield return new WaitForEndOfFrame();
        }

        WristUIController.Instance.timeSlowCooldownText.text = "Cooldown: " + Mathf.Round(timer);
        yield break;
    }

    public void ReverseTime()
    {
        if(canReverseTime)
        {
            TimeBody[] timeBodies = GameObject.FindObjectsOfType<TimeBody>();

            foreach(TimeBody timeBody in timeBodies)
            {
                timeBody.StartRewind();
            }

            reversingTime = true;
            canReverseTime = false;
            wristUI.timeReverseBackground.SetActive(true);
        }
    }

    public void ReverseTimeTargeted()
    {
        if(canReverseTime)
        {
            currentlySelectedTimeReversalObject.GetComponent<IReversable>().DoRewind();

            canReverseTime = false;
            ResetTimeReverseTargeted();
        }
    }

    public void ResetTimeReverse()
    {
        TimeBody[] timeBodies = GameObject.FindObjectsOfType<TimeBody>();

        foreach(TimeBody timeBody in timeBodies)
        {
            timeBody.StopRewind();
        }

        reversingTime = false;

        StartCoroutine(TimeReverseCooldown());
        wristUI.timeReverseBackground.SetActive(false);
    }

    public void ResetTimeReverseTargeted()
    {
        StartCoroutine(TimeReverseCooldown());
    }

    IEnumerator TimeReverseCooldown()
    {
        float timer = reverseTimeCooldown;
        bool b = true;
        WristUIController.Instance.timeReverseCooldownText.text = "Cooldown: " + Mathf.Round(timer);

        while(b)
        {
            timer -= Time.unscaledDeltaTime;
            WristUIController.Instance.timeReverseCooldownText.text = "Cooldown: " + Mathf.Round(timer);

            if(timer <= 0)
            {
                canReverseTime = true;
                b = false;
            }

            yield return new WaitForEndOfFrame();
        }

        WristUIController.Instance.timeReverseCooldownText.text = "Cooldown: " + Mathf.Round(timer);
        yield break;
    }

    public void SetCurrentTimeReversalObject(GameObject gameObj)
    {
        currentlySelectedTimeReversalObject = gameObj;
    }
}

public enum TimeManipulationModes
{
    Stop,
    Slow,
    Reverse
}