using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject deadly;
    public LayerMask mask;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(deadly != null)
            {
                if (!deadly.TryGetComponent<Pickupable>(out Pickupable pickupable))
                {
                    return;
                }
                pickupable.Released();
                pickupable.holder = null;
                deadly = null;
            } else 
            {
                Collider[] col = Physics.OverlapSphere(transform.position, 0.5f, mask);
                if (col == null) return;
                
                if(col.Length > 0)
                {
                    if (!col[0].TryGetComponent<Pickupable>(out Pickupable pickupable))
                    {
                        return;
                    }
                    deadly = col[0].gameObject;
                    pickupable.holder = transform;
                    pickupable.Grabbed();
                }
            }
        }
    }
}
