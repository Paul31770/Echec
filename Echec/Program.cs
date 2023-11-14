using System;
using System.Security.Cryptography.X509Certificates;

class Mouvement
{

    private static string coord = "";
    private static string nouvCoord = "";
    private static Piece valeurCase=null;
    public static void choisirPiece()
    {
        Console.WriteLine("Entrer les coordonnees da la piece que vous voulez bouger: (ex A1)");
        coord = Console.ReadLine();
        int xCoord =8-Convert.ToInt32(coord[1].ToString());
        int yCoord = coord[0]-'A';
        valeurCase = Board.pieces[xCoord, yCoord];
        Console.WriteLine("Entrer les coordonnées ou vous voulez déplacer la pièce:");
        nouvCoord = Console.ReadLine();
        int xNouvCoord = 8-Convert.ToInt32(nouvCoord[1].ToString());
        int yNouvCoord= nouvCoord[0]-'A';

        
        Board.pieces[xNouvCoord, yNouvCoord] = Board.pieces[xCoord, yCoord];

        Board.pieces[xCoord, yCoord] = null;
        Board.InitTable(); 
    }

}
class Program
{
    static void Main()
    {
        Utilisateur.InitJoueur();

        Board.InitPiece();
        Board.InitTable();
    }
}
class Utilisateur
{
    private static string joueur1 = "";
    private static string joueur2 = "";
    public static void InitJoueur()
    {
        Console.WriteLine("Entrer le nom du premier joueur:");
        joueur1 = Console.ReadLine();
        Console.WriteLine("Entrer le nom du deuxième joueur:");
        joueur2 = Console.ReadLine();
    }

    public static string ChoixJoueur(int nbTour)
    {

        if (nbTour % 2 == 0)
        {
            return joueur1;
            
        }
        else
        {
            return joueur2;
            
        }

    }
}
class Board
{
    private static int nbTour = 0;
    private const int BoardSize = 8;

    public static Piece[,] pieces = new Piece[BoardSize, BoardSize];

