using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : IInteractable
{
    public bool opened;

    private Animator animator;

    private void Start()
    {
        opened = false;
        animator = GetComponent<Animator>();
    }

    public override void Activate()
    {
        animator.SetTrigger("Open");
    }

    public override void Deactivate()
    {
        animator.SetTrigger("Close");
    }
}
