using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public GameObject levelSelectPlane;
    Button[] levelSelectButtons;
    int unlockLevelIndex;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        unlockLevelIndex = PlayerPrefs.GetInt("unlockLevelIndex") + 1;
        levelSelectButtons = new Button[levelSelectPlane.transform.childCount];
        for (int i = 0; i < levelSelectPlane.transform.childCount; i++)
        {
            levelSelectButtons[i] = levelSelectPlane.transform.GetChild(i).GetComponent<Button>();
        }

        for (int i = 0; i < levelSelectPlane.transform.childCount; i++)
        {
            levelSelectButtons[i].interactable = false;
        }

        for (int i = 0; i < unlockLevelIndex; i++)
        {
            levelSelectButtons[i].interactable = true;
        }
    }

    void Update()
    {
        
    }

    public void SelectLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void BackMainInterface()
    {
        SceneManager.LoadScene("Menu");
    }
}
