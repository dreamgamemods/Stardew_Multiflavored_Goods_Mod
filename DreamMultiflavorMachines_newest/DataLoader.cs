using MultiFlavorMachines.DisplayLogic;
using MultiFlavorMachines.MachineLogic;
using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MultiFlavorMachines
{
    public class DataLoader
    {
        public static IModHelper Helper;
        public static IMonitor monitor;
        public static SpecialDisplayRuleData specialDisplayRuleData;
        public static MachineLogicData machineLogicData;
        public DataLoader(IModHelper helper, IManifest manifest, IMonitor monitor)
        {
            DataLoader.Helper = helper;
            DataLoader.monitor = monitor;
            DataLoader.specialDisplayRuleData = new SpecialDisplayRuleData();
            DataLoader.machineLogicData = new MachineLogicData();

        }

        public static void LoadContentPacksCommand(string command = null, string[] args = null)
        {
            foreach (IContentPack icontentPack in DataLoader.Helper.ContentPacks.GetOwned())
            {
                try
                {
                    if (File.Exists(Path.Combine(icontentPack.DirectoryPath, "dream.special_display_rule.json")))
                    {
                        foreach (SpecialDisplayRule specialDisplayRule1 in icontentPack.ReadJsonFile<List<SpecialDisplayRule>>("dream.special_display_rule.json"))
                        {
                            SpecialDisplayRule specialDisplayRule = specialDisplayRule1;
                            DataLoader.specialDisplayRuleData.SpecialDisplayRules.RemoveAll((Predicate<SpecialDisplayRule>)(c => c.Id.Contains(specialDisplayRule.Id)));
                            DataLoader.specialDisplayRuleData.SpecialDisplayRules.Add(specialDisplayRule);
                        }
                    }
                }
                catch (Exception ex)
                {
                    monitor.Log("exception while loading special rule json in: " + icontentPack.Manifest.Name, (LogLevel)2);
                    monitor.Log("exception: " + ex, (LogLevel)4);
                }
            }
            }
        }
}
