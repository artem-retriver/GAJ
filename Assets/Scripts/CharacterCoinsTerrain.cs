using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCoinsTerrain : MonoBehaviour
{
    public GameObject[] fvxParticle;
    public Rigidbody[] ragDol;
    Animator animator;
    Rigidbody rb;

    public bool _isAlive = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        animator.Play("Flying");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Terrain>())
        {
            Debug.Log("Death");
            rb.isKinematic = true;
            fvxParticle[0].SetActive(true);
            animator.enabled = false;
            _isAlive = false;

            for (int i = 0; i < ragDol.Length; i++)
            {
                ragDol[i].gameObject.SetActive(true);
            }
        }
    }
}
