using System.Windows.Media;

namespace PascalSpace
{
    internal static class ColourHelpers
    {
        public static Color Lerp(Color a, Color b, float lerp)
        {
            return Color.Add(Color.Multiply(a, 1 - lerp), Color.Multiply(b, lerp));
        }

        public static Color BlendWithEditorBackground(Color color)
        {
            var alpha = (float)EnvironmentExpectations.MarkerAlpha / 255;
            return Lerp(EnvironmentExpectations.BackgroundColour, color, alpha);
        }
    }
}
