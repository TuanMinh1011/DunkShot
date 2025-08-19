using UnityEngine;

[CreateAssetMenu(fileName = "BasketSO", menuName = "Scriptable Objects/BasketSO")]
public class BasketSO : ScriptableObject
{
    public TypesSide currentSide;
    public float startPointHorizontal;
    public float endPointHorizontal;
    public float startPointVertical;
    public float endPointVertical;
}
