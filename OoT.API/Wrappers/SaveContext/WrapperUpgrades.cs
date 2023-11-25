using Newtonsoft.Json.Linq;
using OoT.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OoT.API.Wrappers.SaveContext
{

    public class WrapperUpgrades
    {
        public readonly u32 pointer = 0;

        public Capacity.AmmoUpgrade dekuNutCapacity { get => _dekuNutCapacity(); set => _dekuNutCapacity(value); }
        public Capacity.AmmoUpgrade dekuStickCapacity { get => _dekuStickCapacity(); set => _dekuStickCapacity(value); }
        public Capacity.AmmoUpgrade bulletBag { get => _bulletBag(); set => _bulletBag(value); }
        public Capacity.Wallet wallet { get => _wallet(); set => _wallet(value); }
        public Capacity.Scales scale { get => _scale(); set => _scale(value); }
        public Capacity.Strength strength { get => _strength(); set => _strength(value); }
        public Capacity.AmmoUpgrade bombBag { get => _bombBag(); set => _bombBag(value); }
        public Capacity.AmmoUpgrade quiver { get => _quiver(); set => _quiver(value); }


        public WrapperUpgrades(u32 pointer)
        {
            this.pointer = pointer;
        }

        /* 8011A670:
        0x0:

        0x1:
	        0x01: 0001 - Quiver (30)
	        0x02: 0010 - Quiver (40)
	        0x04: 0100 - 
        0x2:

	        //Lo
	        0x01: 0001 - Silver Scale? // Wrong slot
	        0x02: 0010 - Silver Scale
	        0x04: 0100 - Golden Scale
	        0x08: 1000 - Unused // Wallet icon?
        	//Hi
	        0x10: 0001 - Wallet (Adult)
	        0x20: 0010 - Wallet (Giants)
	        0x30: 0011 - Wallet (Tycoon) // Rando only
	        0x40: 0100 - Bullet Bag (30)
	        0x80: 1000 - Bullet Bag (40)
	        0xC0: 1100 - Bullet Bag (50)

        0x3:
        	//Lo
	        0x01: 0001 - Quiver (30)
	        0x02: 0010 - Quiver (40)
	        0x03: 0011 - Quiver (50)
	        0x08: 1000 - Bomb Bag (20) // 0x18 (0001 1000) = Bomb Bag (40)
	        //Hi
	        0x10: 0001 - Bomb Bag (30)
	        0x20: 0010 - Goron Bracelet // Wrong slot
	        0x40: 0100 - Goron Bracelet 
	        0x80: 1000 - Silver Gauntlets
	        0xC0: 1100 - Golden Gauntlets 

         */

        public Capacity.AmmoUpgrade _dekuStickCapacity()
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x1);

            if ((value & 0x06) == 0x06)
            {
                return Capacity.AmmoUpgrade.Max;
            }
            if ((value & 0x04) == 0x04)
            {
                return Capacity.AmmoUpgrade.Upgrade;
            }
            if ((value & 0x02) == 0x02)
            {
                return Capacity.AmmoUpgrade.Basic;
            }
            return Capacity.AmmoUpgrade.None;
        }

        public void _dekuStickCapacity(Capacity.AmmoUpgrade flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x1);
            u8 temp = value;

            switch (flag)
            {
                case Capacity.AmmoUpgrade.None:
                    value &= 0xFC;
                    Memory.RAM.WriteU8(this.pointer + 0x1, value);
                    break;
                case Capacity.AmmoUpgrade.Basic:
                    value &= 0xFC;
                    value |= 0x02;
                    Memory.RAM.WriteU8(this.pointer + 0x1, value);
                    break;
                case Capacity.AmmoUpgrade.Upgrade:
                    value &= 0xFC;
                    value |= 0x04;
                    Memory.RAM.WriteU8(this.pointer + 0x1, value);
                    break;
                case Capacity.AmmoUpgrade.Max:
                    value &= 0xFC;
                    value |= 0x06;
                    Memory.RAM.WriteU8(this.pointer + 0x1, value);
                    break;
            }
            //Console.WriteLine($"Deku Stick Capacity: {temp.ToString("X")} -> {value.ToString("X")}");
        }

        public Capacity.AmmoUpgrade _dekuNutCapacity()
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x1);

            if ((value & 0x30) == 0x30)
            {
                return Capacity.AmmoUpgrade.Max;
            }
            if ((value & 0x20) == 0x20)
            {
                return Capacity.AmmoUpgrade.Upgrade;
            }
            if ((value & 0x10) == 0x10)
            {
                return Capacity.AmmoUpgrade.Basic;
            }
            return Capacity.AmmoUpgrade.None;
        }

        public void _dekuNutCapacity(Capacity.AmmoUpgrade flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x1);
            u8 temp = value;

            switch (flag)
            {
                case Capacity.AmmoUpgrade.None:
                    value &= 0xCF;
                    Memory.RAM.WriteU8(this.pointer + 0x1, value);
                    break;
                case Capacity.AmmoUpgrade.Basic:
                    value &= 0xCF;
                    value |= 0x10;
                    Memory.RAM.WriteU8(this.pointer + 0x1, value);
                    break;
                case Capacity.AmmoUpgrade.Upgrade:
                    value &= 0xCF;
                    value |= 0x20;
                    Memory.RAM.WriteU8(this.pointer + 0x1, value);
                    break;
                case Capacity.AmmoUpgrade.Max:
                    value &= 0xCF;
                    value |= 0x30;
                    Memory.RAM.WriteU8(this.pointer + 0x1, value);
                    break;
            }
            //Console.WriteLine($"Deku Nut Capacity: {temp.ToString("X")} -> {value.ToString("X")}");
        }

        public Capacity.Wallet _wallet()
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x2);

            if ((value & 0x30) == 0x30)
            {
                return Capacity.Wallet.Tycoon;
            }
            else if ((value & 0x20) == 0x20)
            {
                return Capacity.Wallet.Giant;
            }
            else if ((value & 0x10) == 0x10)
            {
                return Capacity.Wallet.Adult;
            }
            else
            {
                return Capacity.Wallet.None;
            }
        }

        public void _wallet(Capacity.Wallet flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x2);
            u8 temp = value;

            switch (flag)
            {
                case Capacity.Wallet.None:
                    value &= 0xCF;
                    Memory.RAM.WriteU8(this.pointer + 0x2, value);
                    break;
                case Capacity.Wallet.Adult:
                    value &= 0xCF;
                    value |= 0x10;
                    Memory.RAM.WriteU8(this.pointer + 0x2, value);
                    break;
                case Capacity.Wallet.Giant:
                    value &= 0xCF;
                    value |= 0x20;
                    Memory.RAM.WriteU8(this.pointer + 0x2, value);
                    break;
                case Capacity.Wallet.Tycoon:
                    value &= 0xCF;
                    value |= 0x30;
                    Memory.RAM.WriteU8(this.pointer + 0x2, value);
                    break;
            }
            //Console.WriteLine($"Wallet: {temp.ToString("X")} -> {value.ToString("X")}");
        }

        public Capacity.AmmoUpgrade _bulletBag()
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x2);

            if ((value & 0xC0) == 0xC0)
            {
                return Capacity.AmmoUpgrade.Max;
            }
            if ((value & 0x80) == 0x80)
            {
                return Capacity.AmmoUpgrade.Upgrade;
            }
            if ((value & 0x40) == 0x40)
            {
                return Capacity.AmmoUpgrade.Basic;
            }
            return Capacity.AmmoUpgrade.None;
            
        }

        public void _bulletBag(Capacity.AmmoUpgrade flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x2);
            u8 temp = value;

            switch (flag)
            {
                case Capacity.AmmoUpgrade.None:
                    value &= 0x3F;
                    Memory.RAM.WriteU8(this.pointer + 0x2, value);
                    break;
                case Capacity.AmmoUpgrade.Basic:
                    value &= 0x3F;
                    value |= 0x40;
                    Memory.RAM.WriteU8(this.pointer + 0x2, value);
                    break;
                case Capacity.AmmoUpgrade.Upgrade:
                    value &= 0x3F;
                    value |= 0x80;
                    Memory.RAM.WriteU8(this.pointer + 0x2, value);
                    break;
                case Capacity.AmmoUpgrade.Max:
                    value &= 0x3F;
                    value |= 0xC0;
                    Memory.RAM.WriteU8(this.pointer + 0x2, value);
                    break;
            }
            //Console.WriteLine($"Bullet Bag Capacity: {temp.ToString("X")} -> {value.ToString("X")}");
        }

        public Capacity.Scales _scale()
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x2);

            if ((value & 0x04) == 0x04)
            {
                return Capacity.Scales.Golden;
            }
            else if ((value & 0x02) == 0x02)
            {
                return Capacity.Scales.Silver;
            }
            else
            {
                return Capacity.Scales.None;
            }

        }

        public void _scale(Capacity.Scales flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x2);
            u8 temp = value;

            switch (flag)
            {
                case Capacity.Scales.None:
                    value &= 0xF9;
                    Memory.RAM.WriteU8(this.pointer + 0x2, value);
                    break;
                case Capacity.Scales.Silver:
                    value &= 0xF9;
                    value |= 0x02;
                    Memory.RAM.WriteU8(this.pointer + 0x2, value);
                    break;
                case Capacity.Scales.Golden:
                    value &= 0xF9;
                    value |= 0x04;
                    Memory.RAM.WriteU8(this.pointer + 0x2, value);
                    break;
            }
            //Console.WriteLine($"Scale: {temp.ToString("X")} -> {value.ToString("X")}");
        }

        public Capacity.AmmoUpgrade _bombBag()
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x3);

            if ((value & 0x18) == 0x18)
            {
                return Capacity.AmmoUpgrade.Max;
            }
            else if ((value & 0x10) == 0x10)
            {
                return Capacity.AmmoUpgrade.Upgrade;
            }
            else if ((value & 0x08) == 0x08)
            {
                return Capacity.AmmoUpgrade.Basic;
            }
            else
            {
                return Capacity.AmmoUpgrade.None;
            }
        }

        public void _bombBag(Capacity.AmmoUpgrade flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x3);
            u8 temp = value;

            switch (flag)
            {
                case Capacity.AmmoUpgrade.None:
                    value &= 0xE7;
                    Memory.RAM.WriteU8(this.pointer + 0x3, value);
                    break;
                case Capacity.AmmoUpgrade.Basic:
                    value &= 0xE7;
                    value |= 0x08;
                    Memory.RAM.WriteU8(this.pointer + 0x3, value);
                    break;
                case Capacity.AmmoUpgrade.Upgrade:
                    value &= 0xE7;
                    value |= 0x10;
                    Memory.RAM.WriteU8(this.pointer + 0x3, value);
                    break;
                case Capacity.AmmoUpgrade.Max:
                    value &= 0xE7;
                    value |= 0x18;
                    Memory.RAM.WriteU8(this.pointer + 0x3, value);
                    break;
            }
            //Console.WriteLine($"Bomb Bag Capacity: {temp.ToString("X")} -> {value.ToString("X")}");
        }

        public Capacity.Strength _strength()
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x3);

            if ((value & 0xC0) == 0xC0)
            {
                return Capacity.Strength.Golden;
            }
            else if ((value & 0x80) == 0x80)
            {
                return Capacity.Strength.Silver;
            }
            else if ((value & 0x40) == 0x40)
            {
                return Capacity.Strength.Bracelet;
            }
            else
            {
                return Capacity.Strength.None;
            }
        }

        public void _strength(Capacity.Strength flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x3);
            u8 temp = value;
            switch (flag)
            {
                case Capacity.Strength.None:
                    value &= 0x3F;
                    Memory.RAM.WriteU8(this.pointer + 0x3, value);
                    break;
                case Capacity.Strength.Bracelet:
                    value &= 0x3F;
                    value |= 0x40;
                    Memory.RAM.WriteU8(this.pointer + 0x3, value);
                    break;
                case Capacity.Strength.Silver:
                    value &= 0x3F;
                    value |= 0x80;
                    Memory.RAM.WriteU8(this.pointer + 0x3, value);
                    break;
                case Capacity.Strength.Golden:
                    value &= 0x3F;
                    value |= 0xC0;
                    Memory.RAM.WriteU8(this.pointer + 0x3, value);
                    break;
            }
            //Console.WriteLine($"Strength: {temp.ToString("X")} -> {value.ToString("X")}");
        }

        public Capacity.AmmoUpgrade _quiver()
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x3);

            if ((value & 0x03) == 0x03)
            {
                return Capacity.AmmoUpgrade.Max;
            }
            if ((value & 0x02) == 0x02)
            {
                return Capacity.AmmoUpgrade.Upgrade;
            }
            if ((value & 0x01) == 0x01)
            {
                return Capacity.AmmoUpgrade.Basic;
            }
            else { 
                return Capacity.AmmoUpgrade.None; 
            }
        }

        public void _quiver(Capacity.AmmoUpgrade flag)
        {
            u8 value = Memory.RAM.ReadU8(this.pointer + 0x3);
            u8 temp = value;

            switch (flag)
            {
                case Capacity.AmmoUpgrade.None:
                    value &= 0xFC;
                    Memory.RAM.WriteU8(this.pointer + 0x3, value);
                    break;
                case Capacity.AmmoUpgrade.Basic:
                    value &= 0xFC;
                    value |= 0x01;
                    Memory.RAM.WriteU8(this.pointer + 0x3, value);
                    break;
                case Capacity.AmmoUpgrade.Upgrade:
                    value &= 0xFC;
                    value |= 0x02;
                    Memory.RAM.WriteU8(this.pointer + 0x3, value);
                    break;
                case Capacity.AmmoUpgrade.Max:
                    value &= 0xFC;
                    value |= 0x03;
                    Memory.RAM.WriteU8(this.pointer + 0x3, value);
                    break;
            }
            //Console.WriteLine($"Quiver Capacity: {temp.ToString("X")} -> {value.ToString("X")}");
        }
    }
}
