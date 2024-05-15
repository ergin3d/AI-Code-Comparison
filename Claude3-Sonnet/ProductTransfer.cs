using UnityEngine;

public class ProductTransfer : MonoBehaviour
{
    public GameObject box; // Reference to the box GameObject
    public GameObject shelf; // Reference to the shelf GameObject
    public GameObject productPrefab; // Prefab for the product (cube)
    public int numProducts = 10; // Number of products to create initially

    private GameObject selectedProduct; // Currently selected product
    private Vector3 mouseOffset; // Offset between mouse position and product position

    void Start()
    {
        // Create initial products
        for (int i = 0; i < numProducts; i++)
        {
            CreateProduct(box.transform);
        }
    }

    void Update()
    {
        // Check for left mouse button press
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Check if a product is clicked
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Product"))
                {
                    selectedProduct = hit.collider.gameObject;
                    mouseOffset = selectedProduct.transform.position - Camera.main.ScreenPointToRay(Input.mousePosition).origin;
                }
            }
        }

        // Check if a product is being dragged
        if (selectedProduct != null)
        {
            Vector3 newPosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin + mouseOffset;
            selectedProduct.transform.position = newPosition;

            // Check for left mouse button release
            if (Input.GetMouseButtonUp(0))
            {
                // Check if the product is dropped on the box or shelf
                Collider collider = null;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    collider = hit.collider;
                }

                if (collider != null)
                {
                    if (collider.gameObject == box)
                    {
                        selectedProduct.transform.parent = box.transform;
                    }
                    else if (collider.gameObject == shelf)
                    {
                        selectedProduct.transform.parent = shelf.transform;
                    }
                }

                selectedProduct = null;
            }
        }
    }

    void CreateProduct(Transform parent)
    {
        GameObject product = Instantiate(productPrefab, parent);
        product.tag = "Product";
    }
}
