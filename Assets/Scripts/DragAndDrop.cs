using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] public float forceMultiplier = 3f;

    private Vector3 mousePosition;
    private Vector3 startObjectPosition;
    private float distance;
    public Vector3 directionForce;

    private Rigidbody2D rb;

    private bool _isGhost = false; // Set to true if this is a ghost object

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
        if (_isGhost) return;

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
                rb.AddForce(directionForce * forceMultiplier, ForceMode2D.Impulse);
                Debug.Log("Distance: " + distance + " directionForce: " + directionForce);
            }
            else
            {
                transform.position = startObjectPosition;
            }
        }
    }

    public void Init(Vector3 fjfjfj)
    {
        _isGhost = true; // Set this to true if this is a ghost object

        //mousePosition = Input.mousePosition - GetMousePos();
        //startObjectPosition = transform.position;

        //Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);

        //distance = Vector3.Distance(newPosition, startObjectPosition);

        //directionForce = startObjectPosition - newPosition;

        rb.AddForce(fjfjfj * forceMultiplier, ForceMode2D.Impulse);

        //Debug.Log("Distance: " + distance + " directionForce: " + directionForce);
    }
}
