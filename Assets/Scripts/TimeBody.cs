using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    [SerializeField, ReadOnly] bool isRewinding;
    public bool IsRewinding => isRewinding;

    [SerializeField] float recordTime = 6f;
    List<PointInTime> pointsInTime;
    Rigidbody rb;

    Coroutine lastRewindRoutine = null;

    private void Awake()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DoRecord());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartRewind();
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            StopRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
        lastRewindRoutine = StartCoroutine(DoRewind());
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
        StopCoroutine(lastRewindRoutine);
    }

    IEnumerator DoRewind()
    {
        while(true)
        {
            Rewind();
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator DoRecord()
    {
        while(true)
        {
            if(!isRewinding)
            {
                Record();
            }
            
            yield return new WaitForFixedUpdate();
        }
    }

    private void Rewind()
    {
        if(pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            rb.position = pointInTime.position;
            rb.rotation = pointInTime.rotation;
            rb.velocity = pointInTime.velocity;
            rb.angularVelocity = pointInTime.angularVelocity;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
        
    }

    private void Record()
    {
        if(pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(rb.position, rb.rotation, rb.velocity, rb.angularVelocity));
    }

    struct PointInTime
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 velocity;
        public Vector3 angularVelocity;

        public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 _velocity, Vector3 _angularVelocity)
        {
            position = _position;
            rotation = _rotation;
            velocity = _velocity;
            angularVelocity = _angularVelocity;
        }
    }
}
