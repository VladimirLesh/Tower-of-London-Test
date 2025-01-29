using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 targetPositionVertical;
    [SerializeField] private Transform target;

    public Vector3 GetVerticalOffset() => targetPositionVertical;
    public Vector3 GetHorizontalOffset() => targetPosition;
}
