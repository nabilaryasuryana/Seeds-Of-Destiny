using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public string id;
    public Vector3 position;
    public int health;
    public bool isDead; // Status apakah musuh sudah mati
}
