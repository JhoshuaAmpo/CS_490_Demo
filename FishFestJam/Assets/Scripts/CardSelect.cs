using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour
{
    [SerializeField]
    [Range(0,1)]
    private float unselectedCardsColor;

    public Button confirmButton;

    private List<Image> cards;
    private string selectedCard;

    private void Awake()
    {
        cards = new();
        GetComponentsInChildren<Image>(cards);
    }

    private void OnDisable() {
        confirmButton.gameObject.SetActive(false);
    }

    public void ChangeCardRGB(Image card, Color color) {
        card.color = color;
    }

    public void UpgradeSelected(Image upgradeCard) {
        foreach(Image card in cards)
        {
            ChangeCardRGB(card, new Color(unselectedCardsColor,unselectedCardsColor,unselectedCardsColor));
        }
        ChangeCardRGB(upgradeCard, Color.white);
        selectedCard = upgradeCard.name;
        confirmButton.gameObject.SetActive(true);
    }

    public void ConfirmUpgrade() {
        PlayerHandler.Instance.GetComponent<PlayerUpgradeHandler>().ProcessUpgrade(selectedCard);
    }
}
