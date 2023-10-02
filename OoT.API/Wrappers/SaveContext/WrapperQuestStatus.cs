
namespace OoT.API
{

    public class WrapperQuestStatus
    {

        [System.Text.Json.Serialization.JsonIgnoreAttribute()]
        private u32 pointer;

        public u16 equipment { get => this._equipment(); set => this._equipment(value); }//;

        public u32 upgrades { get => this._upgrades(); set => this._upgrades(value); }//;

        public u32 questItems { get => this._questItems(); set => this._questItems(value); }//;

        public u8[] dungeonItems { get => this._dungeonItems(); set => this._dungeonItems(value); }//;

        public s8[] dungeonKeys { get => this._dungeonKeys(); set => this._dungeonKeys(value); }//;

        public s8 defenseHearts { get => this._defenseHearts(); set => this._defenseHearts(value); }//;

        public s16 gsTokens { get => this._gsTokens(); set => this._gsTokens(value); }//;

        // Songs
/*        public bool songLullaby { get => this._songLullaby(); set => this._songLullaby(value); }

        public bool songEpona { get => this._songEpona(); set => this._songEpona(value); }

        public bool songSaria { get => this._songSaria(); set => this._songSaria(value); }

        public bool songSun { get => this._songSun(); set => this._songSun(value); }

        public bool songTime { get => this._songTime(); set => this._songTime(value); }

        public bool songStorms { get => this._songStorms(); set => this._songStorms(value); }

        public bool songPrelude { get => this._songPrelude(); set => this._songPrelude(value); }

        public bool songMinuet { get => this._songMinuet(); set => this._songMinuet(value); }

        public bool songBolero { get => this._songBolero(); set => this._songBolero(value); }

        public bool songSerenade { get => this._songSerenade(); set => this._songSerenade(value); }

        public bool songNocturne { get => this._songNocturne(); set => this._songNocturne(value); }

        public bool songRequiem { get => this._songRequiem(); set => this._songRequiem(value); }*/

        // Medallions 

        public WrapperQuestStatus(u32 pointer)
        {
            this.pointer = pointer;
        }

        public static uint getSize()
        {
            return 0x5E;
        }

        private u16 _equipment()
        {
            return Memory.RAM.ReadU16(this.pointer + 0x28);
        }

        private void _equipment(u16 value)
        {
            Memory.RAM.WriteU16(this.pointer + 0x28, value);
        }

        private u32 _upgrades()
        {
            return Memory.RAM.ReadU32(this.pointer + 0x2C);
        }

        private void _upgrades(u32 value)
        {
            Memory.RAM.WriteU32(this.pointer + 0x2C, value);
        }

        private u32 _questItems()
        {
            return Memory.RAM.ReadU32(this.pointer + 0x30);
        }

        private void _questItems(u32 value)
        {
            Memory.RAM.WriteU32(this.pointer + 0x30, value);
        }

        // #ARRCOUNT 20
        private u8[] _dungeonItems()
        {
            u8[] bytes = new u8[20]; for (u32 i = 0; i < 20; i++) { bytes[i] = Memory.RAM.ReadU8(this.pointer + 0x34 + (i * 1)); }
            return bytes;
        }

        private void _dungeonItems(u8[] value)
        {
            for (u32 i = 0; i < 20; i++) { Memory.RAM.WriteU8(this.pointer + 0x34 + (i * 1), value[i]); }
        }

        // #ARRCOUNT 19
        private s8[] _dungeonKeys()
        {
            s8[] bytes = new s8[19]; for (u32 i = 0; i < 19; i++) { bytes[i] = Memory.RAM.ReadS8(this.pointer + 0x48 + (i * 1)); }
            return bytes;
        }

        private void _dungeonKeys(s8[] value)
        {
            for (u32 i = 0; i < 19; i++) { Memory.RAM.WriteS8(this.pointer + 0x48 + (i * 1), value[i]); }
        }

        private s8 _defenseHearts()
        {
            return Memory.RAM.ReadS8(this.pointer + 0x5B);
        }

        private void _defenseHearts(s8 value)
        {
            Memory.RAM.WriteS8(this.pointer + 0x5B, value);
        }

        private s16 _gsTokens()
        {
            return Memory.RAM.ReadS16(this.pointer + 0x5C);
        }

        private void _gsTokens(s16 value)
        {
            Memory.RAM.WriteS16(this.pointer + 0x5C, value);
        }

        /*private bool _songLullaby()
        {
            //return (questItems )
        }*/


    }
}
