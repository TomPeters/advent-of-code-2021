using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Day8;
using Xunit;

namespace AdventOfCodeTests.Day8;

public class Day8
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(26, Day8Puzzle.NumberOfOutputDigitsThatAreUniquelyIdentifiableBySignalCount(SampleData));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(247, Day8Puzzle.NumberOfOutputDigitsThatAreUniquelyIdentifiableBySignalCount(RealData));
    }

    [Fact]
    public void Part2WorksForSampleData()
    {
        Assert.Equal(61229, Day8Puzzle.SumOfOutputDigits(SampleData));
    }

    [Fact]
    public void Part2WorksForRealData()
    {
        Assert.Equal(933305, Day8Puzzle.SumOfOutputDigits(RealData));
    }

    static IEnumerable<Entry> RealData => GetEntries(FileHelper.ReadFromFile("Day8", "SignalPatterns.txt"));

    static IEnumerable<Entry> SampleData => GetEntries(@"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce");

    static IEnumerable<Entry> GetEntries(string input)
    {
        return input.Split("\n").Select(entryString =>
        {
            var parts = entryString.Split("|");
            var uniqueSignalPatterns = ParseSignalPatternsFromSection(parts[0]);
            var fourSignalOutputValue = ParseSignalPatternsFromSection(parts[1]);
            return new Entry(new UniqueSignalPatterns(uniqueSignalPatterns), new FourDigitOutputValue(fourSignalOutputValue));
        });
    }

    static IEnumerable<SignalPattern> ParseSignalPatternsFromSection(string input)
    {
        return input.Split(" ")
            .Select(sp => sp.Trim())
            .Where(sp => sp != string.Empty)
            .Select(sp => new SignalPattern(sp.Select(sw => Enum.Parse<SignalWire>(sw.ToString()))));
    }
}