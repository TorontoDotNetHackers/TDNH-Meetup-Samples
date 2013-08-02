using System;
using System.Linq;
using System.Text;
using System.Diagnostics;

/* Can only be compiled with /unsafe compiler option.  */

namespace UnsafeSamples {

    class Program {

        /// <summary>
        /// Test speed of unmanaged/unsafe string reversal vs managed reversal. 
        /// Also prove direct string manipulation through pointers can corrupt .NET string pooling. 
        /// </summary>
        /// <param name="args"></param>
        static void Main() {

            // Create a large string to test with 
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10000; ++i) // repeat it 10k times. 
                sb.Append("This is a test.");
            string text = sb.ToString();
            Console.WriteLine("Made string of {0:###,###,##0} characters for testing.", text.Length);
            GC.Collect();

            /* ---------------------- 
             * 1. ** Time managed implementation of string reversal (uses LInQ)
             */

            Stopwatch stopWatch = Stopwatch.StartNew();

            // reverse using LINQ into a char array and back to a string 
            string s1 = new string(text.Reverse().ToArray());

            stopWatch.Stop();
            Console.WriteLine("\nManaged string reversed in {0,10:#,###,##0} ticks", stopWatch.ElapsedTicks, text);
            stopWatch = null;
            GC.Collect();


            /* ----------------------
             * 2. ** Time the unsafe (optimized) implementation of string reversal 
             */

            stopWatch = Stopwatch.StartNew();

            // use our unsafe method to modify the string in place 
            UnsafeReverse(text);

            stopWatch.Stop();
            Console.WriteLine("Unsafe string, reversed in {0,10:#,###,##0} ticks", stopWatch.ElapsedTicks, text);


            /* ----------------------
             * 3. ** Prove .NET string pooling can be corrupted by direct string manipulation. 
             */

            Console.WriteLine("\n** Example of corrupting .NET string pooling **");
            string DontTouchThisString = "straight and narrow";
            Console.WriteLine("The immutable string: \"{0}\"", DontTouchThisString);
            string s = "straight and narrow";
            UnsafeReverse(s);
            Console.WriteLine("Same immutable string (untouched by C# source): \"{0}\" << OOPS!", DontTouchThisString);

            // pause
            Console.WriteLine("\nHit Enter to exit... ");
            Console.ReadLine();
        }

        /// <summary>
        /// An unsafe method to reverse a string in place
        /// using pointers. 
        /// </summary>
        /// <param name="str"></param>
        static unsafe void UnsafeReverse(string str) {
            int len = str.Length;

            fixed (char* pStr = str) {
                char* p1 = pStr;
                char* p2 = pStr + len - 1;

                char tmp;
                while (p1 < p2) {
                    tmp = *p1;
                    *p1 = *p2;
                    *p2 = tmp;
                    ++p1; --p2;
                }
            }
        }


    }
}
