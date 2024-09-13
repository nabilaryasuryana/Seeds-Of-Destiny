INCLUDE globals.ink

// Starting point of the story
-> forest_encounter

=== forest_encounter ===
#speaker:Narrator #portrait:narrator_portrait #layout:left #audio:animal_crossing_high
Aiden walks cautiously through the dense forest, feeling a sense of unease as he ventures deeper into the unknown.

#speaker:Narrator #portrait:narrator_portrait #layout:left
Suddenly, a mysterious voice echoes through the trees, soft yet commanding, urging him forward.

#speaker:??? #portrait:unknown #layout:right
Go ahead and follow the path to the right until you find the mysterious grave.

#speaker:Aiden #portrait:Aiden #layout:left
Wh-What, what was that!?

Maybe i should follow the instruction

#speaker:Narrator #portrait:narrator_portrait #layout:left
Without knowing who or what spoke, Aiden feels compelled by the voice's mysterious nature. Trusting his instincts, he decides to follow the instructions, turning right and continuing his journey towards the unknown.

-> next_step

=== next_step ===
#speaker:Narrator #portrait:narrator_portrait #layout:left
Aiden ventures forward, feeling a mix of curiosity and caution, as the dense forest slowly reveals a hidden path leading towards the mysterious grave.
~ startQuest("firstQuest")

-> END
