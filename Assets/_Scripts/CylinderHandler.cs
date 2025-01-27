using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BNG;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Serialization;

public class CylinderHandler : MonoBehaviour
{
    [SerializeField] private GameObject cylinderPrefab;
    
    [SerializeField] private GameObject _heldCylinder;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    
    public List<GameObject> _cylinders;

    private float MagazineDistance;
    
    public float ClipSnapDistance = 0.075f;
    public float ClipUnsnapDistance = 0.15f;
    public float sumOffset;
    
    private bool magazineInPlace = false;
    private bool lockedInPlace = false;
    private bool _isGeted = false;

    private Grabber[] grabbers;

    private float _minOffset = -0.18f;
    private float _maxOffset = 0.18f;

    private void Start()
    {
        sumOffset = _minOffset;
        grabbers = FindObjectsOfType<Grabber>();
        
        GameObject c1_instance = Instantiate(cylinderPrefab, _startPoint.position, _startPoint.rotation);
        // GameObject c2_instance = Instantiate(cylinderPrefab, _startPoint.position, _startPoint.rotation);
        // GameObject c3_instance = Instantiate(cylinderPrefab, _startPoint.position, _startPoint.rotation);
        // GameObject c4_instance = Instantiate(cylinderPrefab, _startPoint.position, _startPoint.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cylinder _cylinder))
        {
            other.GetComponent<BoxCollider>().isTrigger = true;
            
            _heldCylinder = other.gameObject;
            _cylinders.Add(other.gameObject);
            _cylinder.isPut = true;
            
            // foreach (var grabber in grabbers)
            // {
            //     grabber.onReleaseEvent.AddListener(getCylinder);
            // }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Cylinder cylinder))
        {
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(cylinderPrefab, _startPoint.position, _startPoint.rotation);
        }
        
        if (!_heldCylinder) return;
        if (!_cylinders.Any()) return;
        
        for (int i = 0; i < _cylinders.Count; i++)
        {
            _cylinders[i].transform.parent = transform;

            // Lock in place immediately
            if (lockedInPlace) {
                _cylinders[i].transform.localPosition = Vector3.zero;
                _cylinders[i].transform.localEulerAngles = Vector3.zero;
                return;
            }

            Vector3 localPos = _cylinders[i].transform.localPosition;
            _cylinders[i].transform.localEulerAngles = Vector3.zero;

            // Only allow Y translation. Don't allow to go up and through clip area
            float localY = localPos.y;
            print(localY);

            float Offset = 0;
            for (int j = i; j > 0; j--)
            {
                Offset += _cylinders[j].transform.GetChild(0).localScale.y / 2f;
            }
            _minOffset = Offset;
            print("offset" + Offset);
        
            if(localY > ClipUnsnapDistance)
            {
                _cylinders[i].transform.parent = null;
                _cylinders[i].GetComponent<Cylinder>().isPut = false;
                _cylinders.RemoveAt(i);
                return;
            }
        
            if(localY > _maxOffset) {
                localY = _maxOffset;
            }
            else if(localY < _minOffset) {
                localY = _minOffset;
            }
        
            _cylinders[i].transform.localPosition = new Vector3(0f, localY, 0f);
            // moveMagazine(new Vector3(0, localY, 0));

            MagazineDistance = Vector3.Distance(transform.position, _heldCylinder.transform.position);
            print(MagazineDistance);
               
            bool clipRecentlyGrabbed = Time.time - _heldCylinder.GetComponentInChildren<Grabbable>().LastGrabTime < 1f;
        }
        
        
    }

    void moveMagazine(Vector3 localPosition) {
        _heldCylinder.transform.localPosition = localPosition;
    }

    private void getCylinder(Grabbable grabbed)
    {
        Cylinder cylinder = grabbed.GetComponent<Cylinder>();
        if (cylinder)
        {
            cylinder.isPut = true;
            cylinder.SetCollisionForHands(true);
            _cylinders.Add(cylinder.gameObject);
            _heldCylinder = null;
        }

        foreach (GameObject c in _cylinders)
        {
            c.transform.localPosition = new Vector3(0f, -0.18f, 0f);
        }
    }
}
