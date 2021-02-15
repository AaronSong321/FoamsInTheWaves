using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Jmas
{
    public class ResourceHolder
    {
        public int Value { get; private set; }

        public void Gain(int num)
        {
            if (num <= 0)
            {
                throw new NumericException($"Cannot gain less than 0: {num}");
            }
            Value += num;
        }
        public void Use(int num)
        {
            if (num >= Value)
            {
                throw new NumericException($"Insufficient fund: {num}");
            }
            if (num <= 0)
            {
                throw new NumericException($"Cannot spend less than 0: {num}");
            }
            Value -= num;
        }
    }
}
