using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Plataform_Movement))]
public class Enemy_AI : MonoBehaviour
{
    GameObject player;
    Plataform_Movement mov;

    [SerializeField] int side;
    [SerializeField] float force = 5;
    float delay;

    //shooting stuff
    [SerializeField] GameObject bullet;
    [SerializeField] float fireDelay;
    float rate;

    bool canDrop_Jump = true;

    void Start()
    {
        mov = GetComponent<Plataform_Movement>();
        player = GameObject.FindGameObjectWithTag("Player");

        mov.right = true;
        side = -1;
    }

    //-3
    void Update()
    {
        if (!GetComponent<Rigidbody2D>().simulated) return;

        RaycastHit2D hit;
        LayerMask mask = LayerMask.GetMask("surfaces");
        hit = Physics2D.Raycast(transform.position, new Vector2(0.2f * side * -1, -1), 2, mask);
        Debug.DrawRay(transform.position, new Vector2(0.2f * side * -1, -1) * 2, Color.red);
        //print(hit.collider);
        if (hit.collider == null && delay == 0)
        {
            side *= -1;
            delay = 0.5f;
        }
        else
        {
            if (delay > 0) 
            { 
                delay -= Time.deltaTime; 
            }else if(delay < 0)
            {
                delay = 0;
            }
        }

        //olhos nas costas agora
        mask = LayerMask.GetMask("Player");
        hit = Physics2D.Raycast(transform.position, Vector2.left * side, 12, mask);
        Debug.DrawRay(transform.position, Vector2.left * side * 12, Color.red);
        if (hit.collider != null && rate == 0)
        {
            //print(hit.collider);
            GameObject shot = Instantiate(bullet, transform.position, Quaternion.identity);
            shot.GetComponent<Rigidbody2D>().AddForce(Vector2.left * side * force, ForceMode2D.Impulse);
            Destroy(shot, 5);

            rate = fireDelay;
        }
        else if (hit.collider == null && rate == 0)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right * side, 12, mask);
            Debug.DrawRay(transform.position, Vector2.right * side * 12, Color.yellow);

            if (hit.collider != null)
            {
                /*transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
                //print(hit.collider);
                GameObject shot = Instantiate(bullet, transform.position, Quaternion.identity);
                shot.GetComponent<Rigidbody2D>().AddForce(Vector2.right * side * force, ForceMode2D.Impulse);
                Destroy(shot, 5);

                rate = fireDelay;
                side *= -1;*/
            }
        }
        else
        {
            if (rate > 0)
            {
                rate -= Time.deltaTime;
            }
            if (rate < 0)
            {
                rate = 0;
            }
        }

        if (mov.nochao)
        {
            if (side == -1)
            {
                mov.right = true;
                mov.left = false;
            }
            else
            {
                mov.left = true;
                mov.right = false;
            }

            //pular ou descer da plataforma para chegar no player
            if ((player.transform.position.y < transform.position.y || player.transform.position.y > transform.position.y) && canDrop_Jump)
            {
                canDrop_Jump = false;
                StartCoroutine(DropOrJump());
            }
        }
    }

    IEnumerator DropOrJump()
    {
        yield return new WaitForSeconds(1.5f);
        if (Random.Range(0, 1) <= 0.1f)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (player.transform.position.y - transform.position.y < -1 && mov.chaoPisado.GetComponent<PlatformEffector2D>() != null)
            {
                gameObject.layer = 1;
                yield return new WaitForSeconds(0.5f);
                gameObject.layer = 10;
            }
            else if (player.transform.position.y - transform.position.y > 1)
            {
                mov.jump = true;
            }
        }
        yield return new WaitForSeconds(10);

        canDrop_Jump = true;
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if(c.GetContact(0).normal.x != 0 &&  c.gameObject.tag != "Chão")
        {
            //print("bateu na parede: " + c.collider);
            if (c.GetContact(0).normal.x == side)
            {
                side *= -1;
                //delay = 0.5f;
            }
        }
    }
}
