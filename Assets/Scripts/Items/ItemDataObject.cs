using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items{
[CreateAssetMenu(fileName = "ItemDataObject", menuName = "Items/ItemList", order = 1)]
public class ItemDataObject : ScriptableObject {
    [SerializeField]
    public List<KeyValuePair<int, ItemInfo>> list;
    public List<ItemData> rawList;
    public void Initialize() {
        list = new List<KeyValuePair<int, ItemInfo>>();
        for (int i = 0; i < rawList.Count; i++) {
            list.Add(new KeyValuePair<int, ItemInfo>(rawList[i].ID, rawList[i].Info));
        }
    }
    public String GetClassNameByID(int ID) {
        KeyValuePair<int, ItemInfo> resultPair = list.FirstOrDefault(pair => pair.Key == ID);
        return resultPair.Value.ClassName;
    }
    public ItemInfo GetItemInfoByID(int ID) {
        KeyValuePair<int, ItemInfo> resultPair = list.FirstOrDefault(pair => pair.Key == ID);
        return resultPair.Value;
    }
} 
}