using UnityEngine;
using System.Collections.Generic;

public class SpawnStackerObjects : MonoBehaviour
{
    public GameObject stackerObj;
    public Transform stackerObjParent;

    public static List<GameObject> stackers = new List<GameObject>();

	void Start ()
    {
        SpawnObjects();
    }
	
	void Update ()
    {
       
	}

    void SpawnObjects()
    {
        float height = 0;

        for (int i = 0; i < 18; i++)
        {
            if (i % 2 == 0)
            {
                GameObject tempSpawner0 = Instantiate(stackerObj, new Vector3(-1, height, 0), Quaternion.Euler(0, 90, 0), stackerObjParent) as GameObject;
                GameObject tempSpawner1 = Instantiate(stackerObj, new Vector3(0, height, 0), Quaternion.Euler(0, 90, 0), stackerObjParent) as GameObject;
                GameObject tempSpawner2 = Instantiate(stackerObj, new Vector3(1, height, 0), Quaternion.Euler(0, 90, 0), stackerObjParent) as GameObject;

                stackers.Add(tempSpawner0);
                stackers.Add(tempSpawner1);
                stackers.Add(tempSpawner2);
            }
            else
            {
                GameObject tempSpawner0 = Instantiate(stackerObj, new Vector3(0, height, -1), Quaternion.Euler(0, 0, 0), stackerObjParent) as GameObject;
                GameObject tempSpawner1 = Instantiate(stackerObj, new Vector3(0, height, 0), Quaternion.Euler(0, 0, 0), stackerObjParent) as GameObject;
                GameObject tempSpawner2 = Instantiate(stackerObj, new Vector3(0, height, 1), Quaternion.Euler(0, 0, 0), stackerObjParent) as GameObject;

                stackers.Add(tempSpawner0);
                stackers.Add(tempSpawner1);
                stackers.Add(tempSpawner2);
            }

            height += Random.Range(0.51f, 0.66f);
        }
    }

    public void RestartScene()
    {
        RebuildTheTower();
    }

    void RebuildTheTower()
    {
        float height = 0;

        for (int i = 0; i < 9; i++)
        {
            stackers[6 * i].transform.position = new Vector3(-1, height, 0);
            stackers[6 * i].transform.rotation = Quaternion.Euler(0, 90, 0);

            stackers[6 * i + 1].transform.position = new Vector3(0, height, 0);
            stackers[6 * i + 1].transform.rotation = Quaternion.Euler(0, 90, 0);

            stackers[6 * i + 2].transform.position = new Vector3(1, height, 0);
            stackers[6 * i + 2].transform.rotation = Quaternion.Euler(0, 90, 0);

            height += Random.Range(0.51f, 0.64f);

            stackers[6 * i + 3].transform.position = new Vector3(0, height, -1);
            stackers[6 * i + 3].transform.rotation = Quaternion.Euler(0, 0, 0);

            stackers[6 * i + 4].transform.position = new Vector3(0, height, 0);
            stackers[6 * i + 4].transform.rotation = Quaternion.Euler(0, 0, 0);

            stackers[6 * i + 5].transform.position = new Vector3(0, height, 1);
            stackers[6 * i + 5].transform.rotation = Quaternion.Euler(0, 0, 0);

            height += Random.Range(0.5f, 0.65f);
        }

        for (int i = 0; i < 54; i++)
        {
            stackers[i].GetComponent<Rigidbody>().isKinematic = true;
            stackers[i].GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
