using System.Collections.Generic;
using UnityEngine;

public class TransferProducts : MonoBehaviour
{
    // Reference to the shelf GameObject
    public GameObject shelf;

    // Lists to store products in the box and on the shelf
    private List<GameObject> productsInBox = new List<GameObject>();
    private List<GameObject> productsOnShelf = new List<GameObject>();

    // Reference to the product being dragged
    private GameObject draggingProduct;

    private void Start()
    {
        // Find all products in the box and add them to the list
        GameObject[] products = GameObject.FindGameObjectsWithTag("Product");
        foreach (GameObject product in products)
        {
            productsInBox.Add(product);
        }
    }

    private void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // If a product is being dragged
            if (draggingProduct != null)
            {
                // Cast a ray from the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Check if the ray hits the shelf
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == shelf)
                {
                    // Release the product onto the shelf
                    ReleaseProductToShelf();
                }
                else
                {
                    // Transfer the product back to the box
                    TransferToBox();
                }
            }
        }

        // If a product is being dragged
        if (draggingProduct != null)
        {
            // Move the product with the mouse
            MoveProductWithMouse();
        }
    }

    private void MoveProductWithMouse()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Ensure the product stays on the same z-plane as the camera

        // Move the draggingProduct to the mouse position
        draggingProduct.transform.position = mousePos;
    }

    private void PickUpProduct(GameObject product)
    {
        // Set the product's parent to null to remove it from the shelf
        product.transform.parent = null;
        draggingProduct = product; // Set the draggingProduct to the picked up product

        // Remove the product from the appropriate list
        if (productsInBox.Contains(product))
        {
            productsInBox.Remove(product);
        }
        else if (productsOnShelf.Contains(product))
        {
            productsOnShelf.Remove(product);
        }
    }

    private void ReleaseProductToShelf()
    {
        // Set the product's parent to the shelf to release it onto the shelf
        draggingProduct.transform.parent = shelf.transform;
        draggingProduct = null; // Reset the draggingProduct

        // Add the product to the list of products on the shelf
        productsOnShelf.Add(draggingProduct);
    }

    private void TransferToBox()
    {
        // Set the product's parent to the box to transfer it back to the box
        draggingProduct.transform.parent = transform;
        draggingProduct = null; // Reset the draggingProduct

        // Add the product to the list of products in the box
        productsInBox.Add(draggingProduct);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is a product
        if (other.CompareTag("Product"))
        {
            // Transfer the product to the shelf
            TransferToShelf(other.gameObject);
        }
    }

    private void TransferToShelf(GameObject product)
    {
        // Move the product to the shelf position
        product.transform.position = shelf.transform.position;
        product.transform.parent = shelf.transform; // Make the shelf the parent of the product

        // Add the product to the list of products on the shelf
        productsOnShelf.Add(product);

        // Remove the product from the list of products in the box
        productsInBox.Remove(product);
    }
}
