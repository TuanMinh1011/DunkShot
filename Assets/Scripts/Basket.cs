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
    public float startPointHorizontal;
    public float endPointHorizontal;
    public float startPointVertical;
    public float endPointVertical;
}
