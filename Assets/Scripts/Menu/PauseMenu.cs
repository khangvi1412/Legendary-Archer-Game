using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePause = false;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject aim;
    [SerializeField] private GameObject gameGuide;
    [SerializeField] private GameObject backgroundMusic;
    public GameObject player;
    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        backgroundMusic.GetComponent<AudioSource>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        background.SetActive(false);
        pauseMenu.SetActive(false);
        healthBar.SetActive(true);
        aim.SetActive(true);
        Time.timeScale = 1f;
        gamePause = false;
    }

    void PauseGame()
    {
        backgroundMusic.GetComponent<AudioSource>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        background.SetActive(true);
        pauseMenu.SetActive(true);
        healthBar.SetActive(false);
        aim.SetActive(false);
        Time.timeScale = 0f;
        gamePause = true;
    }

    public void Setting()
    {
         
        settingMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Back()
    {
         
        settingMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void OpenGuide()
    {
        pauseMenu.SetActive(false);
        gameGuide.SetActive(true);

    }

    public void CloseGuide()
    {
        pauseMenu.SetActive(true);
        gameGuide.SetActive(false);

    }
    public void Quit()
    {
        PlayerPrefs.SetInt("health", player.GetComponent<PlayerInformation>().health);
        PlayerPrefs.SetInt("damage", player.GetComponent<PlayerInformation>().damage);
        PlayerPrefs.SetInt("maxHealth", player.GetComponent<PlayerInformation>().maxHealth);
        PlayerPrefs.SetFloat("speed", player.GetComponent<PlayerController>().playerSpeed);
        PlayerPrefs.SetFloat("attackSpeed", player.GetComponent<PlayerController>().reloadTime);
        PlayerPrefs.SetInt("sceneIndex",SceneManager.GetActiveScene().buildIndex);
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        gamePause = false;
    }

    public void Home()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}
