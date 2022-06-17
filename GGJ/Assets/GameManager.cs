using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject titleScreen;


    public void StartGame(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void OpenMenu(GameObject target)
    {
        target.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void CloseMenu(GameObject target)
    {
        target.SetActive(false);
        titleScreen.SetActive(true);
    }
}
