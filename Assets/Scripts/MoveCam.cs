using UnityEngine;
using System.Collections;

public class MoveCam : MonoBehaviour
{
    public static bool canMoveTheCam = true;

    private Camera cam;

    private Vector2 curTouchPos = new Vector2(0, 0);
    private Vector2 prevTouchPos = new Vector2(0, 0);
    private float screenMoveSpeed = 20f;
    private bool escapedFrame = false;
    private bool firstTouch = true;
    private float nthFrameChecker = 15f;

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
        {
            firstTouch = true;
            nthFrameChecker = 10;
        }

        if (canMoveTheCam == true && Input.GetMouseButton(0))
        {
            float posY = Input.mousePosition.y;
            float posX = Input.mousePosition.x;

            if (firstTouch == true)
            {
                curTouchPos = new Vector2(posX, posY);
                firstTouch = false;
            }
            else if (nthFrameChecker > 0)
            {
                nthFrameChecker--;

                if (nthFrameChecker == 0)
                {
                    if (Mathf.Abs(Input.mousePosition.x - curTouchPos.x) < Screen.width / 15 && Mathf.Abs(Input.mousePosition.y - curTouchPos.y) < Screen.width / 15)
                        firstTouch = true;
                    
                }
                    
            }
            else if (firstTouch == false && nthFrameChecker == 0)
            {
                prevTouchPos = new Vector2(curTouchPos.x, curTouchPos.y);
                curTouchPos = new Vector2(posX, posY);

                float xTouchDiff = curTouchPos.x - prevTouchPos.x;
                float yTouchDiff = curTouchPos.y - prevTouchPos.y;

                // Stops the problem where after tapping the screen and then another place of the screen before 
                //      the removal of the previously touched finger, the screen understands it as a rotation 
                //      and camera move and it jumps from one location to a distant one 
                if (Mathf.Abs(xTouchDiff) > Screen.width / 3 || Mathf.Abs(yTouchDiff) > Screen.height / 3)
                    return;

                if (yTouchDiff > 15)
                    yTouchDiff = 15;
                else if (yTouchDiff < -15)
                    yTouchDiff = -15;

                if (xTouchDiff > 15)
                    xTouchDiff = 15;
                else if (xTouchDiff < -15)
                    xTouchDiff = -15;

                // We escape a frame because whenever the camera moves to the desired position, current and previous positions swap
                //      and the camera jumps from one current to previous position and the opposite
                if (escapedFrame == false)
                {
                    // Move the camera up and down
                    Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Camera.main.transform.position + new Vector3(0f , -yTouchDiff * screenMoveSpeed * Time.deltaTime, 0f), 0.05f);

                    // Rotate the camera around the objects
                    transform.RotateAround(Vector3.zero, Vector3.up, xTouchDiff * 15f * Time.deltaTime);     

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
}
