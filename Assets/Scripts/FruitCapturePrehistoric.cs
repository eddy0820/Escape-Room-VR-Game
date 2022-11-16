using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCapturePrehistoric : MonoBehaviour
{
    [SerializeField] GameObject banana;
    [ReadOnly, SerializeField] bool activated;

    [SerializeField] float launchForce = 2;

    Rigidbody rb;

    private void Awake()
    {
        rb = banana.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!activated && other.gameObject.layer == LayerMask.NameToLayer("Spear"))
        {
            activated = true;
            rb.isKinematic = false;
            rb.AddForce(banana.transform.forward * launchForce, ForceMode.Impulse);
        }
    }
}
