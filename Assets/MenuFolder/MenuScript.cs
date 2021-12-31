using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject playController;
    public GameObject exitController;
    public GameObject optionController;
    public GameObject backController;
    public GameObject sliderController;

    private void Start()
    {
        backController.SetActive(false);
        sliderController.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Option()
    {
        //active.Self controlla lo stato di questo oggetto
        if (playController.activeSelf && exitController.activeSelf) 
        {
            playController.SetActive(false);
            exitController.SetActive(false);
            optionController.SetActive(false);
            backController.SetActive(true);
            sliderController.SetActive(true);
        }
    }

    public void Back()
    {
        if(!playController.activeSelf && !exitController.activeSelf && !optionController.activeSelf)
        {
            playController.SetActive(true);
            exitController.SetActive(true);
            optionController.SetActive(true);
            sliderController.SetActive(false);
            backController.SetActive(false);
        }
    }

    /*public void Slide()
    {

    }*/

    public void Exit()
    {
        Application.Quit();

        //UnityEditor.EditorApplication.isPlaying = false; (per verificare che, in fase di test, funzioni)
    }
}
