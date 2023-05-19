using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    Image progressBar;
    float maxDistance;
    public bool _isWin;

    private void Start()
    {
        progressBar = GetComponent<Image>();
        maxDistance = GetDistance();
    }

    private void Update()
    {
        float distance = 1 - (GetDistance() / maxDistance);
        if (startPos.position.z >= endPos.position.z)
        {
            
            SetProgress(distance);
        }
        else
        {
            SetProgress(distance + 1f);
        }
    }

    public float GetDistance()
    {
        return Vector3.Distance(startPos.position, endPos.position);
    }

    public void SetProgress(float d)
    {
        progressBar.fillAmount = d;
        Debug.Log(d);
    }
}
