using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class DoorOpener : MonoBehaviour
{
    private GameObject top;
    private GameObject bottom;
    private Quaternion rotation;
    private bool opened;
    public float openingSize = 2;
    public float openingTime = 1;
    public Text identifierObject;
    public string identifier;
    public AudioSource openingSound;

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
            if (child.name == "top")
            {
                top = child;
            }
            else if (child.name == "bottom")
            {
                bottom = child;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.opened) return;

        CodeGenerationRunner.Instance.AddHiddenCode("open " + this.identifier, delegate (string s)
        {
            this.Open();
            //this.identifierObject.color = Color.green;
            this.identifierObject.gameObject.SetActive(false);
            return true;
        });
        this.identifierObject.gameObject.SetActive(true);
    }

    public void Open()
    {
        if (this.opened) return;

        Vector2 baseLine = new Vector2(0, 1);
        Vector2 direction = (rotation * baseLine).normalized;
        Vector2 topEndPosition = (Vector2)top.transform.position + direction * openingSize;
        Vector2 bottomEndPosition = (Vector2)bottom.transform.position - direction * openingSize;
        StartCoroutine(OpenDoors(topEndPosition, bottomEndPosition, openingTime));
        this.openingSound.Play();
        opened = true;
    }

    private IEnumerator OpenDoors(Vector2 topEndPosition, Vector2 bottomEndPosition, float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            top.transform.position = Vector2.Lerp(top.transform.position, topEndPosition, elapsedTime / time);
            bottom.transform.position = Vector2.Lerp(bottom.transform.position, bottomEndPosition, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
