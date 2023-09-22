using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float PlayerSpeed = 5.0f;

    private Animator animator;

    private void Start()
    {
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
}
