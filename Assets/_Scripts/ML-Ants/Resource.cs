using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ants can collect Resource moving it to the nest
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour
{
    protected Rigidbody rb;

    public int Quantity;

    public void GatherResource()
    {
        if (Quantity <= 0)
        {
            this.gameObject.SetActive(false);
            return;
        }

        Quantity -= 1;
    }

    [SerializeField] protected float mass;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    protected virtual void Start()
    {
        rb.mass = mass;
    }

    protected virtual void TakeResource()
    {

    }
 
}
