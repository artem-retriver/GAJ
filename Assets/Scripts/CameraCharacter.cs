using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraCharacter : MonoBehaviour
{
    private Camera _camera;
    private int zoom = 40;
    private int speed = 2;
    private bool _isFlying;

    void Start()
    {
        _camera = GetComponent<Camera>();
        
    }

    private void Update()
    {
        //_camera.fieldOfView = Mathf.MoveTowards(60, 40, speed * Time.deltaTime);
        if (_isFlying == false)
        {
            MoveCamera();
        }
        else
        {
            FarCamera();
        }
        
    }

    public void MoveCamera()
    {
        //Vector3 endValue = new Vector3(125, 7, 457)
        //transform.DOMoveZ(457, 2);
        transform.DOMoveX(125, 1.5f);
        StartCoroutine(IsFlying());
    }

    public void FarCamera()
    {
        transform.DOMoveX(115, 1.5f);
    }

    IEnumerator IsFlying()
    {
        yield return new WaitForSeconds(1.5f);
        _isFlying = true;
    }
}
