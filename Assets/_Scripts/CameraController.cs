using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Canvas MainCanvas;
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

    public void MoveCamera()
    {
        _camera.transform.position = Screen.orientation == ScreenOrientation.Portrait ? 
            _forVerticalPos.position : 
            _forHorizontalPos.position;
    }

    public void SwitchOrientation()
    {
        if (_isSwitch)
        {
            _camera.transform.position = _forHorizontalPos.position;
        }
        else
        {
            _camera.transform.position = _forVerticalPos.position;
        }
        
        _isSwitch = !_isSwitch;
    }
}
