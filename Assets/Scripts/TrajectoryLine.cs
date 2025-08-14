using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryLine : MonoBehaviour
{
    [Header("Trajectory Line Smoothness/Length")]
    [SerializeField] private int _segmentCount = 50;

    [Header("Other Value")]
    [SerializeField] private Transform _obstaclesParent;

    private LineRenderer _lineRenderer;

    private Scene _simlationScene;
    private PhysicsScene2D _physicsScene;

    private void OnEnable()
    {
        DragAndDrop.OnMouseUp += HandleMouseUp;
    }
    private void OnDisable()
    {
        DragAndDrop.OnMouseUp -= HandleMouseUp;
    }

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        CreatePhysicsScene();
    }

    private void CreatePhysicsScene()
    {
        _simlationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        _physicsScene = _simlationScene.GetPhysicsScene2D();

        foreach(Transform obj in _obstaclesParent)
        {
            var ghost = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghost.GetComponent<Renderer>().enabled = false; // Hide the ghost object
            SceneManager.MoveGameObjectToScene(ghost, _simlationScene); 
        }
    }

    public void SimulateTrajectory(BallController prefab ,Vector3 pos, Vector3 directionForce)
    {
        var ghostObj = Instantiate(prefab, pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, _simlationScene);

        ghostObj.Init(directionForce);

        _lineRenderer.positionCount = _segmentCount;

        for (int i = 0; i < _segmentCount; i++)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _lineRenderer.SetPosition(i, ghostObj.transform.position);
        }

        Destroy(ghostObj.gameObject);
    }

    private BallController ghostObjReal;
    public void SimulateTrajectory1(BallController prefab, Vector3 pos, Vector3 directionForce)
    {
        if (ghostObjReal == null)
        {
            ghostObjReal = Instantiate(prefab, pos, Quaternion.identity);
            ghostObjReal.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObjReal.gameObject, _simlationScene);
        }
        else
        {
            ghostObjReal.transform.position = pos;
            ghostObjReal.transform.rotation = Quaternion.identity; // Reset rotation
        }

        ghostObjReal.gameObject.SetActive(true);

        ghostObjReal.Init(directionForce);

        _lineRenderer.positionCount = _segmentCount;

        for (int i = 0; i < _segmentCount; i++)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _lineRenderer.SetPosition(i, ghostObjReal.transform.position);
        }

        ghostObjReal.gameObject.SetActive(false);
        //Destroy(ghostObj.gameObject);
    }

    private void HandleMouseUp()
    {
        _lineRenderer.positionCount = 0;
    }
}
