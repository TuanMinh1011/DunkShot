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
        SpawnBasket(_startPoint);
    }

    private void Update()
    {
        if (_listBasketInGame.Count < 2)
        {
            CalSpawnBasket();
        }
    }

    private void SpawnBasket(Vector3 spawnPosition)
    {
        BasketController basket = Instantiate(_basketPrefab, spawnPosition, Quaternion.identity);
        basket.SetData(_ballInGame);

        _listBasketInGame.Add(basket);
    }

    private void CalSpawnBasket()
    {
        Vector3 basketPoint = new Vector3(_startPoint.x + 3, _startPoint.y + 2, _startPoint.z);

        SpawnBasket(basketPoint);
    }
}
