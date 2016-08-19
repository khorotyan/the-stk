using UnityEngine;
using System.Collections.Generic;

public class SelectNMoveObject : MonoBehaviour {

    public Material normalObjMaterial;
    public Material selectedObjMaterial;

    private Vector2 curTouchPos = new Vector2(0, 0);
    private Vector2 prevTouchPos = new Vector2(0, 0);

    private List<GameObject> rayastedObjects = new List<GameObject>();
    private float objMoveSpeed = 1f;
    private bool firstTouch = true;
    private bool escapedFrame = false;

    void Start ()
    {
	
	}
	
	void Update ()
    {
        SelectObject();

        if (Input.GetMouseButtonUp(0))
            MoveCam.canMoveTheCam = true; // Finished the moving the StackerObject for this frame, thus can move (rotate) the camera
    }

    // By clicking on the object, the user can select it and becomes visible through objects due to "selectedObjMaterial" material   
    //      Whenever the user clicks on other objects, previously selected one becomes unselected and the new one gets selected
    //      Whenever clicked on other places, the selected one becomes unselected
    void SelectObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 500f))
            {
                // If we previously raycasted an object, change its material to a normal one (unselected) and remove it from list
                if (rayastedObjects.Count > 0)
                {
                    rayastedObjects[0].transform.gameObject.GetComponent<Renderer>().material = normalObjMaterial;
                    rayastedObjects.Remove(rayastedObjects[0]);
                }
                 
                // If our raycast hits the StackerObject, change its material to the selected one and add it to the list
                if (hit.transform.gameObject.tag == "StackerObject") // It is the blocks layer
                {
                    MoveCam.canMoveTheCam = false; // Do not allow the camera to be moved or rotated during moving an object
                                               
                    hit.transform.gameObject.GetComponent<Renderer>().material = selectedObjMaterial;
                    rayastedObjects.Add(hit.transform.gameObject);
                }
            }
        }

        MoveObject();
    }

    // Moves the selected StackerObject which is in rayastedObjects list
    void MoveObject()
    {
        if (rayastedObjects.Count > 0)
        {
            // If we release the mouse, start the whole process from the beginning
            if (Input.GetMouseButtonUp(0))
                firstTouch = true;

            if (Input.GetMouseButton(0))
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
                        rayastedObjects[0].transform.position += new Vector3(xTouchDiff * objMoveSpeed * Time.deltaTime, 0f, yTouchDiff * objMoveSpeed * Time.deltaTime);

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
}
