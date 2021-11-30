using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCleanerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().MakeDead();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("StageForest");
    }

    public void StopGame()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
