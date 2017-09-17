using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : IInteractable
{
    private Animator animator;
    private BoxCollider2D coll2D;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        coll2D = GetComponent<BoxCollider2D>();

        coll2D.enabled = false;
    }

    public override void Activate()
    {
        animator.SetTrigger("Open");
        coll2D.enabled = true;
    }

    public override void Deactivate()
    {
        animator.SetTrigger("Close");
        coll2D.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameMenu>().Win();
    }
}
