INCLUDE globals.ink

// Starting point of the story
-> report_progress

=== report_progress ===
#speaker:Aiden #portrait:Aiden #layout:left #audio:animal_crossing_mid
I have successfully cleared the three areas of corrupted creatures, as you requested.

#speaker:Forest Spirit #portrait:Roh_Hutan #layout:right 
Excellent work, Aiden. However, there is one final task you must complete before the land can be fully restored.

#speaker:Aiden #portrait:Aiden #layout:left
What else do I need to do?

#speaker:Forest Spirit #portrait:Roh_Hutan #layout:right
In one of the areas you have purified, there is a building that still holds a great evil. You must enter this building and defeat the boss within. Only then will you have done enough to prepare the land for the Seed.

#speaker:Aiden #portrait:Aiden #layout:left
Understood. I will go to that building and defeat the boss. The forest's restoration will be completed.

#speaker:Forest Spirit #portrait:Roh_Hutan #layout:right
May the spirits guide you in your final battle. Return to me once you have succeeded.

-> final_mission

=== final_mission ===
#speaker:Aiden #portrait:Aiden #layout:left 
Time to finish this. I will face the boss and ensure the forest is ready for the Seed.

~ newKillQuest("Defeat the Boss", "The Forest Spirit has instructed Aiden to defeat the boss located in the building of one of the purified areas.", 1, 3)
-> END
