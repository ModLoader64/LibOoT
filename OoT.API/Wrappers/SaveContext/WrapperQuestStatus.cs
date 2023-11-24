
namespace OoT.API
{

    public class WrapperQuestStatus
    {

        [System.Text.Json.Serialization.JsonIgnoreAttribute()]
        private u32 pointer;

        public u8[] dungeonItems { get => this._dungeonItems(); set => this._dungeonItems(value); }//;

        public s8[] dungeonKeys { get => this._dungeonKeys(); set => this._dungeonKeys(value); }//;

        public s8 defenseHearts { get => this._defenseHearts(); set => this._defenseHearts(value); }//;

        public s16 gsTokens { get => this._gsTokens(); set => this._gsTokens(value); }//;

        public u8 heartPieces { get => this._heartPieces(); set => this._heartPieces(value); }

        // Songs
        public bool songLullaby { get => this._songLullaby(); set => this._songLullaby(value); }
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
        public bool songRequiem { get => this._songRequiem(); set => this._songRequiem(value); }

        // Stones
        public bool kokiriEmerald { get => this._kokiriEmerald(); set => this._kokiriEmerald(value); }
        public bool goronRuby { get => this._goronRuby(); set => this._goronRuby(value); }
        public bool zoraSapphire { get => this._zoraSapphire(); set => this._zoraSapphire(value); }

        // Medallions 
        public bool medallionForest { get => this._medallionForest(); set => this._medallionForest(value); }
        public bool medallionFire { get => this._medallionFire(); set => this._medallionFire(value); }
        public bool medallionWater { get => this._medallionWater(); set => this._medallionWater(value); }
        public bool medallionSpirit { get => this._medallionSpirit(); set => this._medallionSpirit(value); }
        public bool medallionShadow { get => this._medallionShadow(); set => this._medallionShadow(value); }
        public bool medallionLight { get => this._medallionLight(); set => this._medallionLight(value); }

        // Other
        public bool stoneAgony { get => this._stoneAgony(); set => this._stoneAgony(value); }
        public bool gerudoCard { get => this._gerudoCard(); set => this._gerudoCard(value); }
        public bool hasGoldSkull { get => this._hasGoldSkull(); set => this._hasGoldSkull(value); }


        public WrapperQuestStatus(u32 pointer)
        {
            this.pointer = pointer;
        }

        private u8 _heartPieces()
        {
            return Memory.RAM.ReadU8(this.pointer);
        }

        private void _heartPieces(u8 value)
        {
            if (value > 3) return;
            Memory.RAM.WriteU8(this.pointer, value);
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

        // 0x1  
        public bool _songTime()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 0);
        }

        public void _songTime(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x1);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value |= 0x01);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value &= 0xFE);
            }
        }

        public bool _songStorms()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 1);
        }

        public void _songStorms(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x1);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value |= 0x02);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value &= 0xFD);
            }
        }

        public bool _kokiriEmerald()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 2);
        }

        public void _kokiriEmerald(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x1);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value |= 0x04);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value &= 0xFB);
            }
        }

        public bool _goronRuby()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 3);
        }

        public void _goronRuby(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x1);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value |= 0x08);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value &= 0xF7);
            }
        }

        public bool _zoraSapphire()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 4);
        }

        public void _zoraSapphire(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x1);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value |= 0x10);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value &= 0xEF);
            }
        }

        public bool _stoneAgony()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 5);
        }

        public void _stoneAgony(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x1);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value |= 0x20);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value &= 0xDF);
            }
        }

        public bool _gerudoCard()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 6);
        }

        public void _gerudoCard(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x1);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value |= 0x40);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value &= 0xBF);
            }
        }

        public bool _hasGoldSkull()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 7);
        }

        public void _hasGoldSkull(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x1);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value |= 0x80);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value &= 0x7F);
            }
        }

        // 0x2

        public bool _songSerenade()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x2), 0);
        }

        public void _songSerenade(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x2);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value |= 0x01);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value &= 0xFE);
            }
        }

        public bool _songRequiem()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x2), 1);
        }

        public void _songRequiem(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x2);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value |= 0x02);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value &= 0xFD);
            }
        }

        public bool _songNocturne()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x2), 2);
        }

        public void _songNocturne(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x2);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value |= 0x04);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value &= 0xFB);
            }
        }

        public bool _songPrelude()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x2), 3);
        }

        public void _songPrelude(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x2);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value |= 0x08);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value &= 0xF7);
            }
        }

        public bool _songLullaby()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x2), 4);
        }

        public void _songLullaby(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x2);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value |= 0x10);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value &= 0xEF);
            }
        }

        public bool _songEpona()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x2), 5);
        }

        public void _songEpona(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x2);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value |= 0x20);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value &= 0xDF);
            }
        }

        public bool _songSaria()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x2), 6);
        }

        public void _songSaria(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x2);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value |= 0x40);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value &= 0xBF);
            }
        }

        public bool _songSun()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x2), 7);
        }

        public void _songSun(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x2);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value |= 0x80);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x2, value &= 0x7F);
            }
        }

        // 0x3

        public bool _medallionForest()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x3), 0);
        }

        public void _medallionForest(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x3);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value |= 0x01);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value &= 0xFE);
            }
        }

        public bool _medallionFire()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x3), 1);
        }

        public void _medallionFire(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x3);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value |= 0x02);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value &= 0xFD);
            }
        }

        public bool _medallionWater()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x3), 2);
        }

        public void _medallionWater(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x3);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value |= 0x04);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value &= 0xFB);
            }
        }

        public bool _medallionSpirit()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x3), 3);
        }

        public void _medallionSpirit(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x3);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value |= 0x08);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value &= 0xF7);
            }
        }

        public bool _medallionShadow()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x3), 4);
        }

        public void _medallionShadow(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x3);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value |= 0x10);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value &= 0xEF);
            }
        }

        public bool _medallionLight()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x3), 5);
        }

        public void _medallionLight(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x3);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value |= 0x20);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value &= 0xDF);
            }
        }

        public bool _songMinuet()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x3), 6);
        }

        public void _songMinuet(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x3);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value |= 0x40);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value &= 0xBF);
            }
        }

        public bool _songBolero()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x3), 7);
        }

        public void _songBolero(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x3);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value |= 0x80);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x3, value &= 0x7F);
            }
        }

    }
}
