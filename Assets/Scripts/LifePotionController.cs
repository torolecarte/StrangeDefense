using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePotionController : MonoBehaviour
{
    public float HealthPoints = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player")
            return;

        Debug.Log("LifePotion Enter");
        other.gameObject.GetComponent<PlayerHealth>().AddHealth(HealthPoints);
        Destroy(this.gameObject.transform.root.gameObject);
    }
}
