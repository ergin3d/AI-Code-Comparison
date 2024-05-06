using UnityEngine;

public class DragAndDropSystem : MonoBehaviour {
    private Camera mainCamera;
    private Product selectedProduct;
    private Vector3 startDragPosition;

    private void Awake() {
        mainCamera = Camera.main; // Cache the main camera
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            onMouseDown();
        }
        if (selectedProduct != null && Input.GetMouseButton(0)) {
            onMouseDrag();
        }
        if (selectedProduct != null && Input.GetMouseButtonUp(0)) {
            onMouseUp();
        }
    }

    private void onMouseDown() {
        // Cast a ray from the camera through the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            // Check if the hit object has a Product component
            Product product = hit.collider.GetComponent<Product>();
            if (product != null) {
                selectedProduct = product;
                startDragPosition = product.transform.position; // Save the starting position
                // Optionally add any highlight or selection effect
            }
        }
    }

    private void onMouseDrag() {
        // Convert mouse position to a world point
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.WorldToScreenPoint(selectedProduct.transform.position).z);
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        selectedProduct.transform.position = newPosition; // Update product position
    }

    private void onMouseUp() {
        // Check if the product can be placed in a new container
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        bool placed = false;

        foreach (var hit in hits) {
            Container container = hit.collider.GetComponent<Container>();
            if (container != null && container.ContainsPoint(selectedProduct.transform.position)) {
                // Update product container and add to new container
                if (selectedProduct.currentContainer != null) {
                    selectedProduct.currentContainer.RemoveProduct(selectedProduct);
                }
                container.AddProduct(selectedProduct);
                selectedProduct.currentContainer = container;
                placed = true;
                break;
            }
        }

        if (!placed) {
            // Return product to its start position if not placed in a new container
            selectedProduct.transform.position = startDragPosition;
        }
        selectedProduct = null; // Clear the selection
    }
}
