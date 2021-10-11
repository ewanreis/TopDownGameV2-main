using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class playerController : MonoBehaviour
{   private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    Rigidbody2D playerRigid;
    private Animator anim;
    public GameObject Player, Bullet;

    public bool down, up, left, right;
    public float timer=0;
    
    private void Start()
    {   boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        playerRigid = GetComponent<Rigidbody2D>();
    }private void Update()
    {   if (Input.GetKey("space") && timer <= 0)
        {   float xPosition = Player.transform.position.x, yPosition = Player.transform.position.y
            , xVelocity = Helper.ProjectileVelocity(up, down, left, right, 'x'), yVelocity = Helper.ProjectileVelocity(up, down, left, right, 'y');
            Helper.InstantiateProjectile(Bullet, xPosition, yPosition, xVelocity, yVelocity);
            timer = 20;
        }   timer--;
        Helper.DoRayCollisionCheck(Player);
    }private void FixedUpdate()
    {   float x = Input.GetAxisRaw("Horizontal"),y=Input.GetAxisRaw("Vertical");
        if (x != 0)
            y = 0;
        moveDelta = new Vector3(x, y, 0);

        // Prevent Going inside colliders y
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y)
            , Mathf.Abs(moveDelta.y * Time.deltaTime)
            , LayerMask.GetMask("Enemy", "Blocking"));
        if (hit.collider == null)
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0); // Movement

        // Prevent Going inside colliders x
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.x)
            , Mathf.Abs(moveDelta.x * Time.deltaTime)
            , LayerMask.GetMask("Enemy", "Blocking"));
        if (hit.collider == null)
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0); // Movement

        down = false; up = false; left = false; right=false;
        string direction = Helper.AnimationSwitcher(x, y, anim);
        if (direction =="up")
        up = true;
        if (direction =="down")
        down = true;
        if (direction =="left")
        left = true;
        if (direction =="right")
        right = true;
        else
        down = true;
    }
}