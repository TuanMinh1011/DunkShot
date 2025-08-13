using System;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public static event Action OnMouseDown;
    public static event Action OnMouseDrag;
    public static event Action OnMouseUp;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnMouseDown?.Invoke();

        if (Input.GetMouseButton(0))
            OnMouseDrag?.Invoke();

        if (Input.GetMouseButtonUp(0))
            OnMouseUp?.Invoke();
    }
}
