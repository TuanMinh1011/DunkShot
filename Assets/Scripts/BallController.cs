using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _forceMultiplier = 5f;
    [SerializeField] private float _clampValue = 20f;

    public Vector3 directionForce;
    private Vector3 _mousePosition;
    private Vector3 _startObjectPosition;
    private float _distance;
    private bool _isGhost = false; // Set to true if this is a ghost object

    private Rigidbody2D rb;

    private void OnEnable()
    {
        DragAndDrop.OnMouseDown += HandleMouseDown;
        DragAndDrop.OnMouseDrag += HandleMouseDrag;
        DragAndDrop.OnMouseUp += HandleMouseUp;
    }

    private void OnDisable()
    {
        DragAndDrop.OnMouseDown -= HandleMouseDown;
        DragAndDrop.OnMouseDrag -= HandleMouseDrag;
        DragAndDrop.OnMouseUp -= HandleMouseUp;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void HandleMouseDown()
    {
        if (_isGhost) return;

        _mousePosition = Input.mousePosition - GetMousePos();
        _startObjectPosition = transform.position;
    }

    private void HandleMouseDrag()
    {
        if (_isGhost) return;

        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
        _distance = Vector3.Distance(newPosition, _startObjectPosition);
        directionForce = _startObjectPosition - newPosition;
    }

    private void HandleMouseUp()
    {
        if (_isGhost) return;

        if (_distance > 0.5f)
        {
            Vector2 force = directionForce * _forceMultiplier;
            force = Vector2.ClampMagnitude(force, 20f); // Optional: Limit the force magnitude
            rb.AddForce(force, ForceMode2D.Impulse);

            Debug.Log("Distance: " + _distance + " directionForce: " + directionForce);
        }
        else
        {
            transform.position = _startObjectPosition;
        }
    }

    public void Init(Vector3 directionForce)
    {
        _isGhost = true; // Set this to true if this is a ghost object

        Vector2 force = directionForce * _forceMultiplier;
        force = Vector2.ClampMagnitude(force, 20f);
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
