using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeScale;
    private GameState gameState;

    //UI
    [Header("UI")]
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private GameObject preGameUI;

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
    }

    private IEnumerator LoadAsyncScene(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);


        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SetGameState(GameState.PreStart);
        Debug.Log("GameState set");
    }

    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadAsyncScene(currentSceneIndex));
        //StartCoroutine((currentSceneIndex) => { })
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
    }

    public void ShowEndUI()
    {
        endGameUI.SetActive(true);
    }
    
    public void HideEndUI()
    {
        endGameUI.SetActive(false);
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
