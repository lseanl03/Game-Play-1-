using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  
    [SerializeField] private float directionX=0f; //phương hướng
    [SerializeField] private float speedMove = 3f;
    [SerializeField] private float jumpForce = 10.5f;//lực nhảy
    [SerializeField] private LayerMask jumpableGround;
    private Rigidbody2D rb2d;
    private Animator animator;
    private BoxCollider2D coll; //cuộn dây
    public bool canMove=true;
    public bool facingRight = true;
    public enum Movementstate {Idle, Running, Jumping, Falling}


    private void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.coll= GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (this.canMove)
        {
            this.MovePlayer();
            this.AnimationPlayer();
            this.PLayFlip();
        }
    }
   public void MovePlayer()
    {

        this.directionX = Input.GetAxisRaw("Horizontal");
        this.rb2d.velocity = new Vector2(this.directionX * this.speedMove, this.rb2d.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space)&&IsGrounded())
        {
            this.rb2d.velocity = new Vector2(this.rb2d.velocity.x, this.jumpForce);
            FindObjectOfType<AudioManager>().PlaySounds("Jump");
        }
    }
    void AnimationPlayer()
    {
        Movementstate state;
        
            if (this.directionX > 0f)
            {
                state = Movementstate.Running;
            }
            else if (this.directionX < 0f)
            {
                state = Movementstate.Running;
            }
            else
            {
                state = Movementstate.Idle;
            }
            if (this.rb2d.velocity.y > .1f)
            {
                state = Movementstate.Jumping;
            }
            else if (this.rb2d.velocity.y < -.1f)
            {
                state = Movementstate.Falling;
            }
            animator.SetInteger("State", (int)state);//cài đặt số nguyên
    }
    void PLayFlip()
    {
        if(this.directionX > 0f && !facingRight)
        {
            this.Flip();
        }
        else if(this.directionX<0f && facingRight)
        {
            this.Flip();
        }
    }
    void Flip()
    {
        facingRight=!facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    bool IsGrounded()
    {

        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);//giới hạn
    }

}





