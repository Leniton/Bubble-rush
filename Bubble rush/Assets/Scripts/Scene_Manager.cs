using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager manager;

    private void Awake()
    {
        if(manager != this)
        {
            if(manager == null)
            {
                DontDestroyOnLoad(this);
                manager = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
