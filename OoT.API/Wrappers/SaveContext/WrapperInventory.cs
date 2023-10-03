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

    public WrapperInventory(u32 pointer) : base(pointer, 0x5E)
    {
        InventoryItems = new InventoryItemArray(pointer);
    }

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

    private u16 _equipment()
    {
        return ReadU16(0x28);
    }

    private void _equipment(u16 value)
    {
        WriteU16(0x28, value);
    }

    private u32 _upgrades()
    {
        return ReadU32(0x2C);
    }

    private void _upgrades(u32 value)
    {
        WriteU32(0x2C, value);
    }

    private u32 _questItems()
    {
        return ReadU32(0x30);
    }

    private void _questItems(u32 value)
    {
       WriteU32(0x30, value);
    }

    // #ARRCOUNT 20
    private u8[] _dungeonItems()
    {
        u8[] bytes = new u8[20]; for (u32 i = 0; i < 20; i++) { bytes[i] = ReadU8(0x34 + (i * 1)); }
        return bytes;
    }

    private void _dungeonItems(u8[] value)
    {
        for (u32 i = 0; i < 20; i++) { WriteU8(0x34 + (i * 1), value[i]); }
    }

    // #ARRCOUNT 19
    private s8[] _dungeonKeys()
    {
        s8[] bytes = new s8[19]; for (u32 i = 0; i < 19; i++) { bytes[i] = ReadS8(0x48 + (i * 1)); }
        return bytes;
    }

    private void _dungeonKeys(s8[] value)
    {
        for (u32 i = 0; i < 19; i++) { WriteS8(0x48 + (i * 1), value[i]); }
    }

    private s8 _defenseHearts()
    {
        return ReadS8(0x5B);
    }

    private void _defenseHearts(s8 value)
    {
        WriteS8(0x5B, value);
    }

    private s16 _gsTokens()
    {
        return ReadS16(0x5C);
    }

    private void _gsTokens(s16 value)
    {
        WriteS16(0x5C, value);
    }
}

