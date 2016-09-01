using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SelectNMoveObject : MonoBehaviour {

    public Material normalObjMaterial;
    public Material selectedObjMaterial;

    public static bool canMoveTheObject = true;

    private Vector2 downTouchPos = Vector2.zero;
    private Vector2 upTouchPos = Vector2.zero;
    private Vector2 forceStartPos;
    private Vector2 forceEndPos;
    private List<GameObject> rayastedObjects = new List<GameObject>();

    void Start ()
    {
        
	}

    void Update()
    {
        // Whenever an object is extracted, unselect it
        if (canMoveTheObject == false && rayastedObjects.Count > 0)
        {
            rayastedObjects[0].transform.gameObject.GetComponent<Renderer>().material = normalObjMaterial;
            rayastedObjects.Remove(rayastedObjects[0]);

            canMoveTheObject = true;
        }

        if (canMoveTheObject == true)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                SelectObject();
            }
        }
    }

    // By clicking on the object, the user can select it and becomes visible through objects due to "selectedObjMaterial" material   
    //      Whenever the user clicks on other objects, previously selected one becomes unselected and the new one gets selected
    //      Whenever clicked on other places, the selected one becomes unselected
    void SelectObject()
    {
        CheckIfWasATouch();

        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 500f))
        {
            // If we previously raycasted an object or tabbed another object, 
            //      change its material to a normal one (unselected) and remove it from list
            if (rayastedObjects.Count > 0 && CheckIfWasATouch() == true)
            {
                rayastedObjects[0].transform.gameObject.GetComponent<Renderer>().material = normalObjMaterial;

                // Resets other physics information
                rayastedObjects[0].GetComponent<Rigidbody>().isKinematic = true;
                rayastedObjects[0].GetComponent<Rigidbody>().isKinematic = false;

                // Resets the force information
                DestroyImmediate(rayastedObjects[0].GetComponent<ConstantForce>());
                rayastedObjects[0].AddComponent<ConstantForce>();

                rayastedObjects.Remove(rayastedObjects[0]);
                MoveCam.canMoveTheCam = true;
            }

            // If our raycast hits the StackerObject, change its material to the selected one and add it to the list
            if (hit.transform.gameObject.tag == "StackerObject" && CheckIfWasATouch() == true) // It is the blocks layer
            {
                // Does not allow to select the items on top of the tower
                if (CheckIfObjIsSelectable(hit) == true)
                {
                    MoveCam.canMoveTheCam = false;
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
                rayastedObjects[0].GetComponent<ConstantForce>().force = Camera.main.transform.TransformDirection(xForce * 1.5f, 0, zForce * 1.5f);
            }
        }     
    }

    // Check if an object can be selected, do not allow to select the objects on top of the tower
    bool CheckIfObjIsSelectable(RaycastHit hit)
    {
        if (SpawnStackerObjects.numOfExtractedObjs % 3 == 0)
        {
            if (SpawnStackerObjects.numOfExtractedObjs > 0)
                SpawnStackerObjects.maxHeight += hit.transform.localScale.y;

            return CaseManager(hit, 51, 3);
        }
        else if (SpawnStackerObjects.numOfExtractedObjs % 3 == 1)
            return CaseManager(hit, 50, 4);
        else if (SpawnStackerObjects.numOfExtractedObjs % 3 == 2)    
            return CaseManager(hit, 49, 5);

        return true;
    }

    // Checks if the list of objects contains the items, used to make "CheckIfObjIsSelectable" Method shorter and faster
    bool CaseManager(RaycastHit hit, int index, int count)
    {
        List<GameObject> newList = new List<GameObject>();
        newList = SpawnStackerObjects.stackers.GetRange(index, count);

        if (newList.Contains(hit.transform.gameObject))
        {
            return false;
        }

        return true;
    }

    // Checks the distance between mouse down and up, if smaller than x, it was a touch 
    bool CheckIfWasATouch()
    {
        if (Application.isMobilePlatform)
        {
            if (Input.GetMouseButtonDown(0))
            {
                downTouchPos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                upTouchPos = Input.mousePosition;

                if (Mathf.Abs(upTouchPos.x - downTouchPos.x) < Screen.width / 25 && Mathf.Abs(upTouchPos.y - downTouchPos.y) < Screen.width / 25)
                {
                    return true;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                downTouchPos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                upTouchPos = Input.mousePosition;

                if (Mathf.Abs(upTouchPos.x - downTouchPos.x) < 1f && Mathf.Abs(upTouchPos.y - downTouchPos.y) < 1f)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
