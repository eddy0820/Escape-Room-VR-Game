using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLauncher : MonoBehaviour
{
    [SerializeField] GameObject cubePrefab;

    [Space(15)]

    [SerializeField] float secondsBetweenCubes = 2.0f;

    [Space(15)]
    [SerializeField] float forwardForce = 3f;
    [SerializeField] float upwardsForce = 9f;
    [SerializeField] float torque = 2f;

    [Space(15)]
    [SerializeField] bool setAmount;
    [SerializeField] int amount;

    private void Start()
    {
        StartCoroutine(LaunchCubes());
    }

    IEnumerator LaunchCubes()
    {
        if(setAmount)
        {
            int currAmount = 0;

            while(currAmount < amount)
            {
                if(TimeManager.Instance.ReversingTime == false)
                {
                    DoCubeSpawn();
                    currAmount++;
                }
                yield return new WaitForSeconds(secondsBetweenCubes);
            }
        }
        else
        {
            while(true)
            {
                if(TimeManager.Instance.ReversingTime == false)
                {
                    DoCubeSpawn();
                }
                yield return new WaitForSeconds(secondsBetweenCubes);
            }
        }
    }

    private void DoCubeSpawn()
    {
        GameObject cube = Instantiate(cubePrefab, transform.position, Quaternion.identity);
        Rigidbody cubeRB = cube.GetComponent<Rigidbody>();

        cubeRB.AddForce(new Vector3(transform.forward.x * forwardForce, upwardsForce, transform.forward.z * forwardForce), ForceMode.Impulse);
        cubeRB.AddTorque(cube.transform.up * torque, ForceMode.Impulse);
        cubeRB.AddTorque(cube.transform.forward * torque, ForceMode.Impulse);
        cubeRB.AddTorque(cube.transform.right * torque, ForceMode.Impulse);
    }
}
