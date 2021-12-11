using System;
using System.Linq;

public class Joc{

    private Jugador j1;
    private Enemigo enemy;
    private int turnos;
    private AngryBird [] ArrayCartas;
    private AngryBird [] mazoJugador;
    private AngryBird [] mazoEnemigo;

    public void Marcador(string jugador1,string nombreMaquina, int contJugador1,int contMaquina){
        Console.WriteLine(jugador1+":"+contJugador1);
        Console.WriteLine(nombreMaquina+":"+contMaquina);
    }

    // pa que quede bonito y limpio el programa
    public void LimpiarPantalla(){
        for (int i=0;i<20;i++){
            Console.WriteLine();
        }
    }

    public void start(){
        string opcion;
        do{
            Console.WriteLine("Escoged número de Angry Birds: 1, 3 o 5");
            opcion= Console.ReadLine();
        }while(opcion!="1" && opcion!="3" && opcion!="5");

        int turnos= int.Parse(opcion);

        Cartas cartas= new Cartas();

        //CREAMOS ANGRY BIRDS
        ArrayCartas = cartas.ConstruirCartas();

        //CREAMOS MAZOS
        mazoJugador = cartas.AsignarCartas(ArrayCartas,turnos);
        mazoEnemigo = cartas.AsignarCartas(ArrayCartas,turnos);

        //CREAMOS LOS JUGADORAZOS
        j1 = new Jugador(mazoJugador);

        j1.PreguntaNombre();

        enemy= new Enemigo(mazoEnemigo);


        // Para contar desde 1 el número de turnos ya que la variable int turnos= num total de turnos de la partida.
        int contTurno=1;
        //ANGRYBIRD ESCOGIDO POR EL JUGADOR
        int AngryEscogido1;

        while (turnos!=0){
            do{
                Console.WriteLine("TURNO "+contTurno);
                AngryEscogido1= j1.EscogePajaro();
            }while(AngryEscogido1==-1);

            int AngryEscogido2=enemy.MaquinaEscoge();

            LimpiarPantalla();

            //BATALLA 
            Console.WriteLine(j1.GetNombre()+" invoca a "+j1.GetCartaMazo(AngryEscogido1).GetNom());
            int energiaJugador=j1.GetCartaMazo(AngryEscogido1).GetEnergia();
            Console.WriteLine("Energia "+j1.GetNombre()+": "+energiaJugador);

            Console.WriteLine("\n"+enemy.GetNombre()+" invoca a "+enemy.GetCartaMazo(AngryEscogido1).GetNom());
            int energiaEnemigo=enemy.GetCartaMazo(AngryEscogido1).GetEnergia();
            Console.WriteLine("Energia "+enemy.GetNombre()+": "+energiaEnemigo);

            //RESULTADO DE LA BATALLA
            if (energiaJugador>energiaEnemigo){
                Console.WriteLine("\nEste turno lo gana "+j1.GetNombre()+"\n");
                j1.SetPuntuacion();
            }else if (energiaJugador<energiaEnemigo)
            {
                Console.WriteLine("\nEste turno lo gana "+enemy.GetNombre()+"\n");
                enemy.SetPuntuacion();
            }else
            {
                Console.WriteLine("\nEmpate! nadie suma punto\n");
            }

            //BORRAMOS ANGRY BIRD ESCOGIDO EN ESTE TURNO.
            j1.SetCartaMazo(AngryEscogido1);
            enemy.SetCartaMazo(AngryEscogido1);

            //CÓMO VAMOS?
            Marcador(j1.GetNombre(),enemy.GetNombre(),j1.GetPuntuacion(),enemy.GetPuntuacion());
            Console.ReadLine();

            LimpiarPantalla();

            turnos--;
            contTurno++;
        }

        // QUIÉN GANÓ?
        Console.WriteLine("\nFin de la partida!");

        if (j1.GetPuntuacion()>enemy.GetPuntuacion()){
            Console.WriteLine("\n"+j1.GetNombre()+" has ganado la partida!");
        }else if (j1.GetPuntuacion()<enemy.GetPuntuacion())
        {
            Console.WriteLine("\n"+enemy.GetNombre()+" ha ganado la partida!");
        }else
        {
            Console.WriteLine("\nEMPATE! :O ");
        }


        
    }
}


public class AngryBird {
    private string nom;
    private string especie;
    private string poder;
    private int energia;


//Constructor
    public AngryBird(string name, string species, string power, int energy ){
        nom = name;
        especie = species ;
        poder = power;
        energia = energy;
    }

    public int GetEnergia(){
        return energia;
    }

    public string GetNom(){
        return nom;
    }
 
}


public class Cartas{
        private string [] nombres = {"Red","Chuck","Jay Jake y Jim","Hal","Bomb","Matilda","Bubbles","Stella","Terence"};

