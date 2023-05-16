using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Eagle : MonoBehaviour
{
    public GameObject[] gameObjects;

    private void Update()
    {
        int randomOne = Random.Range(6, 10);
        int randomTwo = Random.Range(8, 12);

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].transform.position.z == 260)
            {
                gameObjects[i].transform.rotation = new Quaternion(0, 0, 0, 0);
                gameObjects[i].transform.DOMoveZ(534, randomOne);
                Debug.Log(randomOne);
            }
            else if (gameObjects[i].transform.position.z == 534)
            {
                gameObjects[i].transform.rotation = new Quaternion(0, 180, 0, 0);
                gameObjects[i].transform.DOMoveZ(260, randomTwo);
                Debug.Log(randomOne);
            }
        }
        
    }
}
