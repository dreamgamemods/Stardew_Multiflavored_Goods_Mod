
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.Machines;
using System.Collections.Generic;
using System;
using System.Linq;
using StardewValley.Objects;
using Color = Microsoft.Xna.Framework.Color;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework.Graphics;
using HarmonyLib;
using StardewModdingAPI.Events;
using MultiFlavorMachines.DisplayLogic;


namespace MultiFlavorMachines;


/// <summary>The mod entry point.</summary>
public sealed class ModEntry : Mod
{
    internal new static IModHelper Helper { get; set; }
    internal static IMonitor monitor { get; set; }
    internal static MultiFlavorMachines.DataLoader DataLoader;

    public static List<KeyValuePair<string, string>> defaultContextTags;

    public static string Id { get; set; }

   // private static void Log(string msg, LogLevel lv = LogLevel.Debug) => ModEntry.monitor.Log(msg, lv);

    public override void Entry(IModHelper helper)
    {
        Helper = helper;
        monitor = this.Monitor;

        helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;

        var harmony = new Harmony(ModManifest.UniqueID);

            harmony.Patch(
        original: AccessTools.Method(typeof(StardewValley.Object), nameof(StardewValley.Object.draw), new[]{typeof
                (SpriteBatch), typeof(int), typeof(int), typeof(float)}),
        prefix: new HarmonyMethod(typeof(ObjectDraw), nameof(ObjectDraw.ObjectDraw_prefix)),
        postfix: new HarmonyMethod(typeof(ObjectDraw), nameof(ObjectDraw.ObjectDraw_postfix))
    );

    harmony.Patch(
    original: AccessTools.Method(typeof(StardewValley.Object),
      nameof(StardewValley.Object.drawInMenu),
      new Type[] {
      typeof(SpriteBatch), typeof(Vector2), typeof(float), typeof(float), typeof(float),
      typeof(StackDrawType), typeof(Color), typeof(bool) }),
        prefix: new HarmonyMethod(typeof(ObjectDraw), nameof(ObjectDraw.Object_drawInMenu_prefix)));

    harmony.Patch(
        original: AccessTools.Method(typeof(StardewValley.Object),
          nameof(StardewValley.Object.drawWhenHeld)),
        prefix: new HarmonyMethod(typeof(ObjectDraw), nameof(ObjectDraw.Object_drawWhenHeld_prefix)));

    harmony.Patch(
        original: AccessTools.Method(typeof(ColoredObject),
          nameof(ColoredObject.drawInMenu),
          new Type[] {
      typeof(SpriteBatch), typeof(Vector2), typeof(float), typeof(float), typeof(float),
      typeof(StackDrawType), typeof(Color), typeof(bool) }),
        prefix: new HarmonyMethod(typeof(ObjectDraw), nameof(ObjectDraw.ColoredObject_drawInMenu_prefix)));

    harmony.Patch(
        original: AccessTools.Method(typeof(ColoredObject),
          nameof(ColoredObject.drawWhenHeld)),
        prefix: new HarmonyMethod(typeof(ObjectDraw), nameof(ObjectDraw.Object_drawWhenHeld_prefix)));

        defaultContextTags = new List<KeyValuePair<string, string>>()
    {
        new("berry_item", "dream.ingredient_BERRY"),
        new("category_vegetable", "dream.ingredient_VEGETABLE"),
        new("vegetable_item", "dream.ingredient_VEGETABLE"),
        new("category_fruits", "dream.ingredient_FRUIT"),
        new("fruit_item", "dream.ingredient_FRUIT"),
        new("category_syrup", "dream.ingredient_SYRUP"),
        new("syrup_item", "dream.ingredient_SYRUP"),
        new("category_milk", "dream.ingredient_MILK"),
        new("milk_item", "dream.ingredient_MILK"),
        new("category_egg", "dream.ingredient_EGG"),
        new("egg_item", "dream.ingredient_EGG"),
        new("category_fish", "dream.ingredient_FISH"),
        new("herb_item", "dream.ingredient_HERB"),
        new("raw_spice_item", "dream.ingredient_SPICE"),
        new("root_vegetable_item", "dream.ingredient_ROOT"),
        new("leafy_vegetable_item", "dream.ingredient_LEAFY"),
        new("nut_item", "dream.ingredient_NUT"),
        new("bean_item", "dream.ingredient_BEAN"),
        new("grain_item", "dream.ingredient_GRAIN"),
        new("flour_item", "dream.ingredient_FLOUR"),
        new("meat_item", "dream.ingredient_MEAT"),
        new("cheese_item", "dream.ingredient_CHEESE"),
        new("alcohol_item", "dream.ingredient_ALCOHOL"),
        new("edible_mushroom_item", "dream.ingredient_MUSHROOM"),
        new("edible_flower_item", "dream.ingredient_FLOWER"),
        new("sugar_item", "dream.ingredient_SWEETENER"),
        new("sugar_item", "dream.ingredient_SUGAR"),
        new("item_sugar", "dream.ingredient_SWEETENER"),
        new("item_sugar", "dream.ingredient_SUGAR"),
        new("citrus_item", "dream.ingredient_CITRUS"),
        new("fruit_butter_item", "dream.ingredient_BUTTER"),
        new("fruit_butter_item", "dream.ingredient_FRUIT_BUTTER"),
        new("butter_item", "dream.ingredient_BUTTER"),
        new("nut_butter_item", "dream.ingredient_BUTTER"),
        new("nut_butter_item", "dream.ingredient_NUT_BUTTER"),
        new("jelly_item", "dream.ingredient_JELLY"),
        new("wine_item", "dream.ingredient_WINE"),
        new("yogurt_item", "dream.ingredient_YOGURT"),
        new("oil_item", "dream.ingredient_OIL"),
        new("spice_item", "dream.ingredient_SPICE"),
        new("mayo_item", "dream.ingredient_MAYO"),
        new("noodle_item", "dream.ingredient_NOODLE"),
        new("pasta_item", "dream.ingredient_NOODLE")
    };
    }

