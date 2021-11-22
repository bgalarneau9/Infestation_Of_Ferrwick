using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoatController : MonoBehaviour
{
    //Movement
    public Vector2 moveBy;
    public float moveSpeed;
    //Other
    private int health;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private BoxCollider2D bc;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private Transform _bloodSplatSpawn;
    [SerializeField]
    private GameObject _bloodSplatPrefab;
    [SerializeField]
    private AudioClip deathsound;
    [SerializeField]
    private AudioSource enemyAudioSource;
    public Animator anim;
    public GameObject EnemyProjectilePrefab;
    public Transform EnemyProjectileSpawnPoint;
    bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        health = 250;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
