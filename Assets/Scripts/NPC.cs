using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class NPC : MonoBehaviour
{
    //Animator
    private Animator animator;
    private const string RAISE = "RaiseHand";

    //Transition Eventhandling
    public static NPC Instance { get; private set; }
    public event EventHandler OnStartDialogue;

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        PlayerController.Instance.OnInteractNPC += Player_OnInteractNPC;
    }

    private void Player_OnInteractNPC(object sender, PlayerController.OnInteractNPCEventArgs e)
    {
        if (e.interactNPC)
        {
            animator.Play(RAISE);
            OnStartDialogue?.Invoke(this, EventArgs.Empty);
        }
        
        
    }

}
