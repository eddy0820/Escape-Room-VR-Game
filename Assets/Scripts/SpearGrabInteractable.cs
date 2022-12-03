using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpearGrabInteractable : XRGrabInteractable
{
    [Space(10)]
    [SerializeField] float savePosTimer = 2f;
    [SerializeField] float respawnTimer = 7f;

    Vector3 position;
    Quaternion rotation;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(SavePos());
    }

    protected override void Detach()
    {
        base.Detach();
        StartCoroutine(ReturnSpear());
    }

    IEnumerator SavePos()
    {
        yield return new WaitForSecondsRealtime(savePosTimer);
        position = transform.position;
        rotation = transform.rotation;
        yield break;
    }

    IEnumerator ReturnSpear()
    {
        yield return new WaitForSecondsRealtime(respawnTimer);
        transform.position = position;
        transform.rotation = rotation;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        yield break;
    }
}
