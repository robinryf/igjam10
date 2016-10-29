using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine.UI;

public class CodeHistory : MonoBehaviour
{

    public List<GameObject> History;

    private List<string> ToAdd;

    public GameObject Content;

    public Text InputText;

    public int maxChildCount;

    // Use this for initialization
    void Start()
    {
        this.ToAdd = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ToAdd.Any()) return;

        string input = this.ToAdd[0];
        this.ToAdd.RemoveAt(0);
        //foreach (string input in ToAdd)
        {
            GameObject newHistoryInput;
            if (this.Content.transform.childCount >= this.maxChildCount)
            {
                newHistoryInput = this.Content.transform.GetChild(0).gameObject;
                newHistoryInput.transform.SetAsLastSibling();
            }
            else
            {
                newHistoryInput = Instantiate(this.InputText).gameObject;
            }
            newHistoryInput.GetComponent<Text>().text = input;
            newHistoryInput.transform.parent = this.Content.transform;
            newHistoryInput.transform.localScale = Vector3.one;

        }
    }

    public void AddHistory(string input)
    {
        this.ToAdd.Add(input);
    }

    public void AddHistory(string[] inputs)
    {
        foreach (string input in inputs)
        {
            this.ToAdd.Add(input);
        }
    }
}
