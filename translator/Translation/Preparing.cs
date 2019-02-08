using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translation
{

    // Types of possible data in the number
    enum DigitalType
    {
        Units,
        Decades,
        Hundreds,
        Special,
        Zero,
        Slavonic,
        Start,
        End
    }

    // Number structure
    struct DigitStruct
    {
        public string Name;
        public double Value;
    }

    // Tagged data structure
    struct TagStruct
    {
        public DigitalType Type;
        public string Name;
        public string RawName;
        public double Value;
    }

    public class Preparing
    {
        // Dictionary of associations
        Dictionary<DigitalType, DigitStruct[]> AssocDict = new Dictionary<DigitalType, DigitStruct[]>()
        {
            {   DigitalType.Zero, new DigitStruct[] {
                    new DigitStruct() { Name = "ноль", Value = 0, },
            } },

            {   DigitalType.Units, new DigitStruct[] {
                    new DigitStruct() { Name = "один", Value = 1, },
                    new DigitStruct() { Name = "два", Value = 2, },
                    new DigitStruct() { Name = "три", Value = 3, },
                    new DigitStruct() { Name = "четыре", Value = 4, },
                    new DigitStruct() { Name = "пять", Value = 5, },
                    new DigitStruct() { Name = "шесть", Value = 6, },
                    new DigitStruct() { Name = "семь", Value = 7, },
                    new DigitStruct() { Name = "восемь", Value = 8, },
                    new DigitStruct() { Name = "девять", Value = 9, },
            } },

            {   DigitalType.Special, new DigitStruct[] {
                    new DigitStruct() { Name = "одиннадцать", Value = 11 },
                    new DigitStruct() { Name = "двенадцать", Value = 12 },
                    new DigitStruct() { Name = "тринадцать", Value = 13 },
                    new DigitStruct() { Name = "четырнадцать", Value = 14 },
                    new DigitStruct() { Name = "пятнадцать", Value = 15 },
                    new DigitStruct() { Name = "шестнадцать", Value = 16 },
                    new DigitStruct() { Name = "семнадцать", Value = 17 },
                    new DigitStruct() { Name = "восемнадцать", Value = 18 },
                    new DigitStruct() { Name = "девятнадцать", Value = 19 }
            } },

            {   DigitalType.Decades, new DigitStruct[] {
                    new DigitStruct() { Name = "десять", Value = 10},
                    new DigitStruct() { Name = "двадцать", Value = 20, },
                    new DigitStruct() { Name = "тридцать", Value = 30, },
                    new DigitStruct() { Name = "сорок", Value = 40, },
                    new DigitStruct() { Name = "пятьдесят", Value = 50, },
                    new DigitStruct() { Name = "шестьдесят", Value = 60, },
                    new DigitStruct() { Name = "семьдесят", Value = 70, },
                    new DigitStruct() { Name = "восемьдесят", Value = 80, },
                    new DigitStruct() { Name = "девяносто", Value = 90, }
            } },

            {   DigitalType.Hundreds, new DigitStruct[] {
                    new DigitStruct() { Name = "сто", Value = 100 },
                    new DigitStruct() { Name = "двести", Value = 200 },
                    new DigitStruct() { Name = "триста", Value = 300 },
                    new DigitStruct() { Name = "четыреста", Value = 400 },
                    new DigitStruct() { Name = "пятьсот", Value = 500 },
                    new DigitStruct() { Name = "шестьсот", Value = 600 },
                    new DigitStruct() { Name = "семьсот", Value = 700 },
                    new DigitStruct() { Name = "восемьсот", Value = 800 },
                    new DigitStruct() { Name = "девятьсот", Value = 900 },
            } },
        };

        // Sequence tree
        Dictionary<DigitalType, DigitalType[]> TagSecuence = new Dictionary<DigitalType, DigitalType[]>()
        {
            {   DigitalType.Start, new DigitalType[] {
                    DigitalType.Units,
                    DigitalType.Special,
                    DigitalType.Decades,
                    DigitalType.Zero,
                    DigitalType.Hundreds,
            } },

            {   DigitalType.Units, new DigitalType[] {
                    DigitalType.End,
            } },

            {   DigitalType.Special, new DigitalType[] {
                    DigitalType.End,
            } },

            {   DigitalType.Decades, new DigitalType[] {
                    DigitalType.Units,
                    DigitalType.End,
            } },

            {   DigitalType.Hundreds, new DigitalType[] {
                    DigitalType.Special,
                    DigitalType.Decades,
                    DigitalType.Units,
                    DigitalType.End,
            } },


            {   DigitalType.Zero, new DigitalType[] {
                    DigitalType.End,
            } },

            {   DigitalType.End, new DigitalType[] {

            } },
        };

        DigitStruct[] Slavonic = new DigitStruct[] {
                    new DigitStruct() { Name = "ФЕРТ", Value = 500 },
                    new DigitStruct() { Name = "РЦЫ", Value = 100 },
                    new DigitStruct() { Name = "ЛЮДИ", Value = 30 },
                    new DigitStruct() { Name = "ИЖ", Value = 8 },
                    new DigitStruct() { Name = "ВЕДИ", Value = 2 },
                    new DigitStruct() { Name = "АЗ", Value = 1 },
        };

    // Tagging words
    List<TagStruct> Tagging(string input)
        {
            string[] words = input.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var classification = new TagStruct[words.Length + 2];

            // Add an additional start mark
            classification[0] = new TagStruct()
            {
                Type = DigitalType.Start,
                Name = "[START]",
                Value = 0,
            };

            for (int i = 0; i < words.Length; i++)
            {
                classification[i + 1] = new TagStruct()
                {
                    RawName = words[i],
                };
                // Enumerating all types in the dictionary
                foreach (var key in AssocDict.Keys)
                {
                    // Iterating the array of valid values ​​for each type
                    foreach (var type in AssocDict[key])
                        if (type.Name == words[i])
                        {
                            classification[i + 1] = new TagStruct()
                            {
                                Name = words[i],
                                Value = type.Value,
                                RawName = words[i],
                                Type = key,
                            };
                            goto TONEXTWORD;
                        }
                }
            TONEXTWORD:;
            }

            // Add an extra end mark
            classification[classification.Length - 1] = new TagStruct()
            {
                Type = DigitalType.End,
                Name = "[END]",
                Value = 0,
            };

            // Check whether all words have been classified (without null values)
            for (int i = 0; i < classification.Length; i++)
                if (classification[i].Name == null) throw new Exception($"Word cannot be recognize \"{classification[i].RawName}\" at position {i}");

            return classification.ToList();
        }

        // Checking the correct word sequence for the tree
        void Validation(List<TagStruct> tagged)
        {
            // We go through all the tags and check the correctness of their following each other
            for (int i = 0; i < tagged.Count - 1; i++)
            {
                var now = tagged[i];
                var next = tagged[i + 1];
                // If the array of valid sequences for the current object type does not contain the next possible object type, then ...
                if (!TagSecuence[now.Type].Contains(next.Type))
                {
                    Console.WriteLine(new Exception($"Word {next.Name} with type {next.Type} isn't allowed after {now.Name} with type {now.Type} at the position {i}"));

                }
            }
        }

        // Calculate the number of tags
        double Convert(List<TagStruct> tagged)
        {
            double sum = 0;
            // Go through all the tags and summarize them.
            for (int i = 0; i < tagged.Count - 1; i++)
            {
                var now = tagged[i];
                sum += now.Value;
            }
            return sum;
        }

        string Translation(DigitStruct[] slav, int number)
        {
            var result = "";
            foreach (var s in slav)
            {
                while (number >= s.Value)
                {
                    number -= (int)s.Value;
                    result += s.Name + " ";
                }
            }
            return result;
        }
        // Public Conversion Method
        public double Calculating(string input)
        {
            var tagged = Tagging(input);
            int res;
            Validation(tagged);

            return Convert(tagged);
        }
        public string ToSlav(int num) => Translation(Slavonic, num);
        //public string ToSlav(int num) { return Translation(Slavonic, num); }
    }
}
