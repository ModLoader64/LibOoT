using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OoT.API
{
    public class DungeonItemArray
    {
        private u32 pointer = 0;
        public DungeonItemArray(u32 pointer)
        {
            this.pointer = pointer;
        }

        public DungeonItems this[int index]
        {
            get
            {
                if(index >= 0x14 || index < 0)
                {
                    Console.WriteLine("ERROR: Dungeon Item Slot out of bounds!");
                    return new DungeonItems();
                }
                return GetItemsFromIndex(index);
            }
            set
            {
                if (index >= 0x14 || index < 0)
                {
                    Console.WriteLine("ERROR: Dungeon Item Slot out of bounds!");
                    return;
                }
                SetItemsFromIndex(value, index);
            }
        }

        public DungeonItems[] GetItemsBuffer()
        {
            DungeonItems[] _items = new DungeonItems[0x14];
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i] = GetItemsFromIndex(i);
            }
            return _items;
        }

        public void SetItemsBuffer(DungeonItems[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                SetItemsFromIndex(items[i], i);
            }
        }

        public DungeonItems GetItemsFromIndex(int index)
        {
            DungeonItems items = new DungeonItems();
            if (index > 0x14) return items;
            u8 value = Memory.RAM.ReadU8(this.pointer + (u8)index);
            items.bossKey = ((value & 0x01) == 0x01);
            items.compass = ((value & 0x02) == 0x02);
            items.map = ((value & 0x04) == 0x04);
            return items;
        }

        public void SetItemsFromIndex(DungeonItems items, int index)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + (u8)index);
            if (items.bossKey)
            {
                value |= 0x01;
            }
            else
            {
                value &= 0xFE;
            }

            if (items.compass)
            {
                value |= 0x02;
            }
            else
            {
                value &= 0xFD;
            }

            if (items.map)
            {
                value |= 0x04;
            } else
            {
                value &= 0xFB;
            }
            Memory.RAM.WriteU8(this.pointer + (u8)index, value);
        }

    }

    public class DungeonKeyArray
    {
        private u32 pointer = 0;
        public DungeonKeyArray(u32 pointer)
        {
            this.pointer = pointer;
        }

        public DungeonKeys this[int index]
        {
            get
            {
                if (index >= 0x14 || index < 0)
                {
                    Console.WriteLine("ERROR: Dungeon Key Slot out of bounds!");
                    return new DungeonKeys();
                }
                return GetKeysFromIndex(index);
            }
            set
            {
                if (index >= 0x14 || index < 0)
                {
                    Console.WriteLine("ERROR: Dungeon Key Slot out of bounds!");
                    return;
                }
                SetKeysFromIndex(value, index);
            }
        }

        public DungeonKeys[] GetKeysBuffer()
        {
            DungeonKeys[] _keys = new DungeonKeys[0x14];
            for (int i = 0; i < _keys.Length; i++)
            {
                _keys[i] = GetKeysFromIndex(i);
            }
            return _keys;
        }

        public void SetKeysBuffer(DungeonKeys[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                SetKeysFromIndex(items[i], i);
            }
        }

        public DungeonKeys GetKeysFromIndex(int index)
        {
            DungeonKeys keys = new DungeonKeys();
            if (index > 0x14) return keys;
            keys.count = Memory.RAM.ReadU8(this.pointer + (u8)index);
            if (keys.count == 0xFF) keys.count = 0;
            return keys;
        }

        public void SetKeysFromIndex(DungeonKeys keys, int index)
        {
            if (index > 0x14) return;
            Memory.RAM.WriteU8(pointer + (u8)index, keys.count);
        }

    }

    public class WrapperDungeonManager
    {
        private u32 pointer = 0;
        public DungeonKeyArray keys {get; set;}
        public DungeonItemArray items { get; set; }

        public WrapperDungeonManager(u32 pointer)
        {
            this.pointer = pointer;
            items = new DungeonItemArray(pointer);
            keys = new DungeonKeyArray(pointer + 0x14);
        }
       
    }


    public class DungeonItems
    {
        public bool bossKey { get; set; }
        public bool compass { get; set; }
        public bool map { get; set; }
    }

    public class DungeonKeys
    {
        public u8 count { get; set; }
    }
}
