using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveBy;
    public Animator anim;
    public float moveSpeed;

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
    }
}

