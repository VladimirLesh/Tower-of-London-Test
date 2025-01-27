using System;
using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    private Grabbable _grabbable;
    private BoxCollider _collider;

    public bool isPut;

    private void Awake()
    {
        _grabbable = GetComponent<Grabbable>();
        _collider = GetComponent<BoxCollider>();
    }

    public void SetCollisionForHands(bool value)
    {
        _grabbable.enabled = value;
    }
}
