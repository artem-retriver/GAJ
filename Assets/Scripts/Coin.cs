using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int rotY;

    private void Update()
    {
        rotY++;
        Vector3 newRot = new Vector3(0, rotY, 0);
        transform.eulerAngles = newRot;
    }
}
