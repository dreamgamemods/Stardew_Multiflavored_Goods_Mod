
using System.Collections.Generic;

namespace MultiFlavorMachines.MachineLogic
{
    public class MachineLogicData
    {
        public List<MachineLogicRule> MachineLogicRules { get; set; }
        public MachineLogicData()
        {
            this.MachineLogicRules = new List<MachineLogicRule>();
        }

    }
}
