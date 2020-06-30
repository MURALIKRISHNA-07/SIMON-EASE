using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    
    [SerializeField] GameObject help;
    [SerializeField] GameObject Highscores;

    public enum UIState { Menu,Help,Highscores }

    public GameObject pause;
    public GameObject Game_Over;
    public GameObject mode;
   
    public Text Gameoverscore;

    public Text Highscore;
    public Text Score;

    public GameObject score;
    public GameObject Gamestart;
    public GameObject gamebutton;
    public GameObject pausebutton;

    public  GameManager manager;
    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if(!PlayerPrefs.HasKey("High"))
        {
            PlayerPrefs.SetInt("High", 0);
        }      
    }

        // Start is called before the first frame update
    void Start()
    {
        SetUIState(UIState.Menu);       
    }
    private void Update()
    {
        //Playing BGM
        if (!Gamestart.activeInHierarchy)
            AudioManager.instance.Bgm.UnPause();
        else
         AudioManager.instance.Bgm.Pause(); 

    }

    public void SetUIState(UIState uIState)
    {       
        switch (uIState)
        {
            case UIState.Menu:
                menu.SetActive(true);
                help.SetActive(false);
                Highscores.SetActive(false);
                break;

            case UIState.Help:
                menu.SetActive(false);
                help.SetActive(true);
                Highscores.SetActive(false);
                break;

            case UIState.Highscores:
                menu.SetActive(false);
                help.SetActive(false);
                Highscores.SetActive(true);
                Highscore.text=" "+PlayerPrefs.GetInt("High");
                break;
        }
    }
    public void Helpmenu()
    {    
        SetUIState(UIState.Help);
    }

    public void backtoMenu()
    {
        SetUIState(UIState.Menu);
        Gamestart.SetActive(false);
        gamebutton.SetActive(false);
        pause.SetActive(false);
        Game_Over.SetActive(false);
        pausebutton.SetActive(false);
        score.SetActive(false);
        Time.timeScale = 1f;
        manager.start = false;      
    }
    
    public void High()
    {
        SetUIState(UIState.Highscores);
    }

    public void Quit()
    {        
        Application.Quit();
    }

    public void play()
    {        
        menu.SetActive(false);
        mode.SetActive(true);
        Gamestart.SetActive(true);
        gamebutton.SetActive(true);       
    }
    public void Pause()
    {      
        Time.timeScale = 0f;
        pause.SetActive(true);
        score.SetActive(false);
        pausebutton.SetActive(false);
        Gamestart.SetActive(false);
    }
    public void resume()
    {     
        Time.timeScale = 1f;
        pause.SetActive(false);
        score.SetActive(true);
        pausebutton.SetActive(true);
        Gamestart.SetActive(true);
    }
}
