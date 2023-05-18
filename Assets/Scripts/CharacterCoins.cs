using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterCoins : MonoBehaviour
{
    public GameObject[] fvxParticle;
    public Rigidbody[] ragDol;

    public SpringJoint springJoint;
    public DragAndShoot dragAnd;
    public SkinnedMeshRenderer meshRenderer;
    Rigidbody rb;
    Animator anim;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        transform.DOMoveZ(419, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z == 419 && dragAnd._isCoin == false)
        {
              meshRenderer.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Terrain>() || collision.gameObject.GetComponent<Rocks>() || collision.gameObject.GetComponent<DragAndShoot>() && dragAnd._isLose == true)
        {
            Debug.Log("Death");
            rb.isKinematic = true;
            fvxParticle[0].SetActive(true);
            anim.enabled = false;

            for (int i = 0; i < ragDol.Length; i++)
            {
                ragDol[i].gameObject.SetActive(true);
            }
        }
        else if (collision.gameObject.GetComponent<SafeZone>())
        {
            Debug.Log("Win!!!");
            collision.collider.isTrigger = true;
            anim.Play("Seatled");
            transform.position = new Vector3(137, 1.062f, 389.982f);
            fvxParticle[1].SetActive(true);
            rb.isKinematic = true;
        }
    }

    public void ActiveObject()
    {
        meshRenderer.enabled = true;
        anim.Play("Flying");
        rb.isKinematic = false;
        StartCoroutine(DestroySpring());
    }

    IEnumerator DestroySpring()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(springJoint.gameObject);
    }
}
