using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextPanelCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Button;
    [SerializeField] GameObject Stats;
    [SerializeField] GameObject Panel;
    [SerializeField] TextMeshProUGUI TextPanel;
    [SerializeField] Button button;

    public Libros book;

    void Start()
    {
        button.GetComponent<Image>().sprite = book.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (TextPanel.text == book.text && Input.GetMouseButtonDown(0))
        {
            Button.SetActive(true);
            Stats.SetActive(true);
            Panel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void showText()
    {
        Button.SetActive(false);
        Stats.SetActive(false);
        Panel.SetActive(true);
        StartCoroutine(Texting());
        Time.timeScale = 0f;


    }




    IEnumerator Texting()
    {
        TextPanel.text = string.Empty;
        foreach(char ch in book.text)
        {
            TextPanel.text += ch;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }


}
