using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckPointed : MonoBehaviour
{
    [HideInInspector] public bool pointed;
    void Start()
    {
        pointed = false;
    }
}
