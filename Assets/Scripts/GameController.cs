using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private AudioSource menuAudioSource;
    public static GameController Instance { get; private set; }
    private int level = 0;
    public string playerName;
    //Main Menu
    [SerializeField]
    private AudioClip menuClip;
    [SerializeField]
    private AudioClip gameOverClip;
    private Button buttonPlayGame;
    private Button buttonPlayerSelect;
    private Button buttonQuitGame;
    private Button buttonTutorial;
    //Sample Scene
    private Button buttonBack;
    //Player Select
    private Button buttonDarkKnight;
    private Button buttonSilverKnight;
    private Button buttonBackPlayerSelect;
    private Button buttonDifficultyHard;
    private Button buttonDifficultyEasy;
    private InputField inputName;
    public bool isHard;
    //Game Over
    private Button buttonTryAgain;
    //Level Cleared
    private Button buttonLevelCleared;
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
            DontDestroyOnLoad(menuAudioSource);
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
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            InitializeComponents_Scene_Level_Cleared();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            InitializeComponents_Scene_Tutorial();
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
            int enemyNumber = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemyNumber == 0)
            {
                SceneManager.LoadScene(4);
            }
        }
    }

    private void InitializeComponents_Scene_Main_Menu()
    {
        menuAudioSource.clip = menuClip;
        if ( menuAudioSource.isPlaying == false)
        {
            menuAudioSource.Play();
        }
        buttonPlayGame = GameObject.Find("Button_Play_Game").GetComponent<Button>();
        buttonPlayGame.onClick.AddListener(() => { onPlayGameClicked(); });
        buttonPlayerSelect = GameObject.Find("Button_Player_Select").GetComponent<Button>();
        buttonPlayerSelect.onClick.AddListener(() => { onPlayerSelectClicked(); });
        buttonQuitGame = GameObject.Find("Button_Quit_Game").GetComponent<Button>();
        buttonQuitGame.onClick.AddListener(() => { onButtonQuitGame(); });
        buttonTutorial = GameObject.Find("Button_Tutorial").GetComponent<Button>();
        buttonTutorial.onClick.AddListener(() => { onButtonTutorial(); });
    }

    public void InitializeComponents_Scene_Sample()
    {
        if (menuAudioSource.isPlaying == true)
        {
            menuAudioSource.Stop();
            menuAudioSource.Play();
        }
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
        buttonDifficultyEasy = GameObject.Find("Button_Easy_Difficulty").GetComponent<Button>();
        buttonDifficultyEasy.onClick.AddListener(() => { onButtonDifficultyEasyClicked(); });
        buttonDifficultyHard = GameObject.Find("Button_Hard_Difficulty").GetComponent<Button>();
        buttonDifficultyHard.onClick.AddListener(() => { onButtonDifficultyHardClicked(); });
        inputName = GameObject.Find("InputField_Name").GetComponent<InputField>();
        inputName.onEndEdit.AddListener(delegate { onEndEditName(); });
    }

    private void onEndEditName()
    {
        playerName = inputName.text.ToString();
        if (playerName.Length > 10)
        {
            playerName = "Hero";
            inputName.text = "Name too long!";
        }
        Debug.Log("Player Name: " + playerName.ToString());
    }

    private void InitializeComponents_Scene_Tutorial()
    {
        buttonBack = GameObject.Find("Button_Back").GetComponent<Button>();
        buttonBack.onClick.AddListener(() => { onButtonBackClicked(); });
    }

    private void onButtonDifficultyHardClicked()
    {
        isHard = true;
    }

    private void onButtonDifficultyEasyClicked()
    {
        isHard = false;
    }

    public void InitializeComponents_Scene_Game_Over()
    {
        if(menuAudioSource.isPlaying == true)
        {
            menuAudioSource.Stop();
            menuAudioSource.clip = gameOverClip;
            menuAudioSource.Play();
        }
        buttonTryAgain = GameObject.Find("Button_Restart").GetComponent<Button>();
        buttonTryAgain.onClick.AddListener(() => { onButtonNextLevelClicked(); });
    }
    public void InitializeComponents_Scene_Level_Cleared()
    {
        buttonLevelCleared = GameObject.Find("Button_Next_Level").GetComponent<Button>();
        buttonLevelCleared.onClick.AddListener(() => { onButtonNextLevelClicked(); });
    }
    private void onButtonTutorial()
    {
        SceneManager.LoadScene(5);
    }
    private void onButtonNextLevelClicked()
    {
        SceneManager.LoadScene(level + 1);
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
    private void onButtonQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
