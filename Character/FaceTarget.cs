using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    public Camera Camera;
    private void Update()
    {
        transform.LookAt(Camera.transform, Vector3.up);
    }
}
