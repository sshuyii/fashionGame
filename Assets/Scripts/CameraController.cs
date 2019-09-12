using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    public GameObject cameraRight;
    public GameObject cameraLeft;
    
    private Vector3 cameraRightPosition;
    private Vector3 cameraLeftPosition;

    private Quaternion cameraRightRotation;
    private Quaternion cameraLeftRotation;

    private bool rightSwipe = false;
    private bool leftSwipe = false;

    private float time = 0;
    public float speed;
        
    void Start()
    {
        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
        cameraRightPosition = cameraRight.transform.position;
        cameraLeftPosition = cameraLeft.transform.position;

        cameraRightRotation = cameraRight.transform.rotation;
        cameraLeftRotation = cameraLeft.transform.rotation;
    }
 
    void Update()
    {
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
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list
 
                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            
                            //Camera turns right
                            rightSwipe = true;
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");

                            leftSwipe = true;
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
        
        
        //camera turn right
        if (rightSwipe)
        {
            if (transform.position != cameraRightPosition && time < 2)
            {
                time += Time.deltaTime;
                //print ("turn right");
                transform.position = Vector3.Lerp(transform.position, cameraRightPosition, Time.deltaTime * speed);
                transform.rotation = Quaternion.Slerp(transform.rotation, cameraRightRotation,  Time.deltaTime * speed);

            }
            else
            {
                rightSwipe = false;
                transform.position = cameraRightPosition;
                transform.rotation = cameraRightRotation;
                time = 0;
            }
        }
        else if (leftSwipe)
        {
            if (transform.position != cameraLeftPosition && time < 2)
            {
                print("turn left");
                time += Time.deltaTime;
                transform.position = Vector3.Lerp(transform.position, cameraLeftPosition, Time.deltaTime * speed);
                transform.rotation = Quaternion.Slerp(transform.rotation, cameraLeftRotation,  Time.deltaTime * speed);
                   
            }
            else
            {
                leftSwipe = false;
                transform.position = cameraLeftPosition;
                transform.rotation = cameraLeftRotation;
                time = 0;
            }

        }

    }

}

