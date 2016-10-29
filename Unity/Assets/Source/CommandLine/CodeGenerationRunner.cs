using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class CodeGenerationRunner : MonoBehaviour
{
    public bool Debug;

    public bool HaveToSubmit;

    public Text CodeUi;

    public Text CorrectCodeUi;

    public CodeHistory HistoryUi;

    public InputField CmdInputUi;

    public bool NewCode;

    private CodeGenerator _codeGenerator;

    private ExceptionGenerator _exceptionGenerator;

    public Color ErrorColor;
    public Action<string, object> CorrectHiddenEvent;

    public Color CorrectColor;
    public Action<string, object> CorrectEvent;

    public Action<string, object> WrongEvent;

    public List<string> HiddenCodes;

    public Dictionary<string, object> CodeMapping;

    // Use this for initialization
    void Start()
    {
        NewCode = false;
        _codeGenerator = new CodeGenerator();
        _exceptionGenerator = new ExceptionGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateCode();
    }

    public void UpdateCode()
    {
        if (!this.NewCode || this.HistoryUi.IsPrinting()) return;

        this.NewCode = false;

        string code = _codeGenerator.Generate(CodeGenerator.DifficultyType.HARD);
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
                    this.HistoryUi.AddHistory(new CodeHistory.Output(text, this.CorrectColor));
                    this.HistoryUi.AddHistory(new CodeHistory.Output("    Code Accepted", this.CorrectColor));
                    this.SendCorrectHiddenEvent();
                    this.Reset();
                    return;
                }
            }
        }

        if (CodeUi.text.Equals(text))
        {
            this.HistoryUi.AddHistory(new CodeHistory.Output(this.CodeUi.text, this.CorrectColor));
            this.HistoryUi.AddHistory(new CodeHistory.Output("    Code Accepted", this.CorrectColor));
            this.SendCorrectEvent();
            this.Reset();
            this.FlagNewCode();
        }
        else
        {
            List<CodeHistory.Output> outputs = new List<CodeHistory.Output>();
            outputs.Add(new CodeHistory.Output(text, this.ErrorColor));

            foreach (string error in this._exceptionGenerator.Generate().Split('\n').ToList())
            {
                outputs.Add(new CodeHistory.Output(error, this.ErrorColor));
            }
            outputs.Add(new CodeHistory.Output("    Code Denied", this.ErrorColor));
            this.SendCorrectEvent();
            this.HistoryUi.AddHistory(outputs.ToArray());
            this.Reset();
            this.FlagNewCode();
        }
    }

    protected void SendCorrectEvent()
    {
        if (CorrectEvent != null)
        {
            object reference = null;
            this.CodeMapping.TryGetValue(this.CodeUi.text, out reference);
            CorrectEvent(this.CodeUi.text, reference);
        }
    }

    protected void SendCorrectHiddenEvent()
    {
        if (CorrectEvent != null)
        {
            object reference = null;
            this.CodeMapping.TryGetValue(this.CodeUi.text, out reference);
            CorrectEvent(this.CodeUi.text, reference);
        }
    }

    protected void SendWrongEvent()
    {
        if (WrongEvent != null)
        {
            object reference = null;
            this.CodeMapping.TryGetValue(this.CodeUi.text, out reference);
            WrongEvent(this.CodeUi.text, reference);
        }
    }

    public void AddHiddenCode(string code, object reference = null)
    {
        this.HiddenCodes.Add(code);

        if (reference == null) return;

        this.CodeMapping.Add(code, reference);
    }

    public void RemoveHiddenCode(string code)
    {
        this.HiddenCodes.Remove(code);
        this.CodeMapping.Remove(code);
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

}
