using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Controller : MonoBehaviour
{
    public int health = 100;
    public Rigidbody2D rb;
    public Vector2 moveBy;
    public Animator anim;
    public float moveSpeed;
    public GameObject HeroProjectilePrefab;
    public Transform HeroProjectileSpawnPoint;
    private bool isAlive;
    [SerializeField]
    private Transform _bloodSplatSpawn;
    [SerializeField]
    private GameObject _bloodSplatPrefab;
    [SerializeField]
    private GameObject heroPrefab;
    [SerializeField]
    private AudioClip deathsound;
    [SerializeField]
    private AudioSource heroAudioSource;
    [SerializeField]
    private SpriteRenderer sr;


    private void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isAlive = true;
    }
    // Update is called once per frame
    void Update()
    {
        moveBy.x = Input.GetAxisRaw("Horizontal");
        moveBy.y = Input.GetAxisRaw("Vertical");
        anim.SetFloat("Horizontal", moveBy.x);
        anim.SetFloat("Vertical", moveBy.y);
        anim.SetFloat("speed", moveBy.sqrMagnitude);
        if (isAlive == false && heroAudioSource.isPlaying == false)
        {
            Destroy(heroPrefab);
        }
    }
    private void FixedUpdate()
    {
        rb.position += moveBy * moveSpeed * Time.fixedDeltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            attack();
            anim.Play("Hero_Attack_Right");
        }
    }
    public void attack()
    {
        int NumberOfPojectiles = GameObject.FindGameObjectsWithTag("Projectile").Length;
        if( NumberOfPojectiles < 2 )
        {
            var heroProjectile = Instantiate(HeroProjectilePrefab, HeroProjectileSpawnPoint.position, Quaternion.identity) as GameObject;
            var heroProjectileRigidBody = heroProjectile.GetComponent<Rigidbody2D>();
            heroProjectileRigidBody.velocity = Quaternion.Euler(0, 0, 0) * Vector3.right * 10; //10 == power
            Destroy(heroProjectile, 0.10f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy_Projectile")
        {
            health -= 25;
            if (health <= 0)
            {
                GameObject bloodSplatter = Instantiate(_bloodSplatPrefab, _bloodSplatSpawn.position, Quaternion.identity);
                bloodSplatter.GetComponent<ParticleSystem>().Play();
                heroAudioSource.clip = deathsound;
                heroAudioSource.Play();
                sr.forceRenderingOff = true;
                isAlive = false;
            }
        }
    }
}

