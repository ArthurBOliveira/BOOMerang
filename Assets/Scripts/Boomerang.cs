using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float boomerangSpeed;
    private float x;
    private float y;

    private bool camingBack;
    public bool backToPlayer;

    private Rigidbody2D rb2d;
    private GameObject player;
    private string direction;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        direction = player.GetComponent<Player>().direction;

        backToPlayer = false;
        camingBack = false;
        boomerangSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().boomerangSpeed;

        x = player.transform.position.x;
    }

    private void Update()
    {
        if (!backToPlayer)
        {
            switch (direction)
            {
                case "right":
                    if (!camingBack && transform.position.x > (x + 6))
                    {
                        camingBack = true;
                        Vector2 force = new Vector2();

                        force = Vector2.left * boomerangSpeed;

                        rb2d.AddForce(force * 2);
                    }
                    break;
                case "left":
                    if (!camingBack && transform.position.x < (x - 6))
                    {
                        camingBack = true;
                        Vector2 force = new Vector2();

                        force = Vector2.right * boomerangSpeed;

                        rb2d.AddForce(force * 2);
                    }
                    break;                
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 0.5f);
        }
    }
}
