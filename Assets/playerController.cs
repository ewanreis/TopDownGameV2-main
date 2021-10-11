using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class playerController : MonoBehaviour
{   private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    private Animator anim;   
    public GameObject Player, Bullet;
    public bool down, up, left, right;
    public float timer=0;
    Rigidbody2D playerRigid;
    private void Start()
    {   boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        playerRigid = GetComponent<Rigidbody2D>();
    }private void Update()
    {   if (Input.GetKey("space") && timer <= 0)
        {   float xPosition = Player.transform.position.x, yPosition = Player.transform.position.y;
            float xVelocity = Helper.ProjectileVelocity(up, down, left, right, 'x'), yVelocity = Helper.ProjectileVelocity(up, down, left, right, 'y');
            Helper.InstantiateProjectile(Bullet, xPosition, yPosition, xVelocity, yVelocity);
            timer = 20;
        }   timer--;
        Helper.DoRayCollisionCheck(Player);
    }private void FixedUpdate()
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
        string direction = Helper.AnimationSwitcher(x, y, anim);
        switch (direction)
        {   case "up":
                up = true;
                break;
            case "down":
                down = true;
                break;
            case "left":
                left = true;
                break;
            case "right":
                right = true;
                break;
            default:
                down = true;
                break;
        }
    }   

}