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
    [SerializeField]
    private AudioClip levelCompleteClip;
    [SerializeField]
    private AudioClip level2;
    [SerializeField]
    private AudioClip level3;
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
    private Text textHeroName;
    private bool isDarkKnight;
    private bool isSilverKnight;
    private bool hasEasyChanged;
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
    private int[] levels;

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
        levels = new int[] { 1, 6, 7};
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            InitializeComponents_Scene_Main_Menu();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
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
        else if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            InitializeComponents_Scene_Level2();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            InitializeComponents_Scene_Level3();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            InitializeComponents_Scene_Credits();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 6 || SceneManager.GetActiveScene().buildIndex == 7)
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
            menuAudioSource.clip = menuClip;
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
    public void InitializeComponents_Scene_Level2()
    {
        if (menuAudioSource.isPlaying == true)
        {
            menuAudioSource.Stop();
            menuAudioSource.clip = level2;
            menuAudioSource.Play();
        }
        buttonBack = GameObject.Find("Button_Back").GetComponent<Button>();
        buttonBack.onClick.AddListener(() => { onButtonBackClicked(); });
        int NumberOfPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        //Only instantiate one player
        if (NumberOfPlayers < 1)
        {
            if (knightChosen == 0)
            {
                //Instantiate Dark Knight
                Instantiate(darkKnight, new Vector2(0, 0), Quaternion.identity);
            }
            else if (knightChosen == 1)
            {
                //Instantiate Silver
                Instantiate(silverKnight, new Vector2(0, 0), Quaternion.identity);
            }
        }
    }
    private void InitializeComponents_Scene_Level3()
    {
        if (menuAudioSource.isPlaying == true)
        {
            menuAudioSource.Stop();
            menuAudioSource.clip = level3;
            menuAudioSource.Play();
        }
        buttonBack = GameObject.Find("Button_Back").GetComponent<Button>();
        buttonBack.onClick.AddListener(() => { onButtonBackClicked(); });
        int NumberOfPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
        //Only instantiate one player
        if (NumberOfPlayers < 1)
        {
            if (knightChosen == 0)
            {
                //Instantiate Dark Knight
                Instantiate(darkKnight, new Vector2(0, 0), Quaternion.identity);
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
        isDarkKnight = false;
        isSilverKnight = false;
        hasEasyChanged = false;
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
        textHeroName = GameObject.Find("Text_Hero_Name").GetComponent<Text>();
        textHeroName.text = "Current name is: " + playerName.ToString();
    }

    private void onEndEditName()
    {
        playerName = inputName.text.ToString();
        if (playerName.Length > 10)
        {
            playerName = "Hero";
            inputName.text = "Name too long!";
        } else
        {
            textHeroName.text = "Current name is: " + inputName.text.ToString();
        }
    }

    private void InitializeComponents_Scene_Tutorial()
    {
        buttonBack = GameObject.Find("Button_Back").GetComponent<Button>();
        buttonBack.onClick.AddListener(() => { onButtonBackClicked(); });
    }
    private void InitializeComponents_Scene_Credits()
    {
        level = 0;
        buttonBack = GameObject.Find("Button_Back").GetComponent<Button>();
        buttonBack.onClick.AddListener(() => { onButtonBackClicked(); });
    }

    private void onButtonDifficultyHardClicked()
    {
        if (isHard == false)
        {
            buttonDifficultyHard.GetComponent<Image>().color = Color.green;
            buttonDifficultyEasy.GetComponent<Image>().color = Color.white;
            isHard = true;
        }
        else
        {
            isHard = false;
            hasEasyChanged = false;
            buttonDifficultyHard.GetComponent<Image>().color = Color.white;
        }
    }

    private void onButtonDifficultyEasyClicked()
    {
        if (isHard == true)
        {
            buttonDifficultyEasy.GetComponent<Image>().color = Color.green;
            buttonDifficultyHard.GetComponent<Image>().color = Color.white;
            isHard = false;
        }
        else
        {
            if (hasEasyChanged == false)
            {
                isHard = false;
                buttonDifficultyEasy.GetComponent<Image>().color = Color.green;
                hasEasyChanged = true;
            } else
            {
                isHard = false;
                buttonDifficultyEasy.GetComponent<Image>().color = Color.white;
                hasEasyChanged = false;
            }
        }
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
        buttonTryAgain.onClick.AddListener(() => { onButtonRestartClicked(); });
    }
    public void InitializeComponents_Scene_Level_Cleared()
    {
        if (menuAudioSource.isPlaying == true)
        {
            menuAudioSource.Stop();
            menuAudioSource.clip = levelCompleteClip;
            menuAudioSource.Play();
        }
        buttonLevelCleared = GameObject.Find("Button_Next_Level").GetComponent<Button>();
        buttonLevelCleared.onClick.AddListener(() => { onButtonNextLevelClicked(); });
    }
    private void onButtonTutorial()
    {
        SceneManager.LoadScene(5);
    }
    private void onButtonNextLevelClicked()
    {
        level += 1;
        //Last level, load main menu, later to be load credits
        if(level == 3)
        {
            SceneManager.LoadScene(8);
        } else
        {
            SceneManager.LoadScene(levels[level]);
        }
    }
    private void onButtonRestartClicked()
    {
        SceneManager.LoadScene(levels[level]);
    }

    private void onButtonSilverKnightClicked()
    {
        knightChosen = 1;
        if (isSilverKnight == false)
        {
            buttonSilverKnight.GetComponent<Image>().color = Color.green;
            buttonDarkKnight.GetComponent<Image>().color = Color.white;
            isDarkKnight = false;
            isSilverKnight = true;
        } else
        {
            isSilverKnight = false;
            buttonSilverKnight.GetComponent<Image>().color = Color.white;
        }
    }

    private void onButtonDarkKnightClicked()
    {
        knightChosen = 0;
        if (isDarkKnight == false )
        {
            buttonDarkKnight.GetComponent<Image>().color = Color.green;
            buttonSilverKnight.GetComponent<Image>().color = Color.white;
            isDarkKnight = true;
            isSilverKnight = false;
        } else
        {
            isDarkKnight = false;
            buttonDarkKnight.GetComponent<Image>().color = Color.white;
        }
    }

    private void onButtonBackClicked()
    {
        SceneManager.LoadScene(0);
    }

    private void onPlayGameClicked()
    {
        //CHANGE TO LEVEL 1
        SceneManager.LoadScene(levels[level]);
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
