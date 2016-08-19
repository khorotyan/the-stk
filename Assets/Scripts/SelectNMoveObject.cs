using UnityEngine;
using System.Collections.Generic;

public class SelectNMoveObject : MonoBehaviour {

    public Material normalObjMaterial;
    public Material selectedObjMaterial;

    private List<GameObject> rayastedObjects = new List<GameObject>();

	void Start ()
    {
	
	}
	
	void Update ()
    {
        SelectObject();
	}

    void SelectObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 50f))
            {
                if (hit.transform.gameObject.tag == "StackerObject") // It is the blocks layer
                {
                    if (rayastedObjects.Count > 0)
                    {
                        rayastedObjects[0].transform.gameObject.GetComponent<Renderer>().material = normalObjMaterial;
                        rayastedObjects.Remove(rayastedObjects[0]);
                    }
                    
                    hit.transform.gameObject.GetComponent<Renderer>().material = selectedObjMaterial;
                    rayastedObjects.Add(hit.transform.gameObject);
                }
            }
        }
    }
}
