using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static CardsInfo;

public class ScriptBoutonGenerer : MonoBehaviour
{
    public GameObject[] cartesJoueur; // Tableau d'objets carte à modifier
    public Carte[] toutesLesCartes; // Tableau de toutes les cartes disponibles
    public AudioSource audioSource; 
    public Button generateButton;
    public TextMeshProUGUI premierTexte;
    public TextMeshProUGUI deuxiemeTexte;

    public int clickCount = 0;
    public int maxClicks = 3; 
    public HashSet<Carte> cartesSelectionnees = new HashSet<Carte>(); //Éviter les doublons de cartes   

    void Start()
    { 
        GenererToutesLesCartes();
    } 

    public void recommencerClicks()
    {
        clickCount = 0;
        generateButton.interactable = true;
        //premierTexte.gameObject.SetActive(true);
        //deuxiemeTexte.gameObject.SetActive(false);
    }

    public void GenererCartes()
    {
        if (clickCount < maxClicks)
        {
            audioSource.Play();
            clickCount++;

            foreach (GameObject carteObjet in cartesJoueur)
            {
                CardsInfo scriptCarte = carteObjet.GetComponent<CardsInfo>();
                Carte carteActuelle = scriptCarte.carte;

                if (!EstCarteGardee(scriptCarte))
                {
                    if (!cartesSelectionnees.Contains(carteActuelle))
                    {
                        Carte carteAleatoire;
                        do
                        {
                            carteAleatoire = toutesLesCartes[UnityEngine.Random.Range(0, toutesLesCartes.Length)];
                        }
                        while (cartesSelectionnees.Contains(carteAleatoire));

                        scriptCarte.AppliquerCarte(carteAleatoire);
                        Debug.Log("Carte générée : " + carteAleatoire.valeur + ", " + carteAleatoire.couleur);
                        cartesSelectionnees.Add(carteAleatoire);
                    }
                    else
                    {
                        scriptCarte.AppliquerCarte(carteActuelle);
                    }
                }
            } 

            if (clickCount >= maxClicks)
            {
                generateButton.interactable = false; 
                premierTexte.gameObject.SetActive(false);
                deuxiemeTexte.gameObject.SetActive(true); 
            } 
        }
    }

    private bool EstCarteGardee(CardsInfo carte)
    {
        return carte.garderText.gameObject.activeSelf;
    }   

    void GenererToutesLesCartes()
    {
        toutesLesCartes = new Carte[52];

        int index = 0;
        foreach (CardsInfo.ValeurCarte valeur in System.Enum.GetValues(typeof(CardsInfo.ValeurCarte)))
        {
            foreach (CardsInfo.CouleurCarte couleur in System.Enum.GetValues(typeof(CardsInfo.CouleurCarte)))
            {
                toutesLesCartes[index] = new Carte(valeur, couleur);
                index++;
            }
        }
    }

}


