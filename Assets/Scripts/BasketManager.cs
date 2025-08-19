using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;

public class BasketManager : MonoBehaviour
{
    [SerializeField] private Vector3 _startPoint;

    private GameManager _gameManager;
    private BallController _ballPrefab;
    private BasketController _basketPrefab;

    private BallController _ballInGame;
    private BasketController _lastBasket;

    private List<BasketController> _listBasketInGame = new List<BasketController>();

    private void Awake()
    {
        _gameManager = GetComponentInParent<GameManager>();
        _ballPrefab = _gameManager._ballPrefab;
        _basketPrefab = _gameManager._basketPrefab;
        _ballInGame = _gameManager._ballInGame;
    }

    private void Start()
    {
        SpawnBasket(_startPoint, TypesSide.Left);
    }

    private void Update()
    {
        if (_listBasketInGame.Count < 2)
        {
            CalSpawnBasket();
        }
    }

    private void SpawnBasket(Vector3 spawnPosition, TypesSide typesSide)
    {
        BasketController basket = Instantiate(_basketPrefab, spawnPosition, Quaternion.identity);
        basket.SetData(_ballInGame, typesSide);
        _lastBasket = basket;

        _listBasketInGame.Add(basket);
    }

    private void CalSpawnBasket()
    {
        TypesSide nextTypeSide = TypesSide.None;

        Vector3 lastBasketPoint = _lastBasket.transform.position;

        if (_lastBasket.currentSide == TypesSide.Left)
        {
            nextTypeSide = TypesSide.Right;
        }
        else
        {
            nextTypeSide = TypesSide.Left;
        }

        Vector3 basketPoint = new Vector3(lastBasketPoint.x + 3, lastBasketPoint.y + 2, lastBasketPoint.z);
        SpawnBasket(basketPoint, nextTypeSide);
    }
}
