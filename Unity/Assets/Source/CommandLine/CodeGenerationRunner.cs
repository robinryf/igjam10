using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

public class CodeGenerationRunner : MonoBehaviour
{
    public Text codeUI;

    public Text correctCodeUI;

    public CodeHistory historyUI;

    public InputField cmdInputUI;

    public bool newCode;

    private CodeGenerator codeGenerator;

    private ExceptionGenerator exceptionGenerator;

	// Use this for initialization
	void Start ()
	{
	    newCode = false;
        codeGenerator = new CodeGenerator();
        exceptionGenerator = new ExceptionGenerator();

        //cmdInputUI.onValueChanged.AddListener(UpdateInput);
        //cmdInputUI.onValueChanged.AddListener(delegate(string text)
        //{

        //});
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (!this.newCode) return;

        this.newCode = false;

	    string code = codeGenerator.Generate(CodeGenerator.DifficultyType.HARD);
	    //GameObject newCode = Instantiate(codeUI);
	    //newCode.transform.parent = this.gameObject.transform;
	    codeUI.GetComponent<Text>().text = code;
	}

    public void UpdateInput(string text)
    {
        if (!codeUI.text.StartsWith(text))
        {
            List<string> error = this.exceptionGenerator.Generate().Split('\n').ToList();
            error.Insert(0, text);
            this.historyUI.AddHistory(error.ToArray());
            this.Reset();
            return;
        }

        correctCodeUI.text = text;

        if (codeUI.text.Equals(text))
        {
            this.historyUI.AddHistory(codeUI.text);
            this.historyUI.AddHistory("    Code Accepted");
            this.Reset();
        }
    }

    public void Reset()
    {
        this.newCode = true;
        this.cmdInputUI.text = String.Empty;
    }
   
}
