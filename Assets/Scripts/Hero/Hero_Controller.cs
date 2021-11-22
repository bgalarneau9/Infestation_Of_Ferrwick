using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hero_Controller : MonoBehaviour
{
    public int health = 100;
    private Text healthText;
    private Text deathText;
    private bool isHard;
    private string playerName;
    private int enemyDamage;
    [SerializeField]
    private int heroType;
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
    private float injuryFlash;
    private float timeBetweenFlash;


    private void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isAlive = true;
        healthText = GameObject.Find("Health_Text").GetComponent<Text>();
        deathText = GameObject.Find("Death_Text").GetComponent<Text>();
        isHard = GameObject.Find("EventSystem").GetComponent<GameController>().isHard;
        playerName = GameObject.Find("EventSystem").GetComponent<GameController>().playerName;
        timeBetweenFlash = 0.15f;

        if (isHard == false)
        {
            enemyDamage = 15;
        } else
        {
            enemyDamage = 25;
        }
    }
    // Update is called once per frame
    void Update()
    {
        moveBy.x = Input.GetAxisRaw("Horizontal");
        moveBy.y = Input.GetAxisRaw("Vertical");
        //Debug.Log("x = " + moveBy.x + " y = " + moveBy.y);
        anim.SetFloat("Horizontal", moveBy.x);
        anim.SetFloat("Vertical", moveBy.y);
        anim.SetFloat("speed", moveBy.sqrMagnitude);
        if (health > 0)
        {
            healthText.text = health.ToString();
        } else
        {
            healthText.text = "0";
        }
        if (isAlive == false && heroAudioSource.isPlaying == false)
        {
            Destroy(heroPrefab);
        }
        if(Time.time >= injuryFlash)
        {
            sr.color = new Color(1, 1, 1);
        }
    }
    private void FixedUpdate()
    {
        rb.position += moveBy * moveSpeed * Time.fixedDeltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            attack();
            if (heroType == 0)
            {
                anim.Play("Hero_Attack_Right");
            }
            else
            {
                anim.Play("Hero_Silver_Attack_Right");
            }
        }
    }
    public void attack()
    {
        if( isAlive )
        {
            int NumberOfPojectiles = GameObject.FindGameObjectsWithTag("Projectile").Length;
            if (NumberOfPojectiles < 2)
            {
                var heroProjectile = Instantiate(HeroProjectilePrefab, HeroProjectileSpawnPoint.position, Quaternion.identity) as GameObject;
                var heroProjectileRigidBody = heroProjectile.GetComponent<Rigidbody2D>();
                heroProjectileRigidBody.velocity = Quaternion.Euler(0, 0, 0) * Vector3.right * 10; //10 == power
                Destroy(heroProjectile, 0.10f);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FlipMovement") {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<BoxCollider2D>(), heroPrefab.GetComponent<BoxCollider2D>());
        }
        else if (collision.gameObject.tag == "Enemy_Projectile")
        {
            sr.color = new Color(1, 0, 0);
            GameObject enemyProjectile = GameObject.FindGameObjectWithTag("Enemy_Projectile");
            //Set the player's velocity in the x direction to the inverse of the projectile speed so it stays in same spot
            rb.velocity = new Vector2(-enemyProjectile.GetComponent<Rigidbody2D>().velocity.x, 0);
            health -= enemyDamage;
            if (health <= 0)
            {
                GameObject bloodSplatter = Instantiate(_bloodSplatPrefab, _bloodSplatSpawn.position, Quaternion.identity);
                bloodSplatter.GetComponent<ParticleSystem>().Play();
                heroAudioSource.clip = deathsound;
                heroAudioSource.Play();
                sr.forceRenderingOff = true;
                isAlive = false;
            }
            injuryFlash = Time.time + timeBetweenFlash;
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            sr.color = new Color(1, 0, 0);
            health -= enemyDamage;
            if (health <= 0)
            {
                GameObject bloodSplatter = Instantiate(_bloodSplatPrefab, _bloodSplatSpawn.position, Quaternion.identity);
                bloodSplatter.GetComponent<ParticleSystem>().Play();
                heroAudioSource.clip = deathsound;
                heroAudioSource.Play();
                sr.forceRenderingOff = true;
                isAlive = false;
            }
            injuryFlash = Time.time + timeBetweenFlash;
        }
        else if (collision.gameObject.tag == "Kill_Zone") {
            health = 0;
            GameObject bloodSplatter = Instantiate(_bloodSplatPrefab, _bloodSplatSpawn.position, Quaternion.identity);
            bloodSplatter.GetComponent<ParticleSystem>().Play();
            heroAudioSource.clip = deathsound;
            heroAudioSource.Play();
            sr.forceRenderingOff = true;
            isAlive = false;
        }
        if(health<=0)
        {
            deathText.text = playerName + " Died!";
        }
    }
}

