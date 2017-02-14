using System.Windows.Media;

namespace PascalSpace
{
    /// <summary>
    /// These values should depend on the expected environment.
    /// (I haven't figured out how to do configuration yet.)
    /// 
    /// For a dark theme, Black and 0x40 seem pretty good.
    /// For a light theme, White and 0x20 was suitable.
    /// </summary>
    internal static class EnvironmentExpectations
    {
        public static Color BackgroundColour = Colors.Black;

        public static byte MarkerAlpha = 0x40;
    }
}
