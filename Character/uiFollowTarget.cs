using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiFollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform Target;
    [SerializeField]
    private Vector3 Offset;

    // Update is called once per frame
    private void Update()
    {
        transform.position = Target.position + Offset;
    }
}
