using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    public GameObject root;
    public TextMeshProUGUI text;

    public void Show(string message)
    {
        root.SetActive(true);
        text.text = "[E] " + message;
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}
