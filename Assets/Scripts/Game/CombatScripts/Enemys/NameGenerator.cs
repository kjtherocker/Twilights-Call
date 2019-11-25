using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class NameGenerator : Singleton<NameGenerator>
{
    private List<string> m_Names; 
    void Awake()
    {
        //Meme names
        m_Names = new List<string>();
        m_Names.Add("Cullen");
        m_Names.Add("Nathaniel");
        m_Names.Add("Stefano");
        m_Names.Add("Timothy");
        m_Names.Add("Jack");
        m_Names.Add("John");
        m_Names.Add("Ambrose");
        m_Names.Add("Almog");
        m_Names.Add("Jesse");
        m_Names.Add("Chris");
        m_Names.Add("James");
        m_Names.Add("Jaimie");

        //Actual Names
        
        
        m_Names.Add("Abigor");
        m_Names.Add("Ace");
        m_Names.Add("Achilles");
        m_Names.Add("Acid");
        m_Names.Add("Adieu");
        m_Names.Add("Adrian");
        m_Names.Add("Aegis");
        m_Names.Add("Agaliarept");
        m_Names.Add("Agares");
        m_Names.Add("Agora");
        m_Names.Add("Agrippa");
        m_Names.Add("Ahya");
        m_Names.Add("Aisha");
        m_Names.Add("Akey");
        m_Names.Add("Al");
        m_Names.Add("Alan");
        m_Names.Add("Albert");
        m_Names.Add("Aldis");
        m_Names.Add("Alex");
        m_Names.Add("Alexander");
        m_Names.Add("Alfred");
        m_Names.Add("Algoreo");
        m_Names.Add("Alice");
        m_Names.Add("Allen");
        m_Names.Add("Alsace");
        m_Names.Add("Alto");
        m_Names.Add("Alucard");
        m_Names.Add("Alumina");
        m_Names.Add("Amdusias");
        m_Names.Add("Amia");
        m_Names.Add("Amnelis");
        m_Names.Add("Amon");
        m_Names.Add("Amulet");
        m_Names.Add("Amy");
        m_Names.Add("Anabell");
        m_Names.Add("Anastasia");
        m_Names.Add("Andras");
        m_Names.Add("Andre");
        m_Names.Add("Baggins");
        m_Names.Add("Baghi");
        m_Names.Add("Bakala");
        m_Names.Add("Balalaika");
        m_Names.Add("Baldr");
        m_Names.Add("Baldwin");
        m_Names.Add("Balin");
        m_Names.Add("Ballad");
        m_Names.Add("Baltis");
        m_Names.Add("Barak");
        m_Names.Add("Barbara");
        m_Names.Add("Barsom");
        m_Names.Add("Beatrice");
        m_Names.Add("Beauchette");
        m_Names.Add("Becky");
        m_Names.Add("Belinus");
        m_Names.Add("Bellatrix");
        m_Names.Add("Belle");
        m_Names.Add("Belphegor");
        m_Names.Add("Benten");
        m_Names.Add("Beowulf");
        m_Names.Add("Bereth");
        m_Names.Add("Bertrand");
        m_Names.Add("Betty");
        m_Names.Add("Bianca");
        m_Names.Add("Bifrons");
        m_Names.Add("Billy");
        m_Names.Add("Birru");
        m_Names.Add("Bismarck");
        m_Names.Add("Blair");
        m_Names.Add("Bob");
        m_Names.Add("Bonaparte");
        m_Names.Add("Bonita");
        m_Names.Add("Boo");
        m_Names.Add("Bossanova");
        m_Names.Add("Bouquet");
        m_Names.Add("Brenda");
        m_Names.Add("Brigit");
        m_Names.Add("Brocken");
        m_Names.Add("Bruce");
        m_Names.Add("Buelle");
        m_Names.Add("Byung");
        m_Names.Add("Cadmus");
        m_Names.Add("Cagliostro");
        m_Names.Add("Cameo");
        m_Names.Add("Cameron");
        m_Names.Add("Canard");
        m_Names.Add("Caradoc");
        m_Names.Add("Carbide");
        m_Names.Add("Carl");
        m_Names.Add("Carlos");
        m_Names.Add("Carlton");
        m_Names.Add("Carmalide");
        m_Names.Add("Carol");
    }

    public string GetName()
    {
        
        int NameNumber = Random.Range(0, m_Names.Count - 1);

        return m_Names[NameNumber];
    }

}
