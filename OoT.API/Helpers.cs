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
            return (this.save.fileNum == 0xFF || this.save.fileNum == 0xFEDC);
        }

        public bool isSceneNumberValid()
        {
            return this.global.sceneID <= 0xFF;
        }

        public bool isPaused() //PauseContext->state
        {
            return Memory.RAM.ReadU16(global.pointer + 0x10934) != 0x0;
        }

        public bool isInterfaceShown()  //InterfaceContext->magicAlpha; // also Rupee and Key counters alpha
        {
            return Memory.RAM.ReadU16(global.pointer + 0x10742) == 0xFF;
        }
    }
}
  
