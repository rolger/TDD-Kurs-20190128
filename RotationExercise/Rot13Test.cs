using System;
using Xunit;
using FluentAssertions;

namespace Rot13Test
{
    /**
     * TODO List
     * 
     * 1.) Hällo&, Wörld34 => 
     * 2.) Füße => 
     * 3.) Gährung =>
     * 
     * 1.) C => P
     * 2.) L => Y
     * 3.) W => J
     * 4.) e => R
     * 5.) , => ,
     * 6.) Ö => BR
     * 6a.) OE => BR
     * 7.) ß => FF
     * 8.) Hello, World => URYYB, JBEYQ
     * 10.) n = 2, A => C
     * 11.) n = 2, 1 => 3
     * 12.) n = 2, 9 => 1
     */
    public class Rot13Test
    {
        [Theory]
        [InlineData("C", "P")]
        [InlineData("L", "Y")]
        public void ShouldEncryptSingleUppercaseLetter_InsideAlphabetEnd(string input, string expectedOutput)
        {

            string result = Encrypt(input);

            result.Should().Be(expectedOutput);
        }


        [Fact]
        public void ShouldEncryptSingleUppercaseLetter_WhenExceedingAlphabet()
        {
            Encrypt("W").Should().Be("J");
        }

        [Fact]
        public void ShouldEncryptSingleLowercaseLetter_InsideAlphabetEnd()
        {
            Encrypt("e").Should().Be("R");
        }

        [Fact]
        public void ShouldNotEncryptNonLetters()
        {
            Encrypt(",").Should().Be(",");
        }
        [Theory]
        [InlineData("Ö", "BR")]
        [InlineData("Ä", "NR")]
        [InlineData("ü", "HR")]
        [InlineData("ß", "FF")]

        public void ShouldConvertUmlaute(string input, string expectedOutput)
        {
            Encrypt(input).Should().Be(expectedOutput);
        }

        [Fact]
        public void ShouldEncryptMultiLetters()
        {
            Encrypt("OE").Should().Be("BR");
        }

        [Theory]
        [InlineData("Hello, World", "URYYB, JBEYQ")]
        
        public void ShouldEncryptMultiWords(string input, string expectedOutput)
        {
            Encrypt(input).Should().Be(expectedOutput);
        }

        [Fact]
        public void ShouldShiftLettersByNPlaces()
        {
            Encrypt("A", 2).Should().Be("C");
        }

        [Fact]
        public void ShouldShiftNumbersByNPlaces()
        {
            Encrypt("1", 2).Should().Be("3");
        }

        private string Encrypt(string input)
        {
            return new RotationEncryptor().Encrypt(input);
        }

        private string Encrypt(string input, int shiftCharactersBy)
        {
            return new RotationEncryptor(shiftCharactersBy).Encrypt(input);
        }
    }
}
