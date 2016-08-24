using UnityEngine;
using System.Collections;

public class ZoomConfig : MonoBehaviour
{
    public Camera mainCam;

    private float perspectiveZoomSpeed = 0.3f;        // The rate of change of the field of view in perspective mode.
    private float orthoZoomSpeed = 0.3f;        // The rate of change of the orthographic size in orthographic mode.

    void Awake()
    {
        //mainCam = GetComponent<Camera>();
    }

    void Update()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            MoveCam.canMoveTheCam = false; // Do not allow the camera to move or rotate while zooming in and out 

            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (mainCam.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                mainCam.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                mainCam.orthographicSize = Mathf.Max(mainCam.orthographicSize, 2.5f);
                mainCam.orthographicSize = Mathf.Min(mainCam.orthographicSize, 10f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                mainCam.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                mainCam.fieldOfView = Mathf.Clamp(mainCam.fieldOfView, 30f, 120f);
            }

            MoveCam.canMoveTheCam = true; // Finished the zooming in (out) for this frame, thus can move (rotate) the camera
        }
    }
}
