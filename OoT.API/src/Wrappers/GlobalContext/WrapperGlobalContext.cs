
namespace OoT.API {

    public class WrapperGlobalContext 
    {
        [System.Text.Json.Serialization.JsonIgnoreAttribute()]
        public readonly u32 pointer;

        public u16 sceneID { get => this._sceneID(); set => this._sceneID(value); }
        
        public u8 roomNum { get => this._roomID(); set => this._roomID(value); }

        public u32 scene_framecount { get => this._scene_framecount(); set => this._scene_framecount(value); }

        public u32 gameplayFrames { get => this._gameplayFrames(); set => this._gameplayFrames(value); }

        public u32 liveChests { get => this._liveChests(); set => this._liveChests(value); }
        public u32 liveClear { get => this._liveClear(); set => this._liveClear(value); }
        public u32 liveSwitch { get => this._liveSwitch(); set => this._liveSwitch(value); }
        public u32 liveTemp { get => this._liveTemp(); set => this._liveTemp(value); }
        public u32 liveCollect { get => this._liveCollect(); set => this._liveCollect(value); }

        public WrapperGlobalContext(u32 pointer)
        {
            this.pointer = pointer;
        }

        private u16 _sceneID()
        {
            return Memory.RAM.ReadU16(this.pointer + 0xA4);
        }

        private void _sceneID(u16 value)
        {
            Memory.RAM.WriteU16(this.pointer + 0xA4, value);
        }

        private u8 _roomID()
        {
            return Memory.RAM.ReadU8(this.pointer + 0x11cbc);
        }

        private void _roomID(u8 value)
        {
            Memory.RAM.WriteU8(this.pointer + 0xA4, value);
        }

        private u32 _scene_framecount()
        {
            return Memory.RAM.ReadU32(this.pointer + 0x9C);
        }

        private void _scene_framecount(u32 value)
        {
            Memory.RAM.WriteU32(this.pointer + 0x9C, value);
        }

        private u32 _gameplayFrames()
        {
            return Memory.RAM.ReadU32(this.pointer + 0x11DE4);
        }

        private void _gameplayFrames(u32 value)
        {
            Memory.RAM.WriteU32(this.pointer + 0x11DE4, value);
        }

        private u32 _liveChests()
        {
            return Memory.RAM.ReadU32(this.pointer + 0x1D38);
        }

        private void _liveChests(u32 value)
        {
            Memory.RAM.WriteU32(this.pointer + 0x1D38, value);
        }

        private u32 _liveClear()
        {
            return Memory.RAM.ReadU32(this.pointer + 0x1D3C);
        }

        private void _liveClear(u32 value)
        {
            Memory.RAM.WriteU32(this.pointer + 0x1D3C, value);
        }

        private u32 _liveTemp()
        {
            return Memory.RAM.ReadU32(this.pointer + 0x1D2C);
        }

        private void _liveTemp(u32 value)
        {
            Memory.RAM.WriteU32(this.pointer + 0x1D2C, value);
        }

        private u32 _liveSwitch()
        {
            return Memory.RAM.ReadU32(this.pointer + 0x1D28);
        }

        private void _liveSwitch(u32 value)
        {
            Memory.RAM.WriteU32(this.pointer + 0x1D28, value);
        }

        private u32 _liveCollect()
        {
            return Memory.RAM.ReadU32(this.pointer + 0x1D44);
        }

        private void _liveCollect(u32 value)
        {
            Memory.RAM.WriteU32(this.pointer + 0x1D44, value);

        }
    }
}
