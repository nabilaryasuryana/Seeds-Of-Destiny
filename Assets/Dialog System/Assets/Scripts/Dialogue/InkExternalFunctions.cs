using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.SceneManagement;  // Pastikan Anda mengimpor namespace ini

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
        story.BindExternalFunction("newKillQuest", (string questName, string questDescription, int amount, int questId) => {
            NewKillQuest(questName, questDescription, amount, questId);
        });
        story.BindExternalFunction("ending", () => {
            Ending();
        });
    }

    public void Unbind(Story story) 
    {
        story.UnbindExternalFunction("playEmote");
        story.UnbindExternalFunction("newClearAreaQuest");
        story.UnbindExternalFunction("newGoToQuest");
        story.UnbindExternalFunction("newKillQuest");
        story.UnbindExternalFunction("ending");
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
        Quest newQuest = new Quest();
        newQuest.questID = Guid.NewGuid().ToString();  // Inisialisasi questID dengan GUID unik
        newQuest.questName = questName;
        newQuest.questDescription = questDescription;
        newQuest.questCategory = 0;

        newQuest.objective = new Quest.Objective();
        newQuest.objective.type = Quest.Objective.Type.ClearArea;
        newQuest.objective.amount = amount;
        newQuest.objective.objectiveId = areaId; // Tetapkan ID area yang ingin dibersihkan

        QuestLog.AddQuest(newQuest);
    }

    private void NewGoToQuest(string questName, string questDescription, int destinationId)
    {
        Quest newQuest = new Quest();
        newQuest.questID = Guid.NewGuid().ToString();  // Inisialisasi questID dengan GUID unik
        newQuest.questName = questName;
        newQuest.questDescription = questDescription;
        newQuest.questCategory = 0;

        newQuest.objective = new Quest.Objective();
        newQuest.objective.type = Quest.Objective.Type.goTo;
        newQuest.objective.amount = 1;
        newQuest.objective.objectiveId = destinationId; // Tetapkan ID tujuan

        QuestLog.AddQuest(newQuest);
    }

    private void NewKillQuest(string questName, string questDescription, int amount, int questId)
    {
        Quest newQuest = new Quest();
        newQuest.questID = Guid.NewGuid().ToString();  // Inisialisasi questID dengan GUID unik
        newQuest.questName = questName;
        newQuest.questDescription = questDescription;
        newQuest.questCategory = 0;

        newQuest.objective = new Quest.Objective();
        newQuest.objective.type = Quest.Objective.Type.kill;
        newQuest.objective.amount = amount;
        newQuest.objective.objectiveId = questId; // Tetapkan ID tujuan

        QuestLog.AddQuest(newQuest);
    }

    // Fungsi untuk memuat scene dengan nama "ending"
    private void Ending()
    {
        SceneManager.LoadScene("ending");
    }
}
