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
    public Hero_Controller Character;
    private AudioClip GameMusicClip;
    private AudioSource GameMusic;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            textHealth.text = Character.health.ToString();
        }
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
        Debug.Log("Sample Scene initialized");
        buttonBack = GameObject.Find("Button_Back").GetComponent<Button>();
        buttonBack.onClick.AddListener(() => { onButtonBackClicked(); });
        textHealth = GameObject.Find("Text_Health").GetComponent<Text>();
        GameMusic = GameObject.Find("Music_Source").GetComponent<AudioSource>();
        GameMusic.clip = GameMusicClip;
        GameMusic.loop = true;
        GameMusic.Play();
    }

    private void onButtonBackClicked()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Load Main Menu Scene!");
    }

    private void onPlayGameClicked()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Load game scene!");
    }
    private void onPlayerSelectClicked()
    {
        Debug.Log("Load player select scene!");
    }
}
