//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OoT.API {
    
    
    public class WrapperItemEquips {
        
        [System.Text.Json.Serialization.JsonIgnoreAttribute()]
        private u32 pointer;
        
        public u8[] buttonItems {get => this._buttonItems(); set => this._buttonItems(value);}//;
        
        public u8[] cButtonSlots {get => this._cButtonSlots(); set => this._cButtonSlots(value);}//;
        
        public u16 equipment {get => this._equipment(); set => this._equipment(value);}//;
        
        public WrapperItemEquips(u32 pointer) {
           this.pointer = pointer;
        }
        
        public static uint getSize() {
          return 0x0A;
        }
        
        // #ARRCOUNT 4
        private u8[] _buttonItems() {
            u8[] bytes = new u8[4]; for(u32 i = 0; i < 4; i++){bytes[i] = Memory.RAM.ReadU8(this.pointer + 0x00 + (i * 1));} return bytes;
        }
        
        private void _buttonItems(u8[] value) {
            for(u32 i = 0; i < 4; i++){Memory.RAM.WriteU8(this.pointer + 0x00 + (i * 1), value[i]);}
        }
        
        // #ARRCOUNT 3
        private u8[] _cButtonSlots() {
            u8[] bytes = new u8[3]; for(u32 i = 0; i < 3; i++){bytes[i] = Memory.RAM.ReadU8(this.pointer + 0x04 + (i * 1));} return bytes;
        }
        
        private void _cButtonSlots(u8[] value) {
            for(u32 i = 0; i < 3; i++){Memory.RAM.WriteU8(this.pointer + 0x04 + (i * 1), value[i]);}
        }
        
        private u16 _equipment() {
            return Memory.RAM.ReadU16(this.pointer + 0x08);
        }
        
        private void _equipment(u16 value) {
            Memory.RAM.WriteU16(this.pointer + 0x08, value);
        }
    }
}
