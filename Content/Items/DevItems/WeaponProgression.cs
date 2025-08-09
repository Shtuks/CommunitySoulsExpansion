using ssm.Core;
using System.Collections.Generic;
using System.Linq;
using Terraria.Localization;

namespace ssm.Content.Items.DevItems
{
    public class WeaponProgression
    {
        public static readonly Dictionary<int, string> ProgressionPoints = new();

        public static void Load()
        {
            ParseProgressionData(Language.GetTextValue("Mods.ssm.WeaponProgressionPoints"));
        }
        private static void ParseProgressionData(string localizationData)
        {
            var lines = localizationData.Split('\n')
                .Where(line => !string.IsNullOrWhiteSpace(line) && line.Contains(':'));

            foreach (var line in lines)
            {
                var parts = line.Split(':', 2);
                if (parts.Length < 2) continue;

                var key = parts[0].Trim();
                var value = parts[1].Trim();

                string digitPart = new string(key.TakeWhile(char.IsDigit).ToArray());
                if (!int.TryParse(digitPart, out int stage)) continue;

                if (stage == 19)
                {
                    if (key.Contains("NOCAL") && !ModCompatibility.Calamity.Loaded)
                    {
                        ProgressionPoints[19] = value;
                    }
                    else if (key.Contains("Shadowspec") && ModCompatibility.Calamity.Loaded)
                    {
                        ProgressionPoints[19] = value;
                    }
                }
                else if (!ProgressionPoints.ContainsKey(stage))
                {
                    ProgressionPoints[stage] = value;
                }
            }
        }
    }
}