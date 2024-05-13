using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject crossHair;
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
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }

    private void EnableGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        crossHair.SetActive(false);
        Time.timeScale = 0f;
    }
}
