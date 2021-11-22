using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoatController : MonoBehaviour
{
    //Attacking Player
    private GameObject player;
    private Vector3 playerPosition;
    private float distBetween;
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
        player = GameObject.FindGameObjectWithTag("Player");
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        health = 250;
        moveSpeed = 0.10f;
        timeBetweenAttack = 4;
        allowableAttackDist = 5;
        nextAttack = Time.time + timeBetweenAttack;
        moveBy = new Vector2(-1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if( isAlive == true )
        {
            tryAttack();
        }
        int numberOfPlayer = GameObject.FindGameObjectsWithTag("Player").Length;
        if (numberOfPlayer > 0)
        {
            distBetween = Vector3.Distance(player.transform.position, enemyPrefab.transform.position);
            tryMove();
        }
    }
    private void tryAttack()
    {
        GameObject hero = GameObject.FindGameObjectWithTag("Player");
        if (Time.time >= nextAttack)
        {
            anim.Play("Goat_Attack");
            var projectile = Instantiate(EnemyProjectilePrefab, EnemyProjectileSpawnPoint.position, Quaternion.identity) as GameObject;
            var projectileRigidBody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidBody.velocity = Quaternion.Euler(0, 0, 0) * Vector3.left * 15; //10 == power
            Destroy(projectile, 2f);
            nextAttack = Time.time + timeBetweenAttack;
            anim.SetTrigger("Idle");
        }
    }
    private void tryMove()
    {
        if (distBetween > 3)
        {
            anim.Play("Goat_Move_Left");
            rb.position += moveBy * moveSpeed * Time.fixedDeltaTime;
        }
        else if (distBetween < 2)
        {
            anim.Play("Goat_Move_Right");
            rb.position -= moveBy * 3 * Time.fixedDeltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FlipMovement")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), enemyPrefab.GetComponent<BoxCollider2D>());
        }
        else if (collision.gameObject.tag == "Player")
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        else if (collision.gameObject.tag == "Projectile")
        {
            rb.velocity = new Vector3(0, 0, 0);
            GameObject heroProjectile = GameObject.FindGameObjectWithTag("Projectile");
            health -= 25;
            if (health <= 0 && isAlive == true)
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
