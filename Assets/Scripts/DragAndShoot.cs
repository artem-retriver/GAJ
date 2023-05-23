using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DragAndShoot : MonoBehaviour
{
    public GameObject[] fvxParticle;
    public CharacterCoinsTerrain[] characterCoinsTerrain;
    public Rigidbody[] ragDol;
    public BoxCollider[] boxCollider;
    public GameObject[] cameraObj;
    public CharacterCoins[] characterCoins;
    public GameObject[] slingshot;

    //public new Camera camera = new();
    //public SwipeController swipeController;
    public GameObject playerFlying;
    public TextMeshProUGUI countLifeText;
    public Trajectory trajectory;
    public MainCamera mainCamera;
    public Rigidbody rb;
    Animator anim;
    public SpringsJoints[] springJoint;

    readonly float releaseTime = 0.15f;
    public int countlife = 1;
    public float power = 1;

    public bool _isPressed;
    public bool _isFired;
    public bool _isFlying;
    public bool _isWin;
    public bool _isLose;
    public bool _isCoin;
    public bool _isSafeZoneEnd;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            Vector3 pos1 = new(137, 2.852f, 390);
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y - 80f, 22f));

            Vector3 speed = (pos - new Vector3(transform.position.x, transform.position.y + 2, transform.position.z)) * power;
            Vector3 speed1 = (pos1 - new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z)) * power;

            //if (Rb.position.z >= 454f && Rb.position.z <= 462f)
            //{
            rb.position = worldPosition;

            if (_isWin == false)
            {
                trajectory.ShowTrajectory(pos, speed);
            }
            else
            {
                trajectory.ShowTrajectory(pos1, speed1);
            }
            

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

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Coin>())
        {
            Destroy(other.gameObject);
            _isCoin = true;
            //characterCoins[0].gameObject.SetActive(true);
            characterCoins[0].ActiveObject();
        }

        if (other.gameObject.GetComponent<CoinTerrain>())
        {
            Destroy(other.gameObject);
            for (int i = 0; i < characterCoinsTerrain.Length; i++)
            {
                characterCoinsTerrain[i].gameObject.SetActive(true);
            }
        }

        if (other.gameObject.GetComponent<SecondLevel>())
        {
            cameraObj[1].SetActive(false);
            cameraObj[2].SetActive(true);
            cameraObj[3].SetActive(true);
            rb.useGravity = false;
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            transform.position = new Vector3(135, 10, transform.position.z);
            
            GetComponent<SwipeController>().enabled = true;
            GetComponent<PlayerController>().enabled = true;
            GetComponent<MoveController>().enabled = true;
            //GetComponent<DragAndShoot>().enabled = false;
            //playerFlying.active = true;
            //gameObject.active = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Terrain>() || collision.gameObject.GetComponent<Rocks>())
        {
            Debug.Log("Death");
            countlife--;
            rb.isKinematic = true;
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
            rb.isKinematic = true;

            slingshot[0].SetActive(false);
            slingshot[1].SetActive(true);
            slingshot[2].SetActive(false);

            _isFlying = false;
            _isPressed = false;
            _isFired = false;

            StartCoroutine(IsWin());
        }
        else if(collision.gameObject.GetComponent<SafeZoneEnd>())
        {
            _isSafeZoneEnd = true;
            _isWin = false;
            cameraObj[0].SetActive(true);
            cameraObj[2].SetActive(true);
            cameraObj[3].SetActive(false);

            GetComponent<SwipeController>().enabled = false;
            GetComponent<PlayerController>().enabled = false;
            GetComponent<MoveController>().enabled = false;

            for (int i = 0; i < characterCoinsTerrain.Length; i++)
            {
                characterCoinsTerrain[i].gameObject.SetActive(false);

                if (characterCoinsTerrain[i]._isAlive == true)
                {
                    countlife++;
                }
            }

            Debug.Log("End Win!!!!!!!!!!!!!!");
            collision.collider.isTrigger = true;
            anim.Play("Seatled");
            //countlife++;
            transform.position = new Vector3(135, 13.38f, 185.557f);
            transform.rotation = new Quaternion(0, 179, 0, 0);
            rb.isKinematic = true;

            
        }
    }

    void OnMouseDown()
    {
        if (_isFired)
        {
            return;
        }
        trajectory.gameObject.SetActive(true);
        _isPressed = true;
        rb.isKinematic = true;
        
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
        rb.isKinematic = false;

        

        /*if(_isWin == false)
        {
            mainCamera.MoveAfterSafeZone();
        }*/

        mainCamera.MoveAfterUp();


        cameraObj[0].SetActive(false);
        cameraObj[1].SetActive(true);
        cameraObj[2].SetActive(false);

        _isFired = true;
        _isFlying = true;

        boxCollider[0].size = new Vector3(1, 0.5f, 1);
        anim.Play("Flying");

        if (springJoint[0]._isActive == true)
        {
            StartCoroutine(ReleaseSecondJoint());
        }
        else
        {
            StartCoroutine(ReleaseFirstJoint());
        }
    }

    public IEnumerator IsWin()
    {
        yield return new WaitForSeconds(2f);
        cameraObj[0].SetActive(true);
        cameraObj[1].SetActive(false);
        cameraObj[2].SetActive(true);
        
        _isWin = true;
    }

    IEnumerator ReleaseFirstJoint()
    {
        yield return new WaitForSeconds(releaseTime);
        springJoint[0]._isActive = true;
        springJoint[0].gameObject.SetActive(false);
    }

    IEnumerator ReleaseSecondJoint()
    {
        yield return new WaitForSeconds(releaseTime);

        springJoint[1].gameObject.SetActive(false);
    }
}
