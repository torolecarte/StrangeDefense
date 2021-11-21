using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Fields.
    private int _scorePoints = 0;

    public void AddPoints(int points)
    {
        _scorePoints += points;
    }
}
