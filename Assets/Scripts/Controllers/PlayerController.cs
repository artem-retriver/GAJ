using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MoveController moveController;
    private Animator animator;
    //Rigidbody rigidbody;
    private bool isAlive = true;

    private void Start()
    {
        //rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        moveController = GetComponent<MoveController>();
        animator.Play("Flying");
        //rigidbody.isKinematic = false;
    }

    public void Update()
    {
        moveController.InputHandler();
        moveController.Movebale();
    }

    public void FixedUpdate()
    {
        if (isAlive == true)
        {
            moveController.Move();
        }
        else
        {
            moveController.UnMove();
        }
        return;
    }



    /*public void IsAlive()
    {
        StartCoroutine(WaitGameCoroutine());
    }

    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Fish fish))
        {
            sourceFishBone.Play();
            _gameManager.IncreaseCoins();
            other.gameObject.SetActive(false);
        }

        if (other.TryGetComponent(out Obstacle obstacle))
        {
            sourceSmash.Play();
            Died();
            StartCoroutine(WaitLoseCoroutine());
        }
    }*/

    private void Died()
    {
        isAlive = false;
        //anim.Play("Death_1");
    }
}
