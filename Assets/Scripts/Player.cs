using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump;
    public float boomerangSpeed;

    public GameObject boomerangObj;
    public GameObject boomerangSpawnerRight;
    public GameObject boomerangSpawnerLeft;

    public Vector3 position0;

    private Rigidbody2D rb2d;

    public bool grounded;
    public bool boomerang;

    private bool canDoubleJump;
    public string direction;

    public void ResetPosition()
    {
        transform.position = position0;

        GameObject.Destroy(GameObject.FindGameObjectWithTag("Boomerang"));

        boomerang = true;
    }

    private void Start()
    {
        direction = "right";
        boomerang = true;
        canDoubleJump = false;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Move - Non Accelarion
        rb2d.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb2d.velocity.y);

        //Jump
        if ((grounded || canDoubleJump) && Input.GetKeyDown(KeyCode.UpArrow))
        {
            canDoubleJump = false;
            rb2d.AddForce(Vector2.up * jump);
        }

        //Boomerang
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Thrown
            if (boomerang)
                ThrowBoomerang();
            //Teleport
            else
                TeleportBoomerang();
        }

        //Call Boomerang Back
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            CallBoomerang();
        }

        //Set up Direction
        if (Input.GetKeyDown(KeyCode.RightArrow))
            direction = "right";
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            direction = "left";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boomerang")
        {
            boomerang = true;
            //canDoubleJump = true;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collider)
    {
        GroundCheck();
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        grounded = false;
    }

    private void GroundCheck()
    {
        RaycastHit2D[] hits;

        //We raycast down 1 pixel from this position to check for a collider
        Vector2 positionToCheck = transform.position;
        hits = Physics2D.RaycastAll(positionToCheck, new Vector2(0, -1), 0.01f);

        //if a collider was hit, we are grounded
        if (hits.Length > 0)
        {
            grounded = true;
            canDoubleJump = false;
        }
    }

    private void ThrowBoomerang()
    {
        boomerang = false;
        Vector2 force = new Vector2();

        GameObject obj;

        switch (direction)
        {
            case "right":
                force = Vector2.right * boomerangSpeed;
                obj = Instantiate(boomerangObj, boomerangSpawnerRight.transform.position, Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().AddForce(force);
                break;
            case "left":
                force = Vector2.left * boomerangSpeed;
                obj = Instantiate(boomerangObj, boomerangSpawnerLeft.transform.position, Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().AddForce(force);
                break;
        }        
    }

    private void CallBoomerang()
    {
        try
        {
            var boomerang = GameObject.FindGameObjectWithTag("Boomerang").GetComponent<Boomerang>();

            if(boomerang.isMagic)
                boomerang.backToPlayer = true;
        }
        catch (Exception e) { }
    }

    private void TeleportBoomerang()
    {
        var boomerang = GameObject.FindGameObjectWithTag("Boomerang").GetComponent<Boomerang>();

        if (boomerang.isMagic && boomerang.isTeleportable)
            transform.position = GameObject.FindGameObjectWithTag("Boomerang").transform.position;
    }
}
