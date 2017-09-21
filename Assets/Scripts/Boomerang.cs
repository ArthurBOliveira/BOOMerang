using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float boomerangSpeed;
    public float gravityScale;
    public float limit;
    private float playerX;

    private bool camingBack;
    public bool backToPlayer;
    public bool isMagic;
    public bool isTeleportable;

    private Rigidbody2D rb2d;
    private CircleCollider2D coll2D;
    private SpriteRenderer renderer;
    private GameObject player;
    private string direction;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coll2D = GetComponent<CircleCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

        direction = player.GetComponent<Player>().direction;

        backToPlayer = false;
        camingBack = false;
        isTeleportable = false;
        boomerangSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().boomerangSpeed;

        playerX = player.transform.position.x;
        rb2d.gravityScale = 0;
        coll2D.enabled = false;
        //renderer.color = new Color(125, 0, 255, 255); //Purple
        isMagic = true;
    }

    private void Update()
    {
        if (!backToPlayer)
        {
            switch (direction)
            {
                case "right":
                    if (!camingBack && transform.position.x > (playerX + limit))
                    {
                        isTeleportable = true;
                        camingBack = true;
                        Vector2 force = new Vector2();

                        force = Vector2.left * boomerangSpeed;

                        rb2d.AddForce(force * 2);
                    }
                    if (!backToPlayer && transform.position.x < playerX)
                    {
                        rb2d.gravityScale = gravityScale;
                        coll2D.enabled = true;
                        //renderer.color = new Color(100, 100, 100, 255); //Gray
                        isMagic = false;
                    }
                    break;
                case "left":
                    if (!camingBack && transform.position.x < (playerX - limit))
                    {
                        camingBack = true;
                        Vector2 force = new Vector2();

                        force = Vector2.right * boomerangSpeed;

                        rb2d.AddForce(force * 2);
                    }
                    if (!backToPlayer && transform.position.x > playerX)
                    {
                        rb2d.gravityScale = gravityScale;
                        coll2D.enabled = true;
                        //renderer.color = new Color(100, 100, 100, 255); //Gray
                        isMagic = false;
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
