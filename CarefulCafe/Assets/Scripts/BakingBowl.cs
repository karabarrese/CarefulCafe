using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BakingBowl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 offset; 
    public RectTransform targetArea; 

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition = eventData.position - (Vector2)rectTransform.position;
        offset = localPointerPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 targetPosition = eventData.position - offset;
        rectTransform.position = targetPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (targetArea != null && RectTransformUtility.RectangleContainsScreenPoint(targetArea, eventData.position))
        {
            Debug.Log("Dropped inside the target area!");
            // Handle logic when dropped inside the area
        }
        else
        {
            Debug.Log("Dropped outside the target area.");
            // Handle logic when dropped outside
        }
    }
}
