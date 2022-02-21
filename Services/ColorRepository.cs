using System.Collections.Generic;
namespace YourTasks.Services
{
    public static class ColorRepository
    {
        public readonly static IEnumerable<Color> Colors = new List<Color>{
            new Color{
                Name ="Red",
                ColorValue = "#D32F2F"
            },
            new Color{
                Name = "Pink",
                ColorValue = "#C2185B"
            },
            new Color{
                Name = "Purple",
                ColorValue = "#7B1FA2"
            },
            new Color{
                Name = "DeepPurple",
                ColorValue = "#512DA8"
            },
            new Color{
                Name = "Indigo",
                ColorValue = "#303F9F"
            },
            new Color{
                Name = "Blue",
                ColorValue = "#1976D2"
            },
            new Color{
                Name = "LightBlue",
                ColorValue = "#0288D1"
            },
             new Color{
                Name = "Cian",
                ColorValue = "#0097A7"
            },
             new Color{
                Name = "Teal",
                ColorValue = "#00796B"
            },
            new Color{
                Name = "Green",
                ColorValue = "#388E3C"
            },
            new Color{
                Name = "LightGreen",
                ColorValue = "#689F38"
            },
            new Color{
                Name = "Lime",
                ColorValue = "#AFB42B"
            },
            new Color{
                Name = "Yellow",
                ColorValue = "#FBC02D"
            },
            new Color{
                Name = "Orange",
                ColorValue = "#F57C00"
            },
            new Color{
                Name = "DeepOrange",
                ColorValue = "#E64A19"
            }
        };
    }

    public struct Color
    {
        public string Name { get; set; }
        public string ColorValue { get; set; }
    }

}