using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Canvas MainCanvas;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _forVerticalPos, _forHorizontalPos;
    
    private Camera _camera;
    private ScreenOrientation currentOrientation;

    private List<Transform> _points = new List<Transform>();

    private bool _isSwitch = true;

    private void Start()
    {
        _camera = Camera.main;
        currentOrientation = Screen.orientation;
        MoveCamera();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            SwitchOrientation();
        }
        
        if (currentOrientation != Screen.orientation)
        {
            MoveCamera();
            currentOrientation = Screen.orientation;
        }
    }

    private void MoveCamera()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            _camera.transform.DOMove(_forVerticalPos.position, _speed);
        }
        else
        {
            _camera.transform.DOMove(_forHorizontalPos.position, _speed);
        }
        
        // _camera.transform.position = Screen.orientation == ScreenOrientation.Portrait ? 
        //      _forVerticalPos.position : 
        //     _forHorizontalPos.position;
    }

    public void SwitchOrientation()
    {
        if (_isSwitch)
        {
            _camera.transform.DOMove(_forVerticalPos.position, _speed);
        }
        else
        {
            _camera.transform.DOMove(_forHorizontalPos.position, _speed);
        }
        
        _isSwitch = !_isSwitch;
    }
}
