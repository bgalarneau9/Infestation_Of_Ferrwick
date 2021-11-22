using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoatController : MonoBehaviour
{
    //Attacking Player
    private float allowableAttackDist;
    private float timeBetweenAttack;
    private float nextAttack;
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
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        health = 250;
        moveSpeed = 1;
        timeBetweenAttack = 2;
        allowableAttackDist = 5;
    }

    // Update is called once per frame
    void Update()
    {
        tryAttack();
    }
    private void tryAttack()
    {
        GameObject hero = GameObject.FindGameObjectWithTag("Player");
        if (Time.time >= nextAttack)
        {
            anim.SetBool("isAttacking", true);
            var projectile = Instantiate(EnemyProjectilePrefab, EnemyProjectileSpawnPoint.position, Quaternion.identity) as GameObject;
            var projectileRigidBody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidBody.velocity = Quaternion.Euler(0, 0, 0) * Vector3.left * 15; //10 == power
            Destroy(projectile, 2f);
            nextAttack = Time.time + timeBetweenAttack;
            anim.SetTrigger("Idle");
        }
    }
}
