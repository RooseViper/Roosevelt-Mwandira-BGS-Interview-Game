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
    /// <summary>
    /// Decides whether the Player can interact with something when they click the interact button.
    /// </summary>
    private bool _canInteract;
    /// <summary>
    /// Checks if the Player is inside the Shop.
    /// </summary>
    private bool _isInShop;

    private ItemDetector _itemDetector;
    public Animator Animator
    {
        get => _animator;
        set => _animator = value;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _itemDetector = GetComponentInChildren<ItemDetector>();
    }

    private void DestroyPickedUpItem()
    {
        if (_itemDetector.pickableItem != null)
        {
            Destroy(_itemDetector.pickableItem.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsPaused || GameManager.Instance.PlayerIsDormant) return;
        Move();
        Interact();
    }
    /// <summary>
    /// Allows the Player to interact with objects or characters in the Environment.
    /// </summary>
    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_canInteract)
            {
                if (_isInShop)
                {
                    _animator.SetFloat("Speed", 0F);
                    GameManager.Instance.OpenPopUpDialogueBox(); 
                }
            }
            if (_itemDetector.isInRange)
            {
                InventoryManager.Instance.PickupItem(_itemDetector.itemInRange);
                DestroyPickedUpItem();
            }
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryManager.Instance.OpenInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.PauseResumeGame();
        }
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
                    _canInteract = true;
                    _isInShop = true;
                    GameManager.Instance.ShowInteractAction("Click E to Talk");
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
                     GameManager.Instance.interactCanvas.enabled = false;
                    _canInteract = false;
                    _isInShop = false;
                }
            }
        }
    }


}
