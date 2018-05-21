using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

    public Object scene;

	public void OnClick()
    {
        SceneManager.LoadScene(scene.name);
        //Destroy(SceneManager.)
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Planet1"));
    }
}
