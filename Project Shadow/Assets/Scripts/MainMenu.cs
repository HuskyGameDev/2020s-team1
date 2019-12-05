using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private void Start()
    {
        if (AudioManager.instance.getCurrMusic() != "MenuBGM")
        {
            AudioManager.instance.Play("MenuBGM");
        }
        
    }
    public void StartNew()
    {
        SceneManager.LoadScene(1);
        AudioManager.instance.SwitchMusic("MenuBGM","Theme1");

    }

    public void ContinueGame()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel < 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else SceneManager.LoadScene(1);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVol", volume);
        Debug.Log("Music: " + volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sFXVol", volume);
        Debug.Log("SFX: " + volume);
    }
    public void FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void ToCredit()
    {
        SceneManager.LoadScene("End Credit");
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
