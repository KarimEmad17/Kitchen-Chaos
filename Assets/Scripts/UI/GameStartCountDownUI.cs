using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{
    private const string numberPopUp = "NumberPopUp";
    [SerializeField] private TextMeshProUGUI text;
    private Animator animator;
    private int previusCountDownNumber;
    // Start is called before the first frame update
    void Start()
    {
        
        KitchenGameManager.Instance.OnStateChange+=KitchenGameManger_OnStateChange;
        Hide();
    }

    private void KitchenGameManger_OnStateChange(object sender, System.EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountDownActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        int countDownNumber = Mathf.CeilToInt(KitchenGameManager.Instance.GetCountDownTimer());
        text.text =countDownNumber.ToString();
        if (previusCountDownNumber!=countDownNumber)
        {
            previusCountDownNumber=countDownNumber;
            animator.SetTrigger(numberPopUp);
            SoundManager.Instance.PlayCountDownSound();

        }
    }
    private void Awake()
    {
        animator=GetComponent<Animator>();
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
