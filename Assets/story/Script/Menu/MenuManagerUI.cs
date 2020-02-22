using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManagerUI : MonoBehaviour
{
    public GameObject VolumeOn;
    public GameObject VolumeOff;
    public GameObject MainMenuImage;
    public GameObject SettingMenuImage;
    public GameObject MainMenuUI;
    public GameObject SettingMenuUI;

    public void Start()
    {
        SettingMenuUI.SetActive(false);
        SettingMenuImage.SetActive(false);
        MainMenuImage.SetActive(true);
        MainMenuUI.SetActive(true);
    }
    public void Play()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("Story");
    }
    public void Setting()
    {
        Debug.Log("Setting");
        SettingMenuUI.SetActive(true);
        SettingMenuImage.SetActive(true);
        MainMenuImage.SetActive(false);
        MainMenuUI.SetActive(false);
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void BackButtonMain()
    {
        SettingMenuUI.SetActive(false);
        SettingMenuImage.SetActive(false);
        MainMenuImage.SetActive(true);
        MainMenuUI.SetActive(true);
    }
    public void VolumeOnClick()
    {
        VolumeOff.SetActive(true);
        VolumeOn.SetActive(false);
    }
    public void VolumeOffClick()
    {
        VolumeOn.SetActive(true);
        VolumeOff.SetActive(false);
    }

}
