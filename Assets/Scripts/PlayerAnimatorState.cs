using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorState : MonoBehaviour
{
    Animator _anim;
    private string currentState;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();    
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        _anim.Play(newState);
        currentState = newState;
    }
}
