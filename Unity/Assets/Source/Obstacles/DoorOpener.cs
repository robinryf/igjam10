using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class DoorOpener : MonoBehaviour
{
    private GameObject left;
    private GameObject right;
    private Quaternion rotation;
    private bool opened;
    public float openingSize = 2;
    public float openingTime = 1;
    public Text identifierObject;
    public string identifier;

    void Awake()
    {
        if (string.IsNullOrEmpty(identifier))
        {
            identifier = Guid.NewGuid().ToString("n").Substring(0, 3);
        }

        this.identifierObject.text = this.identifier;
        this.identifierObject.gameObject.SetActive(false);
    }

    void Start()
    {
        rotation = gameObject.transform.rotation;
        opened = false;
        foreach (Transform childTransform in gameObject.transform)
        {
            GameObject child = childTransform.gameObject;
            if (child.name == "left")
            {
                left = child;
            }
            else if (child.name == "right")
            {
                right = child;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.opened) return;

        CodeGenerationRunner.Instance.AddHiddenCode("open " + this.identifier, delegate (string s)
        {
            this.Open();
            this.identifierObject.color = Color.green;
        });
        this.identifierObject.gameObject.SetActive(true);
    }

    public void Open()
    {
        if (!opened)
        {
            Vector2 baseLine = new Vector2(1, 0);
            Vector2 direction = (rotation * baseLine).normalized;
            Vector2 leftEndPosition = (Vector2)left.transform.position - direction * openingSize;
            Vector2 rightEndPosition = (Vector2)right.transform.position + direction * openingSize;
            StartCoroutine(OpenDoors(leftEndPosition, rightEndPosition, openingTime));
            opened = true;
        }
    }

    private IEnumerator OpenDoors(Vector2 leftEndPosition, Vector2 rightEndPosition, float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            left.transform.position = Vector2.Lerp(left.transform.position, leftEndPosition, elapsedTime / time);
            right.transform.position = Vector2.Lerp(right.transform.position, rightEndPosition, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
