
using System.Collections.Generic;

namespace MultiFlavorMachines.DisplayLogic
{
    public class SpecialDisplayRule
    {
        public string Id { get; set; }
        public DisplayRule DisplayRule { get; set; }
        public MachineDisplayRule MachineDisplayRule { get; set; }
        public SpecialDisplayRule()
        {
            Id = "";
            DisplayRule = new DisplayRule();
            MachineDisplayRule = new MachineDisplayRule();
        }

    }
}
