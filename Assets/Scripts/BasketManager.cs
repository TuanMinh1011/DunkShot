using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;

public class BasketManager : MonoBehaviour
{
    [SerializeField] private Vector3 _startPoint;
    [SerializeField] private BasketSO _leftBasketSO;
    [SerializeField] private BasketSO _rightBasketSO;

    private GameManager _gameManager;
    private BallController _ballPrefab;
    private BasketController _basketPrefab;

    private BallController _ballInGame;
    private BasketController _lastBasket;

    [SerializeField] private List<BasketController> _listBasketInGame = new List<BasketController>();

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
        else
        {
            if (_listBasketInGame[1]._isGoal)
            {
                RemoveBasket(0);
            }
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
        BasketSO nextBasketSO;
        Vector3 lastBasketPoint = _lastBasket.transform.position;

        switch (_lastBasket.currentSide)
        {
            case TypesSide.Left:
                nextBasketSO = _rightBasketSO;
                break;
            case TypesSide.Right:
                nextBasketSO = _leftBasketSO;
                break;
            default:
                Debug.LogError("Invalid basket side");
                return;
        }

        float xRandom = Random.Range(nextBasketSO.startPointHorizontal, nextBasketSO.endPointHorizontal);
        float yRandom = Random.Range(nextBasketSO.startPointVertical, nextBasketSO.endPointVertical);
        TypesSide nextTypeSide = nextBasketSO.side;

        Vector3 basketPoint = new Vector3(lastBasketPoint.x + xRandom, lastBasketPoint.y + yRandom, lastBasketPoint.z);
        SpawnBasket(basketPoint, nextTypeSide);
    }

    private void RemoveBasket(int index)
    {
        Destroy(_listBasketInGame[index].gameObject);
        _listBasketInGame.RemoveAt(index);
    }
}
