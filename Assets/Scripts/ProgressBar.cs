using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public GameObject startPos;
    public GameObject endPos;

    Image progressBar;
    float maxDistance;

    private void Start()
    {
        progressBar = GetComponent<Image>();
        maxDistance = endPos.transform.position.z;

        progressBar.fillAmount = startPos.transform.position.z / maxDistance;
    }

    private void Update()
    {
        if (progressBar.fillAmount < 1)
        {
            progressBar.fillAmount = startPos.transform.position.z / maxDistance;
        }
    }
}
