using StardewModdingAPI;
using System;
using System.Collections.Generic;

namespace MultiFlavorMachines.DisplayLogic
{
    public class DisplayRule
    {
        public string Spritesheet { get; set; }
        public int StartingIndex { get; set; }

        public int RectangleSize { get; set; }
        public string ColorSource { get; set; }
        public string ColorOverrideSpritesheet { get; set; }
        public string OverrideColors { get; set; }
        public float ScaleMenu { get; set; }
        public float ScaleHeld { get; set; }
        public float PaddingX { get; set; }
        public float PaddingY { get; set; }

        public DisplayRule()
        {
            Spritesheet = "ORIGIN";
            StartingIndex = 0;
            RectangleSize = 16;
            ColorSource = "ORIGIN";
            ColorOverrideSpritesheet = null;
            OverrideColors = null;
            ScaleMenu = 1f;
            ScaleHeld = 1f;
            PaddingX = 0f;
            PaddingY = 0f;
        }
    }
}
