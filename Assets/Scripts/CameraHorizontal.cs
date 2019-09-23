using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHorizontal : MonoBehaviour
{
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    public GameObject player;

    //offset between player and camera in z axis
    private float offsetZ;

    private bool rightSwipe = false;
    private bool leftSwipe = false;

    private float time = 0;
    public float speed;
    
    public bool moveUp;
    public bool moveDown;
    
    //camera movement
    private float cameraX;
    private float cameraY;

    public GameObject cameraFirst;
    private Vector3 cameraFirstPosition;
    private Quaternion cameraFirstRotation;
    
    public GameObject cameraSecond;
    private Vector3 cameraSecondPosition;
    private Quaternion cameraSecondRotation;

    public GameObject WallFirst;
    public GameObject WallSecond;

    private MeshRenderer WallSecondMR;
    private MeshRenderer WallFirstMR;

        
    void Start()
    {
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
      
        offsetZ = transform.position.z - player.transform.position.z;

        cameraFirstPosition = cameraFirst.transform.position;
        cameraFirstRotation = cameraFirst.transform.rotation;
        
        cameraSecondPosition = cameraSecond.transform.position;
        cameraSecondRotation = cameraSecond.transform.rotation;

        WallSecondMR = WallSecond.GetComponent<MeshRenderer>();
        WallFirstMR = WallFirst.GetComponent<MeshRenderer>();
        
//        //camera should follow player, but only move in z direction
//        cameraX = transform.position.x;
//        cameraY = transform.position.y;

    }

    void Update()
    {

//        transform.position = new Vector3(cameraX, cameraY, player.transform.position.z + offsetZ);


        print(rightSwipe);


        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;

                
                    //It's a drag
                    //check if the drag is vertical
                    if (Mathf.Abs(lp.x - fp.x) < Mathf.Abs(lp.y - fp.y))
                    {
                        if (lp.y > fp.y) //If the movement was up
                        {
                            //Up swipe
                            Debug.Log("Up Swipe in Process");
                            moveUp = true;
                        }
                        else
                        {
                            //Down swipe
                            Debug.Log("Down Swipe in progress");
                            moveDown = true;
                        }
                    }
                

            }
                else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
                {
                    lp = touch.position; //last touch position. Ommitted if you use list

                    moveUp = false;
                    moveDown = false;

                    //Check if drag distance is greater than 20% of the screen height
                    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                    {
                        //It's a drag
                        //check if the drag is vertical or horizontal
                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {
                            //If the horizontal movement is greater than the vertical movement...
                            if ((lp.x > fp.x)) //If the movement was to the right)
                            {
                                //Right swipe
                                Debug.Log("Right Swipe");

                                //Camera turns right
                                rightSwipe = true;
                            }
                            else
                            {
                                //Left swipe
                                Debug.Log("Left Swipe");

                                leftSwipe = true;
                            }
                        }
                        else
                        {
                            //the vertical movement is greater than the horizontal movement
                            if (lp.y > fp.y) //If the movement was up
                            {
                                //Up swipe
                                Debug.Log("Up Swipe");

                            }
                            else
                            {
                                //Down swipe
                                Debug.Log("Down Swipe");
                            }
                        }
                    }
                    else
                    {
                        //It's a tap as the drag distance is less than 20% of the screen height
                        Debug.Log("Tap");
             
                    }
                }
            
            
//            else if (leftSwipe)
//            {
//                if (transform.position != cameraLeftPosition && time < 2)
//                {
//                    print("turn left");
//                    time += Time.deltaTime;
//                    transform.position = Vector3.Lerp(transform.position, cameraLeftPosition, Time.deltaTime * speed);
//                    transform.rotation =
//                        Quaternion.Slerp(transform.rotation, cameraLeftRotation, Time.deltaTime * speed);
//
//                }
//                else
//                {
//                    leftSwipe = false;
//                    transform.position = cameraLeftPosition;
//                    transform.rotation = cameraLeftRotation;
//                    time = 0;
//                }
//
//            }

            
        }
        
        
        //camera turn right and left
        if (rightSwipe)
        {
            if (transform.position != cameraFirstPosition && time < 2)
            {
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, cameraFirstPosition, Time.deltaTime * speed);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, cameraFirstRotation, Time.deltaTime * speed);

                WallFirstMR.enabled = false;
                WallSecondMR.enabled = true;

            }
            else
            {
                rightSwipe = false;
                transform.position = cameraFirstPosition;
                transform.rotation = cameraFirstRotation;
                time = 0;
            }
        }
        else if (leftSwipe)
        {
            if (transform.position != cameraSecondPosition && time < 2)
            {
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, cameraSecondPosition, Time.deltaTime * speed);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, cameraSecondRotation, Time.deltaTime * speed);

                WallFirstMR.enabled = true;
                WallSecondMR.enabled = false;

            }
            else
            {
                leftSwipe = false;
                transform.position = cameraSecondPosition;
                transform.rotation = cameraSecondRotation;
                time = 0;
            }
        }

        

        
    }

}

