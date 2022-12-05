using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] float rotationAmount = 90;
    [SerializeField] float movementDuration = 1;


    Quaternion target;

    private void Awake()
    {
        target = new Quaternion(door.transform.rotation.x, door.transform.rotation.y + rotationAmount, door.transform.rotation.x, door.transform.rotation.z);
    }
    public void DoDoorOpen()
    {
        StartCoroutine(LerpRotation());
    }

    IEnumerator LerpRotation()
    {
        float time = 0;
        Quaternion startRotation = door.transform.rotation;
        while (time < movementDuration)
        {
            door.transform.rotation = Quaternion.Lerp(startRotation, target, time / movementDuration);
            time += Time.deltaTime;
            yield return null;
        }

        door.transform.rotation = target;

        yield break;
    }
}
