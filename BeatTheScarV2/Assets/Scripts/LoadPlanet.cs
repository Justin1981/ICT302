using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPlanet : MonoBehaviour {

    //public Object scene;

    //public string SceneName;

	public void OnClick()
    {
        //SceneManager.LoadScene(scene.name);
        //Destroy(SceneManager.)
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Planet1"));

        //if(SceneName != null)
        //{
            SceneManager.LoadScene("Planet1");
        //}
    }
}
