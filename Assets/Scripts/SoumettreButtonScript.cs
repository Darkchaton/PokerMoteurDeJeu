using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static VariantCardsScript;

public class SoumettreButtonScript : MonoBehaviour
{
    public GameObject[] cartes; // Tableau d'objets carte à modifier
    public Carte[] toutesLesCartes; // Tableau de toutes les cartes disponibles
    public AudioSource audioSource;
    public Button soumettreButton;
    public Image EcranContinuation;
    private int clickCount = 0;
    public int ClickOnce = 1;

    void Start()
    {
        GenererToutesLesCartes();
    }

    public void GenererCartes()
    {
        if (clickCount < ClickOnce)
        { 
            audioSource.Play();

            clickCount++;

            foreach (GameObject carteObjet in cartes)
            {
                //Random
                Carte carteAleatoire = toutesLesCartes[Random.Range(0, toutesLesCartes.Length)];

                // Chercher le script de la carte
                VariantCardsScript scriptCarte = carteObjet.GetComponent<VariantCardsScript>();

                // Applique la valeur et la couleur de la carte aléatoire
                scriptCarte.AppliquerCarte(carteAleatoire);
            }

            if (clickCount >= ClickOnce)  
            {
                soumettreButton.enabled = false;

                EcranContinuation.gameObject.SetActive(true);
            }
        }
    }

    void GenererToutesLesCartes()
    {
        toutesLesCartes = new Carte[52];

        int index = 0;
        foreach (VariantCardsScript.ValeurCarte valeur in System.Enum.GetValues(typeof(VariantCardsScript.ValeurCarte)))
        {
            foreach (VariantCardsScript.CouleurCarte couleur in System.Enum.GetValues(typeof(VariantCardsScript.CouleurCarte)))
            {
                toutesLesCartes[index] = new Carte(valeur, couleur);
                Debug.Log(valeur + "," + couleur);
                index++;
            }
        }
    }
}
