using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
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

    public Color CorrectColor;

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
        //GameObject newCode = Instantiate(codeUI);
        //newCode.transform.parent = this.gameObject.transform;
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

        if (CodeUi.text.Equals(text))
        {
            this.HistoryUi.AddHistory(new CodeHistory.Output(this.CodeUi.text, this.CorrectColor));
            this.HistoryUi.AddHistory(new CodeHistory.Output("    Code Accepted", this.CorrectColor));
            this.Reset();
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
            this.HistoryUi.AddHistory(outputs.ToArray());
            this.Reset();
        }
        this.CmdInputUi.ActivateInputField();
    }

    public void Reset()
    {
        this.NewCode = true;
        this.CmdInputUi.text = string.Empty;
        this.CorrectCodeUi.text = string.Empty;
    }

}
