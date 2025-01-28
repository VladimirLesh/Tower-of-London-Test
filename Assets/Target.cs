using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 targetPositionVertical;
    [SerializeField] private Transform target;


    private void Update()
    {
        return;
        
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            transform.transform.position = target.position - targetPositionVertical;
        }
        else
        {
            transform.transform.position = target.position - targetPosition;
        }
    }

    public Vector3 GetVerticalOffset() => targetPositionVertical;
    public Vector3 GetHorizontalOffset() => targetPosition;
}
