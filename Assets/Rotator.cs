using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 10f;

    private void OnEnable()
    {
        RotatorsManager.instance.RegisterRotator(this);
    }

    private void OnDisable()
    {
        RotatorsManager.instance.UnRegisterRotator(this);
    }
}
