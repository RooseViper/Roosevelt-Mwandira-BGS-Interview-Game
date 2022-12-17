using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    /// <summary>
    /// Stores the X and Y axis values of the Player
    /// </summary>
    private Vector2 _movement;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    /// <summary>
    /// Allows the Player to Move
    /// </summary>
    private void Move()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        
        _rigidbody.MovePosition(_rigidbody.position + _movement * speed * Time.fixedDeltaTime); 
    }
}
