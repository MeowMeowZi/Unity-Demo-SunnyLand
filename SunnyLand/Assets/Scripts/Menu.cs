using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject UI;

    public void Awake()
    {
        UI.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelBar");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ActionUI()
    {
        UI.SetActive(true);
    }
}
