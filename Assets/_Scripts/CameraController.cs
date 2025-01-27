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
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            SwitchOrientation();
        }
        
        // if (currentOrientation != Screen.orientation)
        // {
        //     MoveCamera();
        // }
    }

    public void MoveCamera()
    {
        _camera.transform.position = MainCanvas.GetComponent<RectTransform>().rect.width > MainCanvas.GetComponent<RectTransform>().rect.height ? 
            _forHorizontalPos.position : 
            _forVerticalPos.position;
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
