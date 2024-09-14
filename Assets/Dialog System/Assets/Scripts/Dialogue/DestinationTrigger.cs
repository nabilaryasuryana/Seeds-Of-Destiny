using System;
using System.Collections.Generic;
using UnityEngine;

public class DestinationTrigger : MonoBehaviour
{
    public int destinationID = 1; // ID dari tujuan untuk quest 'goTo'
    void Awake()
    {
        Debug.Log("Something entered the trigger: ");
    }

    private void Start() 
    {
        Debug.Log("DestinationTrigger script is active on: " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something entered the trigger: " + other.name); // Tambahkan ini untuk memeriksa apa yang masuk trigger
        if (other.CompareTag("Player"))
        {
            // Panggil QuestLog untuk mengecek quest 'goTo'
            QuestLog.CheckQuestObjective(Quest.Objective.Type.ClearArea, destinationID);
            Debug.Log("Player reached destination with ID: " + destinationID); // Periksa apakah kondisi ini terpenuhi
        }
    }


}