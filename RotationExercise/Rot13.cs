using System;
using System.Collections.Generic;
using System.Text;

namespace Rot13Test
{
    internal class RotationEncryptor
    {
        private readonly int numberOfCharactersToRotate;
        private List<Tuple<char, char>> alphabets;

        public RotationEncryptor()
        {
            SetDefaultAlphabet();
            numberOfCharactersToRotate = 13;
        }

        public RotationEncryptor(int shiftCharactersBy)
        {
            SetDefaultAlphabet();
            numberOfCharactersToRotate = shiftCharactersBy;
        }

        private void SetDefaultAlphabet ()
        {
            alphabets = new List<Tuple<char, char>>();
            alphabets.Add(new Tuple<char, char>  ('A', 'Z'));
            alphabets.Add(new Tuple<char, char>  ('0', '9'));
        }

        internal string Encrypt(string input)
        {
            input = ReplaceCharacters(input);
            StringBuilder result = new StringBuilder();

            foreach (char currentChar in input)
            {
                result.Append(RotateCharacter(currentChar));
            }

            return result.ToString();
        }

        private static string ReplaceCharacters(string input)
        {
            input = input.ToUpper()
                .Replace("Ö", "OE")
                .Replace("Ä", "AE")
                .Replace("Ü", "UE")
                .Replace("ß", "SS");
            return input;
        }

        private char RotateCharacter(int characterToAscii)
        {
            char rotated = (char)characterToAscii;

            int startOfAlphabet = 0;
            int endOfAlphabet = 0;
            int alphabetLength = 0;
            bool isInAlphabet = false;

            foreach(Tuple<char,char> currentAlphabet in alphabets)
            {
                startOfAlphabet = currentAlphabet.Item1;
                endOfAlphabet = currentAlphabet.Item2;
                alphabetLength = (endOfAlphabet - startOfAlphabet) + 1;

                if ((characterToAscii >= startOfAlphabet && characterToAscii <= endOfAlphabet))
                {
                    isInAlphabet = true;
                    break;
                }
            }

            if (!isInAlphabet)
            {
                return rotated;
            }

            int rotatedCharacterPosition = characterToAscii + numberOfCharactersToRotate % alphabetLength;
            if (rotatedCharacterPosition > endOfAlphabet)
            {
                rotated = (char)(rotatedCharacterPosition - alphabetLength);
            }
            else
            {
                rotated = (char)rotatedCharacterPosition;
            }

            return rotated;
        }

       
    }
}