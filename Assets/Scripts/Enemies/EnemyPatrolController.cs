using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolController : MonoBehaviour
{
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
    private int moveSpeed;
    private Vector2 moveBy;
    private bool isWalkingRight;
    bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        isWalkingRight = true;
        moveSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == false && enemyAudioSource.isPlaying == false)
        {
            Destroy(enemyPrefab);
        }
    }

    private void FixedUpdate()
    {
        if (isWalkingRight == true )
        {
            moveBy = new Vector2(1, 0);
            anim.Play("Enemy_Skeleton_Walk_Right");
            rb.position += moveBy * moveSpeed * Time.fixedDeltaTime;
        } else
        {
            moveBy = new Vector2(-1, 0);
            anim.Play("Enemy_Skeleton_Walk_Left");
            rb.position += moveBy * moveSpeed * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            GameObject heroProjectile = GameObject.FindGameObjectWithTag("Projectile");
            //Set the enemies velocity in the x direction to the inverse of the projectile speed so it stay in same spot
            rb.velocity = new Vector2(-heroProjectile.GetComponent<Rigidbody2D>().velocity.x, 0);
            health -= 20;
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
        else if (collision.gameObject.tag == "FlipMovement")
        {
            isWalkingRight = !isWalkingRight;
        }
    }
}
