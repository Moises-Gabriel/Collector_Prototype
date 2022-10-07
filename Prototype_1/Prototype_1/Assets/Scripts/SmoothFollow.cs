using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 Offset;
    public float damping;

    private Vector3 _velocity = Vector3.zero;

    private bool _foundTarget = false;
    void FixedUpdate()
    {
        if (!_foundTarget)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            _foundTarget = true;
        }

        Vector3 movePosition = new Vector3(target.position.x, target.position.y, -10) + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref _velocity, damping);
    }
}
