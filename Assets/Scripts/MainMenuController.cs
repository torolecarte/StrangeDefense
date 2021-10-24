using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    public void OnClick_NewGame()
    {
        Debug.Log("NewGame clicked!");
    }
    public void OnClick_Credits()
    {
        Debug.Log("Credits clicked!");
    }
    public void OnClick_Exit()
    {
        Debug.Log("Exit clicked!");
    }


    public void OnHover_Credits()
    {
        Debug.Log("Thank you sooooo much!!");
    }
}
