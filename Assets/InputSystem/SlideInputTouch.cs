using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlideInputTouch : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform restrictedArea;
    private Vector2 previousTouchPosition;
    [SerializeField] new CameraMovement camera;

    void Start()
    {
        restrictedArea = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(restrictedArea, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            if (restrictedArea.rect.Contains(localPoint))
            {
                previousTouchPosition = localPoint;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(restrictedArea, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            if (restrictedArea.rect.Contains(localPoint))
            {
                Vector2 direction = new Vector2(
                    (localPoint.x - previousTouchPosition.x)/restrictedArea.rect.width,
                    (localPoint.y - previousTouchPosition.y)/restrictedArea.rect.height
                );
                //move camera
                camera.Move(direction);
                previousTouchPosition = localPoint;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Reset previous touch position
        previousTouchPosition = Vector2.zero;
    }
}
