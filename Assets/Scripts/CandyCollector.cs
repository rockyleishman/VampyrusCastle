using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCollector : MonoBehaviour
{
    [SerializeField] public float MaxSuctionSpeed = 10.0f;

    private CircleCollider2D triggerCollider;

    private void Start()
    {
        triggerCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Candy candy = other?.GetComponent<Candy>();

        if (candy != null)
        {
            candy.transform.Translate((transform.position - candy.transform.position).normalized * (1 - Vector3.Distance(transform.position, candy.transform.position) / triggerCollider.radius) * MaxSuctionSpeed * Time.deltaTime, Space.World);
        }
    }
}
