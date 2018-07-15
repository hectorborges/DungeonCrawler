using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public int damage;
    public string damageTag;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals(damageTag))
        {
            other.GetComponent<Health>().TookDamage(damage);
        }
    }
}
