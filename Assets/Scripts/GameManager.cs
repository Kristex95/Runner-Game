using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeScale;
    public GameState gameState { get; private set; }

    public UnityEvent OnGameStateChange;

    //UI
    [Header("UI")]
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private GameObject preGameUI;

    private CanvasGroup endGameCanvasGroup;

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(endGameUI);
            DontDestroyOnLoad(preGameUI);
        }

        endGameCanvasGroup = endGameUI.GetComponent<CanvasGroup>();

        SetGameState(GameState.PreStart);
    }

    private void Update()
    {
        if(gameState == GameState.PreStart)
        {
            if(Input.GetMouseButtonDown(0))
            {
                SetGameState(GameState.Playing);
            }
        }

        if(endGameUI.activeSelf == true && endGameCanvasGroup.alpha != 1)
        {
            endGameCanvasGroup.alpha += Time.deltaTime;
        }
    }

    private IEnumerator LoadAsyncScene(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);


        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SetGameState(GameState.PreStart);
    }

    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadAsyncScene(currentSceneIndex));
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;

        if(gameState == GameState.PreStart)
        {
            HideEndUI();
            ShowPreGameUI();
            Time.timeScale = 0f;
        }
        else if(gameState == GameState.Playing)
        {
            HidePreGameUI();
            Time.timeScale = timeScale;
        }
        else if(gameState == GameState.GameOver)
        {
            ShowEndUI();
        }

        OnGameStateChange?.Invoke();
    }

    public void ShowEndUI()
    {
        endGameUI.SetActive(true);
    }
    
    public void HideEndUI()
    {
        endGameUI.SetActive(false);
        endGameCanvasGroup.alpha = 0;
    }
    
    public void ShowPreGameUI()
    {
        preGameUI.SetActive(true);
    }
    
    public void HidePreGameUI()
    {
        preGameUI.SetActive(false);
    }
}
