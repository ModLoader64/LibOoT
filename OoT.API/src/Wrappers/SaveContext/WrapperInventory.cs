using OoT.API.Wrappers.SaveContext;
using System.Reflection;

namespace OoT.API;

public class InventoryItemArray : MemoryObject
{
    public InventoryItemArray(u32 pointer) : base(pointer, (u32)InventorySlot.COUNT) { }

    /// <summary>
    /// Get the InventoryItem within InventorySlot slot
    /// </summary>
    /// <param name="slot">The slot to get the item from</param>
    /// <returns>The InventoryItem within the slot</returns>
    public InventoryItem this[InventorySlot slot]
    {
        get
        {
            if (slot >= InventorySlot.COUNT || slot < 0)
            {
                Console.WriteLine("ERROR: Inventory Slot out of bounds!");
                return InventoryItem.NONE;
            }
            return GetItemInSlot(slot);
        }
        set
        {
            if (slot >= InventorySlot.COUNT || slot < 0)
            {
                Console.WriteLine("ERROR: Inventory Slot out of bounds!");
                return;
            }
            SetItemInSlot(slot, value);
        }
    }

    /// <summary>
    /// Get the InventoryItem within the slot index. Note that bounds checking is disabled on this version.
    /// </summary>
    /// <param name="slot">The slot to get the item from</param>
    /// <returns>The InventoryItem within the slot</returns>
    public InventoryItem this[s32 slot]
    {
        get
        {
            return GetItemInSlot((InventorySlot)slot);
        }
        set
        {
            SetItemInSlot((InventorySlot)slot, value);
        }
    }

    public InventoryItem GetItemInSlot(InventorySlot slot, InventoryItem[]? whitelist = null)
    {
        InventoryItem item = (InventoryItem)ReadU8((u8)slot);
        if (whitelist == null)
        {
            return item;
        }
        else if (whitelist.Contains(item))
        {
            return item;
        }

        return InventoryItem.NONE;
    }

    public void SetItemInSlot(InventorySlot slot, InventoryItem item, InventoryItem[]? whitelist = null)
    {
        if (whitelist == null)
        {
            WriteU8((u8)slot, (u8)item);
        }
        else if (whitelist.Contains(item))
        {
            WriteU8((u8)slot, (u8)item);
        }
    }
}

public class WrapperInventory : MemoryObject
{
    public InventoryItemArray InventoryItems { get; set; }
    private u32 pointer = 0;
    public WrapperInventory(u32 pointer) : base(pointer, 0x5E)
    {
        InventoryItems = new InventoryItemArray(pointer);
        this.pointer = pointer;
    }

    public WrapperEquipment equipment { get => this._equipment(); set => this._equipment(value); }
    public WrapperQuestStatus questStatus { get => this._questItems(); set => this._questItems(value); }
    public WrapperUpgrades upgrades { get => this._upgrades(); set => this._upgrades(value); }
    public WrapperDungeonManager dungeon { get => this._dungeon(); set => this._dungeon(value); }

    public s8 defenseHearts { get => this._defenseHearts(); set => this._defenseHearts(value); }
    public s16 gsTokens { get => this._gsTokens(); set => this._gsTokens(value); }

    // #ARRCOUNT 16
    private s8[] _ammo()
    {
        s8[] bytes = new s8[16]; for (u32 i = 0; i < 16; i++) { bytes[i] = ReadS8(0x18 + (i * 1)); }
        return bytes;
    }

    private void _ammo(s8[] value)
    {
        for (u32 i = 0; i < 16; i++) { WriteS8(0x18 + (i * 1), value[i]); }
    }

    private WrapperEquipment _equipment()
    {
        return new WrapperEquipment(this.pointer + 0x0028);
    }

    private void _equipment(WrapperEquipment value)
    {
        
    }

    private WrapperUpgrades _upgrades()
    {
        return new WrapperUpgrades(this.pointer + 0x2C);
    }

    private void _upgrades(WrapperUpgrades value)
    {

    }

    private WrapperQuestStatus _questItems()
    {
        return new WrapperQuestStatus(this.pointer + 0x30);
    }

    private void _questItems(WrapperQuestStatus value)
    {

    }

    private void _questItems(u32 value)
    {
       WriteU32(0x30, value);
    }

    // #ARRCOUNT 20
    private WrapperDungeonManager _dungeon()
    {
        return new WrapperDungeonManager(this.pointer + 0x34); 
    }

    private void _dungeon(WrapperDungeonManager value)
    {
    }

    private s8 _defenseHearts()
    {
        return (s8)Memory.RAM.ReadU8(this.pointer + 0x5B);
    }

    private void _defenseHearts(s8 value)
    {
        Memory.RAM.WriteU8(this.pointer + 0x5B, (u8)value);
    }

    private s16 _gsTokens()
    {
        return (s16)Memory.RAM.ReadU16(this.pointer + 0x5C);
    }

    private void _gsTokens(s16 value)
    {
        Memory.RAM.WriteU16(this.pointer + 0x5C, (u16)value);
    }
}

