
using StardewValley;
using System.Collections.Generic;
using System;
using System.Linq;
using StardewValley.Objects;
using Color = Microsoft.Xna.Framework.Color;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.ItemTypeDefinitions;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using System.Data;


namespace MultiFlavorMachines.DisplayLogic;


/// <summary>controls the display of sprites and their coloring</summary>
public class ObjectDraw
{
    public static (int r, int g, int b) MixColors(string color1, string color2)
    {
        // Retrieve RGB values for both colors
        (int r1, int g1, int b1) = GetRGB(color1);
        (int r2, int g2, int b2) = GetRGB(color2);

        // Calculate the average of RGB values
        int rMixed = (r1 + r2) / 2;
        int gMixed = (g1 + g2) / 2;
        int bMixed = (b1 + b2) / 2;

        return (rMixed, gMixed, bMixed);
    }

    public static Color GetColorFromTag(string colorTag)
    {
        switch (colorTag)
        {
            case "color_black":
                return new Color(45, 45, 45);
            case "color_gray":
                return Color.Gray;
            case "color_white":
                return Color.White;
            case "color_pink":
                return new Color(255, 163, 186);
            case "color_red":
                return new Color(220, 0, 0);
            case "color_orange":
                return new Color(255, 128, 0);
            case "color_yellow":
                return new Color(255, 230, 0);
            case "color_green":
                return new Color(10, 143, 0);
            case "color_blue":
                return new Color(46, 85, 183);
            case "color_purple":
                return new Color(115, 41, 181);
            case "color_brown":
                return new Color(130, 73, 37);
            case "color_light_cyan":
                return new Color(180, 255, 255);
            case "color_cyan":
                return Color.Cyan;
            case "color_aquamarine":
                return Color.Aquamarine;
            case "color_sea_green":
                return Color.SeaGreen;
            case "color_lime":
                return Color.Lime;
            case "color_yellow_green":
                return Color.GreenYellow;
            case "color_pale_violet_red":
                return Color.PaleVioletRed;
            case "color_salmon":
                return new Color(255, 85, 95);
            case "color_jade":
                return new Color(130, 158, 93);
            case "color_sand":
                return Color.NavajoWhite;
            case "color_poppyseed":
                return new Color(82, 47, 153);
            case "color_dark_red":
                return Color.DarkRed;
            case "color_dark_orange":
                return Color.DarkOrange;
            case "color_dark_yellow":
                return Color.DarkGoldenrod;
            case "color_dark_green":
                return Color.DarkGreen;
            case "color_dark_blue":
                return Color.DarkBlue;
            case "color_dark_purple":
                return Color.DarkViolet;
            case "color_dark_pink":
                return Color.DeepPink;
            case "color_dark_cyan":
                return Color.DarkCyan;
            case "color_dark_gray":
                return Color.DarkGray;
            case "color_dark_brown":
                return Color.SaddleBrown;
            case "color_gold":
                return Color.Gold;
            case "color_copper":
                return new Color(179, 85, 0);
            case "color_iron":
                return new Color(197, 213, 224);
            case "color_iridium":
                return new Color(105, 15, 255);
        }
        return Color.White;
    }

