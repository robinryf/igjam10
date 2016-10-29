using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeGenerator
{

    public enum DifficultyType
    {
        EASY, MEDIUM, HARD
    }

    private string[] commands = new string[] { "hack", "code", "command", "sudo make", "call" };
    private string[] objects = new string[] { "matrix", "game", "world", "humans", "chuck norris", "me" };
    private string[] values = new string[] { "--times 2", "--1337", "42", "a sandwich" };

    public string[] Commands
    {
        get { return commands; }
        set { commands = value; }
    }

    public string[] Objects
    {
        get { return objects; }
        set { objects = value; }
    }

    public string[] Values
    {
        get { return values; }
        set { values = value; }
    }

    public CodeGenerator()
    {
    }

    public CodeGenerator(string[] commands, string[] objects, string[] values)
    {
        Commands = commands;
        Objects = objects;
        Values = values;
    }

    public string Generate(DifficultyType difficulty)
    {
        List<string> strings = new List<string>();
        int nextPick;
        switch (difficulty)
        {
            case DifficultyType.EASY:
                {
                    nextPick = Random.Range(0, Commands.Length);
                    strings.Add(Commands[nextPick]);
                    break;
                }
            case DifficultyType.MEDIUM:
                {
                    nextPick = Random.Range(0, Commands.Length);
                    strings.Add(Commands[nextPick]);
                    nextPick = Random.Range(0, Objects.Length);
                    strings.Add(Objects[nextPick]);
                    break;
                }
            case DifficultyType.HARD:
                nextPick = Random.Range(0, Commands.Length);
                strings.Add(Commands[nextPick]);
                nextPick = Random.Range(0, Objects.Length);
                strings.Add(Objects[nextPick]);
                nextPick = Random.Range(0, Values.Length);
                strings.Add(Values[nextPick]);
                break;
        }
        return string.Join(" ", strings.ToArray());
    }
}
