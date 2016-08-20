using UnityEngine;
using System.Collections;

public class MoveCam : MonoBehaviour
{
    public static bool canMoveTheCam = true;

    private Camera cam;

    private Vector2 curTouchPos = new Vector2(0, 0);
    private Vector2 prevTouchPos = new Vector2(0, 0);
    private int touchCount = 0;
    private float screenMoveSpeed = 1f;
    private bool escapedFrame = false;
    private bool firstTouch = true;
    private bool canCheckTheGesture = true;

    /*
    enum Gestures
    {
        none,
        camUpAndDown,
        camRotate
    }

    Gestures gestures = 0;
    */

    void Awake()
    {
        cam = GetComponent<Camera>();
    }


    void Update()
    {  
        ManualCamMovement();
    }

    // Moves and Rotates the camera based on the gestures that the user makes
    //      Horizontal movement rotates the camera around the objects
    //      Vertical movement moves the camera up and down
    void ManualCamMovement()
    {
        // If we release the mouse, start the whole process from the beginning
        if (Input.GetMouseButtonUp(0))
            firstTouch = true;

        if (canMoveTheCam == true && Input.GetMouseButton(0))
        {
            float posY = Input.mousePosition.y;
            float posX = Input.mousePosition.x;

            if (firstTouch == true)
            {
                curTouchPos = new Vector2(posX, posY);
                firstTouch = false;
            }
            else
            {
                prevTouchPos = new Vector2(curTouchPos.x, curTouchPos.y);
                curTouchPos = new Vector2(posX, posY);

                float xTouchDiff = curTouchPos.x - prevTouchPos.x;
                float yTouchDiff = curTouchPos.y - prevTouchPos.y;

                // We escape a frame because whenever the camera moves to the desired position, current and previous positions swap
                //      and the camera jumps from one current to previous position and the opposite
                if (escapedFrame == false)
                {
                    // Move the camera up and down
                    Camera.main.transform.position -= new Vector3(0f , yTouchDiff * screenMoveSpeed * Time.deltaTime, 0f);

                    // Rotate the camera around the objects
                    transform.RotateAround(Vector3.zero, Vector3.up, xTouchDiff * 25 * Time.deltaTime);     

                    // Do not allow the camera to go behind the platform
                    if (Camera.main.transform.position.y < 3)
                        Camera.main.transform.position = new Vector3(cam.transform.position.x, 3, cam.transform.position.z);

                    escapedFrame = true;
                }
                else
                {
                    escapedFrame = false;
                }
            }   
        }
    }

    /*
    // Gets the fist touch and the 5th one and determines whether the user wants to rotate or move the camera
    //      by comparing its x axis distance with the y axis distance
    void DistinguishTheGesture()
    {
        if (canCheckTheGesture == true)
        {
            Vector2 firstTouch = new Vector2(0, 0);

            if (touchCount == 0)
                firstTouch = Input.mousePosition;

            if (touchCount == 10)
            {
                touchCount = 0;

                float xDist = Mathf.Abs(firstTouch.x - touchPos.x);
                float yDist = Mathf.Abs(firstTouch.y - touchPos.y);

                Debug.Log(xDist + " " + yDist);
                gestures = yDist > xDist ? Gestures.camUpAndDown : Gestures.camRotate;
                Debug.Log(gestures);
                canCheckTheGesture = false;
            }

            touchCount++;
        }
    }
    */
}
