using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform other_side;

    static bool open = true;
    static float delay = 0.3f;

    private void Awake()
    {
        if(other_side.GetComponent<Portal>().other_side == null)
        {
            other_side.GetComponent<Portal>().other_side = transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        //10 é a layer dos inimigos
        if((c.gameObject.tag == "Player" || c.gameObject.layer == 10) && open)
        {
            open = false;
            c.transform.position = other_side.position;

            if(c.gameObject.layer == 10 && other_side.position.y > transform.position.y)
            {
                //kill it
                Destroy(c.gameObject);
            }

            StartCoroutine(Countdown());
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(delay);
        open = true;
    }
}
