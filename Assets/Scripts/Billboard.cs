using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    public Transform PlayerTransform;

    private void Update()
    {
        Vector3 screenpos = Camera.main.WorldToScreenPoint(PlayerTransform.position);
        this.transform.position = new Vector2(screenpos.x, screenpos.y + 100); ;
    }
}
