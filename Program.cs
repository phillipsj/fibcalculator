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
   
        [Params(1, 2, 4)] public int Number;

        [GlobalSetup]
        public void Setup() {
            number = Number;
            memo = new Dictionary<int, int>() { {0,0}, {1,1} };
            CachingServices.DefaultBackend = new MemoryCachingBackend();
        }

        [Benchmark]
        public int Calculate() => Calculate(number);
                     
        private int Calculate(int number) {
            if (number < 2) {
                return number;
            }
            return (Calculate(number - 2) + Calculate(number - 1));
        }
        
        [Benchmark]
        public int CalculateWithMemo() => CalculateWithMemo(number);
        
        private int CalculateWithMemo(int number) {
            if(!memo.ContainsKey(number)) {
                memo[number] = (CalculateWithMemo(number - 2)) + (CalculateWithMemo(number - 1));
            }

            return memo[number];          
        }
        
        [Benchmark]
        public int CalculateWithCache() => CalculateWithCache(number);
         
        [Cache]             
        private int CalculateWithCache(int number) {
            if (number < 2) {
                return number;
            }
            return (CalculateWithCache(number - 2) + CalculateWithCache(number - 1));
        }
    }
    
    class Program {
        static void Main(string[] args) {
            var summary = BenchmarkRunner.Run<Fibonacci>();
        }
    }
}
