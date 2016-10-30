using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class StartScreenInputHandler : MonoBehaviour {

	public EventTrigger.TriggerEvent customCallback;
	private InputField input;

	void Start () 
	{
		input = gameObject.GetComponent<InputField>();
		input.onEndEdit.AddListener(SubmitName);
	}

	void OnLevelWasLoaded(int level)
	{
		EventSystem.current.SetSelectedGameObject(input.gameObject, null);
		input.OnPointerClick (new PointerEventData(EventSystem.current));
	}

	private void SubmitName(string arg0)
	{
		if (arg0.ToLower() == "enter") {
			GoToNextScreen ();
		}
		input.text = "";
	}

	private void GoToNextScreen() {
		BaseEventData eventData= new BaseEventData(EventSystem.current);
		eventData.selectedObject=this.gameObject;
		customCallback.Invoke (eventData);
	}
}
