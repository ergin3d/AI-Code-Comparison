using UnityEngine;

public class ProductTransferController : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    private void OnMouseDown()
    {
        // When the mouse clicks on the product, start dragging
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        // When the mouse releases, stop dragging
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            // Update the product's position based on mouse movement
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x + offset.x, mousePos.y + offset.y, transform.position.z);
        }
    }

    // Add any other logic you need (e.g., collision detection with the box)
}
