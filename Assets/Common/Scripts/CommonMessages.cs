using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CommonMessages : MonoBehaviour {

    public enum MessageType { None, InstructionsStratex, Go, Score, TimeUp }
    public GameObject go;
    public GameObject score;
    public GameObject timeUp;
    public GameObject instructionsStratex;
    public RawImage outputImage;

    Dictionary<MessageType, GameObject> messages = new Dictionary<MessageType, GameObject>();
    Color targetColor;
    Color previousColor;
    float timer;
    float startTime;

    static CommonMessages instance;
    public static CommonMessages Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<CommonMessages>();
            return instance;
        }
    }

    void Awake() {
        messages.Add(MessageType.Go, go);
        messages.Add(MessageType.Score, score);
        messages.Add(MessageType.TimeUp, timeUp);
        messages.Add(MessageType.InstructionsStratex, instructionsStratex);
        outputImage.color = Color.clear;
    }

    void Update() {
        timer -= Time.deltaTime;
        var delta = 1 - timer / startTime;
        outputImage.color = Color.Lerp(previousColor, targetColor, delta);
    }

    public void SetMessage(MessageType messageType) {
        foreach (var message in messages) {
            message.Value.gameObject.SetActive(message.Key == messageType);
        }
    }

    public void Show(float time, Color color) {
        SetTimer(time);
        SetTargetColor(color);
    }

    public void Show(float time) {
        Show(time, Color.white);
    }

    public void Show() {
        Show(0);
    }

    public void Hide(float time, Color color) {
        SetTimer(time);
        SetTargetColor(color);
    }

    public void Hide(float time) {
        Hide(time, Color.clear);
    }

    public void Hide() {
        Hide(0);
    }

    void SetTimer(float timer) {
        this.timer = startTime = timer;
    }

    void SetTargetColor(Color color) {
        previousColor = outputImage.color;
        targetColor = color;
    }
}
