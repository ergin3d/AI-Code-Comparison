public class ProductTransfer : MonoBehaviour
{
    public GameObject box; // Reference to the box GameObject
    public GameObject shelf; // Reference to the shelf GameObject
    public GameObject heldProduct; // Reference to the currently held product

    private bool isDragging; // Flag to indicate if a product is being dragged

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse click
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.transform.parent == box.transform) // Check if clicked on a product in the box
                {
                    PickUpProduct(hit.collider.gameObject);
                }
                else if (hit.collider.gameObject.transform.parent == shelf.transform) // Check if clicked on a product on the shelf
                {
                    PutDownProduct(hit.collider.gameObject);
                }
            }
        }

        if (isDragging)
        {
            // Move the held product with the mouse position
            heldProduct.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

            if (Input.GetMouseButtonUp(0)) // Check for mouse release
            {
                PutDownProduct(heldProduct);
            }
        }
    }

    void PickUpProduct(GameObject product)
    {
        if (heldProduct == null) // Only pick up if no product is currently held
        {
            heldProduct = product;
            isDragging = true;
            heldProduct.transform.parent = null; // Detach product from its previous parent
        }
    }

    void PutDownProduct(GameObject product)
    {
        if (product == heldProduct) // Only put down the currently held product
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == box) // Check if placed on the box
                {
                    heldProduct.transform.parent = box.transform; // Attach product to the box
                }
                else if (hit.collider.gameObject == shelf) // Check if placed on the shelf
                {
                    heldProduct.transform.parent = shelf.transform; // Attach product to the shelf
                }
            }
            heldProduct = null;
            isDragging = false;
        }
    }
}
