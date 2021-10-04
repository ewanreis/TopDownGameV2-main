using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcMovement : MonoBehaviour
{   public GameObject Player;
    public GameObject npc;
    public bool colliding = false;
    void Update()
    {   float distance = Vector3.Distance(npc.transform.position, Player.transform.position);
        if (distance > 1f)
        {
            float step = 0.5f * Time.deltaTime;          
            if (colliding == false)
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);     // Movement
            if (colliding == true)
                print("true");
        
   

        }
        print(distance);
    }
    void OnCollisonEnter2D(Collision2D collision)
    {
        colliding = true;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        colliding = false;
    }
}
