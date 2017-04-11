using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerForce : Power
{
    public int StartValue;


    private void Start()
    {
        base.MaxValue = StartValue;
        base.Initialize();
    }
}