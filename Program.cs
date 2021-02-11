using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
namespace task3
{
    class Program
    {
        static void Main(string[] args)
        {
            int length = args.Length;
            HashSet<string> set = new HashSet<string>(args);

            if (length % 2 != 0 && length >= 3 && set.Count == length)
            {
                byte[] secretkey = new byte[16];
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                Random rnd = new Random();
                int computermove = rnd.Next(0, length - 1);

                rng.GetBytes(secretkey);

                var hmac = new HMACSHA256(secretkey);
                byte[] bstr = Encoding.Default.GetBytes(Convert.ToString(args[computermove]));
                string result = BitConverter.ToString(hmac.ComputeHash(bstr)).Replace("-", string.Empty);

                Console.WriteLine("HMAC: " + result);
                Console.WriteLine("Available moves:");
                for (int i = 0; i < length; i++)
                    Console.WriteLine(i + 1 + " - " + args[i]);
                Console.WriteLine("0 - Exit");
                while (true)
                {

                    Console.Write("Enter your move: ");
                    string move = Console.ReadLine();

                    if (move == "0")
                        Environment.Exit(0);
                    else
                        if (Convert.ToInt32(move) > 0 && Convert.ToInt32(move) <= length)
                    {
                        int humanmove = Convert.ToInt32(move) - 1;
                        Console.WriteLine("Your move: " + args[humanmove]);
                        Console.WriteLine("Computer move: " + args[computermove]);
                        if (humanmove == computermove)
                        {
                            Console.WriteLine("Draw");
                        }
                        else
                        {
                            if (Math.Abs(humanmove - computermove) > length / 2)
                            {
                                if (humanmove < computermove)
                                    Console.WriteLine("You win!");
                                else
                                    Console.WriteLine("Computer win!");
                            }
                            else
                            if (humanmove > computermove)
                                Console.WriteLine("You win!");
                            else
                                Console.WriteLine("Computer win!");
                        }
                        Console.WriteLine("HMAC key: " + BitConverter.ToString(secretkey).Replace("-", string.Empty));
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Wrong move");
                    }
                }
            }
            else
            {
                Console.WriteLine("You entered incorrect data. Example: rock paper scissors lizard spock");
                Environment.Exit(0);
            }
        }
    }
}
