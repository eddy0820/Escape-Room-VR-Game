using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScannerReversable : MonoBehaviour, IReversable
{
    [SerializeField] GameObject brokenScanner;
    [SerializeField] GameObject fixedScanner;
    [SerializeField] float growthSpeed;
    [ReadOnly, SerializeField] bool activated;

    public void DoRewind()
    {
        if(!activated)
        {
            activated = true;
            StartCoroutine(GrowTree());
        }
    }

    IEnumerator GrowTree()
    {
        yield return new WaitForSecondsRealtime(growthSpeed);

        brokenScanner.SetActive(false);
        fixedScanner.SetActive(true);

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
