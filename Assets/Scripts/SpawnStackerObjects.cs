using UnityEngine;
using System.Collections.Generic;

public class SpawnStackerObjects : MonoBehaviour
{
    public GameObject stackerObj;
    public Transform stackerObjParent;

    public static int numOfExtractedObjs = 0;
    public static float maxHeight = 0;
    public static List<GameObject> stackers = new List<GameObject>();

    void Start ()
    {
        SpawnObjects();
    }
	
	void Update ()
    {
       
	}

    // Spawn the tower at the beginning of the game
    void SpawnObjects()
    {
        float height = 0;

        for (int i = 0; i < 18; i++)
        {
            if (i % 2 == 0)
            {
                GameObject tempSpawner0 = Instantiate(stackerObj, new Vector3(-1.02f, height, 0), Quaternion.Euler(0, 90, 0), stackerObjParent) as GameObject;
                GameObject tempSpawner1 = Instantiate(stackerObj, new Vector3(0, height, 0), Quaternion.Euler(0, 90, 0), stackerObjParent) as GameObject;
                GameObject tempSpawner2 = Instantiate(stackerObj, new Vector3(1.02f, height, 0), Quaternion.Euler(0, 90, 0), stackerObjParent) as GameObject;

                stackers.Add(tempSpawner0);
                stackers.Add(tempSpawner1);
                stackers.Add(tempSpawner2);
            }
            else
            {
                GameObject tempSpawner0 = Instantiate(stackerObj, new Vector3(0, height, -1.02f), Quaternion.Euler(0, 0, 0), stackerObjParent) as GameObject;
                GameObject tempSpawner1 = Instantiate(stackerObj, new Vector3(0, height, 0), Quaternion.Euler(0, 0, 0), stackerObjParent) as GameObject;
                GameObject tempSpawner2 = Instantiate(stackerObj, new Vector3(0, height, 1.02f), Quaternion.Euler(0, 0, 0), stackerObjParent) as GameObject;

                stackers.Add(tempSpawner0);
                stackers.Add(tempSpawner1);
                stackers.Add(tempSpawner2);
            }

            maxHeight += 0.5f;
            height += Random.Range(0.5f, 0.64f);
        }
    }

    public void RestartScene()
    {
        SelectNMoveObject.canMoveTheObject = true;
        numOfExtractedObjs = 0;
        maxHeight = 0;

        ResetCombosScoreNCoins();

        RebuildTheTower();
    }

    // Whenever the game is reset, the objects get back to their place and we reset all the information about them
    void RebuildTheTower()
    {
        stackers.Clear();

        foreach (Transform child in stackerObjParent)
            Destroy(child.gameObject);

        SpawnObjects();
    }

    void ResetCombosScoreNCoins()
    {
        ComboManager.currentMultiplier = 1;
        ComboManager.currentCombo = 0;
        ComboManager.comboSlider.value = 0;

        ScoreManage.currentScore = 0;

        CoinManager.currentCoins = 0;
    }
}
