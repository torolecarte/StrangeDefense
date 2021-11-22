using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    // Inspector Properties.
    public float TimeToLive;

    void Start()
    {
        Destroy(gameObject, TimeToLive);
    }
}
