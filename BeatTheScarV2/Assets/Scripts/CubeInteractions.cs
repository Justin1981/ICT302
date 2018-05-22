using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInterations : MonoBehaviour, IFocusable {

    public Color NormalColor;
    public Color HighlightColor;
    private Renderer myRenderer;
    private Material myMaterialInstance;



    private void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
        myMaterialInstance = myRenderer.material;
    }

    public void OnFocusEnter()
    {
        myMaterialInstance.color = HighlightColor;
    }

    public void OnFocusExit()
    {
        myMaterialInstance.color = NormalColor;
    }

    private void OnDestroy()
    {
        Destroy(myMaterialInstance);
    }
}
