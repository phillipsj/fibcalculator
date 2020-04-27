using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace fibcalculator {
    public class Fibonacci {
        private int number;
   
        [Params(1, 2, 4)] public int Number;

        [GlobalSetup]
        public void Setup() {
            number = Number;
        }

        [Benchmark]
        public int Calculate() => Calculate(number);
       
        private int Calculate(int number) {
            if (number < 2) {
                return number;
            }
            return (Calculate(number - 2) + Calculate(number - 1));
        }

    }
    
    class Program {
        static void Main(string[] args) {
            var summary = BenchmarkRunner.Run<Fibonacci>();
        }
    }
}
