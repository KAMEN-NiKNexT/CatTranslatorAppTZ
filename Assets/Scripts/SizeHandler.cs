using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeHandler : MonoBehaviour
{
    [SerializeField] private GameObject _background;

    private void Start()
    {
        if ((float)Screen.height / Screen.width < 1.5)
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            _background.transform.localScale = new Vector3(1.35f, 1.35f, 1.35f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            _background.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
