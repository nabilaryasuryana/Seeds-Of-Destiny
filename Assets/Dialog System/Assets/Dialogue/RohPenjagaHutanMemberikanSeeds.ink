INCLUDE globals.ink

// Starting point of the story
-> grave_encounter

=== grave_encounter ===
#speaker:Forest Spirit #portrait:Roh_Hutan #layout:right #audio:spirit_appears
Welcome, young adventurer. You have finally arrived at the place where your journey truly begins.

#speaker:Aiden #portrait:Aiden #layout:left
Who... are you? And can you tell me where I can plant this Seed of Life?

#speaker:Forest Spirit #portrait:Roh_Hutan #layout:right
Patience, Aiden. Before you can plant the Seed, the forest must first be cleansed of the monsters that corrupt its land. Only then will the soil be ready to receive the Seed's power.

#speaker:Aiden #portrait:Aiden #layout:left
Cleansing the forest... Understood. What else must I do?

#speaker:Forest Spirit #portrait:Roh_Hutan #layout:right
There is one more task. You must find and bring me a sacred item that was lost in these woods. Only with this offering can the ritual to restore the land begin.

#speaker:Aiden #portrait:Aiden #layout:left
Alright, I will defeat the monsters and retrieve the item. I won't let this forest remain corrupted any longer.

#speaker:Forest Spirit #portrait:Roh_Hutan #layout:right
Good. Remember, Aiden, the forest's fate now rests in your hands. Go forth and prove your worth.

-> next_mission

=== next_mission ===
#speaker:Aiden #portrait:Aiden #layout:left #audio:determined_theme
I will not fail. The forest will be restored, no matter what it takes.
~ newClearAreaQuest("Forest purifying", "The Forest Spirit want Aiden to clear the forest from the corupted creature before plant the seed", 3, 2)
-> END
