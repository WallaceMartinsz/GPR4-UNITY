using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject credits;

    public void Jogar()
    {
        SceneManager.LoadScene("Game");
    }
    public void Credits()
    {
        painelMenuInicial.SetActive(false);
        credits.SetActive(true);
    }
    public void ExitCredits()
    {
        credits.SetActive(false);
        painelMenuInicial.SetActive(true);
    }
    public void Quit()
    {
        Debug.Log("sair do jogo");
        Application.Quit();
    }

    
}
