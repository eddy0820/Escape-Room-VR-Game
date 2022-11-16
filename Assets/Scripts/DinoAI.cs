using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class DinoAI : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] float chaseDelay = 4;
    [SerializeField] GameObject rig;
    [SerializeField] Transform endChasePoint;

    bool doChase;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        doChase = true;
    } 

    public void StartChase()
    {
        StartCoroutine(ChaseDelay());
    }

    public void EndChase()
    {
        doChase = false;
        
        agent.SetDestination(endChasePoint.position);
    }

    IEnumerator ChaseDelay()
    {
        yield return new WaitForSeconds(chaseDelay);

        StartCoroutine(RefreshFollowPoint());
        
        yield break;
    }

    IEnumerator RefreshFollowPoint()
    {
        while(doChase)
        {
            agent.SetDestination(rig.transform.position);
            yield return new WaitForEndOfFrame();
        }

        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}