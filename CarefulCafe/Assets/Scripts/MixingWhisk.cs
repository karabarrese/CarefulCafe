using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MixingWhisk : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform; 
    private Image image; 
    private Vector2 offset; 

    public void OnBeginDrag(PointerEventData eventData)
    {
        // makes image transparent
        image.color = new Color32(255, 255, 255, 170);

        // offset from the pointer position to the center of the image
        Vector2 localPointerPosition = eventData.position - (Vector2)rectTransform.position;
        offset = localPointerPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Position the image so the pointer stays at the center
        Vector2 targetPosition = eventData.position - offset;

        // make sure whisk doesn't go outside of bowl
        // Debug.Log($"x {targetPosition.x}");
        // Debug.Log($"y {targetPosition.y}");
        if (targetPosition.y < 198.3487){
            targetPosition.y = 198.3487F;
            if (targetPosition.x > 936){
                targetPosition.x = 936f;
            } else if (targetPosition.x < 664){
                targetPosition.x = 664f;
            }
        } else if (targetPosition.y < 282.3461){
            if (targetPosition.x > 936){
                targetPosition.x = 936f;
            } else if (targetPosition.x < 664){
                targetPosition.x = 664f;
            }
        } else {
            if (targetPosition.x > 998){
                targetPosition.x = 998f;
            } else if (targetPosition.x < 597){
                targetPosition.x = 597f;
            }
        }

        // update position of image
        rectTransform.position = targetPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // reset to default color
        image.color = new Color(255, 255, 255, 255);
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }
}
