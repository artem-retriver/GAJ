using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DragAndShoot : MonoBehaviour
{
    public GameObject[] fvxParticle;
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
        _isFired = true;
        _isFlying = true;
        coll.size = new Vector3(1, 0.5f, 1);
        anim.Play("Flying");
        StartCoroutine(Release());
        //StartCoroutine(mainCamera.CloseZoomCamera());
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(ReleaseTime);
        Destroy(GetComponent<SpringJoint>());
    }
}
