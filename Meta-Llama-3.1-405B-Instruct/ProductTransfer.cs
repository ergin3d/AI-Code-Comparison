using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductTransfer : MonoBehaviour
{
    // The box and shelf game objects
    public GameObject box;
    public GameObject shelf;

    // The product prefab
    public GameObject productPrefab;

    // The product that is currently being dragged
    private GameObject currentProduct;

    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast from the mouse position to detect products
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // If the ray hits a product, pick it up
                if (hit.transform.CompareTag("Product"))
                {
                    currentProduct = hit.transform.gameObject;
                }
            }
        }

        // Check if the left mouse button is held down
        if (Input.GetMouseButton(0) && currentProduct!= null)
        {
            // Move the product to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                currentProduct.transform.position = hit.point;
            }
        }

        // Check if the left mouse button is released
        if (Input.GetMouseButtonUp(0) && currentProduct!= null)
        {
            // Check if the product is over the box or shelf
            if (IsOverBox(currentProduct.transform.position))
            {
                // Transfer the product to the box
                TransferProductToBox(currentProduct);
            }
            else if (IsOverShelf(currentProduct.transform.position))
            {
                // Transfer the product to the shelf
                TransferProductToShelf(currentProduct);
            }

            // Release the product
            currentProduct = null;
        }
    }

    // Check if a position is over the box
    bool IsOverBox(Vector3 position)
    {
        // Raycast down from the position to detect the box
        Ray ray = new Ray(position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // If the ray hits the box, return true
            if (hit.transform == box.transform)
            {
                return true;
            }
        }
        return false;
    }

    // Check if a position is over the shelf
    bool IsOverShelf(Vector3 position)
    {
        // Raycast down from the position to detect the shelf
        Ray ray = new Ray(position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // If the ray hits the shelf, return true
            if (hit.transform == shelf.transform)
            {
                return true;
            }
        }
        return false;
    }

    // Transfer a product to the box
    void TransferProductToBox(GameObject product)
    {
        // Set the product's parent to the box
        product.transform.SetParent(box.transform);

        // Reset the product's position and rotation
        product.transform.localPosition = Vector3.zero;
        product.transform.localRotation = Quaternion.identity;
    }

    // Transfer a product to the shelf
    void TransferProductToShelf(GameObject product)
    {
        // Set the product's parent to the shelf
        product.transform.SetParent(shelf.transform);

        // Reset the product's position and rotation
        product.transform.localPosition = Vector3.zero;
        product.transform.localRotation = Quaternion.identity;
    }
}
