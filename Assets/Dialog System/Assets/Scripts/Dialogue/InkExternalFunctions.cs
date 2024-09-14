using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions
{
    public void Bind(Story story, Animator emoteAnimator)
    {
        story.BindExternalFunction("playEmote", (string emoteName) => PlayEmote(emoteName, emoteAnimator));
        story.BindExternalFunction("newClearAreaQuest", (string questName, string questDescription, int questAmount, int areaId) => {
            NewClearAreaQuest(questName, questDescription, questAmount, areaId);
        });
        story.BindExternalFunction("newGoToQuest", (string questName, string questDescription, int destinationId) => {
            NewGoToQuest(questName, questDescription, destinationId);
        });
    }


    public void Unbind(Story story) 
    {
        story.UnbindExternalFunction("playEmote");
    }

    public void PlayEmote(string emoteName, Animator emoteAnimator)
    {
        if (emoteAnimator != null) 
        {
            emoteAnimator.Play(emoteName);
        }
        else 
        {
            Debug.LogWarning("Tried to play emote, but emote animator was "
                + "not initialized when entering dialogue mode.");
        }
    }

    // Method NewQuest untuk membuat quest dengan parameter yang diberikan
    private void NewClearAreaQuest(string questName, string questDescription, int amount, int areaId)
    {
        // Buat instance quest baru
        Quest newQuest = new Quest();
        
        // Set properti quest sesuai dengan parameter
        newQuest.questName = questName;
        newQuest.questDescription = questDescription;
        newQuest.questCategory = 0; // Atur kategori jika ada, misalnya 0 untuk kategori umum
        
        // Buat objective baru sesuai dengan parameter
        newQuest.objective = new Quest.Objective();
        newQuest.objective.type = Quest.Objective.Type.ClearArea;
        newQuest.objective.amount = amount;
        newQuest.objective.objectiveId = areaId; // Tetapkan ID area yang ingin dibersihkan

        // Tambahkan quest ke QuestLog
        QuestLog.AddQuest(newQuest);
    }


    private void NewGoToQuest(string questName, string questDescription, int destinationId)
    {
        // Buat instance quest baru
        Quest newQuest = new Quest();
        
        // Set properti quest sesuai dengan parameter
        newQuest.questName = questName;
        newQuest.questDescription = questDescription;
        newQuest.questCategory = 0; // Atur kategori jika ada, misalnya 0 untuk kategori umum
        
        // Buat objective baru sesuai dengan parameter
        newQuest.objective = new Quest.Objective();
        newQuest.objective.type = Quest.Objective.Type.goTo;
        newQuest.objective.amount = 1;
        newQuest.objective.objectiveId = destinationId; // Tetapkan ID tujuan

        // Tambahkan quest ke QuestLog
        QuestLog.AddQuest(newQuest);
    }


    
}
