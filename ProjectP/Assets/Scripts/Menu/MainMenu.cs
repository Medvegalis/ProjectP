using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void NewGame()
    {   
        // Creates a new game
        DataPersistenceManager.instance.NewGame();
        
        SceneManager.LoadSceneAsync(1);
    }

    // Loads the save file
    public void LoadGame()
    {
        DataPersistenceManager.instance.SaveGame();
        
        SceneManager.LoadSceneAsync(1);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Debug.Log("EXIT GAME");
        Application.Quit();
    }
}
