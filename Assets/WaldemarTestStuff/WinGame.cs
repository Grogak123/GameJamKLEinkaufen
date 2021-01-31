using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{

    public GameObject GameEndCanvas;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Mother")
        {
            Debug.Log("Test");
            GameEndCanvas.SetActive(true);
        }
    }


}