    private void OnGameLaunched(object sender, GameLaunchedEventArgs args)
    {
        ModEntry.DataLoader = new MultiFlavorMachines.DataLoader(ModEntry.Helper, this.ModManifest, this.Monitor);
        try
        {
            MultiFlavorMachines.DataLoader.LoadContentPacksCommand();
        }
        catch (Exception ex)
        {
            monitor.Log("exception while loading content packs: "+ex, (LogLevel)4);
        }
    }

        public static Item OutputDreamMachine(
                                            StardewValley.Object machine,
                                            Item inputItem,
                                            bool probe,
                                            MachineItemOutput outputData,
                                            out int? overrideMinutesUntilReady)
    {
        overrideMinutesUntilReady = GetOverrideMinutes(outputData);
        double overridePriceMultiplier = GetPriceMultiplier(outputData);

        if (inputItem.HasContextTag("dream.finish_crafting")) {
            overrideMinutesUntilReady = 0;
            return machine.heldObject?.Value ?? inputItem;
        }
        int Iteration = 1;

        if (machine.heldObject?.Value == null)
        {
            if (outputData.CustomData.TryGetValue("dream.recipe_starter", out string recipeStarter) && !string.IsNullOrEmpty(recipeStarter) && !recipeStarter.Equals("true"))
            {
                overrideMinutesUntilReady = 0;
                if(!probe)
                    return inputItem;
            }

            StardewValley.Object outputObject = new StardewValley.Object(inputItem.ItemId, 1, false, -1, inputItem.Quality);
            if (outputObject.HasContextTag("dream.multiflavored_item"))
                outputObject = castColoredObject(outputObject);

            //check if the output objectData is flavored
            Game1.objectData.TryGetValue(inputItem.ItemId, out var value);
            Dictionary<String, String> customFields = value.CustomFields;

            if (outputData.CustomData.TryGetValue("dream.incrementParentsheet", out string incrementMax) && string.IsNullOrEmpty(incrementMax) && Iteration <= int.Parse(incrementMax))
                machine.ParentSheetIndex++;

            //setup moddata
            string combinedTags = String.Join(",", inputItem.GetContextTags().Where(tag => tag.StartsWith("dream.ingredient") || tag.StartsWith("allergen_")));
            string defaultTags = String.Join(",", GetDefaultContextTags(inputItem, false).Where(tag => !inputItem.GetContextTags().Contains(tag)));
            combinedTags = String.Join(",", defaultTags, combinedTags);
            outputObject.modData["selph.ExtraMachineConfig.ExtraContextTags"] = combinedTags + ",dream.multiflavor_Iteration" + Iteration.ToString(); ;
            outputObject.modData["dream.multiflavor_Iteration"] = Iteration.ToString();

            //setup madeWith
            string allowedTokens = null;
            if (customFields != null)
                customFields.TryGetValue("dream.saved_ingredient_tokens", out allowedTokens);

            String[] inputTokens = getItemToken(inputItem);
            String[] tokenss = inputTokens[1].Split(",");
            foreach (string token in tokenss)
            {
                if (!string.IsNullOrEmpty(allowedTokens))
                {
                    if (allowedTokens.Contains(token))
                        outputObject.modData["dream.MadeWith_" + token] = inputItem.ItemId + "//" + ObjectDraw.getColorTagFromItem(inputItem) + "//" + inputTokens[0];
                }
                else
                {
                    outputObject.modData["dream.MadeWith_" + token] = inputItem.ItemId + "//" + ObjectDraw.getColorTagFromItem(inputItem) + "//" + inputTokens[0];
                }
            }

            //setup price, quality & color
            outputObject.modData["dream.multiflavor_Quality"] = inputItem.Quality.ToString();
            outputObject.modData["dream.output_color"] = ObjectDraw.getColorTagFromItem(inputItem);
            if (inputItem is StardewValley.Object inputObject)
                outputObject.modData["dream.multiflavor_Price"] = inputObject.Price.ToString();

            if (customFields != null && customFields.ContainsKey("dream.multiflavored_name_format") && !probe)
            {
                return ProcessNewFlavoredItem(customFields, outputObject, (StardewValley.Object)inputItem);
            }
            else {
                return (Item)outputObject;
            }
        }
        // Log("machine.heldObject?.Value is not null", (LogLevel)2);
        StardewValley.Object heldObject = machine.heldObject.Value;
        Dictionary<string, List<String>> matchingOutputItems = new Dictionary<string, List<String>>();

        //extract held object mod data and contextags
        if (heldObject.modData != null && heldObject.modData.TryGetValue("dream.multiflavor_Iteration", out string IterationString))
        {
            if (!string.IsNullOrEmpty(IterationString) && !probe)
            {
                Iteration = int.Parse(IterationString);
                Iteration++;
                if (outputData.CustomData.TryGetValue("dream.multiflavor_incrementParentsheet", out string incrementMax) && string.IsNullOrEmpty(incrementMax) && Iteration <= int.Parse(incrementMax))
                    machine.ParentSheetIndex++;
            }
        }

        List<string> ingredientTagsCombined = new List<string>();
        ingredientTagsCombined.AddRange(heldObject.GetContextTags().Where(tag => tag.Contains("dream.ingredient")));
        ingredientTagsCombined.AddRange(GetDefaultContextTags(heldObject, false).Where(tag => !ingredientTagsCombined.Contains(tag)));

        List<String> allInputIngredientTags = new List<string>();
        allInputIngredientTags.AddRange(inputItem.GetContextTags().Where(tag => tag.Contains("dream.ingredient")));
        allInputIngredientTags.AddRange(GetDefaultContextTags(inputItem, false).Where(tag => !allInputIngredientTags.Contains(tag)));

        foreach (string tag in allInputIngredientTags)
        {
            if (!tag.EndsWith("_3") && !tag.EndsWith("_2")) 
            {
                if (!ingredientTagsCombined.Contains(tag))
                {
                    ingredientTagsCombined.Add(tag);
                }
                else
                {
                    if (ingredientTagsCombined.Contains(tag + "_2"))
                    {
                        ingredientTagsCombined.Add(tag + "_3");
                    }
                    else
                    {
                        ingredientTagsCombined.Add(tag + "_2");
                    }
                }
            }
        }

        if (!ingredientTagsCombined.Contains("dream.ingredient_" + inputItem.ItemId))
            ingredientTagsCombined.Add("dream.ingredient_" + inputItem.ItemId);

        //search gamedata for all objects that can be used as output for the machine
        foreach (var objectData in Game1.objectData)
        {
            if (objectData.Value.CustomFields != null && objectData.Value.CustomFields.TryGetValue("dream.machine_output", out string outputMachineIds) && outputMachineIds.Contains(machine.ItemId))
            {
                objectData.Value.CustomFields.TryGetValue("dream.recipe_ingredients", out string ingredients);
                List<string> foundObjectsIngredientTags = ingredients?.Split(",").Select(s => s.Trim()).ToList();
                if (foundObjectsIngredientTags.Count > 0)
                {
                    if (foundObjectsIngredientTags.All(item => ingredientTagsCombined.Contains(item)))
                        matchingOutputItems.Add(objectData.Key, foundObjectsIngredientTags);
                }
            }
        }

        String contexttags = String.Join(",", SetUpContextTags(ingredientTagsCombined, inputItem.GetContextTags(), heldObject.GetContextTags()));
        if (!probe)
        {
            string myItemId;
            if (matchingOutputItems.Count > 0)
            {
                var maxEntry = matchingOutputItems.OrderByDescending(kv => kv.Value.Count).FirstOrDefault();
                myItemId = maxEntry.Key;
            }
            else
            {
                myItemId = heldObject.ItemId;
            }
            return createOutputItem(myItemId, (StardewValley.Object)inputItem, Iteration, contexttags, heldObject, machine, overridePriceMultiplier);
        }
        else
        {
            return heldObject;
        }
    }

