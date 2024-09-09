using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle Item", menuName = "Items/Puzzle Item")]
public class PuzzleItem : Item
{
    public string puzzleID; // ID unik untuk mengenali puzzle mana yang bisa diselesaikan

    public override void Use()
    {
        base.Use();
        // Logika khusus untuk menggunakan item ini dalam konteks puzzle
        Debug.Log($"Using puzzle item: {itemName} with Puzzle ID: {puzzleID}");
        // Implementasikan logika sesuai dengan kebutuhan puzzle di game
        if (destroyable)
        {
            DestroyItem();
        }
    }
}
