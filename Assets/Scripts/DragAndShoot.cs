using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DragAndShoot : MonoBehaviour
{
    public GameObject[] fvxParticle;
    public Rigidbody[] ragDol;
    public BoxCollider[] boxCollider;
    public GameObject[] cameraObj;

    public TextMeshProUGUI countLifeText;
    public MainCamera mainCamera;
    public Slingshot slingshot;
    public Rigidbody Rb;
    private Animator anim;
    //private Collider coll;
    readonly float ReleaseTime = 0.15f;
    public int countlife = 1;
    public bool _isPressed;
    public bool _isFired;
    public bool _isFlying;
    public bool _isWin;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //countLifeText = GetComponent<TextMeshProUGUI>();

        for (int i = 0; i < ragDol.Length; i++)
        {
            ragDol[i].isKinematic = false;
            ragDol[i].gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        countLifeText.text = countlife.ToString();

        if (_isPressed && !_isFired)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y - 2f, 22f));
            
            //if (Rb.position.z >= 454f && Rb.position.z <= 462f)
            //{
                Rb.position = worldPosition;
            //}
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Terrain>() || collision.gameObject.GetComponent<Rocks>())
        {
            Debug.Log("Death");
            countlife--;
            Rb.isKinematic = true;
            fvxParticle[1].SetActive(true);
            anim.enabled = false;
            boxCollider[0].isTrigger = true;

            for (int i = 0; i < ragDol.Length; i++)
            {
                ragDol[i].gameObject.SetActive(true);
            }
        }
        else if(collision.gameObject.GetComponent<SafeZone>())
        {
            Debug.Log("Win!!!");
            collision.collider.isTrigger = true;
            anim.Play("Seatled");
            countlife++;
            transform.position = new Vector3(137, 1.062f, 389.982f);
            fvxParticle[0].SetActive(true);
            Rb.isKinematic = true;

            StartCoroutine(IsWin());
        }
    }

    void OnMouseDown()
    {
        if (_isFired)
        {
            return;
        }

        _isPressed = true;
        Rb.isKinematic = true;

        //anim.enabled = false;

        //for (int i = 0; i < ragDol.Length; i++)
        //{
            //ragDol[i].isKinematic = false;
        //    ragDol[i].gameObject.SetActive(true);
        //}
    }

    void OnMouseUp()
    {
        var coll = gameObject.GetComponent<BoxCollider>();

        if (_isFired)
        {
            return;
        }

        _isPressed = false;
        Rb.isKinematic = false;
        cameraObj[0].SetActive(false);
        cameraObj[1].SetActive(true);
        cameraObj[2].SetActive(false);
        _isFired = true;
        _isFlying = true;
        coll.size = new Vector3(1, 0.5f, 1);
        //anim.enabled = false;
        anim.Play("Flying");
        StartCoroutine(Release());

        //StartCoroutine(mainCamera.CloseZoomCamera());
    }

    IEnumerator IsWin()
    {
        yield return new WaitForSeconds(2f);

        cameraObj[1].SetActive(false);
        cameraObj[0].SetActive(true);
        cameraObj[2].SetActive(true);
        _isWin = true;
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(ReleaseTime);
        Destroy(GetComponent<SpringJoint>());
    }
}
