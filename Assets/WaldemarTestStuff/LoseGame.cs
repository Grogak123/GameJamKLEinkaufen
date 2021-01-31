using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseGame : MonoBehaviour
{

    public GameObject GameEndCanvas;

    private void Update()
    {
        if(gameObject.activeSelf == true)
        {
            GameEndCanvas.SetActive(true);
        }

        
    }

}
