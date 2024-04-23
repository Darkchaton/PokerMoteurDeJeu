using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static VariantCardsScript;
using Unity.VisualScripting;

public class SoumettreButtonScript : MonoBehaviour 
{
    public GameObject[] cartes; // Tableau d'objets carte à modifier
    public Carte[] toutesLesCartes; // Tableau de toutes les cartes disponibles
    public AudioSource audioSource;
    public Button soumettreButton;

    public Image EcranContinuation;
    public TextMeshProUGUI textePiecesOrGagnees; 
    private int clickCount = 0;
    public int ClickOnce = 1;

    public ScriptBoutonGenerer scriptBoutonGenerer;

    bool JoueurAeuCouleur = false; //
    bool JoueurAeuPaire = false; //
    bool JoueurAeuBrelan = false; //
    bool JoueurAeuDeuxPaires = false; //  
    bool JoueurAeuCarre = false;
    bool JoueurAeuSuite = false;
    bool JoueurAeuFull = false;

    public TextMeshProUGUI textePiecesOr;
    private int piecesOr = 0;
    private int piecesOrGagnees = 0; 

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
         VerifierMemeValeur();  
         VerifierMemeValeurTroisFois();
         VerifierFull();
         VerifierDeuxPaires(); 
         VerifierMemeValeurQuatreFois();
         VerifierSuite();
         VerifierCouleur();

        //Si le joueur a eu une combinaison
        if (JoueurAeuPaire == true)
        {
            piecesOr += 5; 
            textePiecesOr.text = piecesOr.ToString();
            piecesOrGagnees = 5;
            textePiecesOrGagnees.text = piecesOrGagnees.ToString();
        }
        if (JoueurAeuBrelan == true)
        {
            piecesOr += 10; 
            textePiecesOr.text = piecesOr.ToString();
            piecesOrGagnees = 10;
            textePiecesOrGagnees.text = piecesOrGagnees.ToString(); 
        }
        if (JoueurAeuCarre == true)
        {
            piecesOr += 10; //Comme ça 10 plus 5 de JoueurAeuPaire = 15
            textePiecesOr.text = piecesOr.ToString();
            piecesOrGagnees = 10;
            textePiecesOrGagnees.text = piecesOrGagnees.ToString();
        }
        if (JoueurAeuFull == true)
        {
            piecesOr += 20;
            textePiecesOr.text = piecesOr.ToString();
            piecesOrGagnees = 20;
            textePiecesOrGagnees.text = piecesOrGagnees.ToString();
        }
        if (JoueurAeuDeuxPaires == true)
        {
            piecesOr += 15;
            textePiecesOr.text = piecesOr.ToString();
            piecesOrGagnees = 15;
            textePiecesOrGagnees.text = piecesOrGagnees.ToString();
        }
        if (JoueurAeuSuite == true)
        {
            piecesOr += 30;
            textePiecesOr.text = piecesOr.ToString();
            piecesOrGagnees = 30;
            textePiecesOrGagnees.text = piecesOrGagnees.ToString();
        }
        if (JoueurAeuCouleur == true)
        {
            piecesOr += 25;
            textePiecesOr.text = piecesOr.ToString();
            piecesOrGagnees = 25;
            textePiecesOrGagnees.text = piecesOrGagnees.ToString();
        } 
        
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

    // COMBINAISONS
    public void VerifierMemeValeur() //Vérifier si le joueur a eu une paire
    {
        HashSet<string> valeursVues = new HashSet<string>();
        HashSet<string> valeursDupliquees = new HashSet<string>();

        foreach (GameObject carteObjet in scriptBoutonGenerer.cartesJoueur)
        {
            CardsInfo scriptCarte = carteObjet.GetComponent<CardsInfo>();
            SpriteRenderer spriteRenderer = scriptCarte.spriteRenderer;
            Sprite sprite = spriteRenderer.sprite;
            string spriteName = sprite.name;

            string valeur = spriteName.Split('_')[0];

            if (!valeursVues.Add(valeur))
            {
                valeursDupliquees.Add(valeur);
            }
        }

        if (valeursDupliquees.Count >= 1)
        {
            Debug.Log("Le joueur a deux cartes avec la même valeur !"); 
            JoueurAeuPaire = true;
        }
    } 