    public static (int r, int g, int b) GetRGB(string color)
    {
        switch (color)
        {
            case "color_black":
                return (45, 45, 45);
            case "color_gray":
                return (128, 128, 128);
            case "color_white":
                return (255, 255, 255);
            case "color_pink":
                return (255, 163, 186);
            case "color_red":
                return (220, 0, 0);
            case "color_orange":
                return (255, 128, 0);
            case "color_yellow":
                return (255, 230, 0);
            case "color_green":
                return (10, 143, 0);
            case "color_blue":
                return (46, 85, 183);
            case "color_purple":
                return (115, 41, 181);
            case "color_brown":
                return (130, 73, 37);
            case "color_light_cyan":
                return (180, 255, 255);
            case "color_cyan":
                return (0, 255, 255);
            case "color_aquamarine":
                return (127, 255, 212);
            case "color_sea_green":
                return (46, 139, 87);
            case "color_lime":
                return (0, 255, 0);
            case "color_yellow_green":
                return (173, 255, 47);
            case "color_pale_violet_red":
                return (219, 112, 147);
            case "color_salmon":
                return (255, 85, 95);
            case "color_jade":
                return (130, 158, 93);
            case "color_sand":
                return (244, 164, 96);
            case "color_poppyseed":
                return (82, 47, 153);
            case "color_dark_red":
                return (139, 0, 0);
            case "color_dark_orange":
                return (255, 140, 0);
            case "color_dark_yellow":
                return (184, 134, 11);
            case "color_dark_green":
                return (0, 100, 0);
            case "color_dark_blue":
                return (0, 0, 139);
            case "color_dark_purple":
                return (148, 0, 211);
            case "color_dark_pink":
                return (255, 20, 147);
            case "color_dark_cyan":
                return (0, 139, 139);
            case "color_dark_gray":
                return (169, 169, 169);
            case "color_dark_brown":
                return (139, 69, 19);
            case "color_gold":
                return (255, 215, 0);
            case "color_copper":
                return (179, 85, 0);
            case "color_iron":
                return (197, 213, 224);
            case "color_iridium":
                return (105, 15, 255);
        }
        return (128, 128, 128);
    }

    public static string FindClosestColor((int r, int g, int b) color)
    {
        Dictionary<string, (int r, int g, int b)> colors = new Dictionary<string, (int r, int g, int b)>
        {
            {"color_black", (45, 45, 45)},
            {"color_gray", (128, 128, 128)},
            {"color_white", (255, 255, 255)},
            {"color_pink", (255, 163, 186)},
            {"color_red", (220, 0, 0)},
            {"color_orange", (255, 128, 0)},
            {"color_yellow", (255, 230, 0)},
            {"color_green", (10, 143, 0)},
            {"color_blue", (46, 85, 183)},
            {"color_purple", (115, 41, 181)},
            {"color_brown", (130, 73, 37)},
            {"color_light_cyan", (180, 255, 255)},
            {"color_cyan", (0, 255, 255)},
            {"color_aquamarine", (127, 255, 212)},
            {"color_sea_green", (46, 139, 87)},
            {"color_lime", (0, 255, 0)},
            {"color_yellow_green", (173, 255, 47)},
            {"color_pale_violet_red", (219, 112, 147)},
            {"color_salmon", (255, 85, 95)},
            {"color_jade", (130, 158, 93)},
            {"color_sand", (244, 164, 96)},
            {"color_poppyseed", (82, 47, 153)},
            {"color_dark_red", (139, 0, 0)},
            {"color_dark_orange", (255, 140, 0)},
            {"color_dark_yellow", (184, 134, 11)},
            {"color_dark_green", (0, 100, 0)},
            {"color_dark_blue", (0, 0, 139)},
            {"color_dark_purple", (148, 0, 211)},
            {"color_dark_pink", (255, 20, 147)},
            {"color_dark_cyan", (0, 139, 139)},
            {"color_dark_gray", (169, 169, 169)},
            {"color_dark_brown", (139, 69, 19)},
            {"color_gold", (255, 215, 0)},
            {"color_copper", (179, 85, 0)},
            {"color_iron", (197, 213, 224)},
            {"color_iridium", (105, 15, 255)}
        };

        double minDistanceSquared = double.MaxValue;
        string closestColor = null;

        foreach (var kvp in colors)
        {
            int rDiff = color.r - kvp.Value.r;
            int gDiff = color.g - kvp.Value.g;
            int bDiff = color.b - kvp.Value.b;

            double distanceSquared = rDiff * rDiff + gDiff * gDiff + bDiff * bDiff;

            if (distanceSquared < minDistanceSquared)
            {
                minDistanceSquared = distanceSquared;
                closestColor = kvp.Key;
            }
        }

        return closestColor;
    }


    public static string getColorTagFromItem(Item item)
    {
        if (item.modData.TryGetValue("dream.output_color", out string outputColor) && !string.IsNullOrEmpty(outputColor))
        {
            return outputColor;
        }
        else
        {
            string colorTags = string.Join(",", item.GetContextTags().Where(tag => tag.StartsWith("color_")));
            string foundColor = colorTags.Contains(",") ? colorTags.Substring(0, colorTags.IndexOf(',')) : colorTags;
            if (!string.IsNullOrEmpty(foundColor))
                return foundColor;
        }
        return "color_gray";
    }

