using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : DestructableObject
{
    [Header("Player Attributes")]
    [SerializeField] public float PlayerSpeed = 5.0f;
    [SerializeField][Range(0,0.1f)] public float TimeToChangeDirection = 0.05f;
    private int _inputDirection;
    private float _inputDirectionTime;
    private int _playerDirection;

    private Animator animator;

    protected override void Start()
    {
        base.Start();

        _inputDirection = 0;
        _inputDirectionTime = 0.0f;
        _playerDirection = 0;

        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        Movement();
        Attack();
    }

    private void Movement()
    {
        //find direction
        Vector2 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f).normalized;

        //apply movement
        transform.Translate(moveDirection * PlayerSpeed * Time.deltaTime);

        //change player direction
        int lastDirection = _playerDirection;

        if (moveDirection.x > 0.0f)
        {
            if (moveDirection.y > 0.0f)
            {
                //northeast
                _inputDirection = 5;
                _inputDirectionTime += Time.deltaTime;
            }
            else if (moveDirection.y < 0.0f)
            {
                //southeast
                _inputDirection = 7;
                _inputDirectionTime += Time.deltaTime;
            }
            else
            {
                //east
                _inputDirection = 6;
                _inputDirectionTime += Time.deltaTime;
            }
        }
        else if (moveDirection.x < 0.0f)
        {
            if (moveDirection.y > 0.0f)
            {
                //northwest
                _inputDirection = 3;
                _inputDirectionTime += Time.deltaTime;
            }
            else if (moveDirection.y < 0.0f)
            {
                //southwest
                _inputDirection = 1;
                _inputDirectionTime += Time.deltaTime;
            }
            else
            {
                //west
                _inputDirection = 2;
                _inputDirectionTime += Time.deltaTime;
            }
        }
        else
        {
            if (moveDirection.y > 0.0f)
            {
                //north
                _inputDirection = 4;
                _inputDirectionTime += Time.deltaTime;
            }
            else if (moveDirection.y < 0.0f)
            {
                //south
                _inputDirection = 0;
                _inputDirectionTime += Time.deltaTime;
            }
            else
            {
                //no directional input
                _inputDirectionTime = 0.0f;
            }
        }

        if(_inputDirection == _playerDirection)
        {
            _inputDirectionTime = 0.0f;
        }

        if (_inputDirectionTime > TimeToChangeDirection)
        {
            _playerDirection = _inputDirection;
        }

        //apply animation
        if (lastDirection != _playerDirection)
        {
            animator.SetInteger("PlayerDirection", _playerDirection);
            animator.SetTrigger("ChangeDirection");
        }
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //TODO
            Debug.Log("Player Attack");
        }
    }

    protected override void Destroy()
    {
        //TODO
        Debug.Log("Player Dead");
    }
}
