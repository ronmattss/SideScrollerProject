using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SideScrollerProject
{
    // what will this do?
    // this will allow player to have an inventory system
    // objects that are marked as Item can be stored in the inventory
    // currency is also stored in the players inventory
    // keys and unique items can't be stack
    public class PlayerInventory : MonoBehaviour
    {
        public List<Item> itemInventory = new List<Item>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        //find item inside player's inventory
        public Item FindItem(string name)
        {
            Item item = itemInventory.First(x => x.name == name);
            if (item != null)
                return item;
            else
                return null;

        }
        public void RemoveItem(string name)
        {
            Item item = FindItem(name);
            

        }
        // consume Item if it is not unique
        public void ConsumeItem(string name)
        {
            Item consumableItem = FindItem(name);
            if (consumableItem == null) return;
            if (!consumableItem.isUnique)
            {
                consumableItem.stack--;

            }
            else
            {

            }

        }
    }
}