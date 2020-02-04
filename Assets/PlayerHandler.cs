
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [HideInInspector] public PlayerController pc;
    [HideInInspector] public GameStateHandler gsh;

    public AudioSource playerAudio;

    private void Awake()
    {
        pc = GetComponent<PlayerController>();
        playerAudio = GetComponent<AudioSource>();
    }
}