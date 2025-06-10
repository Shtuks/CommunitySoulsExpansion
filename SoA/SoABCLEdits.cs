using ssm.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ssm.SoA
{
    //public class SoABCLEdits : ModSystem
    //{
    //    public override void PostSetupContent()
    //    {
    //        RemoveFromChecklist("Nihilus");
    //        RemoveFromChecklist("Erazor");
    //        RemoveFromChecklist("???");
    //    }
    //    private static void RemoveFromChecklist(string value)
    //    {
    //        object bossTracker = ModCompatibility.BossChecklist.Mod.GetType().GetField("bossTracker", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
    //        FieldInfo sortedEntriesField = bossTracker.GetType().GetField("SortedEntries", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
    //        object entriesBase = sortedEntriesField.GetValue(bossTracker);
    //        List<object> entries = ((IEnumerable)entriesBase).Cast<object>().ToList();

    //        string entryName = Language.GetTextValue(value);
    //        PropertyInfo entryDisplayNameProperty = entries.First().GetType().GetProperty("DisplayName", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

    //        object newEntries = Activator.CreateInstance(entriesBase.GetType());
    //        IList newEntriesCasted = (IList)newEntries;
    //        foreach (object entry in entries)
    //        {
    //            string displayName = (string)entryDisplayNameProperty.GetValue(entry);
    //            if (displayName != entryName)
    //                newEntriesCasted.Add(entry);
    //        }

    //        sortedEntriesField.SetValue(bossTracker, newEntries);
    //    }
    //}
}
