using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveBy;
    public Animator anim;
    public float moveSpeed;
    public GameObject HeroProjectilePrefab;
    public Transform HeroProjectileSpawnPoint;


    private void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    // Update is called once per frame
    void Update()
    {
        moveBy.x = Input.GetAxisRaw("Horizontal");
        moveBy.y = Input.GetAxisRaw("Vertical");
        Debug.Log("X = " + moveBy.x + " Y = " + moveBy.y);
        anim.SetFloat("Horizontal", moveBy.x);
        anim.SetFloat("Vertical", moveBy.y);
        anim.SetFloat("speed", moveBy.sqrMagnitude);
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
}

