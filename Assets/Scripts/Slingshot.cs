using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [SerializeField] private Transform pointLeft;
    [SerializeField] private Transform pointRight;
    [SerializeField] public DragAndShoot drag;

    public LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(pointLeft && pointRight)
        {
            _lineRenderer.positionCount = 3;
            _lineRenderer.SetPosition(0, pointLeft.position);
            _lineRenderer.SetPosition(1, new Vector3(drag.Rb.position.x, drag.Rb.position.y + 1.5f, drag.Rb.position.z + 0.8f));
            _lineRenderer.SetPosition(2, pointRight.position);

            if (drag._isFlying == true)
            {
                _lineRenderer.SetPosition(1, pointLeft.position);
            }
        }


    }
}
