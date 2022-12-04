using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversableTree : MonoBehaviour, IReversable
{
    [SerializeField] GameObject brokenTree;
    [SerializeField] GameObject fixedTree;
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

        brokenTree.SetActive(false);
        fixedTree.SetActive(true);

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
