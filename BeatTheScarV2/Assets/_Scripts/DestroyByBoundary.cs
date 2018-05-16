using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyByBoundary : MonoBehaviour
{
    public Text destroyText;
    private int count = 0;
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
        count++;
        destroyText.text = "destroy Count: " + count.ToString();
    }
}
