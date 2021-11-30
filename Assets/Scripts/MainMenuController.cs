using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void OnClick_NewGame()
    {
        Debug.Log("NewGame clicked!");
        SceneManager.LoadSceneAsync("StageForest");
    }
    public void OnClick_Credits()
    {
        SceneManager.LoadSceneAsync("CreditsScreen");
        Debug.Log("Credits clicked!");
    }
    public void OnClick_Exit()
    {
        Debug.Log("Exit clicked!");
        Application.Quit();

#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void OnClick_CreditsExit()
    {
        Debug.Log("Exiting credits!");
        SceneManager.LoadSceneAsync("MainScreen");
    }


    public void OnHover_Credits()
    {
        Debug.Log("Thank you sooooo much!!");
    }
}
