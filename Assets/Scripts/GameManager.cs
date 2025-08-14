using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TrajectoryLine _trajectoryLine;

    [SerializeField] private BallController _ballPrefab;
    [SerializeField] private BallController _ballObject;

    private void OnEnable()
    {
        DragAndDrop.OnMouseDrag += HandleMouseDrag;
    }

    private void HandleMouseDrag()
    {
        //_trajectoryLine.SimulateTrajectory(_ballPrefab, _ballObject.transform.position, _ballObject.directionForce);
        _trajectoryLine.SimulateTrajectory1(_ballPrefab, _ballObject.transform.position, _ballObject.directionForce);
    }
}
