using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    /// <summary>
    /// Stores the X and Y axis movement values of the Player
    /// </summary>
    private Vector2 _movement;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Canvas _canvas;
    private Text _popInteractText;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _canvas = GetComponentInChildren<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _popInteractText = _canvas.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        //Position moved in FixedUpdate so that it gives more accurate results since the Players rigidbody affects Physics 
        _rigidbody.MovePosition(_rigidbody.position + _movement * speed * Time.fixedDeltaTime); 
    }

    /// <summary>
    /// Allows the Player to Move
    /// </summary>
    private void Move()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        //Triggers walking animations based on x and y value
        _animator.SetFloat("Horizontal", _movement.x);
        _animator.SetFloat("Vertical", _movement.y);
        //Stores current Speed of the Player to determine if Player is moving or not.
        _animator.SetFloat("Speed", _movement.sqrMagnitude);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Trigger"))
        {
            if (col.GetComponent<Trigger>() is Trigger trigger)
            {
                if (trigger.triggerType == Trigger.TriggerType.ShopEntrance)
                {
                    trigger.GetComponent<DialogueTrigger>().TriggerGreetings();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Trigger"))
        {
            if (col.GetComponent<Trigger>() is Trigger trigger)
            {
                if (trigger.triggerType == Trigger.TriggerType.ShopCounter)
                {
                    ShowInteractAction("Click E to Talk");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Trigger"))
        {
            if (col.GetComponent<Trigger>() is Trigger trigger)
            {
                if (trigger.triggerType == Trigger.TriggerType.ShopCounter)
                {
                    _canvas.enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// Pops up an Interact Action Hover Message telling players to press a button inorder to interact with something.
    /// </summary>
    private void ShowInteractAction(string action)
    {
        _popInteractText.text = action;
        _canvas.enabled = true;
    }
}
