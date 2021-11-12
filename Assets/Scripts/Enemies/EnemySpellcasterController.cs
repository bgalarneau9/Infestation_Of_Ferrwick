using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellcasterController : MonoBehaviour
{
    private int health = 100;
    [SerializeField]
    private GameObject spellCasterPrefab;
    [SerializeField]
    private Transform _bloodSplatSpawn;
    [SerializeField]
    private GameObject _bloodSplatPrefab;
    public AudioClip deathsound;
    private AudioSource MyAudioSrc;
    public Animator anim;
    public GameObject EnemyProjectilePrefab;
    public Transform EnemyProjectileSpawnPoint;

    void Start()
    {
    }
    private void Update()
    {
    }

    private void attack()
    {
        anim.Play("Enemy_Spellcaster_Attack");
        var projectile = Instantiate(EnemyProjectilePrefab, EnemyProjectileSpawnPoint.position, Quaternion.identity) as GameObject;
        var projectileRigidBody = projectile.GetComponent<Rigidbody2D>();
        projectileRigidBody.velocity = Quaternion.Euler(0, 0, 0) * Vector3.right * 10; //10 == power
        Destroy(projectile, 0.25f);
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
                // 4 - Play Death sound
                //MyAudioSrc.Play();
                Destroy(spellCasterPrefab);
            }
        }
    }
}
