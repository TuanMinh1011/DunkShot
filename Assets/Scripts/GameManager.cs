using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefab")]
    public BallController _ballPrefab;
    public BasketController _basketPrefab;

    [Header("Game Objects")]
    public BallController _ballInGame;
}
