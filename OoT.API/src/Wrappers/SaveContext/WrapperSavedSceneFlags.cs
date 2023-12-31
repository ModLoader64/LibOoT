using Buffer = NodeBuffer.Buffer;

namespace OoT.API {
    
    
    public class WrapperSavedSceneFlags {
        
        [System.Text.Json.Serialization.JsonIgnoreAttribute()]
        private u32 pointer;
        
        public u32 chest {get => this._chest(); set => this._chest(value);}//;
        
        public u32 swch {get => this._swch(); set => this._swch(value);}//;
        
        public u32 clear {get => this._clear(); set => this._clear(value);}//;
        
        public u32 collect {get => this._collect(); set => this._collect(value);}//;
        
        public u32 unk {get => this._unk(); set => this._unk(value);}//;
        
        public u32 rooms {get => this._rooms(); set => this._rooms(value);}//;
        
        public u32 floors {get => this._floors(); set => this._floors(value);}//;
        
        public WrapperSavedSceneFlags(u32 pointer) {
           this.pointer = pointer;
        }
        
        public static uint getSize() {
          return 0x1C;
        }
        
        public Buffer GetRawBuffer()
        {
            return new Buffer(new u32[]{ chest, swch, clear, collect, unk, rooms, floors });
        }

        public void SetRawBuffer(Buffer buf)
        {
            if (buf.Size != 0x1C) return;
            chest = buf.ReadU32(0x0);
            swch = buf.ReadU32(0x4);
            clear = buf.ReadU32(0x8);
            collect = buf.ReadU32(0x0C);
            unk = buf.ReadU32(0x10);
            rooms = buf.ReadU32(0x14);
            floors = buf.ReadU32(0x18);
        }

        private u32 _chest() {
            return Memory.RAM.ReadU32(this.pointer + 0x00);
        }
        
        private void _chest(u32 value) {
            Memory.RAM.WriteU32(this.pointer + 0x00, value);
        }
        
        private u32 _swch() {
            return Memory.RAM.ReadU32(this.pointer + 0x04);
        }
        
        private void _swch(u32 value) {
            Memory.RAM.WriteU32(this.pointer + 0x04, value);
        }
        
        private u32 _clear() {
            return Memory.RAM.ReadU32(this.pointer + 0x08);
        }
        
        private void _clear(u32 value) {
            Memory.RAM.WriteU32(this.pointer + 0x08, value);
        }
        
        private u32 _collect() {
            return Memory.RAM.ReadU32(this.pointer + 0x0C);
        }
        
        private void _collect(u32 value) {
            Memory.RAM.WriteU32(this.pointer + 0x0C, value);
        }
        
        private u32 _unk() {
            return Memory.RAM.ReadU32(this.pointer + 0x10);
        }
        
        private void _unk(u32 value) {
            Memory.RAM.WriteU32(this.pointer + 0x10, value);
        }
        
        private u32 _rooms() {
            return Memory.RAM.ReadU32(this.pointer + 0x14);
        }
        
        private void _rooms(u32 value) {
            Memory.RAM.WriteU32(this.pointer + 0x14, value);
        }
        
        private u32 _floors() {
            return Memory.RAM.ReadU32(this.pointer + 0x18);
        }
        
        private void _floors(u32 value) {
            Memory.RAM.WriteU32(this.pointer + 0x18, value);
        }
    }
}
