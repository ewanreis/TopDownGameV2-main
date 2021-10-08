using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class playerController : MonoBehaviour
{   private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    private Animator anim;   
    public GameObject Player;
    public GameObject Bullet;
    public bool down, up, left, right;
    public float timer=0;
    private void Start()
    {   boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {   if (Input.GetKey("space") && timer <= 0)
        {   float xPosition = Player.transform.position.x, yPosition = Player.transform.position.y;
            float xVelocity = 0f, yVelocity = 0f;
            if (up == true)
            {   xVelocity = 0f;
                yVelocity = 3f;
            }if (right == true)
            {   xVelocity = 3f;
                yVelocity = 0f;
            }if (left == true)
            {   xVelocity = -3f;
                yVelocity = 0;
            }if (down == true)
            {   xVelocity = 0f;
                yVelocity = -3f;
            } MakeBullet(Bullet, xPosition, yPosition, xVelocity, yVelocity);
            timer = 20;
        }   timer--;
    }
    private void FixedUpdate()
    {   down = false; up = false; left = false; right=false;
        float x = Input.GetAxisRaw("Horizontal"),y=0;
        if (x != 0)
            y = 0;
        else if (x == 0)
            y = Input.GetAxisRaw("Vertical");        
        moveDelta = new Vector3(x, y, 0);                
        // Prevent Going inside colliders y
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y)
            , Mathf.Abs(moveDelta.y * Time.deltaTime)
            , LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)           
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0); // Movement
        // Prevent Going inside colliders x
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.x)
            , Mathf.Abs(moveDelta.x * Time.deltaTime)
            , LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)            
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0); // Movement
        if (x == 0 && y == 0) // Idle
        {   anim.SetBool("WalkUp", false);
            anim.SetBool("WalkLeft", false);
            anim.SetBool("WalkRight", false);
            anim.SetBool("WalkDown", false);
            down = true;
        } if ( x < -0.01 ) // Walk Left
        {   anim.SetBool("WalkLeft", true);
            anim.SetBool("WalkRight", false);
            anim.SetBool("WalkUp", false);
            anim.SetBool("WalkDown", false);
            left = true;
        } else if ( x > 0 ) // Walk Right
        {   anim.SetBool("WalkLeft", false);
            anim.SetBool("WalkRight", true);
            anim.SetBool("WalkUp", false);
            anim.SetBool("WalkDown", false);
            right = true;
        } if (y < 0) // Walk Down
        {   anim.SetBool("WalkDown", true);
            anim.SetBool("WalkUp", false);
            anim.SetBool("WalkLeft", false);
            anim.SetBool("WalkRight", false);
            down = true;
        } else if (y > 0) // Walk Up
        {   anim.SetBool("WalkUp", true);
            anim.SetBool("WalkDown", false);
            anim.SetBool("WalkLeft", false);
            anim.SetBool("WalkRight", false);
            up = true;
        } 
    }   
    public static void MakeBullet(GameObject prefab, float xpos, float ypos, float xvel, float yvel)
    {   GameObject instance = Instantiate(prefab, new Vector3(xpos, ypos, 0), Quaternion.identity);       
        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(xvel, yvel, 0); // Set Velocity
        // Destroy Bullet after 3 seconds
        Destroy(instance, 3f);
    } 
}