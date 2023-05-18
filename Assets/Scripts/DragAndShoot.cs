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
    public CharacterCoins[] characterCoins;

    //public new Camera camera = new();
    public TextMeshProUGUI countLifeText;
    public Trajectory trajectory;
    public MainCamera mainCamera;
    public Slingshot slingshot;
    public Rigidbody Rb;
    Animator anim;
    public SpringJoint springJoint;

    readonly float ReleaseTime = 0.15f;
    public int countlife = 1;
    public float power = 1;

    public bool _isPressed;
    public bool _isFired;
    public bool _isFlying;
    public bool _isWin;
    public bool _isLose;
    public bool _isCoin;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //springJoint = GetComponent<SpringJoint>();

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
            Vector3 pos = new(137, 2.852f, 457.57f);
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y - 80f, 22f));

            Vector3 speed = (pos - new Vector3(transform.position.x, transform.position.y + 2, transform.position.z)) * power;
            
            //if (Rb.position.z >= 454f && Rb.position.z <= 462f)
            //{
            
            Rb.position = worldPosition;
            trajectory.ShowTrajectory(pos, speed);

            //}
        }

        if (characterCoins[0]._isWin == true)
        {
            StartCoroutine(IsWin());
        }

        if (_isFlying == true)
        {
            trajectory.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Coin>())
        {
            Destroy(other.gameObject);
            _isCoin = true;
            //characterCoins[0].gameObject.SetActive(true);
            characterCoins[0].ActiveObject();
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
            _isLose = true;

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
    }

    [System.Obsolete]
    void OnMouseUp()
    {
        //var coll = gameObject.GetComponent<BoxCollider>();

        if (_isFired)
        {
            return;
        }

        _isPressed = false;
        Rb.isKinematic = false;

        mainCamera.MoveAfterUp();

        cameraObj[0].SetActive(false);
        cameraObj[1].SetActive(true);
        cameraObj[2].SetActive(false);

        _isFired = true;
        _isFlying = true;

        boxCollider[0].size = new Vector3(1, 0.5f, 1);
        anim.Play("Flying");

        StartCoroutine(Release());
    }

    public IEnumerator IsWin()
    {
        yield return new WaitForSeconds(2f);
        cameraObj[0].SetActive(true);
        cameraObj[1].SetActive(false);
        cameraObj[2].SetActive(true);
        _isWin = true;
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(ReleaseTime);

        Destroy(GetComponent<SpringJoint>());
    }
}