    public static Item createOutputItem(String itemId, StardewValley.Object inputItem, int Iteration, String combinedContextTags, StardewValley.Object heldobject, StardewValley.Object machine, double overridePriceMultiplier)
    {
        StardewValley.Object myItemObject = new StardewValley.Object(itemId, 1, false, -1, 0);

        if (myItemObject.HasContextTag("dream.multiflavored_item"))
            myItemObject = castColoredObject(myItemObject);

        Game1.objectData.TryGetValue(itemId, out var OutputObjectData);

        //setup moddata
        if (myItemObject is Item myItem)
        {
            foreach (var key in heldobject.modData.Keys)
            {
                myItem.modData[key] = heldobject.modData[key];
            }
            myItem.modData["selph.ExtraMachineConfig.ExtraContextTags"] = combinedContextTags;
            myItem.modData["dream.multiflavor_Iteration"] = Iteration.ToString();
            myItem.modData["dream.multiflavor_Quality"] = heldobject.modData["dream.multiflavor_Quality"] + "," + inputItem.Quality;
            myItem.modData["dream.multiflavor_Price"] = heldobject.modData["dream.multiflavor_Price"] + "," + inputItem.Price;

            Game1.objectData.TryGetValue(inputItem.ItemId, out var inputValue);
            string inputDisplayName = inputValue.DisplayName;
            if (inputValue.CustomFields != null && inputValue.CustomFields.TryGetValue("shortened_display_name", out var shortenedDisplayName) && !string.IsNullOrEmpty(shortenedDisplayName))
                inputDisplayName = shortenedDisplayName;

            if (inputDisplayName.Contains("PRESERVE")) 
            {
                String preserveSource = inputDisplayName.Split(':')[1];
                if (inputValue.CustomFields.TryGetValue("dream.MadeWith_" + preserveSource, out var preserveDisplayName) && string.IsNullOrEmpty(preserveDisplayName)) 
                    inputDisplayName = inputDisplayName.Replace("PRESERVE:"+ preserveSource, preserveDisplayName.Split("||")[0]);
            }

                String[] inputTokens = getItemToken(inputItem);
                String[] tokens = inputTokens[1].Split(",");

            string allowedTokens = null;
            if (OutputObjectData.CustomFields != null)
                OutputObjectData.CustomFields.TryGetValue("dream.saved_ingredient_tokens", out allowedTokens);

            foreach (string token in tokens)
            {
                if (!string.IsNullOrEmpty(allowedTokens))
                {
                    if (allowedTokens.Contains(token))
                    {
                        if (myItem.modData.ContainsKey("dream.MadeWith_" + token))
                        {
                            myItem.modData["dream.MadeWith_" + token] += "||" + inputItem.ItemId + "//" + ObjectDraw.getColorTagFromItem(inputItem) + "//" + inputDisplayName;
                        }
                        else
                        {
                            myItem.modData["dream.MadeWith_" + token] = inputItem.ItemId + "//" + ObjectDraw.getColorTagFromItem(inputItem) + "//" + inputDisplayName;
                        }
                    }
                }
                else
                {
                    if (myItem.modData.ContainsKey("dream.MadeWith_" + token))
                    {
                        myItem.modData["dream.MadeWith_" + token] += "||" + inputItem.ItemId + "//" + ObjectDraw.getColorTagFromItem(inputItem) + "//" + inputDisplayName;
                    }
                    else
                    {
                        myItem.modData["dream.MadeWith_" + token] = inputItem.ItemId + "//" + ObjectDraw.getColorTagFromItem(inputItem) + "//" + inputDisplayName;
                    }
                }
            }
        }

        //setup name and displayname
        if (OutputObjectData.CustomFields != null && OutputObjectData.CustomFields.TryGetValue("dream.multiflavored_name_format", out var nameFormat))
            myItemObject = setUpFlavoredNameAndDisplay(myItemObject, OutputObjectData.CustomFields);

        //setup color data
        if (myItemObject is ColoredObject coloredObject)
        {
            OutputObjectData.CustomFields.TryGetValue("dream.saved_ingredient_tokens", out string colorTag);
            string mixedColor = ObjectDraw.getMixedColorTag(myItemObject, colorTag.Split(",")[0]);
            myItemObject.modData["dream.output_color"] = mixedColor;
            coloredObject.color.Value = ObjectDraw.GetColorFromTag(mixedColor);
        }

        //setup price and quality
        int quality = calculateAverage(myItemObject);
        myItemObject.Quality = quality;
        int price = calculatePrice(myItemObject, quality, overridePriceMultiplier);
        myItemObject.Price = price;

        return (Item)myItemObject;
    }


