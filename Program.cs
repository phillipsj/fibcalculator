using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using PostSharp.Patterns.Caching;
using PostSharp.Patterns.Caching.Backends;

namespace fibcalculator {
    public class Fibonacci {
        private int number;
        private Dictionary<int, int> memo;
   
        [Params(10, 20, 30)] public int Number;

        [GlobalSetup]
        public void Setup() {
            number = Number;
            memo = new Dictionary<int, int>() { {0,0}, {1,1} };
            CachingServices.DefaultBackend = new MemoryCachingBackend();
        }

        [Benchmark]
        public int Calculate() => Calculate(number);
        
        [Cache]             
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
