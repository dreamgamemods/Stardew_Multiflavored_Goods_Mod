
using System.Collections.Generic;

namespace MultiFlavorMachines.MachineLogic
{
    public class MachineLogicRule
    {
        public string MachineId { get; set; }
        public OutputItems[] OutputItems { get; set; }
        public MachineLogicRule()
        {
            this.OutputItems = new OutputItems[] { new OutputItems() };
        }

    }
}
