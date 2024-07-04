using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsPanel;

    public Slider SensitivitySlider;
    //private void Start()
    //{
    //    GameObject gameObject = GameObject.Find("FinalDoor");
    //    PlayerMovement pm = gameObject.GetComponent<PlayerMovement>();
    //    SensitivitySlider.value = pm.LookSpeed;
    //}

    public void ChangeSens()
    {
        GameObject gameObject = GameObject.Find("FinalDoor");
        PlayerMovement pm = gameObject.GetComponent<PlayerMovement>();
        pm.LookSpeed = SensitivitySlider.value;
    }
        public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Settings()
    {
        settingsPanel.SetActive(true);
    }
    public void Close()
    {
        settingsPanel.SetActive(false);
    }
}
