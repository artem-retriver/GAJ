using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneEnd : MonoBehaviour
{
    public GameObject[] seatCharacter;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<DragAndShoot>())
        {
            for (int i = 0; i < seatCharacter.Length; i++)
            {
                seatCharacter[i].SetActive(true);
            }
        }
    }
}
