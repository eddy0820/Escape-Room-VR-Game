using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScanner : MonoBehaviour
{
    [SerializeField] GameObject doorToOpen;
    [SerializeField] float movementDuration = 5;
    [SerializeField] Transform targetPosition;

    public void OpenDoor()
    {
        StartCoroutine(LerpPosition());
    }

    IEnumerator LerpPosition()
    {
        float time = 0;
        Vector3 startPosition = doorToOpen.transform.localPosition;
        while (time < movementDuration)
        {
            doorToOpen.transform.localPosition = Vector3.Lerp(startPosition, targetPosition.localPosition, time / movementDuration);
            time += Time.deltaTime;
            yield return null;
        }

        doorToOpen.transform.localPosition = targetPosition.localPosition;

        yield break;
    }
}