    public static Color? getColorMaskFromTag(string tag, Item item)
    {
        string myTag = tag;
        int index = 0;
        Color? color = null;
        if (!string.IsNullOrEmpty(tag) && !char.IsDigit(tag[^1]))
        {
            string mixedColor = getMixedColorTag((StardewValley.Object)item, tag);
            color = GetColorFromTag(mixedColor);
        }
        if (!string.IsNullOrEmpty(tag) && char.IsDigit(tag[^1]))
        {
            index = int.Parse(tag[^1].ToString());
            index -= 1;
            myTag = tag[..^1];
        }
        if (item.modData.TryGetValue($"dream.MadeWith_{myTag}", out string madeWithValue) && !string.IsNullOrEmpty(madeWithValue))
        {
            Dictionary<string, List<string>> madeWith = ModEntry.getMadeWithForToken(madeWithValue);
            if (madeWith.TryGetValue("Colors", out List<string> myColors))
            {
                myColors.Sort();
                if (myColors.Count > index)
                    color = GetColorFromTag(myColors[index]);
            }
        }

        return color;
    }

    public static string getColorNameFromTag(string tag, Item item)
    {
        string myTag = tag;
        int index = 0;
        string color = null;
        if (!string.IsNullOrEmpty(tag) && !char.IsDigit(tag[^1]))
            color = getMixedColorTag((StardewValley.Object)item, tag);

        if (!string.IsNullOrEmpty(tag) && char.IsDigit(tag[^1]))
        {
            index = int.Parse(tag[^1].ToString());
            index -= 1;
            myTag = tag[..^1];
        }
        if (item.modData.TryGetValue($"dream.MadeWith_{myTag}", out string madeWithValue) && !string.IsNullOrEmpty(madeWithValue))
        {
            Dictionary<string, List<string>> madeWith = ModEntry.getMadeWithForToken(madeWithValue);
            if (madeWith.TryGetValue("Colors", out List<string> myColors))
            {
                myColors.Sort();
                if (myColors.Count > index)
                    color = myColors[index];
            }
        }

        return color;
    }


    public static string getMixedColorTag(StardewValley.Object myItemObject, string colorToken)
    {
        string closestColor = null;

        string madeWithValue = (myItemObject as Item)?.modData.TryGetValue($"dream.MadeWith_{colorToken}", out string temp) == true ? temp : null;

        List<string> savedColors = new List<string>();
        if (!string.IsNullOrEmpty(madeWithValue))
        {
            Dictionary<string, List<string>> madeWith = ModEntry.getMadeWithForToken(madeWithValue);
            if (madeWith.TryGetValue("Colors", out List<string> myColors))
                savedColors = myColors;
        }

        if (savedColors.Count < 1)
        {
            closestColor = myItemObject.GetContextTags().FirstOrDefault(tag => tag.StartsWith("color_"));
        }
        else
        {
            savedColors.Sort();
            closestColor = savedColors[0];
            for (int i = 1; i < savedColors.Count; i++)
            {
                string color = savedColors[i];
                (int r, int g, int b) mixedColor = MixColors(color, closestColor);
                closestColor = FindClosestColor(mixedColor);
            }
        }

        return closestColor;
    }

    public static bool ObjectDraw_prefix(StardewValley.Object __instance, SpriteBatch spriteBatch, int x, int y, float alpha = 1f)
    {
        if (__instance.TypeDefinitionId.Equals("(O)") && __instance.HasContextTag("dream.special_display_rule"))
        {
            Vector2 location = __instance.TileLocation;
            drawSpritesObject(__instance, spriteBatch, location + new Vector2(8f, 8f) * 1f, alpha, false);
            return false;
        }
        return true;
    }

