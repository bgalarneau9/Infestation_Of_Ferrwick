using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellcasterController : MonoBehaviour
{
    private int health = 100;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject spellCasterPrefab;
    [SerializeField]
    private Transform _bloodSplatSpawn;
    [SerializeField]
    private GameObject _bloodSplatPrefab;
    [SerializeField]
    private AudioClip deathclip;
    [SerializeField]
    private AudioSource deathsound;
    public Animator anim;
    public GameObject EnemyProjectilePrefab;
    public Transform EnemyProjectileSpawnPoint;
    private int Timer = 0;

    void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    private void Update()
    {
        Timer += 1;
        if (Timer > 1000)
        {
            attack();
            Timer = 0;
        }
    }

    private void attack()
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
            if ( health <= 0)
            {
                GameObject bloodSplatter = Instantiate(_bloodSplatPrefab, _bloodSplatSpawn.position, Quaternion.identity);
                bloodSplatter.GetComponent<ParticleSystem>().Play();
                deathsound.clip = deathclip;
                deathsound.Play();
                Destroy(spellCasterPrefab);
            }
        }
    }
}
