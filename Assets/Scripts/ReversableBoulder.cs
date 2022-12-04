using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversableBoulder : MonoBehaviour, IReversable
{
    [SerializeField] Transform targetPosition;
    [SerializeField] Transform boulderOriginalPosition;
    [SerializeField] float movementDuration = 5;
    [ReadOnly, SerializeField] bool activated;


    public void DoRewind()
    {
        if(!activated)
        {
            StartCoroutine(LerpPosition());
            activated = true;
        }
    }

    IEnumerator LerpPosition()
    {
        float time = 0;
        Vector3 startPosition = boulderOriginalPosition.localPosition;
        while (time < movementDuration)
        {
            boulderOriginalPosition.localPosition = Vector3.Lerp(startPosition, targetPosition.localPosition, time / movementDuration);
            time += Time.deltaTime;
            yield return null;
        }

        boulderOriginalPosition.localPosition = targetPosition.localPosition;

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
