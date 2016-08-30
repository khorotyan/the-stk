using UnityEngine;
using System.Collections;

public class StaticContainer : MonoBehaviour
{
    public static string username = "Player";

    void Start()
    {
        Debug.Log(ScoreManage.highestScore);
    }
}
