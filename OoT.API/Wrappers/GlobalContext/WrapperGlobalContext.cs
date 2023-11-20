
namespace OoT.API {

    public class WrapperGlobalContext 
    {
        [System.Text.Json.Serialization.JsonIgnoreAttribute()]
        public readonly u32 pointer;

        public u16 sceneID { get => this._sceneID(); set => this._sceneID(value); }
        
        public u8 roomNum { get => this._roomID(); set => this._roomID(value); }

        public u32 scene_framecount { get => this._scene_framecount(); set => this._scene_framecount(value); }

        public u32 gameplayFrames { get => this._gameplayFrames(); set => this._gameplayFrames(value); }

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
    }
}
