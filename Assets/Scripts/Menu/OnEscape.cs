using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnEscape : MonoBehaviour
{

    [SerializeField] GameObject escapeScreen;
    [SerializeField] GameObject crossHair;
    

    private void OnEnable()
    {
        ExitDoor.OnEscape += EnableEscapeScreen;
    }

    private void OnDisable()
    {
        ExitDoor.OnEscape -= EnableEscapeScreen;
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

    void EnableEscapeScreen()
    {
        escapeScreen.SetActive(true);
        crossHair.SetActive(false);
        Time.timeScale = 0f;

    }

}
