namespace ChordFactory.OpenSilver.extensions
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Windows;

    public static class ColourUtilities
    {
        /// <summary>
        /// The colour dictionary
        /// </summary>
        private static readonly Dictionary<string, Color> ColourDictionary = new Dictionary<string, Color>();

        /// <summary>
        /// Gets the colours.
        /// </summary>
        /// <value>The colours.</value>
        public static Dictionary<string, Color> Colours
        {
            get
            {
                if (ColourDictionary.Count == 0)
                {
                    foreach (var field in typeof(Color).GetFields(BindingFlags.Static | BindingFlags.Public))
                    {
                        ColourDictionary.Add(field.Name, (Color)field.GetValue(Application.Current));
                    }
                }

                return ColourDictionary;
            }
        }
    }
}