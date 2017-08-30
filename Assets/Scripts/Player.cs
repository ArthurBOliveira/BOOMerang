using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump;
    public float boomerangSpeed;

    public GameObject boomerangObj;
    public GameObject boomerangSpawner;

    private Rigidbody2D rb2d;

    public bool grounded;
    public bool boomerang;

    private void Start()
    {
        boomerang = true;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Move - Non Accelarion
        rb2d.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb2d.velocity.y);

        //Jump
        if (grounded && Input.GetKeyDown(KeyCode.UpArrow))
            rb2d.AddForce(Vector2.up * jump);

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Boomerang")
        {
            boomerang = true;
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
        }
    }

    private void ThrowBoomerang()
    {
        boomerang = false;

        GameObject obj = Instantiate(boomerangObj, boomerangSpawner.transform.position, Quaternion.identity);

        obj.GetComponent<Rigidbody2D>().AddForce(Vector2.right * boomerangSpeed);
    }

    private void CallBoomerang()
    {
        try { GameObject.FindGameObjectWithTag("Boomerang").GetComponent<Boomerang>().backToPlayer = true; } catch (Exception e) { }
    }

    private void TeleportBoomerang()
    {
        transform.position = GameObject.FindGameObjectWithTag("Boomerang").transform.position;
    }
}
