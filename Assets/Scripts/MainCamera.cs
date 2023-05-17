using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCamera : MonoBehaviour
{
    public DragAndShoot character;
    public bool _isSinema = true;
    public bool _isClose = false;
    //public bool _isFar;

    private void Start()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, 320f);
        //StartCoroutine(StartSinema());
    }

    private void FixedUpdate()
    {
        if (character._isFlying == true && _isClose == false)
        {
           
        }
        else if(character._isWin == true && _isClose == true)
        {
            StartCoroutine(MoveAfterWin());
        }

        if(character._isFlying == true && _isSinema == true)
        {
            //var zoomX = transform.DOMoveX(Character.transform.position.x - 8, 1.5f);
            //var startValue = new Vector3(Character.Rb.position.x - 8, Character.Rb.position.y + 2, Character.Rb.position.z);
            //transform.DOMoveX(125, 1.2f);
            //StartCoroutine(OffSinema());
        }

        if(character._isFlying == true && _isSinema == false)
        {
            //var endValue = new Vector3(Character.transform.position.x - 17, Character.transform.position.y + 5, Character.transform.position.z - 5);
            //transform.DOMoveX(115, 1.2f);
        }
    }

    public void MoveAfterUp()
    {
        transform.DOMoveZ(389.2245f, 0);
        _isClose = true;
    }

    IEnumerator OffSinema()
    {
        yield return new WaitForSeconds(1.2f);
        _isSinema = false;
    }

    IEnumerator MoveAfterWin()
    {
        yield return new WaitForSeconds(0.5f);
        transform.DOMoveZ(385, 1);
    }

    /*public IEnumerator CloseZoomCamera()
    {
        yield return new WaitForSeconds(0);
        var endValue = new Vector3(character.transform.position.x - 10, character.transform.position.y + 2, character.transform.position.z);
        transform.DOMove(endValue, 1.5f);
        StartCoroutine(FarZoomCamera());
    }

    public IEnumerator FarZoomCamera()
    {
        yield return new WaitForSeconds(1.5f);
        var endValue = new Vector3(character.transform.position.x - 17, character.transform.position.y + 5, character.transform.position.z + 3);
        transform.DOMove(endValue, 1.5f);
    }*/

    IEnumerator StartSinema()
    {
        yield return new WaitForSeconds(2f);
        transform.DOMoveZ(452, 3);
    }
}
