using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    [SerializeField] public string name;

    private void OnValidate()
    {
        name = gameObject.name;
    }
}
