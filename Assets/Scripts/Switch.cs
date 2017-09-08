using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Sprite deactived;
    public Sprite activated;
    public int milliSeconds;

    public GameObject interactable;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boomerang")
        {
            ActionActivating();

            StartCoroutine(Hold());

            collision.GetComponent<Boomerang>().backToPlayer = true;
        }
    }

    private IEnumerator Hold()
    {
        yield return new WaitForSeconds(milliSeconds);

        ActionDeactivating();
    }

    private void ActionActivating()
    {
        sr.sprite = activated;

        interactable.GetComponent<IInteractable>().Activate();
    }

    private void ActionDeactivating()
    {
        sr.sprite = deactived;

        interactable.GetComponent<IInteractable>().Deactivate();
    }
}
