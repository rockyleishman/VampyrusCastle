using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : DestructableObject
{
    [Header("Player Attributes")]
    [SerializeField] public float PlayerSpeed = 5.0f;

    private Animator animator;

    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //find velocity
        Vector2 velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f).normalized * PlayerSpeed;

        //apply movement
        transform.Translate(velocity * Time.deltaTime);

        //apply animation
        animator.SetFloat("Speed", velocity.magnitude / PlayerSpeed);
        animator.SetFloat("HorizontalDirection", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("VerticalDirection", Input.GetAxisRaw("Vertical"));
    }

    protected override void Destroy()
    {
        //TODO
        Debug.Log("Player Dead");
    }
}
