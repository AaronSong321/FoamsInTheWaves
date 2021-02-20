using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Jmas
{
    /// <summary>
    /// Exception thrown when an error happens on an instance of an <see cref="InitOnlyRef{T}"/> or <see cref="InitOnly{T}"/> class instance
    /// </summary>
    public sealed class InitOnlyException : Exception
    {
        public InitOnlyException(string message) : base(message) { }
    }
    
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class InitOnlyRef<T>
            where T: class
    {
        [JsonProperty]
        private T val;
        public InitOnlyRef(T item)
        {
            val = item;
        }
        public InitOnlyRef()
        {
        }
        /// <summary>
        /// Retrieve the value if it is set, or throw an exception otherwise
        /// </summary>
        public T Value {
            get {
                if (val is null)
                    throw new InitOnlyException($"Ref T is not set.");
                return val;
            }
            set {
                if (value is null)
                    throw new InitOnlyException($"cannot set null to a InitOnly value.");
                if (val is null)
                    val = value;
                else throw new InitOnlyException($"Ref T is already set.");
            }
        }
        /// <summary>
        /// Get the value if it is set, or null otherwise
        /// </summary>
        public T TryGet() => val;

        public bool IsSet {
            get => val != null;
        }
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class InitOnly<T>
        where T : struct
    {
        [JsonProperty]
        private T? val;
        public InitOnly(T item)
        {
            val = item;
        }
        public InitOnly()
        {
        }
        public T Value {
            get {
                if (val is null)
                    throw new InitOnlyException("Struct T is not set.");
                return (T) val;
            }
            set {
                if (val is null)
                    val = value;
                throw new InitOnlyException("Struct T is already set.");
            }
        }
        /// <summary>
        /// Get the value if it is set, or null otherwise
        /// </summary>
        public T? TryGet {
            get => val;
        }
        public bool IsSet {
            get => val != null;
        }
    }
}
