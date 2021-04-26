using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator mainMenuAnimator;

    [SerializeField] List<Button> buttons;

    [SerializeField] Color[] textColors = new Color[3];
    [SerializeField] TextMeshProUGUI[] allText = new TextMeshProUGUI[11];

    private void Start()
    {
        GameManager.Instance.FadeOut.AddListener(EndScene);   
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            //FadeOut();
        }

        if(previousState != GameManager.GameState.PREGAME && currentState == GameManager.GameState.PREGAME)
        {
            //FadeIn();
        }
    }

    public void DisablebuttonsDuringAnimation()
    {
        foreach(Button button in buttons)
        {
            button.interactable = false;
        }
        bool optionsToggle = mainMenuAnimator.GetBool("isOptionsOpen");
        mainMenuAnimator.SetBool("isOptionsOpen", !optionsToggle);
        
    }

    void EnableButtons()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
    }

    public void ChangeFontColor(int i)
    {
        foreach(TextMeshProUGUI TMP in allText)
        {
            TMP.color = textColors[i];
        }
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
        mainMenuAnimator.SetBool("fadeMainMenu", true);

    }

    public void EndScene()
    {
        mainMenuAnimator.SetBool("fadeOut", true);
    }


}
