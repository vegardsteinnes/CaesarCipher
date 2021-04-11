using System;
using System.IO;
using static System.Console;

namespace CaesarsCode
{
    class Caesar
    {
        private static int Encrypt(int position, string shift)
        {
            return position + Int32.Parse(shift);
        }

        private static int Decrypt(int position, string shift)
        {
            return position - Int32.Parse(shift);
        }

        private static bool FileOrDirectoryExists(string name)
        {
            return (Directory.Exists(name) || File.Exists(name));
        }
        static void Main(string[] args)
        {
            string fileName = "";
            string newFileName = "";
            string encryptOrDecrypt = "";
            string cipherShift = "";
            char[] cipher = new char[] {' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
            'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'æ', 'ø', 'å', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '\n'};

            WriteLine("Welcome to Vegards Caesar Encryption Tool!");

            for (int i = 0; i < 1;) // Choose file to encrypt/decrypt. Error if file doesn't exist or if it has the wrong file extension.
            {
                Write("Please enter the name of your file: ");
                fileName = ReadLine();
                if ((!FileOrDirectoryExists(fileName)) || (!fileName.EndsWith(".txt"))) WriteLine(@"The file you tried to find doesn't exist or it has the wrong file extension (Must be "".txt""). Try again.");
                if ((FileOrDirectoryExists(fileName)) && (fileName.EndsWith(".txt"))) i++;
            }

            string input = System.IO.File.ReadAllText(fileName);

            for (int i = 0; i < 1;) // Choose amount to shift the cypher by. Error if you don't input a number.
            {
                Write("Enter amount to shift the cipher by: ");
                cipherShift = ReadLine();
                if (!Int32.TryParse(cipherShift, out _)) WriteLine("You didn't input a number. Try again.");
                if (Int32.TryParse(cipherShift, out _)) i++;
            }

            for (int i = 0; i < 1;) // Choose wether to encrypt or decrypt the text file. Error if you input anything else than "E" or "D".
            {
                Write(@"Press ""E"" to encrypt or ""D"" to decrypt: ");
                encryptOrDecrypt = ReadLine();
                if ((encryptOrDecrypt.ToLower() == "e") || (encryptOrDecrypt.ToLower() == "d")) i++;
                if ((encryptOrDecrypt.ToLower() != "e") && (encryptOrDecrypt.ToLower() != "d")) WriteLine(@"You didn't input ""E"" or ""D"". Try again.");
            }

            char[] message = input.ToLower().ToCharArray();
            char[] newMessage = new char[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                char letter = message[i];
                int position = Array.IndexOf(cipher, letter);
                int newPosition = 0;

                if (encryptOrDecrypt.ToLower() == "e") newPosition = (Caesar.Encrypt(position, cipherShift) % cipher.Length);
                if (encryptOrDecrypt.ToLower() == "d") newPosition = (Caesar.Decrypt(position, cipherShift) % cipher.Length);

                if (newPosition < 0) newPosition = (newPosition + cipher.Length);

                char newLetter = cipher[newPosition];
                newMessage[i] = newLetter;
            }

            string encodedString = String.Join("", newMessage);

            for (int i = 0; i < 1;) // Choose name for the new encrypted/decrypted file. Error if filename doesn't end with ".txt".
            {
                if (encryptOrDecrypt.ToLower() == "e") Write("Please enter a filename for your encrypted text: ");
                if (encryptOrDecrypt.ToLower() == "d") Write("Please enter a filename for your decrypted text: ");
                newFileName = ReadLine();
                if (!newFileName.EndsWith(".txt")) WriteLine(@"Incorrect file extension. Should be "".txt"". Try again.");
                if (newFileName.EndsWith(".txt")) i++;
            }

            if (Path.HasExtension(newFileName)) System.IO.File.WriteAllText(newFileName, String.Join("", newMessage));

            if (encryptOrDecrypt.ToLower() == "e") WriteLine("\nHere is your encrypted text:");
            if (encryptOrDecrypt.ToLower() == "d") WriteLine("\nHere is your decrypted text:");

            WriteLine(encodedString + "\n");
            Write("Press any key to close the program.");
            ReadKey();
        }
    }
}