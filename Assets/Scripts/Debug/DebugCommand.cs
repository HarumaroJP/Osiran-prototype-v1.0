using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugCommand : MonoBehaviour {

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject CmdLine;
    [SerializeField] private TMP_InputField CmdField;
    private IController player;


    void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<IController>();
    }


    private void Update() {
        if (Keyboard.current[Key.Slash].wasPressedThisFrame) {
            if (CmdLine.activeInHierarchy)
                Close();
            else
                Open();
        }
    }


    public void Open() {
        CmdLine.SetActive(true);
        CmdField.text = String.Empty;
        CmdField.ActivateInputField();
        player.Disable();
    }


    public void Close() {
        CmdLine.SetActive(false);
        player.Enable();
    }


    public void Run(TextMeshProUGUI cmdText) {
        string[] splitArr = cmdText.text.Split(' ');

        // Remove zero width space.
        splitArr = splitArr.Select(str => str.Replace(((char) 8203).ToString(), ""))
                           .ToArray();

        switch (splitArr[0]) {
            case "player":
                if (splitArr.Length != 2) Debug.LogError("引数の数が不正です！");
                switch (splitArr[1]) {
                    case "stop":
                        player.Idle();
                        break;

                    case "start":
                        player.Run();
                        break;

                    default:
                        Debug.LogError("存在しない引数です！");
                        break;
                }

                break;

            case "restart":
                if (splitArr.Length != 1) Debug.LogError("引数の数が不正です！");
                player.Death();
                break;

            default:
                Debug.LogError("不正なコマンドです！");
                break;
        }

        Close();
    }
}
