using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public static Transition Instance;
    private Animator animator;
    private string currentState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        animator = GetComponent<Animator>();
        animator.speed = 0f;
    }

    public void PlayTransition(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        animator.speed = 1f;

        currentState = newState;
    }
}
