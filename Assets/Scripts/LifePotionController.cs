using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePotionController : MonoBehaviour
{
    // Inspector Properties.
    public float HealthPoints = 10;
    public GameObject RewardEffect;
    private bool _collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !_collected)
        {
            Debug.Log("LifePotion Enter");
            Instantiate(RewardEffect, transform.position, Quaternion.identity);
            other.gameObject.GetComponent<PlayerHealth>().AddHealth(HealthPoints);
            Destroy(this.gameObject.transform.root.gameObject);
            _collected = true;
        }
    }
}
