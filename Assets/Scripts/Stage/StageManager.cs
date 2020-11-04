using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

internal interface IStageController {
    float progress { get; }
    float length { get; }

    void Initialize();
    void Play();
    void Pause();
    void Restart();
    void SetTweenSpeed(float speed);
}

public class StageManager : MonoBehaviour, IStageController {

    [SerializeField] private Transform goal;
    [SerializeField] private Transform start;
    private Transform mainCamera;
    public Tween cameraTween;
    [SerializeField] private List<showElementModel> pgrsList_master;
    [SerializeField] private List<showElementModel> pgrsList;
    private float startPos, goalPos, currentPos;

    public float progress => Mathf.InverseLerp(startPos, goalPos, currentPos) * 100;
    public float length => Vector2.Distance(goal.position, start.position);
    private float tweenTime;

    [Serializable]
    private struct showElementModel {
        public GameObject element;
        public float showPoint;


        public showElementModel(showElementModel model) {
            element = model.element;
            showPoint = model.showPoint;
        }
    }


    public void Initialize() {
        InitCamera();
        InitProgressList();
    }


    public void SetTweenSpeed(float speed) {
        tweenTime = Vector2.Distance(start.position, goal.position) / speed;
    }


    private void InitCamera() {
        mainCamera = Camera.main.transform;

        startPos = start.position.x;
        goalPos = goal.position.x;
        mainCamera.position = start.position;
        cameraTween = mainCamera.DOMoveX(goalPos, tweenTime)
                                .SetEase(Ease.Linear)
                                .OnUpdate(UpdateCurrentPosition);
    }


    private void InitProgressList() {
        pgrsList = new List<showElementModel>(pgrsList_master);

        if (pgrsList != null && pgrsList.Count > 0) {
            pgrsList.First().element.SetActive(true);

            for (int i = 1; i < pgrsList.Count; i++) {
                pgrsList[i].element.SetActive(false);
            }

            pgrsList.Sort((a, b) => a.showPoint.CompareTo(b.showPoint));
        }
    }


    public void Pause() {
        cameraTween.Pause();
    }


    public void Play() {
        cameraTween.Play();
    }


    public void Restart() {
        cameraTween.Rewind();
        Initialize();
    }


    private void UpdateCurrentPosition() {
        currentPos = mainCamera.position.x;
        UpdateCurrentUI();
    }


    private void UpdateCurrentUI() {
        if (pgrsList.All(x => x.showPoint > progress)) return;

        pgrsList.First().element.SetActive(false);
        pgrsList.RemoveAt(0);

        //When a player reached the goal.
        if (pgrsList.Count == 0) return;
        pgrsList.First().element.SetActive(true);
    }
}
