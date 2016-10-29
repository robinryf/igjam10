using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class CodeHistory : MonoBehaviour
{

    public List<GameObject> History;

    private List<Output> ToAdd;

    public GameObject Content;

    public Text InputText;

    public int MaxChildCount;

    // Use this for initialization
    void Start()
    {
        this.ToAdd = new List<Output>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ToAdd.Any()) return;

        Output output = this.ToAdd[0];
        this.ToAdd.RemoveAt(0);
        //foreach (string input in ToAdd)
        {
            GameObject newHistoryInput;
            if (this.Content.transform.childCount >= this.MaxChildCount)
            {
                newHistoryInput = this.Content.transform.GetChild(0).gameObject;
                newHistoryInput.transform.SetAsLastSibling();
            }
            else
            {
                newHistoryInput = Instantiate(this.InputText).gameObject;
            }
            output.ConfigureTextObject(newHistoryInput.GetComponent<Text>());
            newHistoryInput.transform.SetParent(this.Content.transform, false);

        }
    }

    public void AddHistory(Output output)
    {
        this.ToAdd.Add(output);
    }

    public void AddHistory(Output[] outputs)
    {
        foreach (Output output in outputs)
        {
            this.ToAdd.Add(output);
        }
    }

    public class Output
    {
        public string Text;

        public Color Color;

        public Output(string text, Color color)
        {
            this.Text = text;
            this.Color = color;
        }

        public void ConfigureTextObject(Text textObject)
        {
            textObject.text = this.Text;
            textObject.color = this.Color;
        }
    }

    public bool IsPrinting()
    {
        return this.ToAdd.Any();
    }
}
