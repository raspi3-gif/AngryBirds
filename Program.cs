using System;
using System.Linq;

public class Joc{

    public void Marcador(string jugador1,string jugador2, int contJugador1,int contJugador2){
        Console.WriteLine(jugador1+":"+contJugador1);
        Console.WriteLine(jugador2+":"+contJugador2);
    }

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

        AngryBird [] ArrayCartas = cartas.ConstruirPajaros();

        AngryBird [] mazoJugador = cartas.AsignarMazo(ArrayCartas,turnos);
        AngryBird [] mazoEnemigo = cartas.AsignarMazo(ArrayCartas,turnos);

        Jugador j1 = new Jugador(mazoJugador);
        j1.PreguntaNombre();

        Enemigo enemy= new Enemigo(mazoEnemigo);

        int AngryEscogido1;

        while (turnos!=0){
            do{
                AngryEscogido1= j1.EscogePajaro();
            }while(AngryEscogido1==-1);

            int AngryEscogido2=enemy.MaquinaEscoge();

            Console.WriteLine("\nEnergia "+j1.GetNombre()+":");
            Console.Write(mazoJugador[AngryEscogido1].GetEnergia());

            Console.WriteLine("\nEnergia "+enemy.GetNombre()+":");
            Console.Write(mazoEnemigo[AngryEscogido2].GetEnergia());

            if (mazoJugador[AngryEscogido1].GetEnergia()>mazoEnemigo[AngryEscogido2].GetEnergia()){
                Console.WriteLine("\nEste turno lo gana "+j1.GetNombre()+"\n");
                j1.SetPuntuacion();
            }else if (mazoJugador[AngryEscogido1].GetEnergia()<mazoEnemigo[AngryEscogido2].GetEnergia())
            {
                Console.WriteLine("\nEste turno lo gana "+enemy.GetNombre()+"\n");
                enemy.SetPuntuacion();
            }else
            {
                Console.WriteLine("\nEmpate! nadie suma punto\n");
            }

            Marcador(j1.GetNombre(),enemy.GetNombre(),j1.GetPuntuacion(),enemy.GetPuntuacion());
            Console.ReadLine();

            LimpiarPantalla();

            turnos--;
        }

        Console.WriteLine("\nFin de la partida!");

        if (j1.GetPuntuacion()>enemy.GetPuntuacion()){
            Console.WriteLine("\n"+j1.GetNombre()+" ha ganado la partida!");
        }else if (j1.GetPuntuacion()<enemy.GetPuntuacion())
        {
            Console.WriteLine("\n"+enemy.GetNombre()+" ha ganado la partida!");
        }else
        {
            Console.WriteLine("\nEMPATE :O !");
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



    
    public AngryBird [] ConstruirPajaros(){
        AngryBird [] pajarillos= new AngryBird[9];
        for (byte i=0;i<this.nombres.Length;i++){
            pajarillos[i]=new AngryBird(this.nombres[i],this.especies[i],this.poderes[i],this.energia[i]);
        }
        return pajarillos;
    }

    public AngryBird [] AsignarMazo(AngryBird [] pajaros,int turnos){
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


}

public class Jugador{
    private string nombre;

    private int puntuacion=0;

    AngryBird [] Mazo;
    
    public Jugador(AngryBird [] Mazo){
        this.Mazo=Mazo;
    }

    public void PreguntaNombre(){
        Console.WriteLine("Nombre del jugador:");
        nombre =  Console.ReadLine();
    }

    public int EscogePajaro(){
        Console.WriteLine("\nTe toca "+this.nombre+"\n");
        foreach (AngryBird element in this.Mazo){
                Console.WriteLine(element.GetNom()+ "    |  energia: " + element.GetEnergia() );
        }

        Console.WriteLine("Escoge que Angry Bird quieres usar: ");
        string nom = Console.ReadLine();
        for (int i=0;i<this.Mazo.Length;i++){
            if (this.Mazo[i].GetNom()==nom){
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
    
}


public class Enemigo{
    private string nombreMaquina = "C-3P0";
    private int puntuacion=0;
    AngryBird [] Mazo;
    public Enemigo(AngryBird [] Mazo){
        this.Mazo=Mazo;
    }

    public int MaquinaEscoge (){
        Random NumRandom = new Random();
        int random=NumRandom.Next(0,this.Mazo.Length);
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
}


class Program {
    static void Main(string[] args){
                
        Joc j = new Joc();
        j.start();
        
    }
    
}
    


  

        

       

        

        

 

/*
  static void BorrarPajaro(AngryBird [] jugador,int pajaroEscogido){
        jugador[pajaroEscogido].nom="";
    }
*/