    public static StardewValley.Object ProcessNewFlavoredItem(Dictionary<String, String> outputCustomFields, StardewValley.Object myOutputObject, StardewValley.Object inputItem)
    {
        //processing the display and name fields

        outputCustomFields.TryGetValue("dream.multiflavored_name_format", out string flavoredNameFormat);
        String nameAppend = null;
        if (!string.IsNullOrEmpty(flavoredNameFormat))
        {
            List<string> tokens = ExtractTextTokens(flavoredNameFormat);
            Dictionary<String, String> madeWith = new Dictionary<String, String>();

            foreach (string token in tokens)
            {
                string key1 = "dream.MadeWith_" + token;
                if (inputItem.modData.TryGetValue(key1, out string tokenValue))
                {
                    Dictionary<String, List<String>> madeWithData = getMadeWithForToken(tokenValue);
                    if (madeWithData != null)
                    {
                        if (madeWithData.TryGetValue("DisplayNames", out List<String> displayNames))
                            madeWith[token] = string.Join("||", displayNames);
                        if (madeWithData.TryGetValue("Ids", out List<String> ids))
                            nameAppend += "_" + string.Join("_", ids);
                    }
                }
            }

            myOutputObject.displayNameFormat = ReplaceTokens(flavoredNameFormat, madeWith);
        }

        myOutputObject.name += nameAppend ?? $"_{inputItem.ItemId}";
        myOutputObject.preservedParentSheetIndex.Value = inputItem.preservedParentSheetIndex.Value;

        if (myOutputObject is ColoredObject coloredObject)
        {
            String inputColor = ObjectDraw.getColorTagFromItem(inputItem);
            coloredObject.color.Value = inputItem is ColoredObject coloredInput
                ? coloredInput.color.Value
                : ObjectDraw.GetColorFromTag(inputColor);
            myOutputObject.modData["dream.output_color"] = inputColor;
        }
        return myOutputObject;
    }


