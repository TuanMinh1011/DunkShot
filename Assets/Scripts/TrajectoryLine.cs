using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryLine : MonoBehaviour
{
    [Header("Trajectory Line Smoothness/Length")]
    [SerializeField] private int _segmentCount = 50;

    [Header("Other Value")]
    [SerializeField] private DragAndDrop _dragAndDrop;
    [SerializeField] private GameObject _ghostPrefab;
    [SerializeField] private GameObject _ballOject;

    private Vector2[] _segments;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _segments = new Vector2[_segmentCount];

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _segmentCount;

        Physics.simulationMode = SimulationMode.Script;
        CreatePhysicsScene();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SimulateTrajectory(_ghostPrefab, _ballOject.gameObject.transform.position);
            //_physicsScene.Simulate(Time.fixedDeltaTime);
        }
    }

    private Scene _simlationScene;
    private PhysicsScene _physicsScene;
    [SerializeField] private Transform _obstaclesParent;

    private void CreatePhysicsScene()
    {
        _simlationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simlationScene.GetPhysicsScene();

        foreach(Transform obj in _obstaclesParent)
        {
            var ghost = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghost.GetComponent<Renderer>().enabled = false; // Hide the ghost object
            SceneManager.MoveGameObjectToScene(ghost, _simlationScene); 
        }
    }

    private void SimulateTrajectory(GameObject prefab ,Vector3 pos)
    {
        var ghostObj = Instantiate(prefab, pos, Quaternion.identity);
        ghostObj.GetComponent<Renderer>().enabled = false; // Hide the ghost object
        SceneManager.MoveGameObjectToScene(ghostObj, _simlationScene);

        ghostObj.GetComponent<DragAndDrop>().Init(_dragAndDrop.directionForce); // Initialize the ghost object as a drag and drop object

        _lineRenderer.SetPosition(0, pos);
        for (int i = 1; i < _segmentCount; i++)
        {
            //_lineRenderer.SetPosition(i, _dragAndDrop.directionForce * _dragAndDrop.forceMultiplier);
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _lineRenderer.SetPosition(i, ghostObj.transform.position);
        }

        Destroy(ghostObj.gameObject);
    }
}
