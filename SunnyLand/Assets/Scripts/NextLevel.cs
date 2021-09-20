using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (SceneManager.GetActiveScene().buildIndex - 2 >= PlayerPrefs.GetInt("unlockLevelIndex"))
            {
                PlayerPrefs.SetInt("unlockLevelIndex", SceneManager.GetActiveScene().buildIndex + 1 - 2);
            }
            SceneManager.LoadScene("LevelBar");
        }
    }
}
