using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace Jmas
{
    public delegate void CounterCountToStop(int value, int overflow);

    [Serializable]
    public class Counter
    {

        [SerializeField] private readonly int beginAt = 0;
        public int BeginAt { get => beginAt; }
        [SerializeField] private readonly int stopAt;
        public int StopAt { get => stopAt; }
        [SerializeField] private int number;
        public int Number { get => number; private set => number = value; }
        [SerializeField] private readonly bool fromLowToHigh;
        public bool FromLowToHigh { get => fromLowToHigh; }

        public event CounterCountToStop OnCounterStop;

        public Counter(int beginAt, int stopAt, int at, bool fromLowToHigh)
        {
            this.beginAt = beginAt;
            this.stopAt = stopAt;
            number = at;
            this.fromLowToHigh = fromLowToHigh;
        }
        public int CountForward(int stride = 1)
        {
            if (fromLowToHigh)
            {
                if (Number >= stopAt)
                    throw new CounterException("Cannot count a stopped counter");
                Number += stride;
                if (Number >= stopAt)
                {
                    OnCounterStop?.Invoke(stopAt, Number - stopAt);
                }
            }
            else
            {
                if (Number <= stopAt)
                    throw new CounterException("Cannot count a stopped counter");
                Number -= stride;
                if (Number <= stopAt)
                {
                    OnCounterStop?.Invoke(stopAt, Number - stopAt);
                }
            }
            return Number;
        }
        public void Reset()
        {
            number = beginAt;
        }
        public void ResetTo(int value)
        {
            if (FromLowToHigh && (value >= stopAt || value <= beginAt)
                || !FromLowToHigh && (value <= stopAt || value >= beginAt))
                throw new CounterException("Invalid value set.");
            number = value;
        }
    }

    [Serializable]
    public class CounterException : Exception
    {
        public CounterException(string message) : base(message) { }

        public CounterException()
        {
        }

        public CounterException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
