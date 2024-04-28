using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    public Transform boxTransform; // Assign the box transform in the inspector
    public Transform shelfTransform; // Assign the shelf transform in the inspector
    public static Dictionary<string, ProductInfo> boxContents = new Dictionary<string, ProductInfo>();
    public AudioClip transferSound; // Assign the sound effect in the inspector
    private AudioSource audioSource; // AudioSource component to play the sound

    [System.Serializable]
    public class ProductInfo
    {
        public int quantity;
        public Text uiText; // Reference to the UI Text element
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (isDragging)
        {
            transform.position = new Vector3(mouseWorldPosition.x + offset.x, mouseWorldPosition.y + offset.y, transform.position.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                offset = transform.position - mouseWorldPosition;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            CheckDropLocation();
        }
    }

    private void CheckDropLocation()
    {
        if (Vector3.Distance(transform.position, boxTransform.position) < 1.0f)
        {
            AddToBox();
        }
        else if (Vector3.Distance(transform.position, shelfTransform.position) < 1.0f)
        {
            // Logic for placing the product on the shelf
        }
    }

    private void AddToBox()
    {
        string productId = gameObject.name;

        if (boxContents.ContainsKey(productId))
        {
            boxContents[productId].quantity++;
            UpdateProductUI(productId);
            audioSource.PlayOneShot(transferSound);
        }
        else
        {
            ProductInfo newProductInfo = new ProductInfo { quantity = 1, uiText = /* Assign the UI Text reference here */ };
            boxContents.Add(productId, newProductInfo);
            UpdateProductUI(productId);
            audioSource.PlayOneShot(transferSound);
        }

        gameObject.SetActive(false);
    }

    public static void RemoveFromBox(string productId, AudioSource audioSrc)
    {
        if (boxContents.ContainsKey(productId) && boxContents[productId].quantity > 0)
        {
            boxContents[productId].quantity--;
            UpdateProductUI(productId);
            audioSrc.PlayOneShot(audioSrc.clip); // Play the transfer sound effect
        }
    }

    private static void UpdateProductUI(string productId)
    {
        if (boxContents.ContainsKey(productId))
        {
            boxContents[productId].uiText.text = "Quantity: " + boxContents[productId].quantity;
        }
    }
}
