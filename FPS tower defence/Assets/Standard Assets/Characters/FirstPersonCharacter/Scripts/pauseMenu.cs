using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class pauseMenu : MonoBehaviour
{
    public bool paused;
    public GameObject uiCanvas;
    private CanvasGroup menuCanvas;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController play;
    public GameObject controls;

    // Start is called before the first frame update
    void Start()
    {
        uiCanvas.SetActive(true);
        menuCanvas = GetComponent<CanvasGroup>();
        menuCanvas.interactable = false;
        menuCanvas.alpha = 0;
        menuCanvas.blocksRaycasts = false;
        play = FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (paused)
            {
                paused = false;
                menuCanvas.interactable = false;
                menuCanvas.alpha = 0;
                menuCanvas.blocksRaycasts = false;
                uiCanvas.SetActive(true);
                play.m_MouseLook.SetCursorLock(true);
                //Time.timeScale = 1;
            }
            else
            {
                paused = true;
                menuCanvas.interactable = true;
                menuCanvas.alpha = 1;
                menuCanvas.blocksRaycasts = true;
                uiCanvas.SetActive(false);
                play.m_MouseLook.SetCursorLock(false);
                //Time.timeScale = 0;
            }
        }
    }
    public void resume()
    {
        paused = false;
        menuCanvas.interactable = false;
        menuCanvas.alpha = 0;
        menuCanvas.blocksRaycasts = false;
        uiCanvas.SetActive(true);
        play.m_MouseLook.SetCursorLock(true);
    }

    public void showControls(bool show)
    {
        controls.SetActive(show);
        gameObject.SetActive(!show);
    }

    public void quitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
