using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Script : MonoBehaviour
{

    public void loadSceme(int sceneIndex)
    {
        Scene_Manager.manager.LoadScene(sceneIndex);
    }
	
	public void Quit()
	{
		Application.Quit();
	}
	
	public void DelayedLoadScene(int sceneIndex)
	{
		StartCoroutine(Delayed(sceneIndex));
	}
	
	IEnumerator Delayed(int n)
    {
        yield return new WaitForSeconds(0.5f);
		Scene_Manager.manager.LoadScene(n);
	}
}
