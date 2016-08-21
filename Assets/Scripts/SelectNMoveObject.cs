﻿using UnityEngine;
using System.Collections.Generic;

public class SelectNMoveObject : MonoBehaviour {

    public Material normalObjMaterial;
    public Material selectedObjMaterial;

    public static bool canMoveTheObject = true;

    private Vector2 forceStartPos;
    private Vector2 forceEndPos;

    private List<GameObject> rayastedObjects = new List<GameObject>();   

    void Start ()
    {
        
	}
	
	void Update ()
    {
        if (canMoveTheObject == false && rayastedObjects.Count > 0)
        {
            rayastedObjects[0].transform.gameObject.GetComponent<Renderer>().material = normalObjMaterial;
            rayastedObjects.Remove(rayastedObjects[0]);
            
            canMoveTheObject = true;
        }

        if (canMoveTheObject == true)
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
                // If we previously raycasted an object or tabbed another object, 
                //      change its material to a normal one (unselected) and remove it from list
                if (rayastedObjects.Count > 0)
                {
                    rayastedObjects[0].transform.gameObject.GetComponent<Renderer>().material = normalObjMaterial;
                    rayastedObjects.Remove(rayastedObjects[0]);
                }

                // If our raycast hits the StackerObject, change its material to the selected one and add it to the list
                if (hit.transform.gameObject.tag == "StackerObject") // It is the blocks layer
                {
                    Debug.Log(rayastedObjects.Count);
                    MoveCam.canMoveTheCam = false; // Do not allow the camera to be moved or rotated during moving an object

                    hit.transform.gameObject.GetComponent<Renderer>().material = selectedObjMaterial;
                    forceStartPos = Input.mousePosition;
                    rayastedObjects.Add(hit.transform.gameObject);
                }
            }
        }

        MoveObject();
    }

    // Moves the selected StackerObject which is in rayastedObjects list by exerting force on them based on the
    //      starting and the ending (current) mouse positions
    //      See More At "Game Design" Book 1 - Page 11
    void MoveObject()
    {
        if (rayastedObjects.Count > 0)
        {
            if (Input.GetMouseButton(0))
            {
                forceEndPos = Input.mousePosition;

                float xForce = forceEndPos.x - forceStartPos.x;
                float zForce = forceEndPos.y - forceStartPos.y;

                // Apply force based on the transform of the camera, in this case, the direction it is facing
                rayastedObjects[0].GetComponent<ConstantForce>().force = Camera.main.transform.TransformDirection(xForce, 0, zForce);
            }     
            
            // Whenever the mouse is released, get rid of the force
            if (Input.GetMouseButtonUp(0))
            {
                rayastedObjects[0].GetComponent<ConstantForce>().force = Vector3.zero;
            }       
        }     
    }
}
