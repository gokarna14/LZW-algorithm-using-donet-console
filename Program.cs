using System.Text;

namespace LZW
{
    public class LZW
    {
        public static void DisplayInt(List<int> intList)
        {
            foreach (int num in intList)
            {
                Console.Write(num + " ");
            }
        }
        public static List<int> Compress(string input)
        {
            System.Console.WriteLine($"\nLZW Compressing ...\nString received: {input}");

            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            List<int> output = new List<int>();

            for (int i = 0; i < 256; i++)
            {
                dictionary.Add(((char)i).ToString(), i);
            }

            string currentString = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                char nextChar = input[i];
                string newString = currentString + nextChar;

                if (dictionary.ContainsKey(newString))
                {
                    currentString = newString;
                }
                else
                {
                    output.Add(dictionary[currentString]);
                    dictionary.Add(newString, dictionary.Count);
                    currentString = nextChar.ToString();
                }
            }

            if (!string.IsNullOrEmpty(currentString))
            {
                output.Add(dictionary[currentString]);
            }

            System.Console.Write($"Output : ");
            DisplayInt(output);
            System.Console.WriteLine();

            return output;
        }

        public static string Decompress(List<int> input)
        {
            System.Console.WriteLine("\nLZW Decompressing ... ");
            System.Console.Write("Code Received: ");
            DisplayInt(input);
            System.Console.WriteLine();
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < 256; i++)
            {
                dictionary.Add(i, ((char)i).ToString());
            }

            int previousCode = input[0];
            string previousString = dictionary[previousCode];
            output.Append(previousString);

            for (int i = 1; i < input.Count; i++)
            {
                int currentCode = input[i];
                string currentString;

                if (dictionary.ContainsKey(currentCode))
                {
                    currentString = dictionary[currentCode];
                }
                else
                {
                    currentString = previousString + previousString[0];
                }

                output.Append(currentString);
                dictionary.Add(dictionary.Count, previousString + currentString[0]);
                previousString = currentString;
            }

            System.Console.WriteLine($"Output: {output}\n");

            return output.ToString();
        }
        public static void Main(string[] args)
        {
            System.Console.Write("Input the string: ");
            string InputString = Console.ReadLine() ?? "";
            List<int> CompressedCode =  Compress(InputString);
            string OutputString =  Decompress(CompressedCode);
        }
    }
}