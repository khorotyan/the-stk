using UnityEngine;
using System.Collections;

public class MoveCam : MonoBehaviour
{
    public static bool canMoveTheCam = true;

    private Camera cam;

    private float prevYTouchPos = 0;
    private float curYTouchPos = 0;
    private float prevXTouchPos = 0;
    private float curXTouchPos = 0;
    private Vector2 touchPos;
    private int touchCount = 0;
    private float screenMoveSpeed = 0.02f;
    private bool escapedFrame = false;
    private bool firstTouch = true;
    private bool canCheckTheGesture = true;

    enum Gestures
    {
        none,
        camUpAndDown,
        camRotate
    }

    Gestures gestures = 0;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }


    void Update()
    {
        touchPos = Input.mousePosition;

        ManualCamMovement();

    }

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
                curYTouchPos = posY;
                curXTouchPos = posX;
                firstTouch = false;
            }
            else
            {
                prevYTouchPos = curYTouchPos;
                curYTouchPos = posY;
                prevXTouchPos = curXTouchPos;
                curXTouchPos = posX;

                float yTouchDiff = curYTouchPos - prevYTouchPos;
                float xTouchDiff = curXTouchPos - prevXTouchPos;

                // We escape a frame because whenever the camera moves to the desired position, current and previous positions swap
                //      and the camera jumps from one current to previous position and the opposite
                if (escapedFrame == false)
                {
                    Camera.main.transform.position -= new Vector3(0f , yTouchDiff * screenMoveSpeed, 0f);

                    transform.RotateAround(Vector3.zero, Vector3.up, xTouchDiff * 15 * Time.deltaTime);

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
