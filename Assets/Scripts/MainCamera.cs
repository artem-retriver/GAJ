using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCamera : MonoBehaviour
{
    public DragAndShoot Character;
    public bool _isSinema = true;
    public bool _isClose;
    public bool _isFar;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 320f);
        StartCoroutine(StartSinema());
    }

    private void FixedUpdate()
    {
        //if(transform.position.z == 320 && _isSinema == true)
        //{
            
        //    _isSinema = false;
        //}

        if(Character._isFlying == true && _isSinema == true)
        {
            var startValue = new Vector3(Character.transform.position.x - 8, Character.transform.position.y + 2, Character.transform.position.z - 7);
            transform.DOMove(startValue, 1.5f);
            StartCoroutine(OffSinema());
        }

        if(Character._isFlying == true && _isSinema == false)
        {
            var endValue = new Vector3(Character.transform.position.x - 17, Character.transform.position.y + 5, Character.transform.position.z - 5);
            transform.DOMove(endValue, 1.3f);
        }
    }

    IEnumerator OffSinema()
    {
        yield return new WaitForSeconds(1.5f);
        _isSinema = false;
    }

    public IEnumerator CloseZoomCamera()
    {
        yield return new WaitForSeconds(0);
        var endValue = new Vector3(Character.transform.position.x - 10, Character.transform.position.y + 2, Character.transform.position.z);
        transform.DOMove(endValue, 1.5f);
        StartCoroutine(FarZoomCamera());
    }

    public IEnumerator FarZoomCamera()
    {
        yield return new WaitForSeconds(1.5f);
        var endValue = new Vector3(Character.transform.position.x - 17, Character.transform.position.y + 5, Character.transform.position.z + 3);
        transform.DOMove(endValue, 1.5f);
    }

    IEnumerator StartSinema()
    {
        yield return new WaitForSeconds(2f);
        transform.DOMoveZ(452, 3);
    }
}
