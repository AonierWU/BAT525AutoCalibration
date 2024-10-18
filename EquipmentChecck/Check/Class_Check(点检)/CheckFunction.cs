using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{ 
    public partial class CheckItem
    {
        // public Com com = new Com();
        public MyEqmCmd com { get; set; }
        public MyEqmCmd DCsource { get; set; }
        public CheckTestData Checkdata = new CheckTestData();
        public static bool StopStatus = false;
        
        

    }
}
