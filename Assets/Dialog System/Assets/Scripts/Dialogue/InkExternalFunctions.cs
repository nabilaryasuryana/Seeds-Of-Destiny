using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions
{
    public void Bind(Story story, Animator emoteAnimator)
    {
        story.BindExternalFunction("playEmote", (string emoteName) => PlayEmote(emoteName, emoteAnimator));
        story.BindExternalFunction("startQuest", (string questName) => {
            StartQuest(questName);
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

    private void StartQuest(string questName)
    {
        Debug.Log("Quest Started: " + questName);
        // Logika untuk memulai quest atau mengubah status game.
        // Kamu bisa menambahkan quest ke daftar quest, mengubah state, dsb.
    }
    
}
