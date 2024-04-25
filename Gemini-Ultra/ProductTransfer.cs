public class ProductTransfer : MonoBehaviour
{
    public GameObject box; // Reference to the box GameObject
    public GameObject shelf; // Reference to the shelf GameObject
    public GameObject productPrefab; // Prefab of the product you want to transfer

    private GameObject heldProduct; // Reference to the currently held product

    void Update()
    {
        // Raycast to check for product clicks
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Product"))
                {
                    PickUpProduct(hit.collider.gameObject);
                }
            }
        }

        // Move held product with mouse drag
        if (heldProduct != null)
        {
            heldProduct.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        }

        // Check for releasing product on box or shelf
        if (Input.GetMouseButtonUp(0) && heldProduct != null)
        {
            Collider collider = heldProduct.GetComponent<Collider>();
            collider.enabled = true; // Re-enable collider after dragging

            // Check for snapping to box position
            float distanceToBox = Vector3.Distance(heldProduct.transform.position, box.transform.position);
            if (distanceToBox < 0.5f) // Adjust this value based on your box size
            {
                SnapToPosition(heldProduct, box.transform);
            }
            // Check for snapping to shelf position
            else if (IsOverShelf(heldProduct.transform.position))
            {
                SnapToPosition(heldProduct, shelf.transform);
            }
            // Otherwise, drop the product
            else
            {
                heldProduct = null;
            }
        }
    }

    void PickUpProduct(GameObject product)
    {
        if (heldProduct == null)
        {
            heldProduct = product;
            heldProduct.GetComponent<Collider>().enabled = false; // Disable collider while dragging
        }
    }

    void SnapToPosition(GameObject product, Transform target)
    {
        // Find the closest available snap point on the target (optional)
        // ... (implement logic to find closest snap point)

        product.transform.parent = target; // Set product as child of target
        product.transform.localPosition = Vector3.zero; // Reset local position
        product.transform.rotation = Quaternion.identity; // Reset rotation
        heldProduct = null;
    }

    bool IsOverShelf(Vector3 position)
    {
        // Implement your shelf boundary check logic here
        // This example assumes a simple box collider for the shelf
        BoxCollider shelfCollider = shelf.GetComponent<BoxCollider>();
        if (shelfCollider != null)
        {
            return shelfCollider.bounds.Contains(position);
        }
        return false;
    }
}
