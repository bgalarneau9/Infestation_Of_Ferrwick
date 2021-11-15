using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    //Main Menu
    private Button buttonPlayGame;
    private Button buttonPlayerSelect;
    //Sample Scene
    private Button buttonBack;
    private Text textHealth;
    private AudioClip GameMusicClip;
    [SerializeField]
    private AudioSource GameMusic;
    //Player Select
    private Button buttonDarkKnight;
    private Button buttonSilverKnight;

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
    }

    // Update is called once per frame
    void Update()
    {
        //if (SceneManager.GetActiveScene().buildIndex == 0)
        //{
        //    textHealth.text = Character.health.ToString();
        //}
    }

    private void InitializeComponents_Scene_Main_Menu()
    {
        buttonPlayGame = GameObject.Find("Button_Play_Game").GetComponent<Button>();
        buttonPlayGame.onClick.AddListener(() => { onPlayGameClicked(); });
        buttonPlayerSelect = GameObject.Find("Button_Player_Select").GetComponent<Button>();
        buttonPlayerSelect.onClick.AddListener(() => { onPlayerSelectClicked(); });
        GameMusic = GameObject.Find("Music_Source").GetComponent<AudioSource>();
        GameMusic.clip = GameMusicClip;
        GameMusic.loop = true;
        GameMusic.Play();
    }

    public void InitializeComponents_Scene_Sample()
    {
        buttonBack = GameObject.Find("Button_Back").GetComponent<Button>();
        buttonBack.onClick.AddListener(() => { onButtonBackClicked(); });
        textHealth = GameObject.Find("Text_Health").GetComponent<Text>();
        GameMusic = GameObject.Find("Music_Source").GetComponent<AudioSource>();
        GameMusic.clip = GameMusicClip;
        GameMusic.loop = true;
        GameMusic.Play();
    }
    public void InitializeComponents_Scene_Player_Select()
    {
        buttonDarkKnight = GameObject.Find("Button_Dark_Knight").GetComponent<Button>();
        buttonDarkKnight.onClick.AddListener(() => { onButtonDarkKnightClicked(); });
        buttonSilverKnight = GameObject.Find("Button_Silver_Knight").GetComponent<Button>();
        buttonSilverKnight.onClick.AddListener(() => { onButtonSilverKnightClicked(); });
    }

    private void onButtonSilverKnightClicked()
    {
        Debug.Log("SN");
    }

    private void onButtonDarkKnightClicked()
    {
        Debug.Log("DN");
    }

    private void onButtonBackClicked()
    {
        SceneManager.LoadScene(1);
    }

    private void onPlayGameClicked()
    {
        SceneManager.LoadScene(0);
    }
    private void onPlayerSelectClicked()
    {
        SceneManager.LoadScene(2);
    }
}
