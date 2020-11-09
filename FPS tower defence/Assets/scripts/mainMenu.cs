using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject controls;
    public GameObject credits;
    public GameObject levels;
    
    public void showControls(bool show)
    {
        controls.SetActive(show);
        gameObject.SetActive(!show);
    }
    public void loadMap(int lvlNo)
    {
        SceneManager.LoadScene(lvlNo);
    }
    public void quit()
    {
        Application.Quit();
    }
    public void Credits(bool show)
    {
        credits.SetActive(show);
        gameObject.SetActive(!show);
    }
    public void showLevels(bool show)
    {
        levels.SetActive(show);
    }
}
