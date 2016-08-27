using UnityEngine;
using System.Collections;

public class MainMenuAnimConfig : MonoBehaviour
{
    public GameObject objContainer;

	void Start ()
    {
	
	}

	void Update ()
    {
        RotateTheObjs();
	}

    void RotateTheObjs()
    {

        float rotateSpeed = 5 * Time.deltaTime;
        Vector3 objRotVec = new Vector3(1, 0, 1);

        objContainer.transform.RotateAround(objRotVec, Vector3.up, rotateSpeed);
        Camera.main.transform.RotateAround(Vector3.zero, Vector3.up, -rotateSpeed * 3);
    }   
}
