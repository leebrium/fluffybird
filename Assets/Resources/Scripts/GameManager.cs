using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public GameSettings settings;
    public ScoreManager scoreManager;
    public PipeSpawner pipeSpawner;
    public Bird bird;
    public UIManager uiManager;
    public Menu currentMenu;
    public bool isFirstTime = true;
    public AdsManager adsManager;
    public bool isContinue;
    public bool hasContinued;
    public bool gameOver;

    //called zero
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // called first
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        // PlayerPrefs.DeleteAll();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        uiManager = FindObjectOfType<UIManager>();
        pipeSpawner = FindObjectOfType<PipeSpawner>();
        bird = FindObjectOfType<Bird>();

        if (isFirstTime){
            isFirstTime = false;
            currentMenu = Menu.open;
        } else {
            bird.setStartPosition();
            currentMenu = Menu.start;
            
            if(isContinue) {
                uiManager.panelGamePlay.SetActive(true);
                uiManager.AnimateContinueScreen();
            }
        }
        
        uiManager.SwitchMenu(currentMenu);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void PlayGame(){
        pipeSpawner.enabled = true;
        currentMenu = Menu.play;
        uiManager.SwitchMenu(currentMenu);

        if(isContinue) {
            isContinue = false;
            hasContinued = true;
        } else {
            hasContinued = false;
            scoreManager.ResetScore();
        }
    }

    public void GameOver(){
        gameOver = true;
        SoundManager.Instance.playDeath();
        currentMenu = Menu.over;
        uiManager.SwitchMenu(currentMenu);
    }

    public void Replay(){
        gameOver = false;
        SceneManager.LoadScene(0);
    }

    public void ContinueGame() {
        Debug.Log("Continue Game");
        isContinue = true;
        Replay();
    }
}
