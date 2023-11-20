using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OoT.API
{
    public class EventSaveLoaded : IEvent
    {
        public string Id { get; set; } = "EventSaveLoaded";
    }

    public class EventLoadingZone : IEvent
    {
        public string Id { get; set; } = "EventLoadingZone";
    }

    public class EventAgeChange : IEvent
    {
        public string Id { get; set; } = "EventAgeChange";
        public readonly int age;

        public EventAgeChange(int age)
        {
            this.age = age;
        }

    }

    public class EventSceneChange : IEvent
    {
        public string Id { get; set; } = "EventSceneChange";
        public readonly int scene;

        public EventSceneChange(int scene)
        {
            this.scene = scene;
        }

    }

    public class EventRoomChange : IEvent
    {
        public string Id { get; set; } = "EventRoomChange";
        public readonly int room;

        public EventRoomChange(int room)
        {
            this.room = room;
        }

    }

    public class Z64Events : IEvent
    {
        public static string ON_SAVE_LOADED = "onSaveLoaded";
        public static string ON_SCENE_CHANGE = "onSceneChange";
        public static string ON_LOADING_ZONE = "onLoadingZone";
        public static string ON_ACTOR_SPAWN = "onActorSpawn";
        public static string ON_ACTOR_DESPAWN = "onActorDespawn";
        public static string ON_ACTOR_ADDED_TO_CATEGORY = "onActorAddedToCategory";
        public static string ON_ACTOR_REMOVED_FROM_CATEGORY = "onActorRemovedFromCategory";
        public static string ON_ROOM_CHANGE = "onRoomChange";
        public static string ON_ROOM_CHANGE_PRE = "onPreRoomChange";
        public static string ON_AGE_CHANGE = "onAgeChange";
        public static string ON_SAVE_FLAG_CHANGE = "onSaveFlagChange";
        public static string ON_LOCAL_FLAG_CHANGE = "onLocalFlagChange";
        public static string ON_DAY_TRANSITION = "onDayTransition";
        public static string ON_NIGHT_TRANSITION = "onNightTransition";
        public static string ON_HEALTH_CHANGE = "onHealthChange";
        public static string ON_TUNIC_CHANGE = "onTunicChanged";
        public static string ON_ACTOR_UPDATE = "onActorUpdate";
        public static string ON_UNPAUSE = "onUnpause";

        public string Id { get; set; }
        public dynamic Data { get; set; }

        public Z64Events(dynamic optionalData = null)
        {
            this.Data = Data;
        }
    }
}
