using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float boomerangSpeed;
    private float x;

    private bool camingBack;
    public bool backToPlayer;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        backToPlayer = false;
        camingBack = false;
        boomerangSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().boomerangSpeed;

        x = GameObject.FindGameObjectWithTag("Player").transform.position.x;
    }

    private void Update()
    {
        if (!backToPlayer)
        {
            if (!camingBack && transform.position.x > (x + 5))
            {
                camingBack = true;
                rb2d.AddForce(Vector2.left * boomerangSpeed * 2);
            }
        }
        else
        {
            rb2d.MovePosition(GameObject.FindGameObjectWithTag("Player").transform.position * Time.deltaTime);
        }
    }
}
