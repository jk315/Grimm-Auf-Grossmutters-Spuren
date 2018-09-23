using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(BoxCollider)) ,RequireComponent(typeof(Rigidbody))]

public class MazeCreation : MonoBehaviour {
    [System.Serializable]
    public class Cell
    {
        public string name;
        public bool visited;
        public GameObject n; //1
        public GameObject e; //2
        public GameObject w; //3
        public GameObject s; //4

    }

    public GameObject wall;
    public int xSize = 5;
    public int ySize = 5;
    private int toDestroy = 0;
    int alteZelle;
    public float wallLength = 1f;
    private Vector3 initial;
    private ColliderPunkt cp;
    public Cell[] cells;
    private GameObject wallHolder;
    private GameObject[] allWalls;
    private GameObject[] zerstoerteWaende;
    private int zerstoerteWaendeZahl = 0;
    public int currentCell;
    private int visitedCells = 0;
    private int totalCells;
    public GameObject maze, mazeO, mazeU, ball, punkt;
    public int degree = 3;
    private Material m, mB, mP;
    private bool neuGenerieren;

    // Use this for initialization
    void Start ()
    {
        zerstoerteWaende = new GameObject[60];
        m = (Material)Resources.Load("WoodTexture");
        mB = (Material)Resources.Load("GoldTexture");
        mP = (Material)Resources.Load("RedTexture");
        wallHolder = new GameObject();
        maze = new GameObject();
        maze.name = "Komplettes Maze";
        CreateGrid();
        CreateCells();
        CreateMaze();
        PunkteGenerieren();
        wallHolder.transform.parent = maze.transform;
        


        mazeO = GameObject.CreatePrimitive(PrimitiveType.Plane);
        mazeO.transform.localScale = new Vector3(0.5f, 1, 0.5f);
        mazeO.transform.Translate(0.5f, -0.3f, 0);
        mazeO.GetComponent<MeshRenderer>().material = m;
        mazeO.AddComponent<BoxCollider>();
        Collider fc = mazeO.GetComponent<Collider>();
        fc.isTrigger = false;
        mazeO.AddComponent<Rigidbody>();
        Rigidbody rg = mazeO.GetComponent<Rigidbody>();
        rg.useGravity = false;
        rg.isKinematic = true;
       
        mazeO.transform.parent = maze.transform;

        ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ball.AddComponent<Rigidbody>();
        ball.transform.Translate(0, 5, 0);
        ball.transform.localScale = new Vector3(0.38f, 0.38f, 0.38f);
        ball.name = "Ball";
        ball.GetComponent<MeshRenderer>().material = mB;
        ball.AddComponent<ColliderPunkt>();
        cp= ball.GetComponent<ColliderPunkt>();
        neuGenerieren = cp.generateNew;
        Rigidbody brg = ball.GetComponent<Rigidbody>();
        brg.useGravity = false;
        brg.useGravity = true;


        mazeU = GameObject.CreatePrimitive(PrimitiveType.Plane);
        mazeU.transform.Rotate(180, 0, 0);
        mazeU.transform.Translate(0.5f, 1.5f, 0);
        mazeU.AddComponent<CollisionScript>();

       
    }

    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("w"))
        {
            shiftN(degree);
            Debug.Log("w");
        }
        if (Input.GetKeyDown("a"))
        {
            shiftW(degree);
            Debug.Log("a");
        }
        if (Input.GetKeyDown("s"))
        {
            shiftS(degree);
            Debug.Log("s");
        }
        if (Input.GetKeyDown("d"))
        {
            shiftO(degree);
            Debug.Log("d");
        }
        if (Input.GetKeyDown("r"))
        {
            Application.LoadLevel(Application.loadedLevel);
            Debug.Log("r");
        }
        if (Input.GetKeyDown("f"))
        {
            Destroy(punkt);
            PunkteGenerieren();
            Debug.Log("f");
        }

        if (cp.generateNew)
        {
            maze.transform.eulerAngles = new Vector3(0, 0, 0);
            PunkteGenerieren();
            cp.generateNew = false;
            

        }

    }


    void CreateGrid() {
        
        wallHolder.name = "Maze";
        int walls = 0;
        initial = new Vector3(-xSize/2+wallLength/2, 0, -ySize/2 + wallLength/2);
        Vector3 myPos = initial;
        GameObject tempWall;
        //X-Achse
        for(int i =0; i< ySize; i++)
        {
            for (int j=0; j<= xSize; j++)
            {
                myPos = new Vector3(initial.x +(j*wallLength)-wallLength/2, 0, initial.z+(i*wallLength)-wallLength/2);
                tempWall = Instantiate(wall, myPos, Quaternion.identity) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
                tempWall.GetComponent<MeshRenderer>().material = m;
                tempWall.name = "Wand " + walls;
                walls++;

            }
        }

        //Y-Achse
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                myPos = new Vector3(initial.x + (j * wallLength), 0, initial.z + (i * wallLength)-wallLength);
                tempWall = Instantiate(wall, myPos, Quaternion.Euler(0,90,0)) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
                tempWall.GetComponent<MeshRenderer>().material = m;
                tempWall.name = "Wand " + walls;
                walls++;
            }
        }

    }

    void CreateCells()
    {
        totalCells = xSize * ySize;
        int rowcount=1;
        
        int childs = wallHolder.transform.childCount;
        allWalls = new GameObject[childs];
        cells = new Cell[totalCells];
        currentCell = 0;
        int e = 0; //Numerierung der Wände 
        int w = 1;
        int n = 35;
        int s = 30;


        for (int i = 0; i <childs; i++) {
            allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
        }

        for (int process = 0; process<cells.Length; process++) {

            cells[process] = new Cell();
            cells[process].name = "Zelle " + process;
            
            if (rowcount <5 )
            {
                cells[process].e = allWalls[e];
                e++;
            }
            else {
                cells[process].e = allWalls[e];
                e+=2;
            }
            
            cells[process].s = allWalls[s];
            s++;
            if (rowcount < 5)
            {
                cells[process].w = allWalls[w];
                w ++;
                rowcount++;
            }
            else {
                cells[process].w = allWalls[w];
                w += 2;
                rowcount = 0;
            }
            cells[process].n = allWalls[n];
            n++;
            
        }
    }

    int RandomZahl() {
        int rnd = rnd = Random.Range(1, 5);
        return rnd;
    }
    void CreateMaze() {
        int zelle = Random.Range(0, 25);
        while (visitedCells < 26)
        {
            toDestroy = RandomZahl();
            Debug.Log(toDestroy +" " + zelle);


            switch (toDestroy)
            {
                case 1:
                    //______________________________________________WAND ZERSTÖREN_____________________________________
                    Debug.Log("Nördliche Wand"); // Welche Wand?
                    for (int i = 0; i< zerstoerteWaendeZahl; i++) { // Liste zerstörter Wände durchsuchen
                        if(cells[zelle].n.name == zerstoerteWaende[i].name) // Name ausgewählter Wand vergleichen mit Wänden in der Liste
                        {
                            Debug.Log("Schon zerstört, neue Wand"); // switch abbrechehn -> toDestroy neu wählen
                            break;
                        }
                        else{
                            Debug.Log("Wand kann zerstört werden"); // weiter im Code
                        }
                    }
                    zerstoerteWaende[zerstoerteWaendeZahl] = cells[zelle].n; // Zu zerstörtende Wand in Liste zerstörter wände einspeisen
                    zerstoerteWaendeZahl++;
                    Debug.Log("ANZAHL ZERSTÖRTER WÄNDE" + zerstoerteWaendeZahl);//Anzahl zerstörter Wände erhöhen
                    
                    Destroy(cells[zelle].n); // Wand Zerstören
                    Debug.Log(cells[zelle].n + "zerstört"); 


                   //_____________________________NEUE ZELLE SUCHEN_______________________________
                    if (zelle + 5 <= 24) // Schaut ob Nächste Zelle außerhalb des Maze ist
                    {
                        //Schaut ob Zelle schon besucht wurde
                        if (cells[zelle+5].visited == false) // Wenn neue Zelle nicht besucht
                        {
                            cells[zelle].visited = true;
                            visitedCells++; // aktuell Zelle besucht setzen
                            zelle = zelle + 5; // Zelle auf nächste Zelle gesetzt
                            Debug.Log("Nächste Zelle " + zelle); 
                             // Anzahl der Zellen für Abbruchbedingung

                        }
                        else // Wenn nächste Zell besucht
                        {
                            alteZelle = zelle; // letzte Zelle speichern
                            zelle = Random.Range(0, 25); // Zufällige Zelle auswählen
                            while (cells[zelle].visited) // Solange zufällige Zelle besucht
                            {
                                zelle = Random.Range(0, 25); // neue Random Zelle aussuchen
                                Debug.Log("Nächste Zelle " + zelle); //Debug

                            }
                            cells[alteZelle].visited = true; //Zelle als besucht
                            visitedCells++; 

                        }

                        
                        break;
                    }
                    else //nächste Zelle außerhalb vom Maze
                    {
                        alteZelle = zelle; // letzte Zelle speichern
                        zelle = Random.Range(0, 25); // Zufällige Zelle auswählen
                        while (cells[zelle].visited) // Solange zufällige Zelle besucht
                        {
                            zelle = Random.Range(0, 25); // neue Random Zelle aussuchen
                            Debug.Log("Nächste Zelle " + zelle); //Debug

                        }
                        cells[alteZelle].visited = true; //Zelle als besucht
                        visitedCells++;
                        break;
                    }
                   
                case 2:
                    //______________________________________________WAND ZERSTÖREN_____________________________________
                    Debug.Log("Östliche Wand Wand"); // Welche Wand?
                    for (int i = 0; i < zerstoerteWaendeZahl; i++)
                    { // Liste zerstörter Wände durchsuchen
                        if (cells[zelle].e.name == zerstoerteWaende[i].name) // Name ausgewählter Wand vergleichen mit Wänden in der Liste
                        {
                            Debug.Log("Schon zerstört, neue Wand"); // switch abbrechehn -> toDestroy neu wählen
                            break;
                        }
                        else
                        {
                            Debug.Log("Wand kann zerstört werden"); // weiter im Code
                        }
                    }
                    zerstoerteWaende[zerstoerteWaendeZahl] = cells[zelle].e; // Zu zerstörtende Wand in Liste zerstörter wände einspeisen
                    zerstoerteWaendeZahl++;
                    Debug.Log("ANZAHL ZERSTÖRTER WÄNDE" + zerstoerteWaendeZahl);//Anzahl zerstörter Wände erhöhen

                    Destroy(cells[zelle].e); // Wand Zerstören
                    Debug.Log(cells[zelle].e + "zerstört");


                    //_____________________________NEUE ZELLE SUCHEN_______________________________
                    if (zelle + 1 <= 24) // Schaut ob Nächste Zelle außerhalb des Maze ist
                    {
                        //Schaut ob Zelle schon besucht wurde
                        if (cells[zelle + 1].visited == false) // Wenn neue Zelle nicht besucht
                        {
                            cells[zelle].visited = true;
                            visitedCells++; // aktuell Zelle besucht setzen
                            zelle = zelle + 1; // Zelle auf nächste Zelle gesetzt
                            Debug.Log("Nächste Zelle " + zelle);
                             // Anzahl der Zellen für Abbruchbedingung

                        }
                        else // Wenn nächste Zell besucht
                        {
                            alteZelle = zelle; // letzte Zelle speichern
                            zelle = Random.Range(0, 25); // Zufällige Zelle auswählen
                            while (cells[zelle].visited) // Solange zufällige Zelle besucht
                            {
                                zelle = Random.Range(0, 25); // neue Random Zelle aussuchen
                                Debug.Log("Nächste Zelle " + zelle); //Debug

                            }
                            cells[alteZelle].visited = true; //Zelle als besucht
                            visitedCells++;

                        }


                        break;
                    }
                    else //nächste Zelle außerhalb vom Maze
                    {
                        alteZelle = zelle; // letzte Zelle speichern
                        zelle = Random.Range(0, 25); // Zufällige Zelle auswählen
                        while (cells[zelle].visited) // Solange zufällige Zelle besucht
                        {
                            zelle = Random.Range(0, 25); // neue Random Zelle aussuchen
                            Debug.Log("Nächste Zelle " + zelle); //Debug

                        }
                        cells[alteZelle].visited = true; //Zelle als besucht
                        visitedCells++;
                        break;
                    }
                    
                case 3:
                    //______________________________________________WAND ZERSTÖREN_____________________________________
                    Debug.Log("Westliche Wand"); // Welche Wand?
                    for (int i = 0; i < zerstoerteWaendeZahl; i++)
                    { // Liste zerstörter Wände durchsuchen
                        if (cells[zelle].w.name == zerstoerteWaende[i].name) // Name ausgewählter Wand vergleichen mit Wänden in der Liste
                        {
                            Debug.Log("Schon zerstört, neue Wand"); // switch abbrechehn -> toDestroy neu wählen
                            break;
                        }
                        else
                        {
                            Debug.Log("Wand kann zerstört werden"); // weiter im Code
                        }
                    }
                    zerstoerteWaende[zerstoerteWaendeZahl] = cells[zelle].w; // Zu zerstörtende Wand in Liste zerstörter wände einspeisen
                    zerstoerteWaendeZahl++;
                    Debug.Log("ANZAHL ZERSTÖRTER WÄNDE" + zerstoerteWaendeZahl);//Anzahl zerstörter Wände erhöhen

                    Destroy(cells[zelle].w); // Wand Zerstören
                    Debug.Log(cells[zelle].w + "zerstört");


                    //_____________________________NEUE ZELLE SUCHEN_______________________________
                    if (zelle -1 >= 0) // Schaut ob Nächste Zelle außerhalb des Maze ist
                    {
                        //Schaut ob Zelle schon besucht wurde
                        if (cells[zelle -1].visited == false) // Wenn neue Zelle nicht besucht
                        {
                            cells[zelle].visited = true;
                            visitedCells++; // aktuell Zelle besucht setzen
                            zelle = zelle -1; // Zelle auf nächste Zelle gesetzt
                            Debug.Log("Nächste Zelle " + zelle);
                             // Anzahl der Zellen für Abbruchbedingung

                        }
                        else // Wenn nächste Zell besucht
                        {
                            alteZelle = zelle; // letzte Zelle speichern
                            zelle = Random.Range(0, 25); // Zufällige Zelle auswählen
                            while (cells[zelle].visited) // Solange zufällige Zelle besucht
                            {
                                zelle = Random.Range(0, 25); // neue Random Zelle aussuchen
                                Debug.Log("Nächste Zelle " + zelle); //Debug

                            }
                            cells[alteZelle].visited = true; //Zelle als besucht
                            visitedCells++;

                        }


                        break;
                    }
                    else //nächste Zelle außerhalb vom Maze
                    {
                        alteZelle = zelle; // letzte Zelle speichern
                        zelle = Random.Range(0, 25); // Zufällige Zelle auswählen
                        while (cells[zelle].visited) // Solange zufällige Zelle besucht
                        {
                            zelle = Random.Range(0, 25); // neue Random Zelle aussuchen
                            Debug.Log("Nächste Zelle " + zelle); //Debug

                        }
                        cells[alteZelle].visited = true; //Zelle als besucht
                        visitedCells++;
                        break;
                    }
                    
                case 4:
                    //______________________________________________WAND ZERSTÖREN_____________________________________
                    Debug.Log("Südliche Wand"); // Welche Wand?
                    for (int i = 0; i < zerstoerteWaendeZahl; i++)
                    { // Liste zerstörter Wände durchsuchen
                        if (cells[zelle].s.name == zerstoerteWaende[i].name) // Name ausgewählter Wand vergleichen mit Wänden in der Liste
                        {
                            Debug.Log("Schon zerstört, neue Wand"); // switch abbrechehn -> toDestroy neu wählen
                            break;
                        }
                        else
                        {
                            Debug.Log("Wand kann zerstört werden"); // weiter im Code
                        }
                    }
                    zerstoerteWaende[zerstoerteWaendeZahl] = cells[zelle].s; // Zu zerstörtende Wand in Liste zerstörter wände einspeisen
                    zerstoerteWaendeZahl++;
                    Debug.Log("ANZAHL ZERSTÖRTER WÄNDE" + zerstoerteWaendeZahl);//Anzahl zerstörter Wände erhöhen

                    Destroy(cells[zelle].s); // Wand Zerstören
                    Debug.Log(cells[zelle].s + "zerstört");


                    //_____________________________NEUE ZELLE SUCHEN_______________________________
                    if (zelle - 5 >= 0) // Schaut ob Nächste Zelle außerhalb des Maze ist
                    {
                        //Schaut ob Zelle schon besucht wurde
                        if (cells[zelle - 5].visited == false) // Wenn neue Zelle nicht besucht
                        {
                            cells[zelle].visited = true;
                            visitedCells++; // aktuell Zelle besucht setzen
                            zelle = zelle - 5; // Zelle auf nächste Zelle gesetzt
                            Debug.Log("Nächste Zelle " + zelle);
                             // Anzahl der Zellen für Abbruchbedingung

                        }
                        else // Wenn nächste Zell besucht
                        {
                            alteZelle = zelle; // letzte Zelle speichern
                            zelle = Random.Range(0, 25); // Zufällige Zelle auswählen
                            while (cells[zelle].visited) // Solange zufällige Zelle besucht
                            {
                                zelle = Random.Range(0, 25); // neue Random Zelle aussuchen
                                Debug.Log("Nächste Zelle " + zelle); //Debug

                            }
                            cells[alteZelle].visited = true; //Zelle als besucht
                            visitedCells++;

                        }


                        break;
                    }
                    else //nächste Zelle außerhalb vom Maze
                    {
                        alteZelle = zelle; // letzte Zelle speichern
                        zelle = Random.Range(0, 25); // Zufällige Zelle auswählen
                        while (cells[zelle].visited) // Solange zufällige Zelle besucht
                        {
                            zelle = Random.Range(0, 25); // neue Random Zelle aussuchen
                            Debug.Log("Nächste Zelle " + zelle); //Debug

                        }
                        cells[alteZelle].visited = true; //Zelle als besucht
                        visitedCells++;
                        break;
                    }
                                        
            }
        }

        int x = 0;
        int[] wallsToBreak = new int[] {7,8,9,10,13,14,15,16,19,20,21,22,36,37,38,41,42,43,46,47,48,51,52,53};
        int zeile=0;
        int tmpRandom;
        while (x < 10)
        {
            tmpRandom = Random.Range(0, 24);
            Debug.Log("Random ZAHL " + tmpRandom);
            zeile = wallsToBreak[tmpRandom];
            Debug.Log(zeile);

            for (int i = 0; i < zerstoerteWaendeZahl; i++)
            { // Liste zerstörter Wände durchsuchen
                if (allWalls[zeile].name == zerstoerteWaende[i].name) // Name ausgewählter Wand vergleichen mit Wänden in der Liste
                {
                    Debug.Log("Schon zerstört, neue Wand"); // switch abbrechehn -> toDestroy neu wählen
                    zeile = wallsToBreak[Random.Range(0, 24)];
                }
                else
                {
                    Debug.Log("Wand kann zerstört werden"); // weiter im Code
                                                            //Anzahl zerstörter Wände erhöhen


                }
            }

            zerstoerteWaende[zerstoerteWaendeZahl] = allWalls[zeile]; // Zu zerstörtende Wand in Liste zerstörter wände einspeisen
            zerstoerteWaendeZahl++;
            Destroy(allWalls[zeile]); // Wand Zerstören
            Debug.Log(allWalls[zeile] + " zerstört");
            x++;

        }

    }

    void PunkteGenerieren()
    {
        punkt = new GameObject("Punkt");
        punkt = GameObject.CreatePrimitive(PrimitiveType.Cube);

        punkt.transform.position = new Vector3(Random.value * 2.5f, -0.234f, Random.value * 2.5f); 
        punkt.transform.localScale = new Vector3(0.5f, 0.03f, 0.5f);
        punkt.GetComponent<MeshRenderer>().material = mP;
        punkt.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        punkt.AddComponent<BoxCollider>();
        Collider fc = punkt.GetComponent<Collider>();
        fc.isTrigger = true;
        punkt.AddComponent<Rigidbody>();
        Rigidbody rigid = punkt.GetComponent<Rigidbody>();
        rigid.isKinematic = true;
        rigid.useGravity = false;
        punkt.transform.parent = maze.transform;

    }

    void shiftN(int x) {
        maze.transform.Rotate(x, 0, 0);
    }
    void shiftO(int x) {
        maze.transform.Rotate(0, 0, -x);
    }
    void shiftS(int x) {
        maze.transform.Rotate(-x, 0, 0);
    }
    void shiftW(int x) {
        maze.transform.Rotate(0, 0, x);
    }

}