    public static void ObjectDraw_postfix(StardewValley.Object __instance, SpriteBatch spriteBatch, int x, int y, float alpha = 1f)
    {
        if (__instance.TypeDefinitionId.Equals("(BC)") && __instance.HasContextTag("dream.special_display_rule"))
        {
            if (__instance.heldObject.Value != null)
            {
                Vector2 locationMachine = __instance.TileLocation;
                drawSpritesMachine(__instance, spriteBatch, locationMachine);
            }
        } else if (__instance.TypeDefinitionId.Equals("(O)") && __instance.HasContextTag("dream.special_display_rule"))
        {
            Vector2 location = __instance.TileLocation;
            drawSpritesObject(__instance, spriteBatch, location + new Vector2(8f, 8f) * 1f, alpha, false);
        }
    }

    public static bool Object_drawInMenu_prefix(StardewValley.Object __instance, SpriteBatch spriteBatch, Vector2 location, float scaleSize, float transparency, float layerDepth, StackDrawType drawStackNumber, Color color, bool drawShadow)
    {
        if (__instance.TypeDefinitionId.Equals("(O)") && __instance.HasContextTag("dream.special_display_rule"))
        {
            __instance.AdjustMenuDrawForRecipes(ref transparency, ref scaleSize);
            drawSpritesObject(__instance, spriteBatch, location + new Vector2(8f, 8f) * scaleSize, layerDepth, true);
            __instance.DrawMenuIcons(spriteBatch, location, scaleSize, transparency, layerDepth + 3E-05f, drawStackNumber, color);
            return false;
        }
        return true;
    }

    public static bool ColoredObject_drawInMenu_prefix(ColoredObject __instance, SpriteBatch spriteBatch, Vector2 location, float scaleSize, float transparency, float layerDepth, StackDrawType drawStackNumber, Color colorOverride, bool drawShadow)
    {
        if (__instance.TypeDefinitionId.Equals("(O)") && __instance.HasContextTag("dream.special_display_rule"))
        {
            __instance.AdjustMenuDrawForRecipes(ref transparency, ref scaleSize);
            drawSpritesObject(__instance, spriteBatch, location + new Vector2(8f, 8f) * scaleSize, layerDepth, true);
            __instance.DrawMenuIcons(spriteBatch, location, scaleSize, transparency, layerDepth + 3E-05f, drawStackNumber, colorOverride);
            return false;
        }
        return true;
    }

    public static bool Object_drawWhenHeld_prefix(StardewValley.Object __instance, SpriteBatch spriteBatch, Vector2 objectPosition, Farmer f)
    {
        if (__instance.TypeDefinitionId.Equals("(O)") && __instance.HasContextTag("dream.special_display_rule"))
        {
            float layerDepth = Math.Max(0f, (f.StandingPixel.Y + 4) / 10000f);
            drawSpritesObject(__instance, spriteBatch, objectPosition + new Vector2(8f, 8f) * 1f, layerDepth, false);
            return false;
        }
        return true;
    }

