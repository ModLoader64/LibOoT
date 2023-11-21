namespace OoT.API
{
    public class Helper
    {
        private WrapperSaveContext? save;
        private WrapperGlobalContext? global;
        private WrapperPlayerContext? player;

        public Helper(WrapperSaveContext save, WrapperGlobalContext global, WrapperPlayerContext player)
        {
            this.save = save;
            this.global = global;
            this.player = player;
        }

        public bool isTitleScreen()
        {
            return save.gameMode != 0;
        }

        public bool isSceneNumberValid()
        {
            return this.global.sceneID <= 0xFF;
        }

        public bool isPaused() //PauseContext->state
        {
            return Memory.RAM.ReadU16(this.global.pointer + 0x10934) != 0x0;
        }

        public bool isInterfaceShown()  //InterfaceContext->magicAlpha; // also Rupee and Key counters alpha
        {
            return Memory.RAM.ReadU16(this.global.pointer + 0x10742) == 0xFF;
        }

        public bool isLinkEnteringLoadingZone()
        {
            u32 r = this.player.stateFlags1;
            return (r & 0x000000ff) == 1 || this.player.stateFlags1 == 0x20000001 || this.player.stateFlags1 == 0x80000000; // TODO: Actually map state flags
        }
    }
}

