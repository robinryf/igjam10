﻿using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CodeGenerationRunner : MonoBehaviour
{
    public static CodeGenerationRunner Instance { get; private set; }

    public bool Debug;

    public bool HaveToSubmit;

    public Text CodeUi;

    public Text CorrectCodeUi;

    public CodeHistory HistoryUi;

    public InputField CmdInputUi;

    public bool NewCode = false;

    private CodeGenerator _codeGenerator = new CodeGenerator();

    private ExceptionGenerator _exceptionGenerator = new ExceptionGenerator();

    public Action<string> CorrectHiddenEvent;

    public Action<string> CorrectEvent;

    public Action<string> WrongEvent;

    public List<string> HiddenCodes;

    private Dictionary<string, Action<string>> _codeMapping = new Dictionary<string, Action<string>>();

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(CmdInputUi.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        EventSystem.current.SetSelectedGameObject(CmdInputUi.gameObject);
        this.UpdateCode();
    }

    public void UpdateCode()
    {
        if (!this.NewCode || this.HistoryUi.IsPrinting()) return;

        this.NewCode = false;

        string code = _codeGenerator.Generate(this.GetRandomDifficulty());
        if (Debug)
        {
            UnityEngine.Debug.Log(code);
        }
        CodeUi.GetComponent<Text>().text = code;
    }

    public void UpdateInput(string text)
    {
        if (this.HistoryUi.IsPrinting()) return;

        if (this.HaveToSubmit)
        {
            if (!this.CodeUi.text.StartsWith(text)) return;

            CorrectCodeUi.text = text;
        }
        else
        {
            if (!this.CodeUi.text.StartsWith(text))
            {
                this.CheckInput();
                return;
            }

            CorrectCodeUi.text = text;

            if (this.CodeUi.text.Equals(text))
            {
                this.CheckInput();
            }
        }
    }

    public void CheckInput()
    {
        if (this.HistoryUi.IsPrinting()) return;

        string text = this.CmdInputUi.text;
        if (Debug)
        {
            UnityEngine.Debug.Log(text);
        }

        if (this.HaveToSubmit)
        {
            foreach (string hiddenCode in this.HiddenCodes)
            {
                if (hiddenCode.Equals(text))
                {
                    this.HistoryUi.PrintSuccess(text);
                    this.HistoryUi.PrintSuccess("    Code Accepted");
                    this.SendCorrectHiddenEvent(text);
                    this.Reset();
                    return;
                }
            }
        }

        if (CodeUi.text.Equals(text))
        {
            this.HistoryUi.PrintSuccess(this.CodeUi.text);
            this.HistoryUi.PrintSuccess("    Code Accepted");
            this.SendCorrectEvent(text);
            this.Reset();
            this.FlagNewCode();
        }
        else
        {
            List<string> outputs = new List<string>();
            outputs.Add(text);

            foreach (string error in this._exceptionGenerator.Generate().Split('\n').ToList())
            {
                outputs.Add(error);
            }
            outputs.Add("    Code Denied");
            this.SendCorrectEvent(text);
            this.HistoryUi.PrintError(outputs.ToArray());
            this.Reset();
            this.FlagNewCode();
        }
    }

    protected void SendCorrectEvent(string code)
    {
        Action<string> reference = null;
        this._codeMapping.TryGetValue(code, out reference);
        if (reference != null)
        {
            reference(code);
        }

        if (CorrectEvent != null)
        {
            CorrectEvent(code);
        }
    }

    protected void SendCorrectHiddenEvent(string code)
    {
        Action<string> reference = null;
        this._codeMapping.TryGetValue(code, out reference);
        if (reference != null)
        {
            reference(code);
        }

        if (CorrectHiddenEvent != null)
        {
            CorrectHiddenEvent(code);
        }
    }

    protected void SendWrongEvent(string code)
    {
        Action<string> reference = null;
        this._codeMapping.TryGetValue(code, out reference);
        if (reference != null)
        {
            reference(code);
        }

        if (WrongEvent != null)
        {
            WrongEvent(code);
        }
    }

    public void AddHiddenCode(string code, Action<string> reference = null)
    {
        this.HiddenCodes.Add(code);

        if (reference == null) return;

        this._codeMapping.Add(code, reference);
    }

    public void RemoveHiddenCode(string code)
    {
        this.HiddenCodes.Remove(code);
        this._codeMapping.Remove(code);
    }

    public void Reset()
    {
        this.CmdInputUi.text = string.Empty;
        this.CorrectCodeUi.text = string.Empty;
        this.CmdInputUi.ActivateInputField();
    }

    public void FlagNewCode()
    {
        this.NewCode = true;
    }

    public CodeGenerator.DifficultyType GetRandomDifficulty()
    {
        CodeGenerator.DifficultyType[] values = (CodeGenerator.DifficultyType[])Enum.GetValues(typeof(CodeGenerator.DifficultyType));
        CodeGenerator.DifficultyType random = (CodeGenerator.DifficultyType)values.GetValue(Random.Range(0, values.Length));
        return random;
    }

}
