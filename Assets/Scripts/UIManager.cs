using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject pauseMenuUI;
    public GameObject aboutMenuUI;
    public GameObject optionsMenuUI;
    public GameObject howToPlayUI;

    public bool isGamePaused = false;

    void Start()
    {
        ShowMainMenu();
    }

    void Update()
    {

    }

    public void ShowMainMenu()
    {
        Time.timeScale = 0f;
        mainMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        aboutMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        howToPlayUI.SetActive(false);

    }

    public void StartGame()
    {
        mainMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenAboutMenu()
    {
        mainMenuUI.SetActive(false);
        aboutMenuUI.SetActive(true);
    }

    public void OpenOptionsMenu()
    {
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void ShowPauseMenu()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 0f;
        ShowMainMenu();
    }

    public void OpenHowToPlay()
    {
        pauseMenuUI.SetActive(false);
        howToPlayUI.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        howToPlayUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

}
