using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI Instance { get; private set; }
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;
    // Start is called before the first frame update
    void Start()
    {
        KitchenGameManager.Instance.OnGamePause+=KitchenGameManager_OnGamePause;
        KitchenGameManager.Instance.OnGameUnPause+=KitchenGameManager_OnGameUnPause;
        
        Hide();
    }
    private void Awake()
    {
        Instance = this;
        resumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() => 
        {
            OptionsUI.Instance.Show(Show);
            Hide();
        });
    }

    private void KitchenGameManager_OnGameUnPause(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void KitchenGameManager_OnGamePause(object sender, System.EventArgs e)
    {
        Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Show()
    {
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
