using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadLastLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel"));
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel") + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
