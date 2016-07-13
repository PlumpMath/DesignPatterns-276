using System;

namespace BridgePattern
{
    public interface ISum
    {
        int Add(int x, int y);
    }

    public enum SumType
    {
        SumAfterMultiplyByTwo,
        SumAfterMultiplyByThree
    }

    public class Loader : ISum
    {
        class SumAfterMultipyByTwo : ISum
        {
            public int Add(int x, int y)
            {
                return x * 2 + y * 2;
            }
        }

        class SumAfterMultiplyByThree : ISum
        {
            public int Add(int x, int y)
            {
                return x * 3 + y * 3;
            }
        }

        private readonly ISum _sum;
        public Loader(SumType sumType)
        {
            _sum = GetInstanceOfTypeISum(sumType);
        }

        private ISum GetInstanceOfTypeISum(SumType sumType)
        {
            switch (sumType)
            {
                case SumType.SumAfterMultiplyByTwo:
                    return new SumAfterMultipyByTwo();

                case SumType.SumAfterMultiplyByThree:
                    return new SumAfterMultiplyByThree();

                default:
                    throw new NotSupportedException("sumType not supported");
            }
        }

        public int Add(int x, int y)
        {
            if (_sum != null)
            {
                return _sum.Add(x, y);
            }

            // ReSharper disable once UnthrowableException
            throw new NullReferenceException("Loader not instantiated");
        }
    }

    class BridgePattern
    {
        static void Main(string[] args)
        {
            Loader loader = new Loader(SumType.SumAfterMultiplyByThree);
            var sum = loader.Add(2, 3);
            Console.WriteLine(sum);
        }
    }
}
