using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ants can collect Resource moving it to the nest
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour
{
    public enum ResourceType
    {
        type1,
        type2,
        type3
    }

    protected ResourceType _resourceType;
    protected int _quantity;
    protected string _name;

    public Resource(ResourceType resourceType, int quantity)
    {
        _resourceType = resourceType;
        _quantity = quantity;
    }

    protected Rigidbody rb;

    public int Quantity = 5;
    int maxQuantity;

    private void Start()
    {
        maxQuantity = Quantity;
    }
    public bool GatherResource()
    {
        if (Quantity <= 0)
        {
            gameObject.SetActive(false);
            return false;
        }

        Quantity -= 1;

        transform.localScale = Vector3.one * Quantity / maxQuantity;

        return true;
    }

    [SerializeField] protected float mass;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
    }

    protected virtual void TakeResource()
    {

    }
 
}
