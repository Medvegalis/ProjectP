using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance { get; private set; }

    public GameObject pauseMenu;
    public GameObject SaveButton;
    public bool isPaused;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        if (SceneManager.GetActiveScene().name == "RoomContent")
        {
            SaveButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            SaveButton.GetComponent<Button>().interactable = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame() 
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame() 
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGameOther(GameObject menu)
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGameOther(GameObject menu)
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void SaveGame()
    {
        DataPersistenceManager.instance.SaveGame();
    }

    public void GoMainMenu() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        isPaused = false;
    }

    public void QuitGame() 
    {
        Application.Quit();    
    
    }

    //For pausing the game on the deathScreen
    public void SetTimeScaleToZero() 
    {
        Time.timeScale = 0f;    
    }


}