    public static void drawSpritesObject(StardewValley.Object __instance, SpriteBatch spriteBatch, Vector2 location, float layerDepth, bool isMenu)
    {
        Texture2D spritesheet = null;
        int spriteIndex = 0;
        Color? color = null;
        float colorStrength = 1f;
        int rectangleWidth = 16;
        float scale = 1f;
        float paddingX = 0f;
        float paddingY = 0f;

        SpecialDisplayRule specialDisplayRule = new SpecialDisplayRule();

        foreach (SpecialDisplayRule rule in DataLoader.specialDisplayRuleData.SpecialDisplayRules)
        {
            if (rule.Id.Equals(__instance.ItemId))
            {
                specialDisplayRule = rule;
                break;
            }
        }

        if (!string.IsNullOrEmpty(specialDisplayRule.Id))
        {
            DisplayRule rule = specialDisplayRule.DisplayRule;

            if (rule.Spritesheet.Equals("ORIGIN"))
            {
                ParsedItemData dataItem = ItemRegistry.GetDataOrErrorItem(__instance.QualifiedItemId);
                spritesheet = dataItem.GetTexture();
                spriteIndex = dataItem.SpriteIndex;
            }
            else
            {
                spritesheet = Game1.content.Load<Texture2D>(rule.Spritesheet);
                spriteIndex = rule.StartingIndex;
            }

            //1
            rectangleWidth = rule.RectangleSize;
            paddingX = rule.PaddingX;
            paddingY = rule.PaddingY;
            string thisColor = null;
            string[] colors = rule.ColorSource.Split(',').Select(s => s.Trim()).ToArray();

            scale = isMenu ? rule.ScaleMenu : rule.ScaleHeld;

            if (!string.IsNullOrEmpty(colors[0]) && !colors[0].StartsWith("ORIGIN"))
            {
                Color? myColor = getColorMaskFromTag(colors[0], __instance);
                color = myColor ?? Color.White;
                thisColor = colors[0];
            }
            else
            {
                color = Color.White;
            }
            drawSprite(spritesheet, spriteIndex, spriteBatch, rectangleWidth, location, scale, (Color)color, colorStrength, layerDepth, specialDisplayRule, thisColor, false);
            //2
            if (colors.Length > 1)
            {
                spriteIndex = spriteIndex + 1;
                string[] myColorData = colors[1].Split(':');
                colorStrength = float.Parse(myColorData[1]);
                color = getColorMaskFromTag(myColorData[0], __instance);
                if (color.HasValue)
                {
                    thisColor = getColorNameFromTag(myColorData[0], __instance);
                    drawSprite(spritesheet, spriteIndex, spriteBatch, rectangleWidth, location, scale, (Color)color, colorStrength, layerDepth, specialDisplayRule, thisColor, false);
                }
            }
            if (colors.Length > 2)
            {
                spriteIndex = spriteIndex + 1;
                string[] myColorData = colors[2].Split(':');
                colorStrength = float.Parse(myColorData[1]);
                color = getColorMaskFromTag(myColorData[0], __instance);
                if (color.HasValue)
                {
                    thisColor = getColorNameFromTag(myColorData[0], __instance);
                    drawSprite(spritesheet, spriteIndex, spriteBatch, rectangleWidth, location, scale, (Color)color, colorStrength, layerDepth, specialDisplayRule, thisColor, false);
                }
            }
            if (colors.Length > 3)
            {
                spriteIndex = spriteIndex + 1;
                string[] myColorData = colors[3].Split(':');
                colorStrength = float.Parse(myColorData[1]);
                color = getColorMaskFromTag(myColorData[0], __instance);
                if (color.HasValue)
                {
                    thisColor = getColorNameFromTag(myColorData[0], __instance);
                    drawSprite(spritesheet, spriteIndex, spriteBatch, rectangleWidth, location, scale, (Color)color, colorStrength, layerDepth, specialDisplayRule, thisColor, false);
                }
            }
        }
    }


