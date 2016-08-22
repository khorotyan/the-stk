using UnityEngine;
using System.Collections;

public class ObjectCollisionManager : MonoBehaviour
{
    private Collider lastCollider = null;

    private int collisionCount = 0;
    private bool noCollision = false;
    private bool canPlaceAtTop = false;

	void Start ()
    {
	
	}
	
	void Update ()
    {
        ManageCollision();
	}

    void ManageCollision()
    {
        // If there is no collision, move the object to the top of the tower and reset all its physics information
        if (noCollision == true && collisionCount == 0)
        {
            ObjTopPlcManager();

            if (canPlaceAtTop == true)
            {
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, SpawnStackerObjects.maxHeight + 3f, Camera.main.transform.position.z);

                // Resets the force information
                DestroyImmediate(gameObject.GetComponent<ConstantForce>());
                gameObject.AddComponent<ConstantForce>();

                // Resets other physics information
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;

                // Removes the item and puts it at the end of the list to have more controll over the objects
                SpawnStackerObjects.stackers.Remove(gameObject);
                SpawnStackerObjects.stackers.Add(gameObject);

                SpawnStackerObjects.numOfExtractedObjs++;
                SelectNMoveObject.canMoveTheObject = false;
                noCollision = false;
                canPlaceAtTop = false;
            }
        }
    }

    // Configures the position and the rotation of the objects that were extracted from the tower
    void ObjTopPlcManager()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        gameObject.GetComponent<ConstantForce>().force = new Vector3(0, 1500, 0);

        StartCoroutine(WaitBeforePlace());

        if (canPlaceAtTop == true)
        {
            if (SpawnStackerObjects.numOfExtractedObjs % 6 == 0)
            {
                gameObject.transform.position = new Vector3(-1.02f, SpawnStackerObjects.maxHeight + 0.05f, 0);
                gameObject.transform.eulerAngles = new Vector3(0, 90, 0);

            }
            else if (SpawnStackerObjects.numOfExtractedObjs % 6 == 1)
            {
                gameObject.transform.position = new Vector3(0, SpawnStackerObjects.maxHeight + 0.05f, 0);
                gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else if (SpawnStackerObjects.numOfExtractedObjs % 6 == 2)
            {
                gameObject.transform.position = new Vector3(1.02f, SpawnStackerObjects.maxHeight + 0.05f, 0);
                gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else if (SpawnStackerObjects.numOfExtractedObjs % 6 == 3)
            {
                gameObject.transform.position = new Vector3(0, SpawnStackerObjects.maxHeight + 0.05f, 1.02f);
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (SpawnStackerObjects.numOfExtractedObjs % 6 == 4)
            {
                gameObject.transform.position = new Vector3(0, SpawnStackerObjects.maxHeight + 0.05f, 0);
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (SpawnStackerObjects.numOfExtractedObjs % 6 == 5)
            {
                gameObject.transform.position = new Vector3(0, SpawnStackerObjects.maxHeight + 0.05f, -1.02f);
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "StackerObject")
            collisionCount++;
    }

    void OnTriggerStay(Collider collider)
    {
        noCollision = false;
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "StackerObject")
            collisionCount--;

        noCollision = true;
        lastCollider = collider;
    }

    IEnumerator WaitBeforePlace()
    {
        yield return new WaitForSeconds(1f);
        canPlaceAtTop = true;
    }
}