    public static Dictionary<String, List<String>> getMadeWithForToken(String madeWith)
    {
        var outputData = new Dictionary<String, List<String>>
    {
        { "Ids", new List<String>() },
        { "Colors", new List<String>() },
        { "DisplayNames", new List<String>() }
    };

        foreach (var ingredient in madeWith.Split("||").Select(data => data.Split("//")).Where(ingredient => ingredient.Length == 3))
        {
            outputData["Ids"].Add(ingredient[0]);
            outputData["Colors"].Add(ingredient[1]);
            outputData["DisplayNames"].Add(ingredient[2]);
        }

        return outputData;
    }
    static StardewValley.Object setUpFlavoredNameAndDisplay(StardewValley.Object myOutputObject, Dictionary<String, String> outputCustomFields)
    {
        String nameAppend = "";
        //editing displayname
        if (outputCustomFields.TryGetValue("dream.multiflavored_name_format", out string flavoredNameFormat) && !string.IsNullOrEmpty(flavoredNameFormat))
        {
            String[] format = flavoredNameFormat.Split("||");
            if (format.Length > 1)
            {
                String[] tokens = format[1].Split(",");

                Dictionary<String, String> madeWith = new Dictionary<String, String>();

                foreach (string token in tokens)
                {
                    string key1 = "dream.MadeWith_" + token;
                    if (myOutputObject.modData.TryGetValue(key1, out string tokenValue) && !String.IsNullOrEmpty(tokenValue))
                    {
                        Dictionary<String, List<String>> madeWithData = getMadeWithForToken(tokenValue);
                        if (madeWithData != null)
                        {
                            if (madeWithData.TryGetValue("DisplayNames", out List<String> displayNames))
                                madeWith[token] = string.Join("||", displayNames);
                            if (madeWithData.TryGetValue("Ids", out List<String> tokenIds))
                            {
                                tokenIds.Sort();
                                nameAppend += "_" + string.Join("_", tokenIds);
                            }
                        }
                    }
                }
                myOutputObject.displayNameFormat = ReplaceTokens(format[0], madeWith);
            }
            else
            {
                myOutputObject.displayNameFormat = format[0];
            }
            if (!myOutputObject.GetContextTags().Any(tag => tag.Contains("nonvegan")) && myOutputObject.GetContextTags().Any(tag => tag.Contains("display_vegan_info")))
                myOutputObject.displayNameFormat += Helper.Translation.Get("dream.multiflavor_vegan_statement");
        }

        myOutputObject.Name += "_" + nameAppend;

        return myOutputObject;
    }


