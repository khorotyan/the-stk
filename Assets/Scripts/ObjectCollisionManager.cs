﻿using UnityEngine;
using System.Collections;

public class ObjectCollisionManager : MonoBehaviour
{
    private GameOverManager gom;

    private int collisionCount = 0;
    private bool noCollision = false;
    private bool canPlaceAtTop = false;
    private bool itemJustGotOut = false;
    private bool canCheckGameOver = false;

	void Awake ()
    {
        GameObject scriptManager = GameObject.Find("ScriptManager");
        gom = scriptManager.GetComponent<GameOverManager>();
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
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, SpawnStackerObjects.maxHeight + 1f, Camera.main.transform.position.z);

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
                noCollision = false;
                canPlaceAtTop = false;
            }
        }
    }

    // Configures the position and the rotation of the objects that were extracted from the tower
    void ObjTopPlcManager()
    {
        MoveCam.canMoveTheCam = true;
        SelectNMoveObject.canMoveTheObject = false;

        if (itemJustGotOut == false)
        {
            ComboManager.currentCombo++; // + 1 Combo
            ComboManager.comboSlider.value = ComboManager.comboTimerLength; // Resets the combo timer

            itemJustGotOut = true;
        }

        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

        gameObject.GetComponent<ConstantForce>().force = new Vector3(0, 1500, 0);

        StartCoroutine(WaitBeforePlace());

        // The extracted item can now be put on top of the tower
        if (canPlaceAtTop == true)
        {
            // Can Add the score and the coins for putting the object
            ScoreManage.currentScore += ScoreManage.scorePerPlacement * ComboManager.currentMultiplier;
            CoinManager.currentCoins += CoinManager.coinsPerPlacement * ComboManager.currentMultiplier;

            itemJustGotOut = false;

            if (SpawnStackerObjects.numOfExtractedObjs % 6 == 0)
            {
                float extraXRot = SpawnStackerObjects.stackers[52].transform.eulerAngles.x;
                float extraYRot = SpawnStackerObjects.stackers[52].transform.eulerAngles.y;
                float extraZRot = SpawnStackerObjects.stackers[52].transform.eulerAngles.z;

                float pieceOfXPos = SpawnStackerObjects.stackers[52].transform.position.x;
                float pieceOfZPos = SpawnStackerObjects.stackers[52].transform.position.z;

                gameObject.transform.position = new Vector3(-1.02f + pieceOfXPos, SpawnStackerObjects.maxHeight + 0.1f, 0 + pieceOfZPos);
                gameObject.transform.eulerAngles = new Vector3(extraXRot, 90 + extraYRot, -extraZRot);
                
            }
            else if (SpawnStackerObjects.numOfExtractedObjs % 6 == 1)
            {
                float extraXRot = SpawnStackerObjects.stackers[51].transform.eulerAngles.x;
                float extraYRot = SpawnStackerObjects.stackers[51].transform.eulerAngles.y;
                float extraZRot = SpawnStackerObjects.stackers[51].transform.eulerAngles.z;

                float pieceOfXPos = SpawnStackerObjects.stackers[51].transform.position.x;
                float pieceOfZPos = SpawnStackerObjects.stackers[51].transform.position.z;

                gameObject.transform.position = new Vector3(0 + pieceOfXPos, SpawnStackerObjects.maxHeight + 0.1f, 0 + pieceOfZPos);
                gameObject.transform.eulerAngles = new Vector3(extraXRot, 90 + extraYRot, -extraZRot);
            }
            else if (SpawnStackerObjects.numOfExtractedObjs % 6 == 2)
            {
                float extraXRot = SpawnStackerObjects.stackers[50].transform.eulerAngles.x;
                float extraYRot = SpawnStackerObjects.stackers[50].transform.eulerAngles.y;
                float extraZRot = SpawnStackerObjects.stackers[50].transform.eulerAngles.z;

                float pieceOfXPos = SpawnStackerObjects.stackers[50].transform.position.x;
                float pieceOfZPos = SpawnStackerObjects.stackers[50].transform.position.z;

                gameObject.transform.position = new Vector3(1.02f + pieceOfXPos, SpawnStackerObjects.maxHeight + 0.1f, 0 + pieceOfZPos);
                gameObject.transform.eulerAngles = new Vector3(extraXRot, 90 + extraYRot, -extraZRot);
            }
            else if (SpawnStackerObjects.numOfExtractedObjs % 6 == 3)
            {
                float extraXRot = SpawnStackerObjects.stackers[52].transform.eulerAngles.x;
                float extraYRot = SpawnStackerObjects.stackers[52].transform.eulerAngles.y - 90;
                float extraZRot = SpawnStackerObjects.stackers[52].transform.eulerAngles.z;

                float pieceOfXPos = SpawnStackerObjects.stackers[52].transform.position.x;
                float pieceOfZPos = SpawnStackerObjects.stackers[52].transform.position.z;

                gameObject.transform.position = new Vector3(0 + pieceOfXPos, SpawnStackerObjects.maxHeight + 0.1f, 1.02f + pieceOfZPos);
                gameObject.transform.eulerAngles = new Vector3(extraXRot, 0 + extraYRot, -extraZRot);
            }
            else if (SpawnStackerObjects.numOfExtractedObjs % 6 == 4)
            {
                float extraXRot = SpawnStackerObjects.stackers[51].transform.eulerAngles.x;
                float extraYRot = SpawnStackerObjects.stackers[51].transform.eulerAngles.y - 90;
                float extraZRot = SpawnStackerObjects.stackers[51].transform.eulerAngles.z;

                float pieceOfXPos = SpawnStackerObjects.stackers[51].transform.position.x;
                float pieceOfZPos = SpawnStackerObjects.stackers[51].transform.position.z;

                gameObject.transform.position = new Vector3(0 + pieceOfXPos, SpawnStackerObjects.maxHeight + 0.1f, 0 + pieceOfZPos);
                gameObject.transform.eulerAngles = new Vector3(extraXRot, 0 + extraYRot, -extraZRot);
            }
            else if (SpawnStackerObjects.numOfExtractedObjs % 6 == 5)
            {
                float extraXRot = SpawnStackerObjects.stackers[50].transform.eulerAngles.x;
                float extraYRot = SpawnStackerObjects.stackers[50].transform.eulerAngles.y - 90;
                float extraZRot = SpawnStackerObjects.stackers[50].transform.eulerAngles.z;

                float pieceOfXPos = SpawnStackerObjects.stackers[50].transform.position.x;
                float pieceOfZPos = SpawnStackerObjects.stackers[50].transform.position.z;

                gameObject.transform.position = new Vector3(0 + pieceOfXPos, SpawnStackerObjects.maxHeight + 0.1f, -1.02f + pieceOfZPos);
                gameObject.transform.eulerAngles = new Vector3(extraXRot, 0 + extraYRot, -extraZRot);
            }

            SelectNMoveObject.canMoveTheObject = true;
        }       
    }

    void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(WaitUntillGameOverCheck());
        
        // The Game is Over
        if (canCheckGameOver == true && collision.collider.tag == "Background")
        {
            gom.ManageGameOver();
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
    }

    IEnumerator WaitBeforePlace()
    {
        yield return new WaitForSeconds(1.2f);
        canPlaceAtTop = true;
    }

    IEnumerator WaitUntillGameOverCheck()
    {
        yield return new WaitForSeconds(1f);
        canCheckGameOver = true;
    }
}
