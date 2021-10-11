using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Helper : MonoBehaviour
{ 
    public static void FlipSprite(GameObject obj, bool flipLeft)
    {   if (flipLeft == true)
            obj.transform.localRotation = Quaternion.Euler(0, 180, 0);
        else
            obj.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    public static void InstantiateProjectile(GameObject obj, float xpos, float ypos, float xvel, float yvel)
    {   GameObject instance = Instantiate(obj, new Vector3(xpos, ypos, 0), Quaternion.identity);
        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(xvel, yvel, 0); // Set Velocity      
        Destroy(instance, 3f); // Destroy Bullet after 3 seconds
    } // Instantiate any projectile with a set Velocit
    public static float ProjectileVelocity(bool up, bool down, bool left, bool right, char mode)
    {   float velocity = 0;
        if (mode == 'x')
        {   if (up == true)           
                velocity = 0f;           
            if (right == true)           
                velocity = 3f;        
            if (left == true)            
                velocity = -3f; 
            if (down == true)           
                velocity = 0f;
        } else if (mode == 'y')
        {   if (up == true)          
                velocity = 3f;
            if (right == true)
                velocity = 0f;
            if (left == true)           
                velocity = 0;      
            if (down == true)     
                velocity = -3f;    
        } return velocity;
    } public static void AnimationSwitcher(float x, float y, Animator anim)
    {   if (x == 0 && y == 0) // Idle
        {
            anim.SetBool("WalkUp", false);
            anim.SetBool("WalkLeft", false);
            anim.SetBool("WalkRight", false);
            anim.SetBool("WalkDown", false);
        }
        if (x < -0.01) // Walk Left
        {
            anim.SetBool("WalkLeft", true);
            anim.SetBool("WalkRight", false);
            anim.SetBool("WalkUp", false);
            anim.SetBool("WalkDown", false);

        }
        else if (x > 0) // Walk Right
        {
            anim.SetBool("WalkLeft", false);
            anim.SetBool("WalkRight", true);
            anim.SetBool("WalkUp", false);
            anim.SetBool("WalkDown", false);

        }
        if (y < -0.01) // Walk Down
        {
            anim.SetBool("WalkDown", true);
            anim.SetBool("WalkUp", false);
            anim.SetBool("WalkLeft", false);
            anim.SetBool("WalkRight", false);

        }
        else if (y > 0) // Walk Up
        {
            anim.SetBool("WalkUp", true);
            anim.SetBool("WalkDown", false);
            anim.SetBool("WalkLeft", false);
            anim.SetBool("WalkRight", false);

        }
    }
}
