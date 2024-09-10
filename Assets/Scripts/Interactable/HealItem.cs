using UnityEngine;

[CreateAssetMenu(fileName = "New Heal Item", menuName = "Items/Heal Item")]
public class HealItem : Item
{
    public int healAmount; // Jumlah heal yang diberikan item ini

    public override void Use()
    {
        base.Use();
        // Logika untuk heal karakter atau player
        Debug.Log($"Healing for {healAmount} points.");
        PlayerHealth.Instance.HealPlayer(healAmount);
        if (destroyable)
        {
            DestroyItem();
        }
    }
}
