using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] Material[] materials;

    [Space(15)]

    [SerializeField] float lifetime = 6f;   

    private void Awake()
    {
        GetComponent<Renderer>().material = materials[Random.Range(0, materials.Length)];
        
        StartCoroutine(DestroyCube());
    }

    IEnumerator DestroyCube()
    {
        yield return new WaitForSeconds(lifetime);

        if(GetComponent<TimeBody>().IsRewinding)
        {
            StartCoroutine(DestroyCube());
        }
        else
        {
            Destroy(gameObject);
        }
        
        yield break;
    }
}
