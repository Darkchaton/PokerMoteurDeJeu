using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonScript : MonoBehaviour
{
    public SoumettreButtonScript soumettreBoutonGenerer;
    public Image continuationscreen; 
    private Sprite defaultSprite;
    private Sprite defaultSpriteEn;

    public void Awake()
    {
        defaultSprite = Resources.Load<Sprite>("BackCard");
        defaultSpriteEn = Resources.Load<Sprite>("BackCardEn");
    }

    public void ResetCartes()
    {
        soumettreBoutonGenerer.ResetAll(); 
        ResetSprites();
        continuationscreen.gameObject.SetActive(false);
    }

    private void ResetSprites()
    {
        foreach (GameObject carteObjet in soumettreBoutonGenerer.cartes)
        {
            VariantCardsScript scriptCarte = carteObjet.GetComponent<VariantCardsScript>();
            if (scriptCarte != null)
            {
                scriptCarte.spriteRenderer.sprite = defaultSpriteEn;
            }
        }

        foreach (GameObject carteObjet in soumettreBoutonGenerer.scriptBoutonGenerer.cartesJoueur)
        {
            CardsInfo scriptCarte = carteObjet.GetComponent<CardsInfo>();
            if (scriptCarte != null)
            {
                scriptCarte.spriteRenderer.sprite = defaultSprite;
            }
        }
    } 

}
 
