using UnityEngine;

public class ProductTransfer : MonoBehaviour
{
    // The box and shelf game objects
    public GameObject box;
    public GameObject shelf;

    // The product being transferred
    private GameObject product;

    // Update is called once per frame
    void Update()
    {
        // Check for mouse clicks on products
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast from the mouse position to detect product clicks
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // Get the product that was clicked
                product = hit.transform.gameObject;

                // Check if the product is in the box or on the shelf
                if (product.transform.parent == box.transform)
                {
                    // Pick up the product from the box
                    PickUpProduct();
                }
                else if (product.transform.parent == shelf.transform)
                {
                    // Pick up the product from the shelf
                    PickUpProduct();
                }
            }
        }

        // Check for dragging
        if (Input.GetMouseButton(0) && product != null)
        {
            // Move the product to the mouse position
            product.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // Check for releasing the product
        if (Input.GetMouseButtonUp(0) && product != null)
        {
            // Check if the product is over the box or shelf
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // Get the object that was hit
                GameObject hitObject = hit.transform.gameObject;

                // Check if the product should be transferred to the box or shelf
                if (hitObject == box)
                {
                    // Transfer the product to the box
                    TransferProductToBox();
                }
                else if (hitObject == shelf)
                {
                    // Transfer the product to the shelf
                    TransferProductToShelf();
                }
            }

            // Reset the product variable
            product = null;
        }
    }

    // Pick up a product from the box or shelf
    void PickUpProduct()
    {
        // Set the product's parent to the main camera
        product.transform.parent = Camera.main.transform;
    }

    // Transfer a product to the box
    void TransferProductToBox()
    {
        // Set the product's parent to the box
        product.transform.parent = box.transform;
    }

    // Transfer a product to the shelf
    void TransferProductToShelf()
    {
        // Set the product's parent to the shelf
        product.transform.parent = shelf.transform;
    }
}
