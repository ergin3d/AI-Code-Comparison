using UnityEngine;

public class ProductHolder : MonoBehaviour
{
    public float pickUpDistance = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pickUpDistance))
            {
                Product product = hit.transform.GetComponent<Product>();
                if (product!= null)
                {
                    product.holdingObject = transform;
                }
            }
        }
    }
}
