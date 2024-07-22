using Netcode;
using StardewModdingAPI;
using System;
using System.Collections.Generic;

namespace MultiFlavorMachines.DisplayLogic
{
    public class MachineDisplayRule
    {
        public string Spritesheet { get; set; }
        public int StartingIndex { get; set; }
        public int RectangleSize { get; set; }
        public string ColorSource { get; set; }
        public string ColorOverrideSpritesheet { get; set; }
        public string OverrideColors { get; set; }
        public float Scale { get; set; }
        public float PaddingX_16 { get; set; }
        public float PaddingY_16 { get; set; }

        public float PaddingX_32 { get; set; }
        public float PaddingY_32 { get; set; }

        public MachineDisplayRule()
        {
            Spritesheet = null;
            StartingIndex = 0;
            RectangleSize = 16;
            Scale = 1f;
            ColorSource = "ORIGIN";
            ColorOverrideSpritesheet = null;
            OverrideColors = null;
            PaddingX_16 = 999f;
            PaddingY_16 = 999f;
            PaddingX_32 = 999f;
            PaddingY_32 = 999f;
        }
    }
}
