using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;



public class GameManager : Singleton<GameManager>
{
    //black screne
    //game manager load menu scene
    //display menu


    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }

    public Events.EventGameState OnGameStateChanged;
    public Events.AuidoVolumeChanged MusicVolumeChanged;
    public Events.AuidoVolumeChanged SFXVolumeChanged;
    public UnityEvent StartGameEvent;
    public UnityEvent FadeOut;

    public bool queryLoadMain = false;

    //what other persistant systems can the game manager create. Apply in inspector
    public GameObject[] systemPrefabs;

    List<GameObject> instancedSystemPrefabs;

    // what scene is the game currently on
    private string currentScene = string.Empty;

    //used for keeping track of loading operations... not sure when you would need.
    List<AsyncOperation> loadOperations;

    GameState currentGameState = GameState.PREGAME;

    public GameState CurrentGameState
    {
        get { return currentGameState; }
        private set { currentGameState = value; }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        loadOperations = new List<AsyncOperation>();
        instancedSystemPrefabs = new List<GameObject>();

        //InstantiateSystemPrefabs();

        LoadScene("MainMenu");
        

        //UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

    private void Update()
    {
        if (currentGameState == GameState.PREGAME)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);

            if(loadOperations.Count == 0)
            {
                UpdateState(GameState.RUNNING);
            }
        }
        Debug.Log("Load Complete.");
        UnloadScene("Boot");
        InstantiateSystemPrefabs();
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete.");
    }

    void UpdateState(GameState state)
    {
        GameState previousGameState = currentGameState;
        currentGameState = state;

        switch (currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                //insert code
                break;
            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                //insert code
                break;
            case GameState.PAUSED:
                //insert code
                Time.timeScale = 0.0f;
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke(currentGameState, previousGameState);
    }

    private void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        foreach (GameObject go in systemPrefabs)
        {
            prefabInstance = Instantiate(go);
            instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    public void LoadScene(string sceneName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load scene " + sceneName);
            return;
        }
        ao.completed += OnLoadOperationComplete;
        loadOperations.Add(ao);
        currentScene = sceneName;
    }

    public void UnloadScene(string sceneName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(sceneName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to unload scene " + sceneName);
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }

    //pass on the destroy call to managed instances. clear list to clear memory
    protected override void OnDestroy()
    {
        base.OnDestroy();

        foreach (GameObject go in instancedSystemPrefabs)
        {
            Destroy(go);
        }
        instancedSystemPrefabs.Clear();
    }

    public void StartGame()
    {
        StartGameEvent.Invoke();

            //LoadScene("Main");
            
    }

    public void TogglePause()
    {
        //Ternary Operator. Single line if/else statement: condition ? true : false
        UpdateState(currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }

    public void RestartGame()
    {
        UpdateState(GameState.PREGAME);
    }

    public void QuitGame()
    {
        // implement features for quitting
        //UImangager fade out
        Application.Quit();
    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        if (!fadeOut)
        {
            UnloadScene(currentScene);
        }
    }
}
