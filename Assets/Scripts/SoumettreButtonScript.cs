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

    public ScriptBoutonGenerer scriptBoutonGenerer;
   // private bool soumis = false;

    void Start()
    {
        GenererToutesLesCartesEnnemies();
        scriptBoutonGenerer = FindObjectOfType<ScriptBoutonGenerer>();
    } 

    public void GenererCartes() //Est appelé dans Unity
    {
        if (clickCount < ClickOnce)
        { 
            audioSource.Play(); 
            clickCount++;

            foreach (GameObject carteObjet in cartes) //Génère les cartes de l'ennemi  
            { 
                Carte carteAleatoire = toutesLesCartes[Random.Range(0, toutesLesCartes.Length)]; //Random 
                VariantCardsScript scriptCarte = carteObjet.GetComponent<VariantCardsScript>(); //Chercher script carte 
                scriptCarte.AppliquerCarte(carteAleatoire);
            }

            if (clickCount >= ClickOnce)  
            {
                soumettreButton.enabled = false;

                EcranContinuation.gameObject.SetActive(true);
            } 
        }
        scriptBoutonGenerer.VerifierCouleur();
    } 
        void GenererToutesLesCartesEnnemies()
        {
        toutesLesCartes = new Carte[52];

        int index = 0;
        foreach (VariantCardsScript.ValeurCarte valeur in System.Enum.GetValues(typeof(VariantCardsScript.ValeurCarte)))
        {
            foreach (VariantCardsScript.CouleurCarte couleur in System.Enum.GetValues(typeof(VariantCardsScript.CouleurCarte)))
            {
                toutesLesCartes[index] = new Carte(valeur, couleur); 
                index++;
            }
        } 
    }

}
