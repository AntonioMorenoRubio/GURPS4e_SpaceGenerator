using System.Text;

namespace GeneratorLibrary.Models
{
    public class World
    {
        public WorldType? Type { get; set; }

        public World() { }

        public override string ToString()
        {
            StringBuilder sb = new();

            //STEP 2: World Type
            sb.AppendLine($"Size: {Type?.Size} Type: {Type?.SubType}");

            return sb.ToString();
        }
    }
}
