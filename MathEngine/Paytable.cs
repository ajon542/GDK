using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEngine
{
    public class Paytable
    {
        public ReelGroup reelGroup;
        public PaylineGroup paylineGroup;
        public PayComboGroup payComboGroup;

        public void ConstructDummyPaytable()
        {
            reelGroup = new ReelGroup();
            paylineGroup = new PaylineGroup();
            payComboGroup = new PayComboGroup();
        }
    }
}
