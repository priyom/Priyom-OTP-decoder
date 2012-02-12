using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriyomOTPcoder
{
    class RandomGen
    {
        public Random Random()
        {
            this.Randomvar = new Random();
            return this.Randomvar;
        }
        public Random Randomvar;
    }
}
