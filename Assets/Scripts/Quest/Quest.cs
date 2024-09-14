using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public string questName;
    public string questDescription;
    public Objective objective;
    public short questCategory;

    [System.Serializable]
    public class Objective
    {
        public enum Type { kill, ClearArea, collect }
        public int objectiveId;
        public int amount;
        [System.NonSerialized]
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
                // case Type.goTo:
                //     return "Go to " + /* ItemList.ItemNameFromID(objectiveId) + " " +*/ currentAmount + "/" + amount;
            }
            return "";
        }
    }
}
