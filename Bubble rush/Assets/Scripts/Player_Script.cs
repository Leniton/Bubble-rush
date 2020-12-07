using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    public int HP;
    public int MaxHp = 5;

    public int pontuação = 0;

    void Start()
    {
        HP = MaxHp;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Finish")
        {
            HP -= 1;
            Destroy(c.gameObject);
        }
        else if(c.tag == "Point")
        {
            pontuação++;
            Destroy(c.gameObject);
        }

        if (HP <= 0)
        {
            //ded
            FindObjectOfType<Game_manager>().GameOver(false);
        }
    }
}
