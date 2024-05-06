using UnityEngine;
using System.Collections.Generic;

public abstract class Container : MonoBehaviour {
    public List<Product> products = new List<Product>();

    public bool ContainsPoint(Vector3 point) {
        // Simple collider-based containment check
        return GetComponent<Collider>().bounds.Contains(point);
    }

    public void AddProduct(Product product) {
        products.Add(product);
    }

    public void RemoveProduct(Product product) {
        products.Remove(product);
    }
}
