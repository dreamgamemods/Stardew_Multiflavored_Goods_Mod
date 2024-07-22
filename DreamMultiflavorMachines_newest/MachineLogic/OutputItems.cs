
using System.Collections.Generic;

namespace MultiFlavorMachines.MachineLogic
{
    public class OutputItems
    {
        public int Id { get; set; }
        public string Recipe { get; set; }
        public OutputItems()
        {
            this.Id = 0;
            this.Recipe = null;
        }

    }
}
