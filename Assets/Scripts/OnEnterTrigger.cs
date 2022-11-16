using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnterTrigger : MonoBehaviour
{
    [SerializeField] TriggerEntryEvent function;

    [ReadOnly, SerializeField] bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if(!activated && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            activated = true;
            function.Invoke();
        }
    }


    [System.Serializable]
    public class TriggerEntryEvent : UnityEvent{}
}
