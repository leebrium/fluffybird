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
        }
        uiManager.SwitchMenu(currentMenu);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    public void PlayGame(){
        pipeSpawner.enabled = true;
        currentMenu = Menu.play;
        uiManager.SwitchMenu(currentMenu);
        scoreManager.ResetScore();
    }

    public void GameOver(){
        SoundManager.Instance.playDeath();
        Time.timeScale = 0;
        currentMenu = Menu.over;
        uiManager.SwitchMenu(currentMenu);
    }

    public void Replay(){
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
