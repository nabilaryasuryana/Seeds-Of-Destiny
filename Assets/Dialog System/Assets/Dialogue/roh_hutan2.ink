INCLUDE globals.ink

// Starting point of the story
-> boss_defeated_report

=== boss_defeated_report ===
#speaker:Aiden #portrait:Aiden #layout:left #audio:animal_crossing_mid
I've done it! The boss has been defeated, and the corruption in the area is fading away.

#speaker:Forest Spirit #portrait:Roh_Hutan #layout:right
You have done well, Aiden. With the defeat of the corrupted creature, the forest begins to breathe again. But there is one final task that remains.

#speaker:Aiden #portrait:Aiden #layout:left
What's next? Where can I plant the Seed of Life?

#speaker:Forest Spirit #portrait:Roh_Hutan #layout:right
In the center of the area where the battle took place, you will find a sacred grove. That is where you must plant the Seed of Life to restore the forest's balance.

#speaker:Aiden #portrait:Aiden #layout:left
Understood. I will go there and plant the Seed.

#speaker:Forest Spirit #portrait:Roh_Hutan #layout:right
Go now, Aiden. The forest depends on you.

~ newGoToQuest("Plant the Seed", "Plant the Seed of Life in the sacred grove at the center of the area where the boss was defeated.", 4)

-> END
