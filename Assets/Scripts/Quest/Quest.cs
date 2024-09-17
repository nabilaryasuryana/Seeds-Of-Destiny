using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public string questID; 
    public string questName;
    public string questDescription;
    public Objective objective;
    public short questCategory;

    [System.Serializable]
    public class Objective
    {
        public enum Type { kill, ClearArea, collect, goTo }
        public int objectiveId;
        public int amount;
        public int currentAmount;
        public Type type;

        public bool CheckObjectiveCompleted(Type type, int id) {
            if (this.type == type && id == objectiveId)
                currentAmount++;
            return currentAmount >= amount;
        }

        public bool ForceAddObjective(int amount) {
            currentAmount += amount;
            return currentAmount >= amount;
        }

        public override string ToString() {
            switch (type) {
                case Type.kill:
                    return "Kill " + /* MonsterList.MonsterNameFromID(objectiveId) + " " +*/ currentAmount + "/" + amount;
                case Type.ClearArea:
                    return "Cleared " + /*+ NpcList.NpcNameFromID(objectiveId) */ currentAmount + "/" + amount + " Area";
                case Type.collect:
                    return "Collect " + /* ItemList.ItemNameFromID(objectiveId) + " " +*/ currentAmount + "/" + amount;
                case Type.goTo:
                    return "Go to the destination"/* ItemList.ItemNameFromID(objectiveId) + " " +*/;
            }
            return "";
        }
    }
}

[System.Serializable]
public class QuestProgressData
{
    public List<Quest> activeQuests;
    public List<Quest> completedQuests;
}
