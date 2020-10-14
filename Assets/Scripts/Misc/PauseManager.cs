using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject fadeOutPanel;
    [SerializeField] GameObject firstSelectedButton;

    private bool isPaused;
    private SoundManager soundManager;

    private void Start()
    {
        isPaused = false;
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if(isPaused)
            {
                StartCoroutine(OpenPauseMenuCo());

            }
            else
            {
                soundManager.PlaySound("UI_Pause_Close");
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    IEnumerator OpenPauseMenuCo()
    {
        soundManager.PlaySound("UI_Pause_Open");
        pausePanel.SetActive(true);
        FindObjectOfType<EventSystem>().SetSelectedGameObject(null);
        yield return null;
        FindObjectOfType<EventSystem>().SetSelectedGameObject(firstSelectedButton);
        //FindObjectOfType<EventSystem>().SetSelectedGameObject(FindObjectOfType<EventSystem>().firstSelectedGameObject);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        soundManager.PlaySound("UI_Pause_Close");
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitToMainMenu()
    {
        FindObjectOfType<GameSaveManager>().SaveScriptables();
        StartCoroutine(LoadMainMenuCo());
    }

    IEnumerator LoadMainMenuCo()
    {
        Time.timeScale = 1f;
        // disable input while we wait for the menu to dissapear
        FindObjectOfType<EventSystem>().enabled = false;
        soundManager.PlaySound("UI_Pause_Select");
        Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
        
    }
}
