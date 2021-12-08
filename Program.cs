using System;
using System.Linq;

public class AngryBird {
	public string nom;
	public string especie;
	public string poder;
	public int energia;

//Constructor
    public AngryBird(string name, string species, string power, int energy ){
        nom = name;
        especie = species ;
        poder = power;
        energia = energy;
    }

    public void MostarEnergia(){
        Console.WriteLine(energia);
    }
}

class Program    {
    static AngryBird [] ConstruirPajaros(string [] nombres,string [] especies,string [] poderes,int [] energia,int turnos){
        AngryBird [] pajarillos= new AngryBird[9];
        for (byte i=0;i<nombres.Length;i++){
            pajarillos[i]=new AngryBird(nombres[i],especies[i],poderes[i],energia[i]);
        }
        return pajarillos;
    }
    static AngryBird [] AsignarPajaros(AngryBird [] pajaros,int turnos){
            Random NumRandom = new Random();
            int [] NumRandoms=new int[turnos];

            for (int i=0;i<turnos;i++){
                NumRandoms[i]=NumRandom.Next(0,8);
            }

            AngryBird [] pajaritos= new AngryBird[turnos];

            for (int i=0;i<turnos;i++){
                pajaritos[i]=pajaros[NumRandoms[i]];
            }

            return pajaritos;
    }
    static int EscogePajaro(AngryBird [] jugador,string nombreJugador){
        Console.WriteLine("\nTe toca "+nombreJugador+"\n");
        foreach (AngryBird element in jugador){
                Console.WriteLine(element.nom + "    |  energia: " + element.energia);
        }
        Console.WriteLine("Escoge que Angry Bird quieres usar: ");
        string nom = Console.ReadLine();
        for (int i=0;i<jugador.Length;i++){
            if (jugador[i].nom==nom){
                return i;
            }
        }
        return -1;
    }

    static void BorrarPajaro(AngryBird [] jugador,int pajaroEscogido){
        jugador[pajaroEscogido].nom="";
    }
    static int MaquinaEscoge (AngryBird [] jugador){
        Random NumRandom = new Random();
        int random=NumRandom.Next(0,jugador.Length);
        return random;
    }

    static void Marcador(string jugador1,string jugador2, int contJugador1,int contJugador2){
        Console.WriteLine(jugador1+":"+contJugador1);
        Console.WriteLine(jugador2+":"+contJugador2);
    }

    static void LimpiarPantalla(){
        for (int i=0;i<20;i++){
            Console.WriteLine();
        }
    }
    static void Main(string[] args)
    {
        
        string [] pajaros={"rojo","amarillo","azul","verde","negro","blanco","naranja","rosa","rojo"};
        string [] nombres={"Red","Chuck","Jay Jake y Jim","Hal","Bomb","Matilda","Bubbles","Stella","Terence"};
        string [] especies={"cardenal","canario","azulejo","tucán","cuervo","gallina","gorrión","cacatua Galah","cardenal"};
        string [] poderes={"none","velocidad","dividirse en tres","efecto bumerang","explotar","lanzar huevo explosivo","hincharse","hacer burbujas","su peso"};
        int [] energia={2,23,64,45,67,91,13,31,44};

        string opcion;

        do{
            Console.WriteLine("Escoged número de Angry Birds: 1, 3 o 5");
            opcion= Console.ReadLine();
        }while(opcion!="1" && opcion!="3" && opcion!="5");

        Console.WriteLine("Nombre del jugador:");
        string nombre1 =  Console.ReadLine();

        string nombreMaquina = "Roberto";

        int turnos= int.Parse(opcion);

        AngryBird [] pajarillos=ConstruirPajaros(nombres,especies,poderes,energia,turnos);

        AngryBird [] jugador1=AsignarPajaros(pajarillos,turnos);
        AngryBird [] jugador2=AsignarPajaros(pajarillos,turnos);

        int contJug1=0;
        int contJug2=0;

        int AngryEscogido1;

        while (turnos!=0){
            do{
                AngryEscogido1=EscogePajaro(jugador1,nombre1);
            }while(AngryEscogido1==-1);

            int AngryEscogido2=MaquinaEscoge(jugador2);

            Console.WriteLine("\nEnergia "+nombre1+":");
            jugador1[AngryEscogido1].MostarEnergia();

            Console.WriteLine("\nEnergia "+nombreMaquina+":");
            jugador2[AngryEscogido2].MostarEnergia();

            if (jugador1[AngryEscogido1].energia>jugador2[AngryEscogido2].energia){
                Console.WriteLine("\nGana "+nombre1+"\n");
                contJug1++;
            }else if (jugador1[AngryEscogido1].energia<jugador2[AngryEscogido2].energia)
            {
                Console.WriteLine("\nGana "+nombreMaquina+"\n");
                contJug2++;
            }else
            {
                Console.WriteLine("\nEmpate! nadie suma punto\n");
            }

            jugador1 = jugador1.Where((source, index) =>index != AngryEscogido1).ToArray();
            jugador2 = jugador2.Where((source, index) =>index != AngryEscogido2).ToArray();
            
            Marcador(nombre1,nombreMaquina,contJug1,contJug2);
            Console.ReadLine();

            LimpiarPantalla();

            turnos--;
        }

        Console.WriteLine("\nFin de la partida!");
        if (contJug1>contJug2){
            Console.WriteLine("\nEsta vez gana "+nombre1+"!");
        }else if (contJug1<contJug2)
        {
            Console.WriteLine("\nEsta vez gana "+nombreMaquina+"!");
        }else
        {
            Console.WriteLine("\nEMPATE :O !");
        }



    }

}