    static string ReplaceTokens(string input, Dictionary<string, string> tokenDictionary)
    {
        foreach (var token in tokenDictionary)
        {
            String[] ingredients = token.Value.Split("||");
            Array.Sort(ingredients);
            if (ingredients.Length > 1)
            {

                if (ingredients.Length > 2)
                {
                    input = input.Replace("{"+ token.Key + "}", Helper.Translation.Get("dream.multiflavor_join_statement_3"));
                    input = input.Replace("1", ingredients[0]);
                    input = input.Replace("2", ingredients[1]);
                    input = input.Replace("3", ingredients[2]);
                }
                else
                {
                    input = input.Replace("{" + token.Key + "}", Helper.Translation.Get("dream.multiflavor_join_statement_2"));
                    input = input.Replace("1", ingredients[0]);
                    input = input.Replace("2", ingredients[1]);
                }
            }
            else {
                input = input.Replace("{" + token.Key + "}", ingredients[0]);
            }
        }
        return input;
    }

    static string[] getItemToken(Item item)
    {
        String itemId = item.ItemId;
        string[] inputTokens = new string[] { "", "" };
        if (Game1.objectData.TryGetValue(itemId, out var value) && value != null)
        {
            if (value.CustomFields != null && value.CustomFields.TryGetValue("shortened_display_name", out var shortenedDisplay) && !string.IsNullOrEmpty(shortenedDisplay))
                inputTokens[0] = shortenedDisplay;
            List<String> contexttags = new List<String>();
            contexttags.AddRange(value.ContextTags?.Where(tag => tag.Contains("dream.ingredient")).Select(tag => tag.Substring(tag.LastIndexOf('_') + 1)));
            contexttags.AddRange(GetDefaultContextTags(item, true).Where(tag => !contexttags.Contains(tag)));
            inputTokens[1] = String.Join(",", contexttags);
        }
        return inputTokens;
    }

