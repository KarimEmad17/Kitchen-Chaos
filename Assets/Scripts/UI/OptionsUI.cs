using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button CloseButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button altButton;
    [SerializeField] private Button PauseButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI altText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Transform pressToReBindKeyTransform;
    private Action onCloseButtonAction;

    // Start is called before the first frame update
    void Start()
    {
        UpdateVisual();
        Hide();
        HidePressToRebindKey();
        KitchenGameManager.Instance.OnGameUnPause+=KitchenGameManager_OnGameUnPause;
    }

    private void KitchenGameManager_OnGameUnPause(object sender, EventArgs e)
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        Instance=this;
        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        CloseButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });

        moveUpButton.onClick.AddListener(() =>
        {
            ReBindBinding(GameInput.Binding.Move_Up);
        });
        moveDownButton.onClick.AddListener(() =>
        {
            ReBindBinding(GameInput.Binding.Move_Down);
        });
        moveRightButton.onClick.AddListener(() =>
        {
            ReBindBinding(GameInput.Binding.Move_Right);
        });
        moveLeftButton.onClick.AddListener(() =>
        {
            ReBindBinding(GameInput.Binding.Move_Left);
        });
        PauseButton.onClick.AddListener(() =>
        {
            ReBindBinding(GameInput.Binding.Pause);
        });
        altButton.onClick.AddListener(() =>
        {
            ReBindBinding(GameInput.Binding.InteractAlternate);
        });

    }
    private void UpdateVisual()
    {
        soundEffectText.text = "Sound Effects :" +Mathf.Round(SoundManager.Instance.GetVolume()*10f);
        musicText.text = "Music : " +Mathf.Round(MusicManager.Instance.GetVolume()*10f);
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);     
        altText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction=onCloseButtonAction;
        gameObject.SetActive(true);
        soundEffectButton.Select();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void ShowPressToRebindKey()
    {
        pressToReBindKeyTransform.gameObject.SetActive(true);
    }
    public void HidePressToRebindKey()
    {
        pressToReBindKeyTransform.gameObject.SetActive(false);
    }
    public void ReBindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.ReBindBinding(binding, () => {

            HidePressToRebindKey();
            UpdateVisual();
        });

    }
}
