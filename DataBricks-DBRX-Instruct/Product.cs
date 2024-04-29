using UnityEngine;

public class Product : MonoBehaviour
{
    public bool isHeld = false;
    public Transform holdingObject;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        isHeld = true;
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void OnMouseUp()
    {
        isHeld = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        holdingObject = null;
    }

    void FixedUpdate()
    {
        if (isHeld)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = holdingObject.position.z;
            transform.position = mousePosition;
        }
    }
}
