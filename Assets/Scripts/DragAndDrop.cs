using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 startObjectPosition;
    private float distance;
    private Vector3 directionForce;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition - GetMousePos();
            startObjectPosition = transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);

            distance = Vector3.Distance(newPosition, startObjectPosition);

            directionForce = startObjectPosition - newPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (distance > 0.5f)
            {
                rb.AddForce(directionForce * 3, ForceMode2D.Impulse);
            }
            else
            {
                transform.position = startObjectPosition;
            }
        }
    }
}