    public void VerifierMemeValeurTroisFois() //Vérifier si le joueur a eu la même carte 3 fois
    {
        HashSet<string> valeursVues = new HashSet<string>();
        HashSet<string> valeursDupliquees = new HashSet<string>();

        foreach (GameObject carteObjet in scriptBoutonGenerer.cartesJoueur)
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
                    JoueurAeuPaire = false;
                }
            }
        }

        if (valeursDupliquees.Count == 0)
        {
            Debug.Log("Le joueur n'a pas trois cartes avec la même valeur.");
        }
    }

    public void VerifierFull() //Vérifier si le joueur a une paire en plus de 3 cartes identiques
    {
        if (JoueurAeuPaire == true)
        {
            if (JoueurAeuBrelan == true)
            {
                JoueurAeuFull = true;
                Debug.Log("Le joueur a eu un full !");
            }
        }
    }

    public void VerifierDeuxPaires() //Vérifier si le joueur a eu deux paires différentes
    {
        HashSet<string> pairesVues = new HashSet<string>();
        HashSet<string> pairesDupliquees = new HashSet<string>();

        foreach (GameObject carteObjet in scriptBoutonGenerer.cartesJoueur)
        {
            CardsInfo scriptCarte = carteObjet.GetComponent<CardsInfo>();
            SpriteRenderer spriteRenderer = scriptCarte.spriteRenderer;
            Sprite sprite = spriteRenderer.sprite;
            string spriteName = sprite.name;

            string valeur = spriteName.Split('_')[0];

            if (!pairesVues.Add(valeur))
            {
                if (!pairesDupliquees.Contains(valeur))
                {
                    pairesDupliquees.Add(valeur);
                }
            }
        }

        if (pairesDupliquees.Count >= 2)
        {
            Debug.Log("Le joueur a deux paires de cartes différentes !");
            JoueurAeuPaire = false;
            JoueurAeuDeuxPaires = true;
        } 
    }

    public void VerifierMemeValeurQuatreFois() //Vérifier si le joueur a 4 fois la même valeur
    {
        Dictionary<string, int> occurencesValeurs = new Dictionary<string, int>();

        foreach (GameObject carteObjet in scriptBoutonGenerer.cartesJoueur)
        {
            CardsInfo scriptCarte = carteObjet.GetComponent<CardsInfo>();
            SpriteRenderer spriteRenderer = scriptCarte.spriteRenderer;
            Sprite sprite = spriteRenderer.sprite;
            string spriteName = sprite.name;

            string valeur = spriteName.Split('_')[0];

            if (!occurencesValeurs.ContainsKey(valeur))
            {
                occurencesValeurs[valeur] = 1;
            }
            else
            {
                occurencesValeurs[valeur]++;
            }
        }

        foreach (var kvp in occurencesValeurs)
        {
            if (kvp.Value == 4)
            {
                Debug.Log("Le joueur a au moins quatre cartes avec la même valeur : " + kvp.Key);
                JoueurAeuCarre = true;
            }
        }
    }

    public void VerifierSuite() //Vérifier si le joueur a eut 5 cartes qui forment une suite mais sans la même couleur
    {
        Dictionary<string, int> valeurToIndex = new Dictionary<string, int>()
    {
        { "As", 1 },
        { "Deux", 2 },
        { "Trois", 3 },
        { "Quatre", 4 },
        { "Cinq", 5 },
        { "Six", 6 },
        { "Sept", 7 },
        { "Huit", 8 },
        { "Neuf", 9 },
        { "Dix", 10 },
        { "Valet", 11 },
        { "Dame", 12 },
        { "Roi", 13 }
    };

        HashSet<int> valeursVues = new HashSet<int>();

        foreach (GameObject carteObjet in scriptBoutonGenerer.cartesJoueur)
        {
            CardsInfo scriptCarte = carteObjet.GetComponent<CardsInfo>();
            SpriteRenderer spriteRenderer = scriptCarte.spriteRenderer;
            Sprite sprite = spriteRenderer.sprite;
            string spriteName = sprite.name;

            string valeur = spriteName.Split('_')[0];
            if (valeurToIndex.ContainsKey(valeur))
            {
                int index = valeurToIndex[valeur];
                valeursVues.Add(index);
            }
        }

        int consecutiveCount = 0;
        for (int i = 1; i <= 13; i++)
        {
            if (valeursVues.Contains(i))
            {
                consecutiveCount++;
                if (consecutiveCount == 5)
                {
                    Debug.Log("Le joueur a une suite !");
                    JoueurAeuSuite = true;
                    return;
                }
            }
            else
            {
                consecutiveCount = 0;
            }
        } 
    }

    public void VerifierCouleur() //Vérifier si les cartes du joueur sont toutes les mêmes
    {
        int[] countParCouleur = new int[4];

        foreach (GameObject carteObjet in scriptBoutonGenerer.cartesJoueur)
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
                Debug.Log("Le joueur a une couleur !");
                JoueurAeuCouleur = true;
                JoueurAeuDeuxPaires = false; 
                return;
            }
        }
    }

    public void ResetAll()
    { 
        JoueurAeuCouleur = false;
        JoueurAeuPaire = false;
        JoueurAeuBrelan = false;
        JoueurAeuDeuxPaires = false;
        JoueurAeuCarre = false;
        JoueurAeuSuite = false;
        JoueurAeuFull = false; 
        piecesOrGagnees = 0; 
        textePiecesOrGagnees.text = "0";
        soumettreButton.enabled = true;
        scriptBoutonGenerer.generateButton.enabled = true; 
        clickCount = 0;
        scriptBoutonGenerer.clickCount = 0;
    } 

}
