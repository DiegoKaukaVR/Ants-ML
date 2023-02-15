using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public Character user;

    public Collider ColliderDamage;

    [HideInInspector]
    public List<Character> charactersHit = new List<Character>();
    public Character currentTarget;

    public float damage;

    public int targetColony = 0;
   

    private void Awake()
    {
        user = GetComponentInParent<Character>();
        ColliderDamage = GetComponent<Collider>();
        ColliderDamage.enabled = false;
    }

    
    public void CleanCharacterHits()
    {
        charactersHit.Clear();
    }

    public LayerMask layerMask;
    private void OnTriggerEnter(Collider other)
    {
        if (Tools.DetectLayer.LayerInLayerMask(other.gameObject.layer, layerMask))
        {
            currentTarget = other.GetComponent<Character>();
            if (currentTarget.colonyID == targetColony)
            {
                DoDamage(currentTarget);
                charactersHit.Add(currentTarget);
            }
        }
    }


    void DoDamage(Character target)
    {
        target.ReceiveDamage(Mathf.RoundToInt(damage));
       
    }
}
