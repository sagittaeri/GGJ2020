using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup group;
    public GameObject hideOnStart;
    public Image background;
    public Color initialTint;

    public void StartGame()
    {
        group.interactable = false;
        hideOnStart.SetActive(false);
        background.DOColor(initialTint, 1f).OnComplete(() =>
        {
            SceneManager.LoadScene("Prototype", LoadSceneMode.Single);
        });
    }
}
