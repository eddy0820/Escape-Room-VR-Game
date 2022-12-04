using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSceneHolder : MonoBehaviour
{
    [Scene]
    [SerializeField] string sceneToLoad;

    public string SceneToLoad => sceneToLoad;
}
