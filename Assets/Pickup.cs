using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    GameObject deadly;
    public LayerMask mask;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(deadly != null)
            {
                deadly.transform.parent = null;
                deadly = null;
            } else 
            {
                Collider[] col = Physics.OverlapSphere(transform.position, 0.5f, mask);
                if (col == null) return;
                if(col.Length > 0)
                {
                    deadly = col[0].gameObject;
                    deadly.transform.parent = transform;
                    deadly.transform.localPosition = Vector3.zero;
                }
            }
        }
    }
}
