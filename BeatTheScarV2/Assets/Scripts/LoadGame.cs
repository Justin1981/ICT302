using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

    public Object scene;

    public string SceneName;

	public void OnClick()
    {
        //SceneManager.LoadScene(scene.name);
        //Destroy(SceneManager.)
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Planet1"));

        System.Diagnostics.Debug.WriteLine("Debugger working");

        if(SceneName != null)
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
