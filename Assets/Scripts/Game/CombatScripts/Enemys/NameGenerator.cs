﻿using System;
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
        
        
        m_Names.Add("Faustina");
        m_Names.Add("Fay");
        m_Names.Add("Fei-Hung");
        m_Names.Add("Felicia");
        m_Names.Add("Fenella");
        m_Names.Add("Ferne");
        m_Names.Add("Ferri");
        m_Names.Add("Fiador");
        m_Names.Add("Fiona");
        m_Names.Add("Fixer");
        m_Names.Add("Flauros");
        m_Names.Add("Flea");
        m_Names.Add("Fleur");
        m_Names.Add("Flora");
        m_Names.Add("Florence");
        m_Names.Add("Fondue");
        m_Names.Add("Foon");
        m_Names.Add("Francine");
        m_Names.Add("Frank");
        m_Names.Add("Freda");
        m_Names.Add("Frederica");
        m_Names.Add("Freya");
        m_Names.Add("Friedrich");
        m_Names.Add("Fubuki");
        m_Names.Add("Fuke");
        m_Names.Add("Fyz");
        m_Names.Add("Gabbot");
        m_Names.Add("Gainer");
        m_Names.Add("Gair");
        m_Names.Add("Gairu");
        m_Names.Add("Galaxy");
        m_Names.Add("Gallant");
        m_Names.Add("Gambit");
        m_Names.Add("Gandalf");
        m_Names.Add("Gansel");
        m_Names.Add("Garcon");
        m_Names.Add("Gareth");
        m_Names.Add("Garnet");
        m_Names.Add("Garon");
        m_Names.Add("Garosh");
        m_Names.Add("Gash");
        m_Names.Add("Gaston");
        m_Names.Add("Gaufres");
        m_Names.Add("Gawein");
        m_Names.Add("Gaws");
        m_Names.Add("Gazette");
        m_Names.Add("Geese");
        m_Names.Add("Geist");
        m_Names.Add("Gemeiner");
        m_Names.Add("General");
        m_Names.Add("George");
        m_Names.Add("Geraint");
        m_Names.Add("Gerhard");
        m_Names.Add("Geronimo");
        m_Names.Add("Gesellschaft");
        m_Names.Add("Gestalt");
        m_Names.Add("Gewalt");
        m_Names.Add("Giko");
        m_Names.Add("Gillian");
        m_Names.Add("Gimlet");
        m_Names.Add("Gin");
        m_Names.Add("Ginger");
        m_Names.Add("Gingham");
        m_Names.Add("Gloria");
        m_Names.Add("Gnocchi");
        m_Names.Add("Goblet");
        m_Names.Add("Goemon");
        m_Names.Add("Goldie");
        m_Names.Add("Grace");
        m_Names.Add("Grammy");
        m_Names.Add("Gray");
        m_Names.Add("Great");
        m_Names.Add("Gremory");
        m_Names.Add("Griffon");
        m_Names.Add("Grune");
        m_Names.Add("Guenever");
        m_Names.Add("Gustav");
        m_Names.Add("Guy");
        m_Names.Add("Haken");
        m_Names.Add("Halver");
        m_Names.Add("Hannah");
        m_Names.Add("Hanzo");
        m_Names.Add("Harlequin");
        m_Names.Add("Harrison");
        m_Names.Add("Harry");
        m_Names.Add("Hartwin");
        m_Names.Add("Haydn");
        m_Names.Add("Hazuki");
        m_Names.Add("Hector");
        m_Names.Add("Hedwig");
        m_Names.Add("Heinrich");
        m_Names.Add("Heinz");
        m_Names.Add("Helen");
        m_Names.Add("Hellicios");
        m_Names.Add("Hermina");
        m_Names.Add("Hickory");
        m_Names.Add("Hilde");
        m_Names.Add("Homeros");
        m_Names.Add("Horchata");
        m_Names.Add("Hrothgar");
        m_Names.Add("Hulloc");
        m_Names.Add("Hun");
        m_Names.Add("Husky");
        m_Names.Add("Idea");
        m_Names.Add("Idola");
        m_Names.Add("Ilumina");
        m_Names.Add("Ingrid");
        m_Names.Add("Ipos");
        m_Names.Add("Ippril");
        m_Names.Add("Iris");
        m_Names.Add("Irma");
        m_Names.Add("Isabel");
        m_Names.Add("Ishtar");
        m_Names.Add("Isolde");
        m_Names.Add("Ivan");
        m_Names.Add("Jaccard");
        m_Names.Add("Jack");
        m_Names.Add("Jackal");
        m_Names.Add("Jam");
        m_Names.Add("James");
        m_Names.Add("Jane");
        m_Names.Add("Jarble");
        m_Names.Add("Jasmine");
        m_Names.Add("Jeanne");
        m_Names.Add("Jessica");
        m_Names.Add("Jesus");
        m_Names.Add("Jet");
        m_Names.Add("Jeyal");
        m_Names.Add("Jill");
        m_Names.Add("Jim");
        m_Names.Add("Joanna");
        m_Names.Add("Joclyn");
        m_Names.Add("Joe");
        m_Names.Add("Johann");
        m_Names.Add("Johanson");
        m_Names.Add("John");
        m_Names.Add("Johnson");
        m_Names.Add("Joker");
        m_Names.Add("Jordan");
        m_Names.Add("Jruu");
        m_Names.Add("Jubei");
        m_Names.Add("Judas");
        m_Names.Add("Judith");
        m_Names.Add("Julie");
        m_Names.Add("Julienne");
        m_Names.Add("Julietta");
        m_Names.Add("Julius");
        m_Names.Add("Justice");
        m_Names.Add("Kain");
        m_Names.Add("Kaiser");
        m_Names.Add("Kanna");
        m_Names.Add("Karen");
        m_Names.Add("Karin");
        m_Names.Add("Karla");
        m_Names.Add("Karrey");
        m_Names.Add("Kary");
        m_Names.Add("Kasekuchen");
        m_Names.Add("Kasuba");
        m_Names.Add("Kasumi");
        m_Names.Add("Kate");
        m_Names.Add("Katerine");
        m_Names.Add("Katrina");
        m_Names.Add("Keeseling");
        m_Names.Add("Keim");
        m_Names.Add("Keith");
        m_Names.Add("Kennel");
        m_Names.Add("Kessler");
        m_Names.Add("Khmer");
        m_Names.Add("Kinkan");
        m_Names.Add("Kit");
        m_Names.Add("Kiwi");
        m_Names.Add("Klara");
        m_Names.Add("Klimina");
        m_Names.Add("Klomn");
        m_Names.Add("Kocher");
        m_Names.Add("Komugi");
        m_Names.Add("Konga");
        m_Names.Add("Konga");
        m_Names.Add("Kosmos");
        m_Names.Add("Krajicek");
        m_Names.Add("Kriemhild");
        m_Names.Add("Kurt");
        m_Names.Add("Kyle");
        m_Names.Add("Kyrielich");
        m_Names.Add("Lachesis");
        m_Names.Add("Laelia");
        m_Names.Add("Lafeene");
        m_Names.Add("Lambda");
        m_Names.Add("Lana");
        m_Names.Add("Lancelot");
        m_Names.Add("Lancia");
        m_Names.Add("Lao");
        m_Names.Add("Lapiz");
        m_Names.Add("Larugo");
        m_Names.Add("Laslo");
        m_Names.Add("Laudigan");
        m_Names.Add("Launceor");
        m_Names.Add("Laura");
        m_Names.Add("Lauren");
        m_Names.Add("Laverna");
        m_Names.Add("Leia");
        m_Names.Add("Leighton");
        m_Names.Add("Lemon");
        m_Names.Add("Lena");
        m_Names.Add("Lenn");
        m_Names.Add("Leon");
        m_Names.Add("Leticia");
        m_Names.Add("Liberty");
        m_Names.Add("Liese");
        m_Names.Add("Ligia");
        m_Names.Add("Lily");
        m_Names.Add("Limbo");
        m_Names.Add("Lime");
        m_Names.Add("Linda");
        m_Names.Add("Linker");
        m_Names.Add("Lionel");
        m_Names.Add("Lisa");
        m_Names.Add("Lita");
        m_Names.Add("Logan");
        m_Names.Add("Lone Wolf");
        m_Names.Add("Lorelei");
        m_Names.Add("Love");
        m_Names.Add("Lowell");
        m_Names.Add("Lucille");
        m_Names.Add("Lucretia");
        m_Names.Add("Ludwig");
        m_Names.Add("Luke");
        m_Names.Add("Luna");
        m_Names.Add("Lundi");
        m_Names.Add("Lunista");
        m_Names.Add("Luphina");
        m_Names.Add("Lutius");
        m_Names.Add("Lynn");
        m_Names.Add("Macky");
        m_Names.Add("Madonna");
        m_Names.Add("Magdalena");
        m_Names.Add("Magenta");
        m_Names.Add("Maggie");
        m_Names.Add("Maggiore");
        m_Names.Add("Magnolia");
        m_Names.Add("Magnus");
        m_Names.Add("Malcolm");
        m_Names.Add("Malthus");
        m_Names.Add("Mao");
        m_Names.Add("Marco");
        m_Names.Add("Margarita");
        m_Names.Add("Mariah");
        m_Names.Add("Mariell");
        m_Names.Add("Marin");
        m_Names.Add("Marissa");
        m_Names.Add("Marius");
        m_Names.Add("Mark");
        m_Names.Add("Marl");
        m_Names.Add("Marlone");
        m_Names.Add("Mars");
        m_Names.Add("Martin");
        m_Names.Add("Mary");
        m_Names.Add("Matilda");
        m_Names.Add("Matthias");
        m_Names.Add("Maximillian");
        m_Names.Add("Maximus");
        m_Names.Add("May");
        m_Names.Add("Maya");
        m_Names.Add("Mayden");
        m_Names.Add("Mazurka");
        m_Names.Add("Medea");
        m_Names.Add("Meena");
        m_Names.Add("Megan");
        m_Names.Add("Meimu");
        m_Names.Add("Meindorf");
        m_Names.Add("Melchior");
        m_Names.Add("Melissa");
        m_Names.Add("Melody");
        m_Names.Add("Memory");
        m_Names.Add("Menuette");
        m_Names.Add("Mephisto");
        m_Names.Add("Mercia");
        m_Names.Add("Merium");
        m_Names.Add("Merle");
        m_Names.Add("Meryl");
        m_Names.Add("Metiee");
        m_Names.Add("Meyer");
        m_Names.Add("Michelle");
        m_Names.Add("Mihail");
        m_Names.Add("Mike");
        m_Names.Add("Milan");
        m_Names.Add("Millia");
        m_Names.Add("Minerva");
        m_Names.Add("Ming-Sia");
        m_Names.Add("Mint");
        m_Names.Add("Mirage");
        m_Names.Add("Misha");
        m_Names.Add("Mocchus");
        m_Names.Add("Mocci");
        m_Names.Add("Modero");
        m_Names.Add("Mohawk");
        m_Names.Add("Moira");
        m_Names.Add("Moloch");
        m_Names.Add("Monar");
        m_Names.Add("Mordred");
        m_Names.Add("Morpheus");
        m_Names.Add("Mort");
        m_Names.Add("Mourvin");
        m_Names.Add("Muireann");
        m_Names.Add("Musashi");
        m_Names.Add("Mustafa");
        m_Names.Add("Muzari");
        m_Names.Add("Myrddin");
        m_Names.Add("Nadia");
        m_Names.Add("Napoleon");
        m_Names.Add("Natalie");
        m_Names.Add("Nebula");
        m_Names.Add("Neidhardt");
        m_Names.Add("Neige");
        m_Names.Add("Neirin");
        m_Names.Add("Nel");
        m_Names.Add("Nero");
        m_Names.Add("Nessler");
        m_Names.Add("Nicholas");
        m_Names.Add("Nirva");
        m_Names.Add("Nitro");
        m_Names.Add("Noelle");
        m_Names.Add("Noin");
        m_Names.Add("Noir");
        m_Names.Add("Non");
        m_Names.Add("Nyah");
        m_Names.Add("Oath");
        m_Names.Add("Oce");
        m_Names.Add("Octavia");
        m_Names.Add("Odessia");
        m_Names.Add("Oidepus");
        m_Names.Add("Olga");
        m_Names.Add("Olias");
        m_Names.Add("Olive");
        m_Names.Add("Olivier");
        m_Names.Add("Olympia");
        m_Names.Add("Omega");
        m_Names.Add("Oracle");
        m_Names.Add("Orivea");
        m_Names.Add("Orson");
        m_Names.Add("Oscar");
        m_Names.Add("Otto");
        m_Names.Add("Ouzo");
        m_Names.Add("Ozmyere");
        m_Names.Add("Palmiro");
        m_Names.Add("Pamela");
        m_Names.Add("Pamille");
        m_Names.Add("Pancho");
        m_Names.Add("Pastel");
        m_Names.Add("Patrick");
        m_Names.Add("Paulman");
        m_Names.Add("Peetan");
        m_Names.Add("Penelope");
        m_Names.Add("Percival");
        m_Names.Add("Perfume");
        m_Names.Add("Perrin");
        m_Names.Add("Peter");
        m_Names.Add("Petite");
        m_Names.Add("Phobos");
        m_Names.Add("Phoenix");
        m_Names.Add("Phyllis");
        m_Names.Add("Pia");
        m_Names.Add("Pierina");
        m_Names.Add("Pizzicato");
        m_Names.Add("Pock");
        m_Names.Add("Poette");
        m_Names.Add("Polly");
        m_Names.Add("Pollyanna");
        m_Names.Add("Pratima");
        m_Names.Add("Praxis");
        m_Names.Add("Primula");
        m_Names.Add("Pris");
        m_Names.Add("Pritny");
        m_Names.Add("Pucelle");
        m_Names.Add("Quess");
        m_Names.Add("Quilt");
        m_Names.Add("Quinine");
        m_Names.Add("Qwerty");
        m_Names.Add("Rachel");
        m_Names.Add("Raelene");
        m_Names.Add("Ralph");
        m_Names.Add("Ran");
        m_Names.Add("Rana");
        m_Names.Add("Rangue");
        m_Names.Add("Rastel");
        m_Names.Add("Ray");
        m_Names.Add("Rayrord");
        m_Names.Add("Razz");
        m_Names.Add("Reckendorf");
        m_Names.Add("Red");
        m_Names.Add("Rem");
        m_Names.Add("Remiel");
        m_Names.Add("Remy");
        m_Names.Add("Rena");
        m_Names.Add("Rhea");
        m_Names.Add("Rhett");
        m_Names.Add("Riali");
        m_Names.Add("Ribbon");
        m_Names.Add("Ricardo");
        m_Names.Add("Rich");
        m_Names.Add("Richard");
        m_Names.Add("Ripley");
        m_Names.Add("Robert");
        m_Names.Add("Robinson");
        m_Names.Add("Robyn");
        m_Names.Add("Rocielle");
        m_Names.Add("Rockwell");
        m_Names.Add("Roderick");
        m_Names.Add("Roger");
        m_Names.Add("Rolan");
        m_Names.Add("Romeo");
        m_Names.Add("Ronica");
        m_Names.Add("Rose");
        m_Names.Add("Rosetta");
        m_Names.Add("Rosewood");
        m_Names.Add("Rouge");
        m_Names.Add("Roxanne");
        m_Names.Add("Rubashka");
        m_Names.Add("Ruger");
        m_Names.Add("Russell");
        m_Names.Add("Ryan");
        m_Names.Add("Sabrina");
        m_Names.Add("Sabrosa");
        m_Names.Add("Sakura");
        m_Names.Add("Saladin");
        m_Names.Add("Salmun");
        m_Names.Add("Salvia");
        m_Names.Add("Samson");
        m_Names.Add("Samuel");
        m_Names.Add("Sarafan");
        m_Names.Add("Sarah");
        m_Names.Add("Saria");
        m_Names.Add("Sarome");
        m_Names.Add("Savarin");
        m_Names.Add("Scarlet");
        m_Names.Add("Scherzo");
        m_Names.Add("Schia");
        m_Names.Add("Scotch");
        m_Names.Add("Sebastian");
        m_Names.Add("Sector");
        m_Names.Add("Seila");
        m_Names.Add("Seimei");
        m_Names.Add("Seito");
        m_Names.Add("Selene");
        m_Names.Add("Sepia");
        m_Names.Add("Shade");
        m_Names.Add("Shadow");
        m_Names.Add("Shamrock");
        m_Names.Add("Sharon");
        m_Names.Add("Shelby");
        m_Names.Add("Sherbert");
        m_Names.Add("Sherry");
        m_Names.Add("Shidoshi");
        m_Names.Add("Shredder");
        m_Names.Add("Si");
        m_Names.Add("Sialon");
        m_Names.Add("Sibyl");
        m_Names.Add("Sigma");
        m_Names.Add("Sigurd");
        m_Names.Add("Sigurd");
        m_Names.Add("Silica");
        m_Names.Add("Silver");
        m_Names.Add("Silvia");
        m_Names.Add("Siniud");
        m_Names.Add("Siren");
        m_Names.Add("Sirius");
        m_Names.Add("Skald");
        m_Names.Add("Skinner");
        m_Names.Add("Sludge");
        m_Names.Add("Snob");
        m_Names.Add("Solaris");
        m_Names.Add("Soleil");
        m_Names.Add("Solis");
        m_Names.Add("Soliton");
        m_Names.Add("Sombart");
        m_Names.Add("Sonata");
        m_Names.Add("Sonette");
        m_Names.Add("Sonic");
        m_Names.Add("Sonic");
        m_Names.Add("Sophia");
        m_Names.Add("Souffle");
        m_Names.Add("Spade");
        m_Names.Add("Spartan");
        m_Names.Add("Spiral");
        m_Names.Add("Staden");
        m_Names.Add("Star");
        m_Names.Add("Steilhang");
        m_Names.Add("Stigma");
        m_Names.Add("Stinger");
        m_Names.Add("Stout");
        m_Names.Add("Strawberry");
        m_Names.Add("Sunday");
        m_Names.Add("Sushi");
        m_Names.Add("Suzanna");
        m_Names.Add("Suzie");
        m_Names.Add("Suzu");
        m_Names.Add("Svana");
        m_Names.Add("Sweet");
        m_Names.Add("Tamarin");
        m_Names.Add("Tamia");
        m_Names.Add("Tamiel");
        m_Names.Add("Tanya");
        m_Names.Add("Tao");
        m_Names.Add("Tarte");
        m_Names.Add("Teresa");
        m_Names.Add("Terrine");
        m_Names.Add("Tesse");
        m_Names.Add("Theo");
        m_Names.Add("Theta");
        m_Names.Add("Thomas");
        m_Names.Add("Tiffany");
        m_Names.Add("Tigre");
        m_Names.Add("Tina");
        m_Names.Add("Tinker");
        m_Names.Add("Titan");
        m_Names.Add("Torquay");
        m_Names.Add("Tranza");
        m_Names.Add("Trianna");
        m_Names.Add("Trinity");
        m_Names.Add("Tristan");
        m_Names.Add("Tristram");
        m_Names.Add("Turbo");
        m_Names.Add("Ulrich");
        m_Names.Add("Undine");
        m_Names.Add("Uranus");
        m_Names.Add("Uriel");
        m_Names.Add("Va");
        m_Names.Add("Valencia");
        m_Names.Add("Valerius");
        m_Names.Add("Valgus");
        m_Names.Add("Vanessa");
        m_Names.Add("Vanessa");
        m_Names.Add("Vasquez");
        m_Names.Add("Vediva");
        m_Names.Add("Vega");
        m_Names.Add("Venus");
        m_Names.Add("Vera");
        m_Names.Add("Veritace");
        m_Names.Add("Verne");
        m_Names.Add("Verrier");
        m_Names.Add("Vicky");
        m_Names.Add("Victoria");
        m_Names.Add("Vilma");
        m_Names.Add("Vine");
        m_Names.Add("Violette");
        m_Names.Add("Vladimir");
        m_Names.Add("Vodka");
        m_Names.Add("Volac");
        m_Names.Add("Waffle");
        m_Names.Add("Walter");
        m_Names.Add("Warren");
        m_Names.Add("Wasabi");
        m_Names.Add("Wendy");
        m_Names.Add("Werner");
        m_Names.Add("Wesker");
        m_Names.Add("Wilhelm");
        m_Names.Add("Willaby");
        m_Names.Add("William");
        m_Names.Add("Willock");
        m_Names.Add("Windy");
        m_Names.Add("Wing");
        m_Names.Add("Winney");
        m_Names.Add("Wolf");
        m_Names.Add("Xabia");
        m_Names.Add("Xandria");
        m_Names.Add("Xe");
        m_Names.Add("Xenon");
        m_Names.Add("Xig");
        m_Names.Add("Yanyan");
        m_Names.Add("Ygerne");
        m_Names.Add("York");
        m_Names.Add("Yuki");
        m_Names.Add("Yvette");
        m_Names.Add("Yvonne");
        m_Names.Add("Z");
        m_Names.Add("Zanac");
        m_Names.Add("Zebra");
        m_Names.Add("Zeeman");
        m_Names.Add("Zeke");
        m_Names.Add("Zenia");
        m_Names.Add("Zeolite");
        m_Names.Add("Zero");
        m_Names.Add("Zeus");
        m_Names.Add("Zeveck");
        m_Names.Add("Zodiac");
 
    }

    public string GetName()
    {
        
        int NameNumber = Random.Range(0, m_Names.Count - 1);

        return m_Names[NameNumber];
    }

}