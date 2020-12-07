using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_manager : MonoBehaviour
{
    GameObject player;

    [SerializeField] GameObject FimScreen;

    [Space]
    //tempo do jogo
    [SerializeField] Text tempo;
    float time;
    [SerializeField]float totalTime;

    [Space]
    [SerializeField] Slider Bubble_bar;

    [Space]
    //HP do player
    [SerializeField] Slider HP_Bar;
    [SerializeField] Text pointText;
    [SerializeField] int points_To_Win;

    [Space]
    //spawn de itens
    [Tooltip("mínimo valor aleatório para o spawn")] public float MinSpawnRate = 5;
    [Tooltip("máximo valor aleatório para o spawn")] public float MaxSpawnRate = 10;
    [SerializeField] Transform spawn_spots;
    [SerializeField] GameObject[] items;

    [Space]
    [SerializeField] int MaxEnemies;
    [Tooltip("mínimo valor aleatório para o spawn")] public float MinEnemySpawnRate = 7;
    [Tooltip("máximo valor aleatório para o spawn")] public float MaxEnemySpawnRate = 14;
    [SerializeField] Transform EnemySpawn_spots;
    [SerializeField] GameObject[] Enemies;

    private void Start()
    {
        Time.timeScale = 1;
        player = GameObject.FindGameObjectWithTag("Player");

        Bubble_bar.maxValue = player.GetComponent<Shoot_bubble>().MaxCharges;
        Bubble_bar.value = player.GetComponent<Shoot_bubble>().Bubble_charge;

        HP_Bar.maxValue = player.GetComponent<Player_Script>().MaxHp;
        HP_Bar.value = player.GetComponent<Player_Script>().HP;

        time = 0;
        StartCoroutine(item_Spawner());
        StartCoroutine(Enemy_Spawner());
    }

    private void Update()
    {
        if (time >= totalTime) return;

        Bubble_bar.value = player.GetComponent<Shoot_bubble>().Bubble_charge;
        
        HP_Bar.value = player.GetComponent<Player_Script>().HP;

        pointText.text = player.GetComponent<Player_Script>().pontuação + "/" + points_To_Win;

        if(player.GetComponent<Player_Script>().pontuação >= points_To_Win)
        {
            GameOver(true);
        }

        time += Time.deltaTime;
        tempo.text = Mathf.FloorToInt((totalTime - time) / 60).ToString("D2") + ":" + Mathf.FloorToInt((totalTime - time) % 60).ToString("D2");
        if(time >= totalTime)
        {
            GameOver(false);
            tempo.text = "Acabou o tempo";
        }
    }

    IEnumerator item_Spawner()
    {
        do
        {
            bool All_Taken = true;
            for (int i = 0; i < spawn_spots.childCount; i++)
            {
                if(spawn_spots.GetChild(i).childCount == 0)
                {
                    All_Taken = false;
                    break;
                }
            }

            if (!All_Taken)
            {

                int r;
                do
                {
                    r = Random.Range(0, spawn_spots.childCount);
                } while (spawn_spots.GetChild(r).childCount > 0);

                yield return new WaitForSeconds(Random.Range(MinSpawnRate, MaxSpawnRate));

                Instantiate(items[Random.Range(0, items.Length)], spawn_spots.GetChild(r));
            }
            else
            {
                yield return new WaitForSeconds(1);
            }

        } while (time < totalTime);
    }

    IEnumerator Enemy_Spawner()
    {
        int rand = Random.Range(1, EnemySpawn_spots.childCount);
        Instantiate(Enemies[Random.Range(0, Enemies.Length)], EnemySpawn_spots.GetChild(rand)).transform.parent.DetachChildren();
        do
        {
            bool All_Taken = FindObjectsOfType<Enemy>().Length >= MaxEnemies;

            if (!All_Taken)
            {

                int r;
                do
                {
                    r = Random.Range(0, EnemySpawn_spots.childCount);
                } while (EnemySpawn_spots.GetChild(r).childCount > 0);

                yield return new WaitForSeconds(Random.Range(MinEnemySpawnRate, MaxEnemySpawnRate));

                Instantiate(Enemies[Random.Range(0, Enemies.Length)], EnemySpawn_spots.GetChild(r)).transform.parent.DetachChildren();
            }
            else
            {
                yield return new WaitForSeconds(1);
            }

        } while (time < totalTime);
    }

    public void GameOver(bool win)
    {
        Time.timeScale = 0;
        ///FimScreen.transform.GetChild(0).GetComponent<Text>().text += ": " + msg;
        GetComponent<AudioSource>().Stop();

        if (win)
        {
            FimScreen.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            FimScreen.transform.GetChild(0).gameObject.SetActive(true);
        }
        
    }
}
