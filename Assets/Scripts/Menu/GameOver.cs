using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    private void OnEnable()
    {
        CharacterController.onPlayerDead += EnableGameOverScreen;
    }
    private void OnDisable()
    {
        CharacterController.onPlayerDead -= EnableGameOverScreen;
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }

    private void EnableGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }
}
