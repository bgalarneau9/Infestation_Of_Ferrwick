using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private int level = 0;
    //Main Menu
    private Button buttonPlayGame;
    private Button buttonPlayerSelect;
    //Sample Scene
    private Button buttonBack;
    //Player Select
    private Button buttonDarkKnight;
    private Button buttonSilverKnight;
    private Button buttonBackPlayerSelect;
    //Game Over
    private Button buttonTryAgain;
    //Deal with player selection
    [SerializeField]
    private GameObject darkKnight;
    [SerializeField]
    private GameObject silverKnight;
    private int knightChosen = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            Instance.Start(); //Update instance for whatever scene we begin on (main menu for this project)
        }
        else
        {
            Destroy(gameObject);
            Instance.Start(); //Update instance for the current scene after destorying the game object we don't need
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            InitializeComponents_Scene_Main_Menu();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            InitializeComponents_Scene_Sample();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            InitializeComponents_Scene_Player_Select();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            InitializeComponents_Scene_Game_Over();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 )
        {
            int heroNumber= GameObject.FindGameObjectsWithTag("Player").Length;
            if ( heroNumber == 0)
            {
                SceneManager.LoadScene(3);
            }
        }
    }

    private void InitializeComponents_Scene_Main_Menu()
    {
        buttonPlayGame = GameObject.Find("Button_Play_Game").GetComponent<Button>();
        buttonPlayGame.onClick.AddListener(() => { onPlayGameClicked(); });
        buttonPlayerSelect = GameObject.Find("Button_Player_Select").GetComponent<Button>();
        buttonPlayerSelect.onClick.AddListener(() => { onPlayerSelectClicked(); });
    }

    public void InitializeComponents_Scene_Sample()
    {
        buttonBack = GameObject.Find("Button_Back").GetComponent<Button>();
        buttonBack.onClick.AddListener(() => { onButtonBackClicked(); });
        int NumberOfPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        //Only instantiate one player
        if (NumberOfPlayers < 1 )
        {
            if (knightChosen == 0)
            {
                //Instantiate Dark Knight
                Instantiate(darkKnight, new Vector2(0,0), Quaternion.identity);
            }
            else if (knightChosen == 1)
            {
                //Instantiate Silver
                Instantiate(silverKnight, new Vector2(0, 0), Quaternion.identity);
            }
        }
    }
    public void InitializeComponents_Scene_Player_Select()
    {
        buttonDarkKnight = GameObject.Find("Button_Dark_Knight").GetComponent<Button>();
        buttonDarkKnight.onClick.AddListener(() => { onButtonDarkKnightClicked(); });
        buttonSilverKnight = GameObject.Find("Button_Silver_Knight").GetComponent<Button>();
        buttonSilverKnight.onClick.AddListener(() => { onButtonSilverKnightClicked(); });
        buttonBackPlayerSelect = GameObject.Find("Button_Back").GetComponent<Button>();
        buttonBackPlayerSelect.onClick.AddListener(() => { onButtonBackClicked(); });
    }
    public void InitializeComponents_Scene_Game_Over()
    {
        buttonTryAgain = GameObject.Find("Button_Restart").GetComponent<Button>();
        buttonTryAgain.onClick.AddListener(() => { onButtonRestartClicked(); });
    }

    private void onButtonRestartClicked()
    {
        SceneManager.LoadScene(level);
    }

    private void onButtonSilverKnightClicked()
    {
        knightChosen = 1;
    }

    private void onButtonDarkKnightClicked()
    {
        knightChosen = 0;
    }

    private void onButtonBackClicked()
    {
        SceneManager.LoadScene(1);
    }

    private void onPlayGameClicked()
    {
        SceneManager.LoadScene(level);
    }
    private void onPlayerSelectClicked()
    {
        SceneManager.LoadScene(2);
    }
}
