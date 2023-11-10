using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject characterPanel;
    [SerializeField] private Button characterButton;
        
    [SerializeField] private GameObject craftPanel;
    [SerializeField] private Button craftButton;

    private void Start()
    {
        ShowCharacterPanel();
    }

    private void OnEnable()
    {
        characterButton.onClick.AddListener(ShowCharacterPanel);
        craftButton.onClick.AddListener(ShowCraftPanel);
    }
    
    private void OnDisable()
    {
        characterButton.onClick.RemoveAllListeners();
        craftButton.onClick.RemoveAllListeners();
    }
    
    private void ShowCharacterPanel()
    {
        characterButton.interactable = false;
        craftButton.interactable = true;
        characterPanel.SetActive(true);
        craftPanel.SetActive(false);
    }
    
    private void ShowCraftPanel()
    {
        characterButton.interactable = true;
        craftButton.interactable = false;
        characterPanel.SetActive(false);
        craftPanel.SetActive(true);
    }
}