        private string [] especies={"cardenal","canario","azulejo","tucán","cuervo","gallina","gorrión","cacatua Galah","cardenal"};

        private string [] poderes={"none","velocidad","dividirse en tres","efecto bumerang","explotar","lanzar huevo explosivo","hincharse","hacer burbujas","su peso"};

        private int [] energia={2,23,64,45,67,91,13,31,44};

    
    //CONSTRUYE LOS ANGRY BIRDS DE FORMA AUTOMÁTICA COGIENDO LOS ATRIBUTOS DE LA CLASE
    public AngryBird [] ConstruirCartas(){
        AngryBird [] pajarillos= new AngryBird[9];
        for (byte i=0;i<nombres.Length;i++){
            pajarillos[i]=new AngryBird(nombres[i],especies[i],poderes[i],energia[i]);
        }
        return pajarillos;
    }

    //REPARTIR CARTAS DE FORMA ALEATORIA AL JUGADOR USANDO 1 ARRAY DE NÚMEROS ALEATORIOS
    public AngryBird [] AsignarCartas(AngryBird [] pajaros,int turnos){
        Random NumRandom = new Random();
        int [] NumRandoms=new int[turnos];
       
        //COMPROBAR QUE NO SE REPITAN LAS CARTAS EN EL MAZO
        for(int i = 0; i < NumRandoms.Length; i++){
            Boolean existeNumero;
            int NumeroAleatorio;
            do{
                NumeroAleatorio= NumRandom.Next(0,8);
                //AQUÍ COMPRUEBA:
                existeNumero = NumRandoms.Contains(NumeroAleatorio);
            }while(existeNumero);
            NumRandoms[i]=NumeroAleatorio;
        }
        //ARRAY CON LAS CARTAS NO REPETIDAS (SERÁ EL MAZO DEL JUGADOR)
        AngryBird [] pajaritos= new AngryBird[turnos];

        for (int i=0;i<turnos;i++){
            // CADA PAJARO SE INTRODUCE DENTRO DE LA ARRAY GRACIAS A LA OTRA ARRAY DE NÚMEROS ALEATORIOS.
            pajaritos[i]=pajaros[NumRandoms[i]];
        }

        return pajaritos;
    }


}

public class Jugador{
    private string nombre;

    private int puntuacion=0;

    private AngryBird [] Mazo;
    
    // LE PASAMOS EL MAZO CREADO ANTERIORMENTE
    public Jugador(AngryBird [] Mazo){
        this.Mazo=Mazo;
    }

    public void PreguntaNombre(){
        Console.WriteLine("Nombre del jugador:");
        nombre =  Console.ReadLine();
    }

    public int EscogePajaro(){
        //MOSTRAMOS LOS ANGRYBIRDS DEL JUGADOR
        Console.WriteLine("Te toca "+nombre+"\n");
        foreach (AngryBird element in Mazo){
            Console.WriteLine(element.GetNom()+ "    |  energia: " + element.GetEnergia() );
        }

        //QUE ESCOJA POR EL NOMBRE 
        Console.WriteLine("Escoge que Angry Bird quieres usar: ");
        string nom = Console.ReadLine();
        for (int i=0;i<Mazo.Length;i++){
            if (Mazo[i].GetNom()==nom){
                return i;
            }
        }
        return -1;
    }
    public string GetNombre(){
        return nombre;
    }

    public void SetPuntuacion(){
        puntuacion++;
    }

    public int GetPuntuacion(){
        return puntuacion;
    }
    
    public AngryBird GetCartaMazo(int posicion){
        return Mazo[posicion];
    }

    //PARA BORRAR LA CARTA 
    public void SetCartaMazo(int posicion){
        Mazo = Mazo.Where((source, index) =>index != posicion).ToArray();
    }


}


public class Enemigo{
    private string nombreMaquina = "C-3P0";
    private int puntuacion=0;
    AngryBird [] Mazo;
    public Enemigo(AngryBird [] Mazo){
        this.Mazo=Mazo;
    }

    // LA SUPER IA ESCOGE DE FORMA ALEATORIA 
    public int MaquinaEscoge (){
        Random NumRandom = new Random();
        int random=NumRandom.Next(0,Mazo.Length);
        return random;
    }

    public string GetNombre(){
        return nombreMaquina;
    }
    
    public void SetPuntuacion(){
        puntuacion++;
    }

    public int GetPuntuacion(){
        return puntuacion;
    }

    public AngryBird GetCartaMazo(int posicion){
        return Mazo[posicion];
    }

    //BORRAMOS
    public void SetCartaMazo(int posicion){
        Mazo = Mazo.Where((source, index) =>index != posicion).ToArray();
    }
}


class Program {
    static void Main(string[] args){
                
        Joc j = new Joc();
        j.start();
        
    }
    
}
    


  

        

       

        

        

 


