using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaturailUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyPausetext;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyInteractAlternateText;
    // Start is called before the first frame update
    void Start()
    {
        GameInput.Instance.onBindingRebind+=GameInput_onBindingRebind;
        KitchenGameManager.Instance.OnStateChange+=kitchenGameManager_OnStateChange;
        UpdateVisual();
        Show();
    }

    private void kitchenGameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if(KitchenGameManager.Instance.IsCountDownActive())
            Hide();
    }

    private void GameInput_onBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        keyPausetext.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
    }

    private void Show()
    {
        gameObject.SetActive(true);
        
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
