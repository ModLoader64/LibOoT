
namespace OoT.API
{
    public class WrapperPlayerContext
    {
        [System.Text.Json.Serialization.JsonIgnoreAttribute()]
        public readonly u32 pointer;

        public WrapperPlayerContext(u32 pointer)
        {
            this.pointer = pointer;
        }
    }
}
