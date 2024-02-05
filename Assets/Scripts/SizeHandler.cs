using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeHandler : MonoBehaviour
{
    private void Start()
    {
        if ((float)Screen.height / Screen.width < 1.5)
        {
            transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        }
        else transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
