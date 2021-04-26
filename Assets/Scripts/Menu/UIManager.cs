using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] MainMenu mainMenu;
    [SerializeField] Camera mainCamera;
    [SerializeField] TextMeshProUGUI UISizeText;
    [SerializeField] TextMeshProUGUI musicVolumeText;
    [SerializeField] TextMeshProUGUI SFXVolumeText;

    //public Events.EventFadeComplete OnMainMenuFadeComplete;

    void Start()
    {
        //mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
        //GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);

    }

    private void Update()
    {
        //if(GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME)
        //{
            //return;
       // }
    }

    public void SetCameraActive(bool active)
    {
        mainCamera.gameObject.SetActive(active);
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        //pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        //OnMainMenuFadeComplete.Invoke(fadeOut);
    }

    public void onUIScaleChange(float value)
    {
        UISizeText.text = Mathf.Round((value -1) *100 +50) + "%";
    }

     public void MusicVolumeChanged(float value)
    {
        GameManager.Instance.MusicVolumeChanged.Invoke(value);
        musicVolumeText.text = Mathf.Round(value * 100) + "%";
    }
    public void SFXVolumeChanged(float value)
    {
        GameManager.Instance.SFXVolumeChanged.Invoke(value);
        SFXVolumeText.text = Mathf.Round(value * 100) + "%";
    }
}
