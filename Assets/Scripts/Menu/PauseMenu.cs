using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(HandleResumeClicked);
        restartButton.onClick.AddListener(HandleRestartClicked);
        quitButton.onClick.AddListener(HandleQuitClicked);
    }

    void HandleResumeClicked()
    {
        GameManager.Instance.TogglePause();
    }
    void HandleRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }
    void HandleQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }
}
