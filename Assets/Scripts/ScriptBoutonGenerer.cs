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

    private int clickCount = 0;
    public int maxClicks = 3; 
    private HashSet<Carte> cartesSelectionnees = new HashSet<Carte>(); //Éviter les doublons de cartes 
    bool JoueurAeuCouleur = false; //
    bool JoueurAeuPaire = false; //
    bool JoueurAeuBrelan = false;
    bool JoueurAeuDeuxPaires = false;
    bool JoueurAeuFlush = false;
    bool JoueurAeuFlushRoyale = false;


    void Start()
    { 
        GenererToutesLesCartes();
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
            //   VerifierMemeValeur();
            VerifierMemeValeurTroisFois();

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

    public void VerifierCouleur()
    {
        int[] countParCouleur = new int[4]; 

        foreach (GameObject carteObjet in cartesJoueur)
        {
            CardsInfo scriptCarte = carteObjet.GetComponent<CardsInfo>();
            SpriteRenderer spriteRenderer = scriptCarte.spriteRenderer;
            Sprite sprite = spriteRenderer.sprite;
            string spriteName = sprite.name;

            CardsInfo.CouleurCarte couleurCarte;
            if (spriteName.Contains("Pique")) couleurCarte = CardsInfo.CouleurCarte.Pique;
            else if (spriteName.Contains("Coeur")) couleurCarte = CardsInfo.CouleurCarte.Coeur;
            else if (spriteName.Contains("Trefle")) couleurCarte = CardsInfo.CouleurCarte.Trefle;
            else couleurCarte = CardsInfo.CouleurCarte.Carreau;

            int indexCouleur = (int)couleurCarte;
            countParCouleur[indexCouleur]++;
        }

        for (int i = 0; i < countParCouleur.Length; i++)
        {
            if (countParCouleur[i] >= 5)
            { 
                Debug.Log("Le joueur a une flush !");
                JoueurAeuCouleur = true;
                return;
            }
        } 
        Debug.Log("Le joueur n'a pas de flush.");
    }

    //void VerifierMemeValeur()
    //{
    //    HashSet<string> valeursVues = new HashSet<string>();
    //    HashSet<string> valeursDupliquees = new HashSet<string>();

    //    foreach (GameObject carteObjet in cartesJoueur)
    //    {
    //        CardsInfo scriptCarte = carteObjet.GetComponent<CardsInfo>();
    //        SpriteRenderer spriteRenderer = scriptCarte.spriteRenderer;
    //        Sprite sprite = spriteRenderer.sprite;
    //        string spriteName = sprite.name;

    //        string valeur = spriteName.Split('_')[0];

    //        if (!valeursVues.Add(valeur))
    //        {
    //            valeursDupliquees.Add(valeur);
    //        }
    //    }

    //    if (valeursDupliquees.Count >= 1)
    //    {
    //        Debug.Log("Le joueur a deux cartes avec la même valeur !");
    //        JoueurAeuPaire = true;
    //    }
    //    else
    //    {
    //        Debug.Log("Le joueur n'a pas deux cartes avec la même valeur.");
    //    }
    //}

    void VerifierMemeValeurTroisFois()
    {
        HashSet<string> valeursVues = new HashSet<string>();
        HashSet<string> valeursDupliquees = new HashSet<string>();

        foreach (GameObject carteObjet in cartesJoueur)
        {
            CardsInfo scriptCarte = carteObjet.GetComponent<CardsInfo>();
            SpriteRenderer spriteRenderer = scriptCarte.spriteRenderer;
            Sprite sprite = spriteRenderer.sprite;
            string spriteName = sprite.name;

            string valeur = spriteName.Split('_')[0];

            if (!valeursVues.Add(valeur))
            {
                if (!valeursDupliquees.Contains(valeur))
                {
                    valeursDupliquees.Add(valeur);
                }
                else
                { 
                    Debug.Log("Le joueur a au moins trois cartes avec la même valeur : " + valeur);
                    JoueurAeuBrelan = true;
                }
            }
        }

        if (valeursDupliquees.Count == 0)
        {
            Debug.Log("Le joueur n'a pas trois cartes avec la même valeur.");
        }
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


