using UnityEngine;

public enum TypesSide
{
    None,
    Left,
    Right
}

public class Basket: MonoBehaviour
{
    [Header("Default Parameter")]
    public TypesSide currentSide;
    public bool _isGoal = false;
    public bool _isTouch = false;
}