    public static void drawSpritesMachine(StardewValley.Object __instance, SpriteBatch spriteBatch, Vector2 location)
    {
        //offset1 = 8.0; offset2 = 40.0; for 32
        //offset1 = 32.0; offset2 = 16.0; for 16
        float layerDepth = (float)((location.Y + 1.0) * 64.0 / 10000.0);
        StardewValley.Object @object = __instance.heldObject.Value;
        string itemId = @object.ItemId;
        SpecialDisplayRule specialDisplayRule = new SpecialDisplayRule();
        SpecialDisplayRule machineDefaultRule = new SpecialDisplayRule();
        
        foreach (SpecialDisplayRule specialRule in DataLoader.specialDisplayRuleData.SpecialDisplayRules)
        {
            if (specialRule.Id.Equals(__instance.ItemId + "_default"))
                machineDefaultRule = specialRule;
            if (specialRule.Id.Equals(itemId))
                specialDisplayRule = specialRule;
        }

        Vector2 myLocation = Game1.GlobalToLocal(Game1.viewport, new Vector2((float)(location.X * 64.0 + 32.0), (float)(location.Y * 64.0 - 16.0)));
        Texture2D spritesheet = null;
        int spriteIndex = 0;
        Color? color = null;
        float colorStrength = 1f;
        int rectangleWidth = 16;
        float scale = 1f;
        float paddingX = 32.0f;
        float paddingY = 16.0f;
        string thisColor = null;

            MachineDisplayRule rule = specialDisplayRule.MachineDisplayRule;
            rule = SetUpDefaults(rule, machineDefaultRule.MachineDisplayRule);

            //1
            if (rule.Spritesheet.Equals("ORIGIN")) {
                ParsedItemData dataItem = ItemRegistry.GetDataOrErrorItem(@object.QualifiedItemId);
                spritesheet = dataItem.GetTexture();
                spriteIndex = dataItem.SpriteIndex;
            }
            else
            {
                spritesheet = Game1.content.Load<Texture2D>(rule.Spritesheet);
                spriteIndex = rule.StartingIndex;
            }
            scale = rule.Scale;
            rectangleWidth = rule.RectangleSize;
            if (rectangleWidth > 16)
            {
                paddingX = rule.PaddingX_32;
                paddingY = rule.PaddingY_32;
            }
            else{
                paddingX = rule.PaddingX_16;
                paddingY = rule.PaddingY_16;
            }
            string[] colors = rule.ColorSource.Split(',').Select(s => s.Trim()).ToArray();
            if (!string.IsNullOrEmpty(colors[0]) && !colors[0].StartsWith("ORIGIN"))
            {
                string[] myColorData = colors[0].Split(':');
                colorStrength = float.Parse(myColorData[1]);
                Color? myColor = getColorMaskFromTag(myColorData[0], @object);
                color = myColor ?? Color.White;
                thisColor = myColorData[0];
            }
            else
            {
                color = Color.White;
            }
            myLocation = Game1.GlobalToLocal(Game1.viewport, new Vector2((float)(location.X * 64.0 + paddingX), (float)(location.Y * 64.0 - paddingY)));

            drawSprite(spritesheet, spriteIndex, spriteBatch, rectangleWidth, myLocation, scale, (Color)color, colorStrength, layerDepth, specialDisplayRule, thisColor, true);

            if (rule.Spritesheet.Equals("ORIGIN") && @object is ColoredObject coloredObject)
            {
                spriteIndex = spriteIndex + 1;
                colorStrength = 1f;
                color = coloredObject.color.Value;
                thisColor = "ColoredObject";
                if (color.HasValue)
                    drawSprite(spritesheet, spriteIndex, spriteBatch, rectangleWidth, myLocation, scale, (Color)color, colorStrength, MathF.BitIncrement(layerDepth), specialDisplayRule, thisColor, true);
            }

            //2
            if (colors.Length > 1)
            {
                spriteIndex = spriteIndex + 1;
                string[] myColorData = colors[1].Split(':');
                colorStrength = float.Parse(myColorData[1]);
                color = getColorMaskFromTag(myColorData[0], @object);
                thisColor = getColorNameFromTag(myColorData[0], @object);
                if (color.HasValue)
                    drawSprite(spritesheet, spriteIndex, spriteBatch, rectangleWidth, myLocation, scale, (Color)color, colorStrength, MathF.BitIncrement(layerDepth), specialDisplayRule, thisColor, true);
            }
            //3
            if (colors.Length > 2)
            {
                spriteIndex = spriteIndex + 1;
                string[] myColorData = colors[2].Split(':');
                colorStrength = float.Parse(myColorData[1]);
                color = getColorMaskFromTag(myColorData[0], @object);
                thisColor = getColorNameFromTag(myColorData[0], @object);
                if (color.HasValue)
                    drawSprite(spritesheet, spriteIndex, spriteBatch, rectangleWidth, myLocation, scale, (Color)color, colorStrength, MathF.BitIncrement(layerDepth), specialDisplayRule, thisColor, true);
            }
            if (colors.Length > 3)
            {
                spriteIndex = spriteIndex + 1;
                string[] myColorData = colors[3].Split(':');
                colorStrength = float.Parse(myColorData[1]);
                color = getColorMaskFromTag(myColorData[0], @object);
                thisColor = getColorNameFromTag(myColorData[0], @object);
                if (color.HasValue)
                    drawSprite(spritesheet, spriteIndex, spriteBatch, rectangleWidth, myLocation, scale, (Color)color, colorStrength, MathF.BitIncrement(layerDepth), specialDisplayRule, thisColor, true);
            }
    }



