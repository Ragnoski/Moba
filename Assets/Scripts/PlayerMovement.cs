using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _horizontalvalue;
    private float _verticalvalue;


    private void Move()
    {
        transform.Rotate(0f, _horizontalvalue, 0f);
        transform.Translate(0, 0, _verticalvalue);
    }

    private void Update()
    {
        _horizontalvalue = Input.GetAxis("Horizontal") * Time.deltaTime * 150f;
        _verticalvalue = Input.GetAxis("Vertical") * Time.deltaTime * 1.5f;

        Move();
    }
}