    static int calculateAverage(StardewValley.Object heldObject)
    {
        int finalQuality;
        heldObject.modData.TryGetValue("dream.multiflavor_Quality", out string qualityString);

        String[] qualityparts = qualityString.Split(",");

        if (qualityparts.Length > 1)
        {
            int[] qualityInts = Array.ConvertAll(qualityparts, int.Parse);
            finalQuality = (int)qualityInts.Average();
        }
        else {
            finalQuality = int.Parse(qualityparts[0]);
        }

        return finalQuality;
    }

    static int calculatePrice(StardewValley.Object heldObject, int quality, double priceMultiplier)
    {
        heldObject.modData.TryGetValue("dream.multiflavor_Price", out string priceString);

        double calculatedPrice = 0;
        if (!string.IsNullOrEmpty(priceString))
        {
            String[] priceparts = priceString.Split(",");
            foreach (string part in priceparts)
            {
                calculatedPrice += int.Parse(part) * priceMultiplier;
            }
        }
        else {
            calculatedPrice = heldObject.Price;
        }

        calculatedPrice += calculatedPrice * quality;

        return (int)Math.Round(calculatedPrice);
    }


    static StardewValley.Object castColoredObject(StardewValley.Object outputObject)
    {
        StardewValley.Objects.ColoredObject newColoredObject = new StardewValley.Objects.ColoredObject(
    outputObject.ItemId,
    outputObject.Stack,
    Color.White
    );
        Helper.Reflection.GetMethod(newColoredObject, "GetOneCopyFrom").Invoke(outputObject);
        newColoredObject.Stack = outputObject.Stack;
        return newColoredObject;
    }

    static List<string> GetDefaultContextTags(Item inputObject, Boolean tokenOnly)
    {
        List<string> tags = new List<string>();

        foreach (string tag in inputObject.GetContextTags())
        {
            if (defaultContextTags.Any(kvp => kvp.Key == tag))
            {
                var results = defaultContextTags.Where(kvp => kvp.Key == tag);
                foreach (var kvp in results)
                {
                    string valueToAdd = tokenOnly ? kvp.Value.Substring(kvp.Value.LastIndexOf('_') + 1) : kvp.Value;

                    if (!inputObject.HasContextTag(kvp.Value) && !tags.Contains(valueToAdd))
                        tags.Add(valueToAdd);
                }
            }
        }

        return tags;
    }

    static HashSet<string> SetUpContextTags(List<string> combinedTags, HashSet<string> inputTags, HashSet<string> heldItemTags)
    {

        HashSet<string> tags = new HashSet<string>(combinedTags);
        tags.UnionWith(inputTags.Where(tag => tag.Contains("allergen") || tag.Contains("nonvegan")));
        tags.UnionWith(heldItemTags.Where(tag => tag.Contains("allergen") || tag.Contains("nonvegan")));

        return tags;
    }


    private static List<string> ExtractTextTokens(string text)
    {
        text = text.Trim();
        int startIndex;
        List<string> tokens = new List<string>();
        do
        {
            startIndex = text.LastIndexOf('{');
            if (startIndex >= 0)
            {
                int num = text.IndexOf('}', startIndex);
                if (num == -1)
                    return tokens;
                tokens.Add(text.Substring(startIndex + 1, num - startIndex - 1));
            }
        }
        while (startIndex >= 0);
        return tokens;
    }

    private static int? GetOverrideMinutes(MachineItemOutput outputData)
    {
        if (outputData.CustomData.TryGetValue("dream.minutesUntilReady", out string minutesUntilReadyStr) &&
            int.TryParse(minutesUntilReadyStr, out int minutesUntilReady))
        {
            return minutesUntilReady;
        }
        return 100;
    }

    private static double GetPriceMultiplier(MachineItemOutput outputData)
    {
        if (outputData.CustomData.TryGetValue("dream.priceMultiplier", out string priceMultiplierStr) &&
            double.TryParse(priceMultiplierStr, out double priceMultiplier))
        {
            return priceMultiplier;
        }
        return 1.5;
    }

}

