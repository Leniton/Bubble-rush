using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Plataform_Movement))]
public class PlayerController : MonoBehaviour
{

    Plataform_Movement mov;
    Animator anim;

    void Start()
    {
        mov = GetComponent<Plataform_Movement>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        //quando pressionar
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            mov.jump = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            mov.left = true;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            mov.right = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Dropping());
        }

        //quando soltar o botão
        if (Input.GetKeyUp(KeyCode.A))
        {
            mov.left = false;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            mov.right = false;
        }

        //animações
        if(mov.estado == Plataform_Movement.Estado.andando)
        {
            anim.SetBool("Walking", true);
        }else if (mov.estado != Plataform_Movement.Estado.andando)
        {
            anim.SetBool("Walking", false);
        }
		if(mov.jump == true)
        anim.SetTrigger("Jump");
		
		anim.SetBool("Jumping", mov.estado == Plataform_Movement.Estado.pulando);
    }

    IEnumerator Dropping()
    {
        yield return new WaitForSeconds(0.2f);
        if (Input.GetKey(KeyCode.S) && mov.chaoPisado.GetComponent<PlatformEffector2D>() != null){
            gameObject.layer = 1;
            yield return new WaitForSeconds(0.8f);
            gameObject.layer = 11;
        }
    }
}
