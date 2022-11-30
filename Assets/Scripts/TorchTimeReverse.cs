using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class TorchTimeReverse : MonoBehaviour, IReversable
{
    GameObject rig;
    [SerializeField] bool activated;
    GameObject lightObject;

    private void Awake()
    {
        rig = FindObjectOfType<XROrigin>().gameObject;
        lightObject = transform.GetChild(0).gameObject;

        if(activated)
        {
            lightObject.SetActive(true);
        }
        else
        {
            lightObject.SetActive(false);
        }
    }

    public void DoRewind()
    {
        if(!activated)
        {
            lightObject.SetActive(true);
            activated = true;
        }
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