    public static void InitPiece()
    {
        // Pion noir
        pieces[1, 0] = new Pion(1, 0, ConsoleColor.Black);
        pieces[1, 1] = new Pion(1, 2, ConsoleColor.Black);
        pieces[1, 2] = new Pion(1, 2, ConsoleColor.Black);
        pieces[1, 3] = new Pion(1, 3, ConsoleColor.Black);
        pieces[1, 4] = new Pion(1, 4, ConsoleColor.Black);
        pieces[1, 5] = new Pion(1, 5, ConsoleColor.Black);
        pieces[1, 6] = new Pion(1, 6, ConsoleColor.Black);
        pieces[1, 7] = new Pion(1, 7, ConsoleColor.Black);
        pieces[0, 0] = new Tour(0, 0, ConsoleColor.Black);
        pieces[0, 7] = new Tour(0, 7, ConsoleColor.Black);
        pieces[0, 1] = new Chevalier(0, 1, ConsoleColor.Black);
        pieces[0, 6] = new Chevalier(0, 6, ConsoleColor.Black);
        pieces[0, 2] = new Fou(0, 2, ConsoleColor.Black);
        pieces[0, 5] = new Fou(0, 5, ConsoleColor.Black);
        pieces[0, 3] = new Roi(0, 3, ConsoleColor.Black);
        pieces[0, 4] = new Reine(0, 4, ConsoleColor.Black);

        //Pion blanc
        pieces[6, 0] = new Pion(1, 0, ConsoleColor.White);
        pieces[6, 1] = new Pion(1, 2, ConsoleColor.White);
        pieces[6, 2] = new Pion(1, 2, ConsoleColor.White);
        pieces[6, 3] = new Pion(1, 3, ConsoleColor.White);
        pieces[6, 4] = new Pion(1, 4, ConsoleColor.White);
        pieces[6, 5] = new Pion(1, 5, ConsoleColor.White);
        pieces[6, 6] = new Pion(1, 6, ConsoleColor.White);
        pieces[6, 7] = new Pion(1, 7, ConsoleColor.White);
        pieces[7, 0] = new Tour(0, 0, ConsoleColor.White);
        pieces[7, 7] = new Tour(0, 7, ConsoleColor.White);
        pieces[7, 1] = new Chevalier(0, 1, ConsoleColor.White);
        pieces[7, 6] = new Chevalier(0, 6, ConsoleColor.White);
        pieces[7, 2] = new Fou(0, 2, ConsoleColor.White);
        pieces[7, 5] = new Fou(0, 5, ConsoleColor.White);
        pieces[7, 3] = new Roi(0, 3, ConsoleColor.White);
        pieces[7, 4] = new Reine(0, 4, ConsoleColor.White);
    }
    public static bool verifRoir()
    {
        int count = 0;
        List<string> jsp=new List<string>();

        for(int i=0; i < BoardSize; i++)
        {
            for(int y=0; y<BoardSize; y++)
            {
             if(pieces[i, y] != null && pieces[i, y].GetType() == typeof(Roi))
            {
                count++;
            }

            }
        }
        if (count == 1)
        {
            return true;
        }
        return false;
        
     
    }
    public static void InitTable()
    {
        if (verifRoir())
        {
            Console.WriteLine(Utilisateur.ChoixJoueur(nbTour)+" à gagné");
            Environment.Exit(0);
        }
        
        Console.Write("   ");
        for (char c = 'A'; c < 'A' + BoardSize; c++)
        {
            Console.Write($" {c} ");
        }
        Console.WriteLine();

        for (int row = 0; row < BoardSize; row++)
        {

            Console.Write($" {BoardSize - row} ");

            for (int col = 0; col < BoardSize; col++)
            {
                if ((row + col) % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }

                if (pieces[row, col] != null)
                {
                    pieces[row, col].Display();
                }
                else
                {
                    Console.Write("   ");
                }

                Console.ResetColor();
            }
            Console.WriteLine();
        }

        nbTour += 1;
        Console.WriteLine("   ________________________");
        Console.WriteLine("     "+ Utilisateur.ChoixJoueur(nbTour));
        Console.WriteLine("   ¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
        Mouvement.choisirPiece();

        
        
        
    }


}

public abstract class Piece
{
    public int X { get; set; }
    public int Y { get; set; }
    public ConsoleColor Color { get; set; }

    public Piece(int x, int y, ConsoleColor color)
    {
        X = x;
        Y = y;
        Color = color;
    }
    public abstract void Display();
}

public class Pion : Piece
{
    public Pion(int x, int y, ConsoleColor color) : base(x, y, color) { }
    public override void Display()
    {
        String name = "P";
        Console.ForegroundColor = Color;
        Console.Write(" P ");
        Console.ResetColor();
    }
}

public class Tour : Piece
{
    public Tour(int x, int y, ConsoleColor color) : base(x, y, color) { }
    public override void Display()
    {
        String name = "T";
        Console.ForegroundColor = Color;
        Console.Write(" T ");
        Console.ResetColor();
    }
}

public class Chevalier : Piece
{
    public Chevalier(int x, int y, ConsoleColor color) : base(x, y, color) { }
    public override void Display()
    {
        String name = "C";
        Console.ForegroundColor = Color;
        Console.Write(" C ");
        Console.ResetColor();
    }
}

public class Fou : Piece
{
    public Fou(int x, int y, ConsoleColor color) : base(x, y, color) { }
    public override void Display()
    {
        String name = "F";
        Console.ForegroundColor = Color;
        Console.Write(" F ");
        Console.ResetColor();
    }
}
public class Roi : Piece
{
    public Roi(int x, int y, ConsoleColor color) : base(x, y, color) { }
    public override void Display()
    {
        String name = "Ro";
        Console.ForegroundColor = Color;
        Console.Write(" Ro");
        Console.ResetColor();
    }
}
public class Reine : Piece
{
    public Reine(int x, int y, ConsoleColor color) : base(x, y, color) { }
    public override void Display()
    {
        String name = "Re";
        Console.ForegroundColor = Color;
        Console.Write(" Re");
        Console.ResetColor();
    }


}

