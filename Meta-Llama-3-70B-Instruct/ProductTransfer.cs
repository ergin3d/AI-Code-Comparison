using UnityEngine;
using UnityEngine.EventSystems;

public class ProductTransfer : MonoBehaviour
{
    // Set up the box and shelf game objects
    public GameObject box;
    public GameObject shelf;

    // Set up the product prefab
    public GameObject productPrefab;

    // List of products in the box and shelf
    private List<GameObject> boxProducts = new List<GameObject>();
    private List<GameObject> shelfProducts = new List<GameObject>();

    // Currently picked up product
    private GameObject pickedUpProduct;

    // Is the product being dragged?
    private bool isDragging = false;

    void Update()
    {
        // Check for mouse clicks
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits a product
            if (Physics.Raycast(ray, out hit))
            {
                // Get the product game object
                GameObject product = hit.transform.gameObject;

                // Check if the product is in the box or shelf
                if (boxProducts.Contains(product) || shelfProducts.Contains(product))
                {
                    // Pick up the product
                    PickUpProduct(product);
                }
            }
        }

        // Check for mouse drag
        if (Input.GetMouseButton(0) && isDragging)
        {
            // Move the picked up product to the mouse position
            pickedUpProduct.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // Check for mouse release
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            // Drop the product
            DropProduct();
        }
    }

    // Pick up a product
    private void PickUpProduct(GameObject product)
    {
        // Set the picked up product
        pickedUpProduct = product;

        // Remove the product from its current list
        if (boxProducts.Contains(product))
        {
            boxProducts.Remove(product);
        }
        else if (shelfProducts.Contains(product))
        {
            shelfProducts.Remove(product);
        }

        // Set the product's parent to null
        product.transform.parent = null;

        // Set the isDragging flag
        isDragging = true;
    }

    // Drop the product
    private void DropProduct()
    {
        // Raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits the box or shelf
        if (Physics.Raycast(ray, out hit))
        {
            // Get the hit game object
            GameObject hitObject = hit.transform.gameObject;

            // Check if the hit object is the box or shelf
            if (hitObject == box)
            {
                // Add the product to the box
                boxProducts.Add(pickedUpProduct);
                pickedUpProduct.transform.parent = box.transform;
            }
            else if (hitObject == shelf)
            {
                // Add the product to the shelf
                shelfProducts.Add(pickedUpProduct);
                pickedUpProduct.transform.parent = shelf.transform;
            }
        }

        // Reset the picked up product and isDragging flag
        pickedUpProduct = null;
        isDragging = false;
    }

    // Add a product to the box or shelf
    public void AddProduct(GameObject product, bool addToBox)
    {
        // Instantiate the product prefab
        GameObject newProduct = Instantiate(productPrefab, product.transform.position, product.transform.rotation);

        // Add the product to the box or shelf
        if (addToBox)
        {
            boxProducts.Add(newProduct);
            newProduct.transform.parent = box.transform;
        }
        else
        {
            shelfProducts.Add(newProduct);
            newProduct.transform.parent = shelf.transform;
        }
    }
}
