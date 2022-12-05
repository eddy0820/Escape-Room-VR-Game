using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitReverse : MonoBehaviour, IReversable
{
    [SerializeField] GameObject rotten;
    [SerializeField] GameObject whole;
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

        rotten.SetActive(false);
        whole.SetActive(true);

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
