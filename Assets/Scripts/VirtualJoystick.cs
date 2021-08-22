using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10, 150)] private float leverRange = 65;

    public Vector2 inputDirection = Vector2.zero;
    public bool isInput = false;

    [SerializeField] private PlayerController controller;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inputDirection = Vector2.zero;
        lever.anchoredPosition = inputDirection;
        isInput = false;
        //controller.Move(Vector2.zero);
        //controller.Rotate(Vector2.zero);
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
    }

    private void InputControlVector()
    {
        //controller.Move(inputDirection);
        //controller.Rotate(inputDirection);
        Debug.Log(inputDirection.x + " / " + inputDirection.y);
    }

    private void Update()
    {
        if(isInput)
        {
            InputControlVector();
        }
    }
}
