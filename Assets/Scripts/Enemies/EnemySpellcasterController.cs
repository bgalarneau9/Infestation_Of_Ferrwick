using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellcasterController : MonoBehaviour
{
    //Attacking player
    private GameObject player;
    private float distBetween;
    private float allowableAttackDist;
    //Other
    private int health = 100;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private BoxCollider2D bc;
    [SerializeField]
    private GameObject spellCasterPrefab;
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
    private int Timer = 0;
    bool isAlive = true;

    void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        player = GameObject.FindGameObjectWithTag("Player");
        allowableAttackDist = 4;
    }
    private void Update()
    {
        distBetween = Vector3.Distance(player.transform.position, spellCasterPrefab.transform.position);
        Debug.Log(distBetween);
        if (isAlive == false && enemyAudioSource.isPlaying == false)
        {
            Destroy(spellCasterPrefab);
        }
        if (distBetween < allowableAttackDist)
        {
            tryAttack();
        }
    }

    private void tryAttack()
    {
            anim.Play("Enemy_Spellcaster_Attack");
            var projectile = Instantiate(EnemyProjectilePrefab, EnemyProjectileSpawnPoint.position, Quaternion.identity) as GameObject;
            var projectileRigidBody = projectile.GetComponent<Rigidbody2D>();
            projectileRigidBody.velocity = Quaternion.Euler(0, 0, 0) * Vector3.left * 15; //10 == power
            Destroy(projectile, 0.20f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            health -= 25;
            if ( health <= 0 && isAlive == true)
            {
                GameObject bloodSplatter = Instantiate(_bloodSplatPrefab, _bloodSplatSpawn.position, Quaternion.identity);
                bloodSplatter.GetComponent<ParticleSystem>().Play();
                enemyAudioSource.clip = deathsound;
                enemyAudioSource.PlayOneShot(deathsound);
                bc.enabled = false;
                //Make spellcaster invisible while death audio plays
                sr.forceRenderingOff = true;
                //Make sure spellcaster can't throw out an attack when he is dead
                isAlive = false;
            }
        }
    }
}
