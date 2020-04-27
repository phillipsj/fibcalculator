using System;

namespace fibcalculator {
    public class Fibonacci {
        private int number;

        public int Calculate() => Calculate(number);
        
        public Fibonacci(int number) {
            this.number = number;
        }

        private int Calculate(int number) {
            if (number < 2) {
                return number;
            }
            return (Calculate(number - 2) + Calculate(number - 1));
        }

    }
    
    class Program {
        static void Main(string[] args) {
            var fib = new Fibonacci(4);
        }
    }
}
