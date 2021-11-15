using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public float SmoothingRate = 5f;

    private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - Target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetCamPosition = Target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPosition, SmoothingRate * Time.deltaTime);
    }
}
