using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class WatchConsumption : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Fruit"))
        {
            SceneManager.LoadScene(other.GetComponent<FruitSceneHolder>().SceneToLoad);
        }
    } 
}
