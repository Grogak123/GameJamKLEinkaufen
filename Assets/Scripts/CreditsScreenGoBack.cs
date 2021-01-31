using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreenGoBack : MonoBehaviour
{


    public GameObject MainMenuUI;
    public GameObject CreditsUI;


    public void ActivateMainMenu()
    {
        MainMenuUI.SetActive(true);
        CreditsUI.SetActive(false);
    }

    public void ActivateCredits()
    {
        MainMenuUI.SetActive(false);
        CreditsUI.SetActive(true);
    }

}
