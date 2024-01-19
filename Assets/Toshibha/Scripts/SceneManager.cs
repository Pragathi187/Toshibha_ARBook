using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Specialized;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    [SerializeField]
    private Button ReloadBtn;
    [SerializeField]
    private Button ScaleBtn;
    [SerializeField]
    private Button RotateBtn;
    [SerializeField]
    private Button MoveBtn;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ReloadBtn.onClick.AddListener(OnReloadBtnClicked);
        ScaleBtn.onClick.AddListener(OnScaleBtnClicked);
        RotateBtn.onClick.AddListener(OnRotateBtnClicked);
        MoveBtn.onClick.AddListener(OnMoveBtnClicked);
    }

    void OnReloadBtnClicked()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    void OnScaleBtnClicked()
    {

    }

    void OnRotateBtnClicked()
    {

    }

    void OnMoveBtnClicked()
    {

    }


    void Update()
    {

    }
}
