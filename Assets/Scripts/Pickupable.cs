using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    private Rigidbody rb;
    [HideInInspector] public Transform holder;
    private float oldDrag;
    public float grabbedDrag = 20;

    // Start is called before the first frame update
    void Start()
    {
        if(!TryGetComponent<Rigidbody>(out Rigidbody rigidBody))
        {
            Debug.Log("Object " + transform.name + " has a pickupable component but no rigidbody.");
            gameObject.SetActive(false);
            return;
        }
        rb = rigidBody;
        oldDrag = rb.drag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (holder == null)
            return;
        if (rb == null)
            return;

        Vector3 difference = holder.position - transform.position;
        if(difference.magnitude > 1)
        {
            difference = difference.normalized * difference.sqrMagnitude;
        }else if (difference.magnitude < 0.3)
        {
            difference = difference.normalized * difference.sqrMagnitude;
        }

        rb.AddForce(difference * GameReferences.playerMovement.grabStrength);
    }

    public void Grabbed()
    {
        oldDrag = rb.drag;
        rb.drag = grabbedDrag;
    }

    public void Released()
    {
        rb.drag = oldDrag;
    }
}
