using UnityEngine;

public class ProductTransfer : MonoBehaviour
{
    private GameObject currentParent;

    void Start()
    {
        currentParent = transform.parent.gameObject;
    }

    void OnMouseDown()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        transform.rotation = Quaternion.identity;
    }

    void OnMouseUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.gameObject == currentParent || hit.transform.gameObject.tag == "Shelf" || hit.transform.gameObject.tag == "Box")
            {
                transform.parent = hit.transform;
                GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
