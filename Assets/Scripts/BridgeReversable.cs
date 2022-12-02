using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeReversable : MonoBehaviour, IReversable
{
    [SerializeField] GameObject bridgeFixed;
    [SerializeField] GameObject bridgePieces;
    [SerializeField] GameObject bridgeMissingPiece;

    [Space(10)]
    [SerializeField] Vector3 bridgeMissingPieceStartingPosition;
    [SerializeField] Quaternion bridgeMissingPieceStartingRotation;

    [SerializeField] Vector3 bridgeMissingPieceEndingPosition;
    [SerializeField] Quaternion bridgeMissingPieceEndingRotation;
    
    [Space(10)]
    [SerializeField] PositionsObject positionsObject;

    [Space(10)]
    [SerializeField] float upForce;
    [SerializeField] float force;
    [SerializeField] bool rightOrLeftForce;
    [SerializeField] float torque;
    [SerializeField] bool rightOrLeftTorque;
    [SerializeField] float secondBetweenEveryStep = 0;

    [SerializeField] bool debug;

    bool isRecording;
    bool isRewinding;

    Vector3 forceDirection;
    Vector3 torqueDirection;

    List<PositionsObject.PointInTime> pointsInTimeHere;

    private void Awake()
    {
        if(rightOrLeftForce)
        {
            forceDirection = -bridgeMissingPiece.transform.right;
        }
        else
        {
            forceDirection = bridgeMissingPiece.transform.right;
        }

        if(rightOrLeftTorque)
        {
            torqueDirection = -bridgeMissingPiece.transform.forward;
        }
        else
        {
            torqueDirection = bridgeMissingPiece.transform.forward;
        }

        pointsInTimeHere = new List<PositionsObject.PointInTime>(positionsObject.pointsInTime);
    }

    [ContextMenu("SetStartPos")]
    public void SetStartPos()
    {
        bridgeMissingPiece.transform.localPosition = bridgeMissingPieceStartingPosition;
        bridgeMissingPiece.transform.localRotation = bridgeMissingPieceStartingRotation;
    }

    [ContextMenu("SetEndPos")]
    public void SetEndPos()
    {
        bridgeMissingPiece.transform.localPosition = bridgeMissingPieceEndingPosition;
        bridgeMissingPiece.transform.localRotation = bridgeMissingPieceEndingRotation;
    }

    private void Update()
    {
        if(debug == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isRecording = true;
                pointsInTimeHere = new List<PositionsObject.PointInTime>();
                
                bridgeMissingPiece.GetComponent<Rigidbody>().AddForce(forceDirection * force, ForceMode.Impulse);
                bridgeMissingPiece.GetComponent<Rigidbody>().AddForce(Vector3.up * upForce, ForceMode.Impulse);
                bridgeMissingPiece.GetComponent<Rigidbody>().AddTorque(torqueDirection * torque, ForceMode.Impulse);

                bridgeMissingPiece.GetComponent<Rigidbody>().useGravity = true;
            }
            else if(Input.GetKeyDown(KeyCode.Return))
            {
                isRecording = false;
                pointsInTimeHere = new List<PositionsObject.PointInTime>(positionsObject.pointsInTime);
            }
            else if(Input.GetKeyDown(KeyCode.Tab))
            {
                positionsObject.pointsInTime.Clear();
            }
            else if(Input.GetKeyDown(KeyCode.A))
            {
                isRewinding = true;
            }

            if(isRecording)
            {
                positionsObject.Record(bridgeMissingPiece.transform.localPosition, bridgeMissingPiece.transform.localRotation);
            }

            if(isRewinding)
            {
                if(pointsInTimeHere.Count > 0)
                {
                    PositionsObject.PointInTime pointInTime = pointsInTimeHere[0];
                    bridgeMissingPiece.transform.localPosition = pointInTime.position;
                    bridgeMissingPiece.transform.localRotation = pointInTime.rotation;
                    pointsInTimeHere.RemoveAt(0);
                }
                else
                {
                    isRewinding = false;
                    bridgeMissingPiece.GetComponent<Rigidbody>().useGravity = false;
                    bridgeMissingPiece.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }  
    }


    public void DoRewind()
    {
        pointsInTimeHere = new List<PositionsObject.PointInTime>(positionsObject.pointsInTime);
        StartCoroutine(RewindTime());
    }

    IEnumerator RewindTime()
    {
        bool b = true;

        while(b)
        {
            if(pointsInTimeHere.Count > 0)
            {
                PositionsObject.PointInTime pointInTime = pointsInTimeHere[0];
                bridgeMissingPiece.transform.localPosition = pointInTime.position;
                bridgeMissingPiece.transform.localRotation = pointInTime.rotation;
                pointsInTimeHere.RemoveAt(0);
            }
            else
            {
                bridgeFixed.SetActive(true);
                bridgePieces.SetActive(false);
                bridgeMissingPiece.SetActive(false);
                b = false;
                bridgeMissingPiece.GetComponent<Rigidbody>().useGravity = false;
                bridgeMissingPiece.GetComponent<Rigidbody>().isKinematic = true;
            }

            yield return new WaitForEndOfFrame();
        }

        yield break;
    }

    public void SetAsCurrentTimeReversalObject()
    {
        TimeManager.Instance.SetCurrentTimeReversalObject(gameObject);
    }

    public void UnsetAsCurrentTimeReversalObject()
    {
        TimeManager.Instance.SetCurrentTimeReversalObject(null);
    }
}
