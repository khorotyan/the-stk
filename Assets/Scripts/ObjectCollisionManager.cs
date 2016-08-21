using UnityEngine;
using System.Collections;

public class ObjectCollisionManager : MonoBehaviour
{
    private Collider lastCollider = null;

    private int collisionCount = 0;
    private bool noCollision = false;

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
        if (noCollision == true)
        {
            gameObject.transform.position = new Vector3(0, SpawnStackerObjects.maxHeight + 4f, 0);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x , SpawnStackerObjects.maxHeight + 5f, Camera.main.transform.position.z);

            gameObject.transform.rotation = Quaternion.identity;

            DestroyImmediate(gameObject.GetComponent<ConstantForce>());
            gameObject.AddComponent<ConstantForce>();

            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

            SelectNMoveObject.canMoveTheObject = false;
            noCollision = false;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        noCollision = false;
    }

    void OnTriggerExit(Collider collider)
    {
        noCollision = true;
        lastCollider = collider;
    }

}
