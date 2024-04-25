using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private Transform originalParent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Başlangıç pozisyonunu ve ebeveyni geçersiz bırakma durumu için kaydet
        startPosition = rectTransform.position;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Sürüklenen nesnenin görünürlüğünü artır
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Nesneyi fare ile taşı
        rectTransform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Görünürlüğü sıfırla
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Bırakma bölgesi üzerinde değilse veya izin verilen bir alanda değilse pozisyonu sıfırla
        if (transform.parent == originalParent)
        {
            rectTransform.position = startPosition;
        }
    }
}
