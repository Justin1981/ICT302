using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class LoadPlanet : MonoBehaviour {

    //public Object scene;

    public Text Debug;

	public void OnClick()
    {
        //SceneManager.LoadScene(scene.name);
        //Destroy(SceneManager.)
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Planet1"));

        //if(SceneName != null)
        //{

        Debug.text += "OnClick Called ";


        //SceneManager.LoadScene("Planet1");
        //}
    }
}
