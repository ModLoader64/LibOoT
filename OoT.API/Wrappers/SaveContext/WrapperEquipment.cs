using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OoT.API.Wrappers.SaveContext
{

    public class WrapperEquipment
    {
        public readonly u32 pointer = 0;
        public readonly u32 biggoronDmgAddr = (uint)OoTVersionPointers.SaveContext + 0x36;
        public readonly u32 biggoronFlg = (uint)OoTVersionPointers.SaveContext + 0x3E;

        // Swords
        public bool kokiriSword { get => this._kokiriSword(); set => this._kokiriSword(value); }
        public bool masterSword { get => this._masterSword(); set => this._masterSword(value); }
        public bool giantsKnife { get => this._giantsKnife(); set => this._giantsKnife(value); }
        public bool biggoronSword { get => this._biggoronSword(); set => this._biggoronSword(value); }

        // Shields
        public bool dekuShield { get => this._dekuShield(); set => this._dekuShield(value); }
        public bool hylianShield { get => this._hylianShield(); set => this._hylianShield(value); }
        public bool mirrorShield { get => this._mirrorShield(); set => this._mirrorShield(value); }

        // Tunics
        public bool kokiriTunic { get => this._kokiriTunic(); set => this._kokiriTunic(value); }
        public bool goronTunic { get => this._goronTunic(); set => this._goronTunic(value); }
        public bool zoraTunic { get => this._zoraTunic(); set => this._zoraTunic(value); }

        // Boots
        public bool kokiriBoots { get => this._kokiriBoots(); set => this._kokiriBoots(value); }
        public bool ironBoots { get => this._ironBoots(); set => this._ironBoots(value); }
        public bool hoverBoots { get => this._hoverBoots(); set => this._hoverBoots(value); }


        public WrapperEquipment(u32 pointer)
        {
            this.pointer = pointer;
        }

        public bool _kokiriSword()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 0);
        }

        public void _kokiriSword(bool flag)
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

        public bool _masterSword()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 1);
        }

        public void _masterSword(bool flag)
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

        public bool _giantsKnife()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 2) && Memory.RAM.ReadU8(biggoronFlg) == 0x0;
        }

        public void _giantsKnife(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x1);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value |= 0x04);
                Memory.RAM.WriteU8(biggoronFlg, 0x0);
                Memory.RAM.WriteU16(biggoronDmgAddr, 0x8);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value &= 0xFB);
                Memory.RAM.WriteU8(biggoronFlg, 0x0);
                Memory.RAM.WriteU16(biggoronDmgAddr, 0x0);
            }
        }

        public bool _biggoronSword()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 2) && Memory.RAM.ReadU8(biggoronFlg) == 0x1;
        }

        public void _biggoronSword(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x1);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value |= 0x04);
                Memory.RAM.WriteU8(biggoronFlg, 0x1);
                Memory.RAM.WriteU16(biggoronDmgAddr, 0x8);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer + 0x1, value &= 0xFB);
                Memory.RAM.WriteU8(biggoronFlg, 0x0);
                Memory.RAM.WriteU16(biggoronDmgAddr, 0x0);
            }
        }

        public bool _dekuShield()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 4);
        }

        public void _dekuShield(bool flag)
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

        public bool _hylianShield()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 5);
        }

        public void _hylianShield(bool flag)
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

        public bool _mirrorShield()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer + 0x1), 6);
        }

        public void _mirrorShield(bool flag)
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

        public bool _kokiriTunic()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer), 0);
        }

        public void _kokiriTunic(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer, value |= 0x01);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer, value &= 0xFE);
            }
        }

        public bool _goronTunic()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer), 1);
        }

        public void _goronTunic(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer, value |= 0x02);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer, value &= 0xFD);
            }
        }

        public bool _zoraTunic()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer), 2);
        }

        public void _zoraTunic(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer, value |= 0x04);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer, value &= 0xFB);
            }
        }

        public bool _kokiriBoots()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer), 4);
        }

        public void _kokiriBoots(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer, value |= 0x10);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer, value &= 0xEF);
            }
        }

        public bool _ironBoots()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer), 5);
        }

        public void _ironBoots(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer, value |= 0x20);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer, value &= 0xDF);
            }
        }

        public bool _hoverBoots()
        {
            return Utils.isBitSet(Memory.RAM.ReadU8(this.pointer), 6);
        }

        public void _hoverBoots(bool flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer);
            if (flag)
            {
                Memory.RAM.WriteU8(this.pointer, value |= 0x40);
            }
            else
            {
                Memory.RAM.WriteU8(this.pointer, value &= 0xBF);
            }
        }
    }
}
