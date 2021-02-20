using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Jmas
{
    [Serializable]
    public class ResourceHolder
    {
        public int Value { get; private set; }
        public int MaxValue { get; }
        public event Action<int> ResourceExceed;

        public ResourceHolder(int value = 0, int maxValue = Int32.MaxValue)
        {
            Value = value;
            MaxValue = maxValue;
        }

        public void Set(int num)
        {
            if (num >= 0 && num<=MaxValue)
                Value = num;
            else {
                throw new NumericException($"Cannot set value to larger than MaxValue {MaxValue}");
            }
        }
        public void Gain(int num)
        {
            if (num < 0)
                throw new NumericException($"Cannot gain value less than 0 : {num}");
            if (Value > MaxValue - num) {
                ResourceExceed?.Invoke(Value - MaxValue + num);
                Value = MaxValue;
            }
            else {
                Value += num;
            }
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
