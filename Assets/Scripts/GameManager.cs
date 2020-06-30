using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public SpriteRenderer[] Scolours;
    public Image[] colours;
    public bool select;

    [HideInInspector]
    public int setcolour;

    public float countdowntime;//how much time after the color should respond
    private float timecounter;

    public float waitTime;// Time to be waited between Lights
    private float waitTimecounter;

    private bool shouldbelit;
    private bool shouldbedark;

    public List<int> ActiveSequence;
    private int PositioninSequence;

    private int InputinSequence;
    private bool isGameActive;

    [HideInInspector] public bool start;

    [SerializeField] public int c_score;

    // Start is called before the first frame update
    void Start()
    {
        start = false;
        for (int i = 0; i < colours.Length; i++)
        {
            colours[i].color = new Color(colours[i].color.r, colours[i].color.g, colours[i].color.b, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        UIManager.instance.Score.text="SCORE::" + c_score;

        if (c_score > PlayerPrefs.GetInt("High"))
        {
            PlayerPrefs.SetInt("High", c_score);
        }
        Game();

        if (start == false)
        {
            //stoping sequence of lights
            shouldbelit = false;
        }
    }

    private void Game()
    {
        if (shouldbelit)
        {
            timecounter -= Time.deltaTime;
            if (timecounter < 0)
            {

                colours[ActiveSequence[PositioninSequence]].color = new Color(colours[ActiveSequence[PositioninSequence]].color.r, colours[ActiveSequence[PositioninSequence]].color.g, colours[ActiveSequence[PositioninSequence]].color.b, 0.40f);
                int i;
                i = ActiveSequence[PositioninSequence] + 1;
                AudioManager.instance.Stop(i);


                shouldbelit = false;

                shouldbedark = true;
                waitTimecounter = waitTime;

                PositioninSequence++;

            }
        }
        if (shouldbedark)
        {
            waitTimecounter -= Time.deltaTime;

            if (PositioninSequence >= ActiveSequence.Count)
            {
                shouldbedark = false;
                isGameActive = true;
            }
            else
            {
                if (waitTimecounter < 0)
                {

                    colours[ActiveSequence[PositioninSequence]].color = new Color(colours[ActiveSequence[PositioninSequence]].color.r, colours[ActiveSequence[PositioninSequence]].color.g, colours[ActiveSequence[PositioninSequence]].color.b, 1f);

                    int i;
                    i = ActiveSequence[PositioninSequence] + 1;
                    AudioManager.instance.Play(i);

                    timecounter = countdowntime;
                    shouldbelit = true;
                    shouldbedark = false;
                }
            }
        }
    }

    public void StartGame()
    {
        start = true;
        ActiveSequence.Clear();//emptying List

        UIManager.instance.score.SetActive(true);
        UIManager.instance.pausebutton.SetActive(true);
        UIManager.instance.Gamestart.SetActive(true);
        UIManager.instance.mode.SetActive(false);
        UIManager.instance.Game_Over.SetActive(false);
        UIManager.instance.gamebutton.SetActive(false);

        c_score = 0;
        SetGame();
    }

    public void ColorPressed(int whichButton)
    {
        if (start)
        {
            if (isGameActive)
            {
                if (ActiveSequence[InputinSequence] == whichButton)
                {
                    InputinSequence++;
                    c_score++;
                    if (InputinSequence >= ActiveSequence.Count)
                    {
                        Invoke("SetGame", 1);
                           // SetGame();
                            isGameActive = false;
                    }
                }
                else
                {
                    UIManager.instance.score.SetActive(false);
                    UIManager.instance.pausebutton.SetActive(false);
                    UIManager.instance.Gamestart.SetActive(false);
                    UIManager.instance.Game_Over.SetActive(true);
                    UIManager.instance.Gameoverscore.text=" " + c_score;
                    isGameActive = false;
                }
            }
        }
    }

    public void SetGame()
    {
        PositioninSequence = 0;
        InputinSequence = 0;

        setcolour = Random.Range(0, colours.Length);
        ActiveSequence.Add(setcolour);

        colours[ActiveSequence[PositioninSequence]].color = new Color(colours[ActiveSequence[PositioninSequence]].color.r, colours[ActiveSequence[PositioninSequence]].color.g, colours[ActiveSequence[PositioninSequence]].color.b, 1f);

        int i;
        i = ActiveSequence[PositioninSequence] + 1;
        AudioManager.instance.Play(i);

        timecounter = countdowntime;
        shouldbelit = true;
    }
    public void Default()
    {
        colours[0].color = Color.yellow;
        colours[1].color = Color.green;
        colours[2].color = Color.white;
        colours[3].color = Color.blue;
        colours[4].color = Color.red;
        colours[5].color = Color.cyan;

        for (int i = 0; i < colours.Length; i++)
        {
            colours[i].color = new Color(colours[i].color.r, colours[i].color.g, colours[i].color.b, 0.5f);
        }
    }
        
    public void random()
    {
        for (int i = 0; i < colours.Length; i++)
        {
            //PICKING new color Range is for not picking Black
            float R = Random.Range(0.2f, 1f);
            float G = Random.Range(0.2f, 1f);
            float B = Random.Range(0.2f, 1f);
            colours[i].color = new Color(R, G, B, 0.5f);
        }
    }
}
