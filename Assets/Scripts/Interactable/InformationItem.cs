using UnityEngine;

[CreateAssetMenu(fileName = "New Information Item", menuName = "Items/Information Item")]
public class InformationItem : Item
{
    public string informationText; // Informasi yang akan ditampilkan

    public override void Use()
    {
        base.Use();
        // Menampilkan informasi item menggunakan UIManager
        UIManager.Instance.ShowUseItemInfo(informationText);
        
        if (destroyable)
        {
            DestroyItem();
        }
    }
}
