using UnityEngine;
using UnityEngine.UI;

public class ProductTransfer : MonoBehaviour
{
    public GameObject box;
    public GameObject shelf;
    public GameObject productPrefab;

    private GameObject selectedProduct;
    private Vector3 offset;

    void Start()
    {
        //  Initialise the product list in the box
        for (int i = 0; i < 5; i++)
        {
            Instantiate(productPrefab, box.transform);
        }
    }

    void Update()
    {
        if (selectedProduct != null)
        {
            //  Drag the selected product with the mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedProduct.transform.position = mousePos + offset;

            if (Input.GetMouseButtonUp(0))
            {
                //  Check if the product is dropped over the shelf or the box
                if (IsOver(shelf.transform))
                {
                    selectedProduct.transform.SetParent(shelf.transform);
                    selectedProduct.transform.localPosition = Vector3.zero;
                }
                else if (IsOver(box.transform))
                {
                    selectedProduct.transform.SetParent(box.transform);
                    selectedProduct.transform.localPosition = Vector3.zero;
                }

                //  Release the product
                selectedProduct = null;
            }
        }

        //  Pick up a product with the left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Product")
                {
                    //  Store the selected product and offset for dragging
                    selectedProduct = hit.collider.gameObject;
                    offset = selectedProduct.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
        }
    }

    //  Helper method to check if a GameObject is over another GameObject
    bool IsOver(Transform target)
    {
        //  Check if the product is inside the bounds of the shelf or box
        if (selectedProduct.transform.position.x >= target.position.x - target.localScale.x / 2 &&
            selectedProduct.transform.position.x <= target.position.x + target.localScale.x / 2 &&
            selectedProduct.transform.position.z >= target.position.z - target.localScale.z / 2 &&
            selectedProduct.transform.position.z <= target.position.z + target.localScale.z / 2)
        {
            return true;
        }

        return false;
    }
}
