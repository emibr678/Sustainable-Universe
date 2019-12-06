using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepMovement : MonoBehaviour
{

    public float moveSpeed;
    public bool isWalking;
    public float walkTime;
    public float waitTime;
    private float walkCounter;
    private float waitCounter;
    private Rigidbody2D rb;
    public Animator anim;

    private int WalkDirection;


    void Start() {

        rb = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;
        //anim = GetComponent<Animator> ();
		
        ChooseDirection();
    }

    void Update() {
        if(isWalking) {
            walkCounter -= Time.deltaTime;
            
            switch(WalkDirection) {
                case 0:
                    rb.velocity = new Vector2(0, 2);
                    break;
                
                case 1:
                    rb.velocity = new Vector2(1, 0);
                    break;
               
                case 2:
                    rb.velocity = new Vector2(0, -2);
                    break;
                
                case 3:
                    rb.velocity = new Vector2(-1, 0);
                    break;
            }

            if(walkCounter < 0) {
                isWalking = false;
                waitCounter = waitTime;
            }
        } 
        
        else {
            waitCounter -= Time.deltaTime;
            rb.velocity = Vector2.zero;
            if(waitCounter < 0){
                ChooseDirection(); 
            }

        }
    }

    public void ChooseDirection() {
        WalkDirection =  Random.Range(0, 8);
       // Debug.Log(WalkDirection);
        isWalking = true;
        walkCounter = walkTime;
    }
  
}
