using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DoNotEnter : MonoBehaviour
{
    public Vector3 spawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement pm))
        {
            if(pm.rb == null) return;
            pm.rb.position = spawnPoint;
            pm.rb.velocity = Vector3.zero;
        }
    }
}
