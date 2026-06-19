using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private Image imageBar;
    private IHasProgress hasProgress;
    // Start is called before the first frame update
    void Start()
    {
        hasProgress = baseCounter.GetComponent<IHasProgress>();
        hasProgress.OnProgressChange+=HasProgress_OnProgressChange;
        Hide();
    }

    private void HasProgress_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e)
    {
        imageBar.fillAmount = e.progressNormalized;
        if (e.progressNormalized==0f||e.progressNormalized==1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
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
