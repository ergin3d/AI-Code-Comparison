using UnityEngine;

public class ProductTransfer : MonoBehaviour
{
    private Camera cam;
    private GameObject selectedProduct;
    private Vector3 offset;
    private bool isDragging = false;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Product"))
            {
                selectedProduct = hit.collider.gameObject;
                offset = selectedProduct.transform.position - cam.ScreenToWorldPoint(Input.mousePosition);
                isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            selectedProduct = null;
        }

        if (isDragging && selectedProduct != null)
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition) + offset;
            selectedProduct.transform.position = new Vector3(mousePosition.x, mousePosition.y, selectedProduct.transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Product"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Product"))
        {
            other.transform.SetParent(null);
        }
    }
}
