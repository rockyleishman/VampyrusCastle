using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float PlayerSpeed = 5.0f;

    [SerializeField] public Sprite SpriteNorth;

    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
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
    }
}
