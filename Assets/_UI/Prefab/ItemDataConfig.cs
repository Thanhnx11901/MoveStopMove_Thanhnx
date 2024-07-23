using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataConfig", menuName = "ScriptableObjects/ItemDataConfig", order = 1)]
public class ItemDataConfig : ScriptableObject
{
    public List<ItemData> PantDataConfig = new();
    public List<ItemData> HairDataConfig = new();
    public List<ItemData> ShieldDataConfig = new();
    public List<ItemData> SetFullDataConfig = new();

    public List<ItemData> WeaponDataConfig = new();
    public ItemData GetItemData(ESkin type, int id)
    {
        switch (type)
        {
            case ESkin.Pant:
                for (int i = 0; i < PantDataConfig.Count; i++)
                {
                    if (PantDataConfig[i].Id == id)
                    {
                        return PantDataConfig[i];
                    }
                }
                break;
            case ESkin.Hair:
                for (int i = 0; i < HairDataConfig.Count; i++)
                {
                    if (HairDataConfig[i].Id == id)
                    {
                        return HairDataConfig[i];
                    }
                }
                break;
            case ESkin.Shiel:
                for (int i = 0; i < ShieldDataConfig.Count; i++)
                {
                    if (ShieldDataConfig[i].Id == id)
                    {
                        return ShieldDataConfig[i];
                    }
                }
                break;
            case ESkin.Set:
                for (int i = 0; i < SetFullDataConfig.Count; i++)
                {
                    if (SetFullDataConfig[i].Id == id)
                    {
                        return SetFullDataConfig[i];
                    }
                }
                break;
            case ESkin.Weapon:
                for (int i = 0; i < WeaponDataConfig.Count; i++)
                {
                    if (WeaponDataConfig[i].Id == id)
                    {
                        return WeaponDataConfig[i];
                    }
                }
                break;
        }
        return null;
    }

}
[System.Serializable]
public class ItemData
{
    public int Id;
    public string Name;
    public string Description;
    public Sprite icon;
    public ESkin ESkin;
    public int Price;
}