    private static void drawSprite(Texture2D texture2D, int spriteindex, SpriteBatch spriteBatch, int rectangleWidth, Vector2 location, float scaleSize, Color color, float colorStrength, float layerDepth, SpecialDisplayRule rule, string baseColor, bool isMachine)
    {
        Rectangle rectangle = rectangleWidth > 16
            ? Game1.getSquareSourceRectForNonStandardTileSheet(texture2D, rectangleWidth, rectangleWidth, spriteindex)
            : Game1.getSourceRectForStandardTileSheet(texture2D, spriteindex, rectangleWidth, rectangleWidth);

        float num = 4f * scaleSize;
        Vector2 vector = new Vector2(8f, 8f);

        if (color != Color.White && !baseColor.Equals("ColoredObject"))
        {
            if (rule != null && baseColor != null)
            {
                string[] overrideIndex = ColorOverride(rule, baseColor, isMachine);
                if (!string.IsNullOrEmpty(overrideIndex[0]))
                {
                    texture2D = Game1.content.Load<Texture2D>(overrideIndex[0]);
                    spriteindex = int.Parse(overrideIndex[1]);
                    colorStrength = 0.3f;
                }
            }

            rectangle = rectangleWidth > 16
                ? Game1.getSquareSourceRectForNonStandardTileSheet(texture2D, rectangleWidth, rectangleWidth, spriteindex)
                : Game1.getSourceRectForStandardTileSheet(texture2D, spriteindex, rectangleWidth, rectangleWidth);

            spriteBatch.Draw(texture2D, location, rectangle, Color.White * 1f, 0f, vector * scaleSize, num, SpriteEffects.None, layerDepth);
            spriteBatch.Draw(texture2D, location, rectangle, color * colorStrength, 0f, vector * scaleSize, num, SpriteEffects.None, MathF.BitIncrement(layerDepth));
        }
        else {
            spriteBatch.Draw(texture2D, location, rectangle, color * 1f, 0f, vector * scaleSize, num, SpriteEffects.None, layerDepth);
        }
    }

    private static MachineDisplayRule SetUpDefaults(MachineDisplayRule rule, MachineDisplayRule defaultRule) 
    {
        if (string.IsNullOrEmpty(rule.Spritesheet))
            rule.Spritesheet = defaultRule.Spritesheet;
        if (rule.PaddingX_16 == 999f)
            rule.PaddingX_16 = defaultRule.PaddingX_16;
        if (rule.PaddingY_16 == 999f)
            rule.PaddingY_16 = defaultRule.PaddingY_16;
        if (rule.PaddingX_32 == 999f)
            rule.PaddingX_32 = defaultRule.PaddingX_32;
        if (rule.PaddingY_32 == 999f)
            rule.PaddingY_32 = defaultRule.PaddingY_32;

        return rule;
    }
    private static string[] ColorOverride(SpecialDisplayRule rule, string baseColor, bool isMachine)
    {
        string[] colorOverride = { null, null };
        if (isMachine)
        {
            if (rule.MachineDisplayRule != null && rule.MachineDisplayRule.OverrideColors != null && rule.MachineDisplayRule.OverrideColors.Contains(baseColor))
            {
                string[] colors = rule.MachineDisplayRule.OverrideColors.Split(",");
                foreach (string color in colors)
                {
                    if (color.Contains(baseColor))
                    {
                        //ModEntry.monitor.Log("color.Contains(baseColor)", (LogLevel)2);
                        string[] colorData = color.Split(":");
                        colorOverride[0] = rule.MachineDisplayRule.ColorOverrideSpritesheet;
                        colorOverride[1] = colorData[1];
                        break;
                    }
                }
            }
        }
        else
        {
            if (rule.DisplayRule != null && rule.DisplayRule.OverrideColors != null && rule.DisplayRule.OverrideColors.Contains(baseColor))
            {
                string[] colors = rule.DisplayRule.OverrideColors.Split(",");
                foreach (string color in colors)
                {
                    if (color.Contains(baseColor))
                    {
                        string[] colorData = color.Split(":");
                        colorOverride[0] = rule.DisplayRule.ColorOverrideSpritesheet;
                        colorOverride[1] = colorData[1];
                        break;
                    }
                }
            }
        }
        return colorOverride;
    }

}

