using System;
using UnityEngine;

public enum BallState
{
    None,
    InBasket,
    Flying
}

public class BallController : MonoBehaviour
{
    [SerializeField] private float _forceMultiplier = 5f;
    [SerializeField] private float _clampValue = 20f;

    public BallState _currentState;
    public Vector3 directionForce;

    private Vector3 _mousePosition;
    private Vector3 _startObjectPosition;
    private float _distance;
    private bool _isGhost = false;

    private BasketController _currentBasket;
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
        if (_isGhost || _currentState != BallState.InBasket) return;

        _mousePosition = Input.mousePosition - GetMousePos();
        _startObjectPosition = transform.position;
    }

    private void HandleMouseDrag()
    {
        if (_isGhost || _currentState != BallState.InBasket) return;

        rb.simulated = false;
        transform.position = _currentBasket.transform.position;

        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePosition);
        _distance = Vector3.Distance(newPosition, _startObjectPosition);
        directionForce = _startObjectPosition - newPosition;

        Quaternion rotaionBasket = Quaternion.LookRotation(_currentBasket.transform.forward, directionForce) * Quaternion.Euler(0, 0, 60);
        _currentBasket.transform.rotation = new Quaternion(rotaionBasket.x, rotaionBasket.y, rotaionBasket.z, rotaionBasket.w);
        //Debug.Log("rotation: "  + _currentBasket.transform.forward);
    }

    private void HandleMouseUp()
    {
        if (_isGhost || _currentState != BallState.InBasket) return;

        if (_distance > 0.5f)
        {
            rb.simulated = true;

            Vector2 force = directionForce * _forceMultiplier;
            force = Vector2.ClampMagnitude(force, _clampValue);
            rb.AddForce(force, ForceMode2D.Impulse);
            //Debug.Log("Distance: " + _distance + " directionForce: " + directionForce);
        }
        else
        {
            transform.position = _startObjectPosition;
        }
    }

    public void SwitchBallFlyingState()
    {
        _currentState = BallState.Flying;
    }

    public void SwitchBallInBasketState(BasketController basketController)
    {
        _currentState = BallState.InBasket;

        _currentBasket = basketController;
    }

    public void Init(Vector3 directionForce)
    {
        _isGhost = true; // Set this to true if this is a ghost object

        rb.angularVelocity = 0f; // Reset angular velocity
        rb.linearVelocity = Vector2.zero; // Reset angular velocity

        Vector2 force = directionForce * _forceMultiplier;
        force = Vector2.ClampMagnitude(force, _clampValue);
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
