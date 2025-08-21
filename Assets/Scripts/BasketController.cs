using UnityEngine;

public class BasketController : Basket
{
    //[Header("ReadOnly Parameter")]
    //[SerializeField] private bool _isGoal = false;
    //[SerializeField] private bool _isTouch = false;

    private BallController _ballObject;
    private GameObject _colliderGoal;
    private GameObject _colliderFailed;

    private void OnEnable()
    {
        DragAndDrop.OnMouseDown += HandleMouseDown;
    }

    private void OnDisable()
    {
        DragAndDrop.OnMouseDown -= HandleMouseDown;
    }

    private void Awake()
    {
        _colliderGoal = transform.GetChild(0).gameObject;
        _colliderFailed = transform.GetChild(1).gameObject;
    }

    private void Start()
    {
        Reset();
    }

    public void SetData(BallController ballController, TypesSide typesSide)
    {
        _ballObject = ballController;
        currentSide = typesSide;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_ballObject._currentState == BallState.InBasket)
        {
            _ballObject.SwitchBallFlyingState();
            return;
        }

        _ballObject.SwitchBallInBasketState(this);

        if (_isTouch) return;

        _isGoal = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isGoal) return;

        _isTouch = true;
    }

    private void HandleMouseDown()
    {
        Reset();
    }

    private void Reset()
    {
        _isGoal = false;
        _isTouch = false;
        transform.rotation = Quaternion.Euler(0, 0, 60);
    }
}
