using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [Header("New Game Variables")]
    [SerializeField] Vector2Value playerPostion;
    [SerializeField] BoolValue startedGame;
    [SerializeField] Vector2 playerStartPosition;
    [SerializeField] Vector2 playerResumePosition;

    [Header("Animation Variables")]
    [SerializeField] GameObject fadeInPanel;
    [SerializeField] GameObject fadeOutPanel;
    [SerializeField] float fadeWait = 1f;

    private SoundManager soundManager;
    // TODO: we could do some wicked animations here

    private void Awake()
    {
        GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
        Destroy(panel, 1);
    }

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.PlayMusic("introSong");
    }

    public void ResetGame()
    {
        FindObjectOfType<GameSaveManager>().ResetScriptables();
    }

    public void PlayGame()
    {
        Vector2 loadPosition = startedGame.RuntimeValue ? playerResumePosition : playerStartPosition;
        startedGame.RuntimeValue = true;
        StartCoroutine(LoadSceneCo(loadPosition));
    }

    IEnumerator LoadSceneCo(Vector2 startPosition)
    {
        // disable input while we wait for the menu to dissapear
        FindObjectOfType<EventSystem>().enabled = false;
        Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        playerPostion.RuntimeValue = startPosition;
        yield return new WaitForSeconds(fadeWait);
        SceneManager.LoadScene("HouseInterior");
    }

    public void QuitGame()
    {
        Application.Quit();   
    }
}
