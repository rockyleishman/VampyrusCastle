using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : DestructableObject
{
    [Header("Movement")]
    [SerializeField] public float MovementSpeed = 5.0f;
    [SerializeField][Range(0.0f, 0.1f)] public float TimeToChangeDirection = 0.05f;
    private int _inputDirection;
    private float _inputDirectionTime;
    private int _playerDirection;

    [Header("Sword Attack")]
    [SerializeField] public PlayerMeleeAttack MeleeAttack;
    [SerializeField] public float Damage = 5.0f;
    [SerializeField] public float Range = 1.5f;
    [SerializeField] public float KnockbackDistance = 2.0f;
    [SerializeField] public float KnockbackVelocity = 20.0f;

    private Animator _animator;

    //constant structs
    private Quaternion k_rotS = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    private Quaternion k_rotSE = Quaternion.Euler(0.0f, 0.0f, 45.0f);
    private Quaternion k_rotE = Quaternion.Euler(0.0f, 0.0f, 90.0f);
    private Quaternion k_rotNE = Quaternion.Euler(0.0f, 0.0f, 135.0f);
    private Quaternion k_rotN = Quaternion.Euler(0.0f, 0.0f, 180.0f);
    private Quaternion k_rotNW = Quaternion.Euler(0.0f, 0.0f, 225.0f);
    private Quaternion k_rotW = Quaternion.Euler(0.0f, 0.0f, 270.0f);
    private Quaternion k_rotSW = Quaternion.Euler(0.0f, 0.0f, 315.0f);

    protected override void Start()
    {
        base.Start();

        _inputDirection = 0;
        _inputDirectionTime = 0.0f;
        _playerDirection = 0;

        _animator = GetComponent<Animator>();
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
        transform.Translate(moveDirection * MovementSpeed * Time.deltaTime);

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
            _animator.SetInteger("PlayerDirection", _playerDirection);
            _animator.SetTrigger("ChangeDirection");
        }
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //determine attack position and rotation
            Vector3 position;
            Quaternion rotation;
            switch (_playerDirection)
            {
                case 1:
                    //SW
                    position = transform.position + ((Vector3.down + Vector3.left).normalized * Range);
                    rotation = k_rotSW;
                    break;

                case 2:
                    //W
                    position = transform.position + (Vector3.left * Range);
                    rotation = k_rotW;
                    break;

                case 3:
                    //NW
                    position = transform.position + ((Vector3.up + Vector3.left).normalized * Range);
                    rotation = k_rotNW;
                    break;

                case 4:
                    //N
                    position = transform.position + (Vector3.up * Range);
                    rotation = k_rotN;
                    break;

                case 5:
                    //NE
                    position = transform.position + ((Vector3.up + Vector3.right).normalized * Range);
                    rotation = k_rotNE;
                    break;

                case 6:
                    //E
                    position = transform.position + (Vector3.right * Range);
                    rotation = k_rotE;
                    break;

                case 7:
                    //SE
                    position = transform.position + ((Vector3.down + Vector3.right).normalized * Range);
                    rotation = k_rotSE;
                    break;

                default:
                    //S
                    position = transform.position + (Vector3.down * Range);
                    rotation = k_rotS;
                    break;
            }
            
            //spawn attack
            PlayerMeleeAttack meleeAttack = (PlayerMeleeAttack)PoolManager.Instance.Spawn(MeleeAttack.name, position, rotation);
            meleeAttack.Init(Damage, KnockbackDistance, KnockbackVelocity);
        }
    }

    protected override void DestroyObject()
    {
        //TODO
        Debug.Log("Player Dead");
    }
}
