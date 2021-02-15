// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
//
//
// namespace Jmas.Arithmetic
// {
//     public interface IArithmetic<T>: IComparable<T>, IComparable<IArithmetic<T>>
//     {
//         T Value { get; }
//         IArithmetic<T> Add(IArithmetic<T> r);
//         IArithmetic<T> Sub(IArithmetic<T> r);
//         IArithmetic<T> Mul(IArithmetic<T> r);
//         IArithmetic<T> Div(IArithmetic<T> r);
//         //IArithmetic<T> Mod(IArithmetic<T> r);
//
//         // void AddAssign(IArithmetic<T> r);
//         // void SubAssign(IArithmetic<T> r);
//         // void MulAssign(IArithmetic<T> r);
//         // void DivAssign(IArithmetic<T> r);
//         
//         IArithmetic<T> GetMaxValue();
//         IArithmetic<T> GetMinValue();
//     }
//     
//
//     public interface IInteger<T> : IArithmetic<T>
//     {
//         // IArithmetic<T> Increment();
//         // IArithmetic<T> Decrement();
//         IArithmetic<T> LeftShift(int r);
//         IArithmetic<T> RightShift(int r);
//         
//     }
//
//     public interface IMutableInteger<T>
//     {
//         
//     }
//     
//     public class Int: IInteger<int>
//     {
//         protected bool Equals(Int other)
//         {
//             return Value == other.Value;
//         }
//         public override bool Equals(object obj)
//         {
//             if (ReferenceEquals(null, obj)) return false;
//             if (ReferenceEquals(this, obj)) return true;
//             if (obj.GetType() != this.GetType()) return false;
//             return Equals((Int) obj);
//         }
//         public override int GetHashCode()
//         {
//             return Value;
//         }
//         
//         public int Value { get; }
//         public static implicit operator int(Int a)
//         {
//             return a.Value;
//         }
//         public static implicit operator Int(int a)
//         {
//             return new Int(a);
//         }
//         
//         public Int(int value)
//         {
//             Value = value;
//         }
//         
//         public IArithmetic<int> Add(IArithmetic<int> r)
//         {
//             return new Int(Value + r.Value);
//         }
//         public IArithmetic<int> Sub(IArithmetic<int> r)
//         {
//             return new Int(Value - r.Value);
//         }
//         public IArithmetic<int> Mul(IArithmetic<int> r)
//         {
//             return new Int(Value * r.Value);
//         }
//         public IArithmetic<int> Div(IArithmetic<int> r)
//         {
//             return new Int(Value / r.Value);
//         }
//         public IArithmetic<int> GetMaxValue()
//         {
//             return new Int(int.MaxValue);
//         }
//         public IArithmetic<int> GetMinValue()
//         {
//             return new Int(int.MinValue);
//         }
//         public int CompareTo(int other)
//         {
//             return Value - other;
//         }
//         public int CompareTo(IArithmetic<int> other)
//         {
//             return Value - other.Value;
//         }
//         public IArithmetic<int> LeftShift(int r)
//         {
//             return new Int(Value << r);
//         }
//         public IArithmetic<int> RightShift(int r)
//         {
//             return new Int(Value >> r);
//         }
//
//         public static bool operator >(Int a, Int b)
//         {
//             return a.CompareTo(b.Value) > 0;
//         }
//         public static bool operator <(Int a, Int b)
//         {
//             return a.CompareTo(b.Value) < 0;
//         }
//         public static bool operator ==(Int a, Int b)
//         {
//             return a.Value == b.Value;
//         }
//         public static bool operator !=(Int a, Int b)
//         {
//             return a.Value != b.Value;
//         }
//     }
//     
//     public class Float: IArithmetic<float>
//     {
//         protected bool Equals(Float other)
//         {
//             return Value.Equals(other.Value);
//         }
//         public override bool Equals(object obj)
//         {
//             if (ReferenceEquals(null, obj)) return false;
//             if (ReferenceEquals(this, obj)) return true;
//             if (obj.GetType() != this.GetType()) return false;
//             return Equals((Float) obj);
//         }
//         public override int GetHashCode()
//         {
//             return Value.GetHashCode();
//         }
//         public float Value { get; }
//         public static implicit operator float(Float a)
//         {
//             return a.Value;
//         }
//         public static implicit operator Float(float a)
//         {
//             return new Float(a);
//         }
//         
//         public Float(float value)
//         {
//             Value = value;
//         }
//         
//         public IArithmetic<float> Add(IArithmetic<float> r)
//         {
//             return new Float(Value + r.Value);
//         }
//         public IArithmetic<float> Sub(IArithmetic<float> r)
//         {
//             return new Float(Value - r.Value);
//         }
//         public IArithmetic<float> Mul(IArithmetic<float> r)
//         {
//             return new Float(Value * r.Value);
//         }
//         public IArithmetic<float> Div(IArithmetic<float> r)
//         {
//             return new Float(Value / r.Value);
//         }
//         public IArithmetic<float> GetMaxValue()
//         {
//             return new Float(float.MaxValue);
//         }
//         public IArithmetic<float> GetMinValue()
//         {
//             return new Float(float.MinValue);
//         }
//         public int CompareTo(float other)
//         {
//             float a = Value - other;
//             return a > 0 ? 1 : a < 0 ? -1 : 0;
//         }
//         public int CompareTo(IArithmetic<float> other)
//         {
//             float a = Value - other.Value;
//             return a > 0 ? 1 : a < 0 ? -1 : 0;
//         }
//
//         public static bool operator >(Float a, Float b)
//         {
//             return a.CompareTo(b.Value) > 0;
//         }
//         public static bool operator <(Float a, Float b)
//         {
//             return a.CompareTo(b.Value) < 0;
//         }
//         public static bool operator ==(Float a, Float b)
//         {
//             return a.Value == b.Value;
//         }
//         public static bool operator !=(Float a, Float b)
//         {
//             return a.Value != b.Value;
//         }
//     }
// }
