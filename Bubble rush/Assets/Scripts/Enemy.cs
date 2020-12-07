using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool naBolha;
    Rigidbody2D rb;
    public float popTime = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        //9 é a layer da bolha | 10 a layer do inimigo
        if (c.collider.gameObject.layer == 9 && !naBolha)
        {
            GameObject bubble = Instantiate(c.gameObject, transform.position, Quaternion.identity);
            Destroy(c.collider.gameObject);

            GetComponent<Collider2D>().enabled = false;
            rb.simulated = false;

            bubble.layer = 10;

            transform.SetParent(bubble.transform);
			
            naBolha = true;

            StartCoroutine(popBubble());
        }
    }

    IEnumerator popBubble()
    {
        yield return new WaitForSeconds(popTime);

        GameObject bubble = transform.parent.gameObject;

        bubble.transform.DetachChildren();
        Destroy(bubble);

        GetComponent<Collider2D>().enabled = true;
        rb.simulated = true;
        naBolha = false;
    }
}
