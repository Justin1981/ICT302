using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

	public void OnClick()
    {
        SceneManager.LoadScene("Planet1");
        //Destroy(SceneManager.)
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Planet1"));
    }
}
