using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Jmas
{
    public class IntScore
    {
        public int Value { get; private set; }
        private Predicate<int> validRange;
        private bool IsValid(int value)
        {
            return validRange?.Invoke(value) ?? true;
        }

        public IntScore(int value, Predicate<int> validRange)
        {
            Value = value;
            this.validRange = validRange;
        }

        public void Set(int v)
        {
            if (v >= 0 && IsValid(v))
                Value = v;
            else {
                throw new NumericException($"{nameof(IntScore)} cannot be set below zero or out of bound.");
            }
        }
        public int Increment(int v)
        {
            int g = Value + v;
            if (v >= 0 && IsValid(v))
                Value = g;
            else {
                throw new NumericException($"{nameof(IntScore)} cannot be set below zero or out of bound.");
            }
            return g;
        }
        public int Decrement(int v)
        {
            int g = Value - v;
            if (v >= 0 && IsValid(v))
                Value = g;
            else 
                throw new NumericException($"{nameof(IntScore)} cannot be set below zero or out of bound.");
            return g;
        }
    }
}
