﻿using System.Runtime.InteropServices;
using System;
using OoT.API;

namespace OoT;

[Plugin("OoT")]
public class OcarinaOfTime : IPlugin
{
    public static void Destroy()
    {
        Console.WriteLine("OoT Destroy");
    }

    public static void Init()
    {
        Console.WriteLine("OoT Init");
    }
}

[BootstrapFilter]
public class Core : IBootstrapFilter
{
    public static bool is_save_loaded = false;
    public static bool touching_loading_zone = false;

    public static int last_known_scene = -1;
    public static int last_known_room = -1;
    public static int last_known_age;

    public static N64RomHeader romHeader;

    public static WrapperSaveContext? save;
    public static WrapperGlobalContext? global;
    public static WrapperPlayerContext? player;
    public static Helper? helper;

    public static bool isReady = false;

    public static bool DoesLoad(byte[] e)
    {
        romHeader = GetRomHeader(e);

        return ((romHeader.id == API.Enums.RomRegions.DEBUG_OOT) || (romHeader.id == API.Enums.RomRegions.NTSC_OOT && romHeader.version == (int)API.Enums.ROM_VERSIONS.N0));
    }

    public static void InitOoT()
    {
        if (romHeader.id == API.Enums.RomRegions.NTSC_OOT && romHeader.version == (int)API.Enums.ROM_VERSIONS.N0)
        {
            Console.WriteLine("OoT 1.0 NTSC");
            OoTVersionPointers.SaveContext = (Ptr)0x8011A5D0;
            OoTVersionPointers.GlobalContext = (Ptr)0x801C84A0;
            OoTVersionPointers.PlayerContext = (Ptr)0x801DAA30;
        }
        else if (romHeader.id == API.Enums.RomRegions.DEBUG_OOT)
        {
            Console.WriteLine("OoT DEBUG");
            OoTVersionPointers.SaveContext = (Ptr)0x8015E660;
            OoTVersionPointers.GlobalContext = (Ptr)0x80212020;
            OoTVersionPointers.PlayerContext = (Ptr)0x802245B0;
        }

        save = new WrapperSaveContext((uint)OoTVersionPointers.SaveContext);
        global = new WrapperGlobalContext((uint)OoTVersionPointers.GlobalContext);
        player = new WrapperPlayerContext((uint)OoTVersionPointers.PlayerContext);
        helper = new Helper(save, global, player);

        Console.WriteLine($"save: {OoTVersionPointers.SaveContext.ToString("X")}");
        Console.WriteLine($"global: {OoTVersionPointers.GlobalContext.ToString("X")}");
        Console.WriteLine($"player: {OoTVersionPointers.PlayerContext.ToString("X")}");

        isReady = true;
    }


    public static N64RomHeader GetRomHeader(byte[] rom)
    {
        var header = new u8[0x40];
        Array.Copy(rom, header, 0x40);
        GCHandle pinnedBytes = GCHandle.Alloc(header, GCHandleType.Pinned);
        N64RomHeader romHeader = (N64RomHeader)Marshal.PtrToStructure(pinnedBytes.AddrOfPinnedObject(), typeof(N64RomHeader));
        pinnedBytes.Free();
        return romHeader;
    }

    [OnInit]
    public static void OnInit(EventPluginsLoaded evt)
    {
        Console.WriteLine("OoT Core: Init");
    }

    public static void Destroy()
    {
        Console.WriteLine("Destroy");
    }

    [OnFrame]
    public static void OnTick(EventNewFrame e)
    {
        if (!isReady) { return; }

        if (is_save_loaded && helper.isTitleScreen())
        {
            Console.WriteLine("Soft Reset Detected!");
            is_save_loaded = false;
            touching_loading_zone = false;
            last_known_age = 0;
            last_known_scene = -1;
            last_known_room = -1;
            PubEventBus.bus.PushEvent(new EventSoftReset());
        }

        if (save.linkAge != last_known_age)
        {
            Console.WriteLine("Age Change: " + last_known_age + " -> " + save.linkAge);
            PubEventBus.bus.PushEvent(new EventAgeChange(last_known_age));
            last_known_age = save.linkAge;
        }
        if (!is_save_loaded && helper.isSceneNumberValid() && !helper.isTitleScreen())
        {
            is_save_loaded = true;
            PubEventBus.bus.PushEvent(new EventSaveLoaded());
            Console.WriteLine("Save Loaded");
        }
        if (helper.isLinkEnteringLoadingZone() && !touching_loading_zone)
        {
            PubEventBus.bus.PushEvent(new EventLoadingZone());
            touching_loading_zone = true;
            Console.WriteLine("Loading Zone Triggered");
        }
        if (global.scene_framecount == 1 && !helper.isTitleScreen() && helper.isSceneNumberValid())
        {
            u16 curScene = global.sceneID;
            Console.WriteLine("Scene Change: " + last_known_scene + " -> " + curScene);
            last_known_scene = curScene;
            PubEventBus.bus.PushEvent(new EventSceneChange(last_known_scene));
            touching_loading_zone = false;
        }

        u8 curRoom = global.roomNum;
        if (last_known_room != curRoom)
        {
            Console.WriteLine("Room Change: " + last_known_room + " -> " + curRoom);
            last_known_room = curRoom;
            PubEventBus.bus.PushEvent(new EventRoomChange(last_known_room));
        }

    }

    [OnViUpdate]
    public static void OnViUpdate(EventNewVi e)
    {
    }

    [OnEmulatorStart]
    public static void OnEmulatorStart(EventEmulatorStart e)
    {
        InitOoT();
    }

    [EventHandler("EventSceneChange")]
    public static void OnSceneChange(EventSceneChange evt)
    {

    }

    [EventHandler("EventRomLoaded")]
    public static void OnRomLoaded(EventRomLoaded e)
    {
        Console.WriteLine("OnRomLoaded GetRomHeader");
        var header = new u8[0x40];
        Array.Copy(e.rom, header, 0x40);
        GCHandle pinnedBytes = GCHandle.Alloc(header, GCHandleType.Pinned);
        romHeader = (N64RomHeader)Marshal.PtrToStructure(pinnedBytes.AddrOfPinnedObject(), typeof(N64RomHeader));
        pinnedBytes.Free();
    }
}
