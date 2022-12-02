using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColliderSize : MonoBehaviour
{
    CharacterController characterController;
    CapsuleCollider capsuleCollider;

    private void Awake()
    {
        characterController = transform.parent.parent.GetComponent<CharacterController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        capsuleCollider.center = characterController.center;
        capsuleCollider.radius = characterController.radius;
        capsuleCollider.height = characterController.height;
    }
}
