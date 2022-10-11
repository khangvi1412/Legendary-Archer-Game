using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControll : MonoBehaviour
{
    [SerializeField] private AudioSource selectSound;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject gameGuide;
    [SerializeField] private GameObject nameText;
    [SerializeField] private GameObject continueBtn;

    public void Start()
    {
        int indexScene = PlayerPrefs.GetInt("sceneIndex", 1);
        if (indexScene == 1)
        {
            continueBtn.SetActive(false);
        }
        else
        {
            continueBtn.SetActive(true);
        }
    }
    public void PLayGame()
    {
        LoadGame();
    }
    public void QuitGame()
    {
        Quit();
    }

    public void Option()
    {
        OpenOption();
    }
        
    public void Back()
    {
        CloseOption();
    }
        
    public void BackGuide()
    {
        CloseGuide();
    }
        
    public void Guide()
    {
        OpenGuide();
    }

    private void PlaySelect()
    {
        selectSound.Play();
    }
    private void No() { }
    private void LoadGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    private void Quit()
    {
        Application.Quit();
    }
    public void Continue()
    {   
        Time.timeScale = 1f;
        int indexScene = PlayerPrefs.GetInt("sceneIndex",1);
        Debug.Log("Scene index: " + indexScene);
        SceneManager.LoadScene("GamePlay_"+indexScene.ToString());
    }


    public void OpenOption()
    {
        mainMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    public void CloseOption()
    {
        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
    }

    public void OpenGuide()
    {
        mainMenu.SetActive(false);
        nameText.SetActive(false);
        gameGuide.SetActive(true);
        
    }

    public void CloseGuide()
    {
        mainMenu.SetActive(true);
        nameText.SetActive(true);
        gameGuide.SetActive(false);
        
    }

    public void PlaySound()
    {
        selectSound.Play();
    }


}
