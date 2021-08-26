using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    int currentScene;

    private void Start()
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UpdateLevelText();
    }
    void UpdateLevelText()
    {
        UIControl.Instance.SetLevelText(currentScene);
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
        Debug.Log("Restart");

    }
    public void NextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene );
    }
}
