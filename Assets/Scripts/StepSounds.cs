using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSounds : MonoBehaviour
{
    [SerializeField] AudioClip walkSound;
    [SerializeField] float soundClipTimer = 0.75f;

    CharacterController character;

    float timer;
    bool playSound;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        timer = soundClipTimer;
    }

    private void Update()
    {
        timer -= Time.unscaledDeltaTime;
        playSound = false;

        if(timer <= 0)
        {
            playSound = true;
            timer = soundClipTimer;
        }

        if(character.velocity.magnitude > 1 && playSound)
        {
            AudioSource.PlayClipAtPoint(walkSound, transform.position);
        }
    }
}
