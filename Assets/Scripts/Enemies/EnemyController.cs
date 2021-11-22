using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Attacking player
    private GameObject player;
    private Vector3 playerPosition;
    private float distBetween;
    private float allowableAttackDist;
    private float timeBetweenAttack;
    private float nextAttack;
    //Moving towards player
    private int allowableFollowDistance;
    public Vector2 moveBy;
    public float moveSpeed;
    //Other
    private int health = 100;
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

    void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform.position;
        timeBetweenAttack = 3;
        allowableAttackDist = 4;
        allowableFollowDistance = 5;
        moveSpeed = 2;
    }
    private void Update()
    {
        int numberOfPlayer = GameObject.FindGameObjectsWithTag("Player").Length;
        if (numberOfPlayer > 0)
        {
            distBetween = Vector3.Distance(player.transform.position, enemyPrefab.transform.position);
        }
        if (isAlive == false && enemyAudioSource.isPlaying == false)
        {
            Destroy(enemyPrefab);
        }
        if (distBetween < allowableAttackDist)
        {
            tryAttack();
        }
    }
    private void FixedUpdate()
    {
        moveBy.x = Input.GetAxisRaw("Horizontal");
        moveBy.y = 0;
        if(distBetween < allowableFollowDistance)
        {
            if (moveBy.x < 0)
            {
                rb.position += moveBy * moveSpeed * Time.fixedDeltaTime;
            } else
            {
                rb.position -= moveBy * moveSpeed * Time.fixedDeltaTime;
            }
        }
    }

    private void tryAttack()
    {
        GameObject hero = GameObject.FindGameObjectWithTag("Player");
        if (Time.time >= nextAttack)
        {
            anim.Play("Enemy_Spellcaster_Attack");
            var projectile = Instantiate(EnemyProjectilePrefab, EnemyProjectileSpawnPoint.position, Quaternion.identity) as GameObject;
            var projectileRigidBody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidBody.velocity = Quaternion.Euler(0, 0, 0) * Vector3.left * 15; //10 == power
            Destroy(projectile, 0.20f);
            nextAttack = Time.time + timeBetweenAttack;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FlipMovement")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), enemyPrefab.GetComponent<BoxCollider2D>());
        }
        else if (collision.gameObject.tag == "Projectile")
        {
            GameObject heroProjectile = GameObject.FindGameObjectWithTag("Projectile");
            //Set the enemies velocity in the x direction to the inverse of the projectile speed so it stay in same spot
            rb.velocity = new Vector2(-heroProjectile.GetComponent<Rigidbody2D>().velocity.x, 0);
            health -= 25;
            if ( health <= 0 && isAlive == true)
            {
                GameObject bloodSplatter = Instantiate(_bloodSplatPrefab, _bloodSplatSpawn.position, Quaternion.identity);
                bloodSplatter.GetComponent<ParticleSystem>().Play();
                enemyAudioSource.clip = deathsound;
                enemyAudioSource.PlayOneShot(deathsound);
                bc.enabled = false;
                //Make enemy invisible while death audio plays
                sr.forceRenderingOff = true;
                //Make sure enemy can't throw out an attack when he is dead
                isAlive = false;
            }
        }
    }
}
