using System.Collections.Generic;
namespace YourTasks.Services
{
    public static class ColorRepository
    {
        public readonly static IEnumerable<Color> Colors = new List<Color>{};
    }

    public struct Color
    {
        public string Name { get; set; }
        public string ColorValue { get; set; }
    }

}