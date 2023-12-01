
namespace OoT.API
{
    public class WrapperPlayerContext
    {
        [System.Text.Json.Serialization.JsonIgnoreAttribute()]
        public u32 pointer;

        public WrapperPlayerContext(u32 pointer)
        {
            this.pointer = pointer;
        }

        private u32 roomAddr { get {  return pointer + 0x3; } }
        private u32 posAddr { get { return pointer + 0x24; } }
        private u32 rotAddr { get { return pointer + 0x30; } }
        private u32 scaleAddr { get { return pointer + 0x50; } }
        private u32 velocityAddr { get { return pointer + 0x5C; } }
        private u32 speedAddr { get { return pointer + 0x68; } }
        private u32 gravityAddr { get { return pointer + 0x6C; } }
        private u32 freezeTimerAddr { get { return pointer + 0x110; } }
        private u32 stateFlags1Addr { get { return pointer + 0x67C; } }
        private u32 stateFlags2Addr { get { return pointer + 0x680; } }

        public s8 room { get => _room(); set => _room(); }
        public WrapperVec3f pos { get => _pos(); set => _pos(); }
        public WrapperVec3s rot { get => _rot(); set => _rot(); }
        public WrapperVec3f scale { get => _scale(); set => _scale(); }
        public WrapperVec3f velocity { get => _velocity(); set => _velocity(); }
        public f32 speed { get => _speed(); set => _speed(); }
        public f32 gravity { get => _gravity(); set => _gravity(); }
        public u16 freezeTimer { get => _freezeTimer(); set => _freezeTimer(); }
        public u32 stateFlags1 { get => _stateFlags1(); set => _stateFlags1(); }
        public u32 stateFlags2 { get => _stateFlags2(); set => _stateFlags2(); }

        public s8 _room()
        {
            return Memory.RAM.ReadS8(roomAddr);
        }
        public void _room(s8 value)
        {
            Memory.RAM.WriteS8(roomAddr, value);
        }

        public WrapperVec3f _pos()
        {
            return new WrapperVec3f(posAddr);
        }
        public void _pos(WrapperVec3f value)
        {
            WrapperVec3f pos = new WrapperVec3f(posAddr);
            pos.x = value.x;
            pos.y = value.y;
            pos.z = value.z;
        }

        public WrapperVec3s _rot()
        {
            return new WrapperVec3s(rotAddr);
        }
        public void _rot(WrapperVec3s value)
        {
            WrapperVec3s pos = new WrapperVec3s(rotAddr);
            pos.x = value.x;
            pos.y = value.y;
            pos.z = value.z;
        }

        public WrapperVec3f _scale()
        {
            return new WrapperVec3f(scaleAddr);
        }
        public void _scale(WrapperVec3f value)
        {
            WrapperVec3f pos = new WrapperVec3f(scaleAddr);
            pos.x = value.x;
            pos.y = value.y;
            pos.z = value.z;
        }

        public WrapperVec3f _velocity()
        {
            return new WrapperVec3f(velocityAddr);
        }
        public void _velocity(WrapperVec3f value)
        {
            WrapperVec3f pos = new WrapperVec3f(velocityAddr);
            pos.x = value.x;
            pos.y = value.y;
            pos.z = value.z;
        }

        public f32 _speed()
        {
            return Memory.RAM.ReadF32(speedAddr);
        }
        public void _speed(f32 value)
        {
            Memory.RAM.WriteF32(speedAddr, value);
        }

        public f32 _gravity()
        {
            return Memory.RAM.ReadF32(gravityAddr);
        }
        public void _gravity(f32 value)
        {
            Memory.RAM.WriteF32(gravityAddr, value);
        }

        public u16 _freezeTimer()
        {
            return Memory.RAM.ReadU16(freezeTimerAddr);
        }
        public void _freezeTimer(u16 value)
        {
            Memory.RAM.WriteU16(freezeTimerAddr, value);
        }

        public u32 _stateFlags1()
        {
            return Memory.RAM.ReadU32(stateFlags1Addr);
        }
        public void _stateFlags1(u32 value)
        {
            Memory.RAM.WriteU32(stateFlags1Addr, value);
        }

        public u32 _stateFlags2()
        {
            return Memory.RAM.ReadU32(stateFlags2Addr);
        }
        public void _stateFlags2(u32 value)
        {
            Memory.RAM.WriteU32(stateFlags2Addr, value);
        }

    }
}
