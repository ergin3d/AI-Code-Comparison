using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    private Color originalColor;

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();

        // Save the original color and change the color of the object to red when picked up
        originalColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.red;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    void OnMouseUp()
    {
        // Restore the original color when the object is put down
        GetComponent<Renderer>().material.color = originalColor;

        // Check if we are over a box or a shelf
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            // If the object we hit is a box or a shelf, parent this object to it
            if (objectHit.CompareTag("Box") || objectHit.CompareTag("Shelf"))
            {
                this.transform.parent = objectHit;
            }
        }
    }
}

