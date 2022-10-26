using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance {get; private set; }
    [SerializeField] float startingTimeScale = 1f;
    [SerializeField] float slowDownTimeScale = 0.5f;
    [SerializeField] float timeStopSlowDownStepSpeed = 0.05f;

    [Header("Debug")]
    [SerializeField] bool debug;
    [ReadOnly, SerializeField] bool stoppingTime;
    [ReadOnly, SerializeField] bool slowingDownTime;
    [ReadOnly, SerializeField] bool reversingTime;
    public bool ReversingTime => reversingTime;

    Coroutine timestopCoroutine;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = startingTimeScale;
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

    public void StopTime()
    {
        timestopCoroutine = StartCoroutine(DoTimeStop());
        stoppingTime = true;
    }

    public void ResetTimeStop()
    {
        Time.timeScale = startingTimeScale;
        StopCoroutine(timestopCoroutine);
        stoppingTime = false;
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
        Time.timeScale = slowDownTimeScale;
        slowingDownTime = true;
    }

    public void ResetTimeSlow()
    {
        Time.timeScale = startingTimeScale;
        slowingDownTime = false;
    }

    public void ReverseTime()
    {
        TimeBody[] timeBodies = GameObject.FindObjectsOfType<TimeBody>();

        foreach(TimeBody timeBody in timeBodies)
        {
            timeBody.StartRewind();
        }

        reversingTime = true;
    }

    public void ResetTimeReverse()
    {
        TimeBody[] timeBodies = GameObject.FindObjectsOfType<TimeBody>();

        foreach(TimeBody timeBody in timeBodies)
        {
            timeBody.StopRewind();
        }

        reversingTime = false;
    }
}
