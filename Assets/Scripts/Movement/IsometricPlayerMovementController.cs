using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
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
        anim = GetComponent<Animator> ();
		
        ChooseDirection();
    }

    void Update() {
        if(isWalking) {
            walkCounter -= Time.deltaTime;
            
            switch(WalkDirection) {
                case 0:
                    rb.velocity = new Vector2(0, 2);
                    //anim.SetBool("NW", true);
                    //Debug.Log("0");
                    break;
                
                case 1:
                    rb.velocity = new Vector2(1, 0);
                  //   anim.SetBool("NE", true);
                   // Debug.Log("1");
                    break;
               
                case 2:
                    rb.velocity = new Vector2(0, -2);
                 //    anim.SetBool("SW", true);
                   // Debug.Log("2");
                    break;
                
                case 3:
                    rb.velocity = new Vector2(-1, 0);
                //     anim.SetBool("SE", true);
                  //  Debug.Log("3");
                    break;

                /*case 4:
                    rb.velocity = new Vector2(0, 1);
                   // Debug.Log("4");
                    break;
                case 5:
                    rb.velocity = new Vector2(0, -1);
                   // Debug.Log("5");
                    break;
                case 6:
                    rb.velocity = new Vector2(1, -1);
                  //  Debug.Log("6");
                    break;
                case 7:
                    rb.velocity = new Vector2(1, 1);
                   // Debug.Log("7");
                    break; */
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
    /*public float movementSpeed = 0.1f;
    IsometricCharacterRenderer isoRenderer;

    Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;
        //float horizontalInput = 0;
       // float verticalInput = -4;
       //Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        Vector2 pos = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        pos = Vector2.ClampMagnitude(pos, 1);
        Vector2 movement = pos * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
       
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }
    */
}
