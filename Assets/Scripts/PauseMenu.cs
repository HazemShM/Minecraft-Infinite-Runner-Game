using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    private bool isPaused = false;

    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private GameObject gamePlayUI;
    private PlayerMovement playerMovement;

    [SerializeField]
    private Text finalScoreText;
    public void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        playerMovement.SetLost(false);
        AudioManager.manager.PlayMusic("Infinity And Beyond");

    }

    private void Update()
    {
        if (playerMovement.GameOver())
        {
            AudioManager.manager.PlayMusic("Main Theme");
            gamePlayUI.SetActive(false);
            finalScoreText.text = "" + playerMovement.GetScore();
            gameOverMenu.SetActive(true);

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

    }
    public void Pause()
    {
        AudioManager.manager.PlayMusic("Main Theme");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }
    public void Resume()
    {
        // print("in resume");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        AudioManager.manager.PlayMusic("Infinity And Beyond");
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        // playerMovement.SetLost(false);     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }


    public void OnButtonClick()
    {
        AudioManager.manager.PlaySFX("click");
    }

}
