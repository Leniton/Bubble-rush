using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_bubble : MonoBehaviour
{
    public int Bubble_charge = 5;
    public int MaxCharges = 5;
    public float force;
    public GameObject bubble;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && Bubble_charge > 0)
        {
            Rigidbody2D rb = Instantiate(bubble, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();

            if (!Input.GetKey(KeyCode.S))
            {
                rb.AddForce(Vector2.right * transform.localScale.x * force, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(((Vector2.right * transform.localScale.x) + Vector2.down*2) * force, ForceMode2D.Impulse);
            }

            Destroy(rb.gameObject, 3);
            Bubble_charge--;

            //audio
            if(GetComponent<AudioSource>() != null && Time.timeScale > 0)
            {
                GetComponent<AudioSource>().Play();
            }
            GetComponent<Animator>().SetTrigger("shoot");
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Bubble Charge")
        {
            if (Bubble_charge < MaxCharges)
            {
                Bubble_charge++;
                Destroy(c.gameObject);
            }
        }
    }
}
