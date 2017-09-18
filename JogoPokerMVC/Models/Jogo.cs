using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JogoPokerMVC.Models
{
    public class Jogo
    {
        public  List<String> cartas = new List<String>();

        public  String[] maos10Cartas = new String[10];

        public  String[] gerarCartasDosJogadores()
        {
            //Criar cartas
            String[] naipes = new String[] { "o", "c", "p", "s" };
            foreach (var naipe in naipes)
            {
                for (int i = 2; i < 15; i++)
                {
                    if (i > 9)
                        cartas.Add(i + naipe);
                    else
                        cartas.Add("0" + i + naipe);
                }
            }
            //Selecionar 10 cartas aleatorias
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                int randnumero = rand.Next(1, cartas.Count());
                maos10Cartas[i] = cartas[randnumero];
                cartas[randnumero].Remove(0);
            }
            return maos10Cartas;
        }
        public  int[] ResultadoDasCartas(String[] maos10Cartas)
        {   
            //distribuir as cartas e naipes separados 
            int[] valorCartasJog1 = new int[5];
            string[] naipeCartasJog1 = new String[5];
            int[] valorCartasJog2 = new int[5];
            string[] naipeCartasJog2 = new String[5];

            int[] resultado2Jogadores = new int[2];
            for (int i = 0; i < 5; i++)
            {
                valorCartasJog1[i] = Convert.ToInt32(maos10Cartas[i][0] + "" + maos10Cartas[i][1] + "");
                naipeCartasJog1[i] = "" + maos10Cartas[i][2];
            }
            for (int i = 5; i < 10; i++)
            {
                valorCartasJog2[i-5] = Convert.ToInt16(maos10Cartas[i][0] +""+ maos10Cartas[i][1] + "");
                naipeCartasJog2[i-5] = "" + maos10Cartas[i][2];
            }
            Array.Sort(valorCartasJog1);
            Array.Sort(valorCartasJog2);

            resultado2Jogadores[0] = ContarPontos(valorCartasJog1, naipeCartasJog1);
            resultado2Jogadores[1] = ContarPontos(valorCartasJog2, naipeCartasJog2);
            return resultado2Jogadores;
        }
        //Verificar qual é a combinação das mãos
        public  int ContarPontos(int[] valorJog,String[] naipe)
        {
            int resultadoJogador = 0;

            if (RoyalFlush(naipe, valorJog))
            {
                return resultadoJogador = 10;
            }
            if (StrightFlush(naipe, valorJog))
            {
                return resultadoJogador = 9;
            }
            if (FourOfAKind(valorJog))
            {
                return resultadoJogador=8;
            }
            if (FullHouse (valorJog))
            {
                return resultadoJogador=7;
            }
            if (Flush(naipe))
            {
                return resultadoJogador=6;
            }
            if (Stright(valorJog))
            {
                return resultadoJogador=5;
            }
            if (ThreeOfAKind(valorJog))
            {
                return resultadoJogador=4;
            }
            if (TwoPairs(valorJog))
            {
                return resultadoJogador=3;
            }
            if (OnePair(valorJog))
            {
                return resultadoJogador=2;
            }

            return resultadoJogador;
        }
        
        
        
        //verificar combinações

        public  Boolean RoyalFlush(String[] naipesCartas, int[] valorCartas)
        {
            var item = naipesCartas.FirstOrDefault();
            if (!naipesCartas.Skip(1).All(i => i == item))
                return false;
            if (!valorCartas.Skip(1).All(i => i >= 10))
                return false;
            else
                return true;
        }
        public  Boolean StrightFlush(String[] naipesCartas, int[] valorCartas)
        {
            var item = naipesCartas.FirstOrDefault();
            if (!naipesCartas.Skip(1).All(i => i == item))
                return false;

            for (int i = 0; i < valorCartas.Length - 1; i++)
            {
                if (!(valorCartas[i] + 1 == valorCartas[i + 1]))
                    return false;
            }
            return true;
        }
        public  Boolean FourOfAKind(int[] valorCartas)
        {
            int Cont = 0;
            foreach (var item in valorCartas)
            {
                if (!(item == valorCartas[2]))
                    Cont++;
            }
            if (Cont >= 2)
                return false;
            else
                return true;
        }
        public  Boolean FullHouse(int[] valorCartas)
        {
            int cont = 0;
            foreach (var item in valorCartas)
            {
                if (item == valorCartas.FirstOrDefault())
                    cont++;
            }
            foreach (var item in valorCartas)
            {
                if (item == valorCartas.Last())
                    cont++;
            }
            if (cont == 5)
                return true;
            else
                return false;
        }
        public  Boolean Flush(String[] naipesCartas)
        {
            if (!naipesCartas.Skip(1).All(i => i == naipesCartas.FirstOrDefault()))
                return false;
            else
                return true;
        }
        public  Boolean Stright(int[] valorCartas)
        {
            for (int i = 0; i < valorCartas.Length - 1; i++)
            {
                if (!(valorCartas[i] + 1 == valorCartas[i + 1]))
                    return false;
            }
            return true;
        }
        public  Boolean ThreeOfAKind(int[] valorCartas)
        {
            int Cont = 0;
            foreach (var item in valorCartas)
            {
                if (item == valorCartas[2])
                    Cont++;
            }
            if (Cont < 3)
                return false;
            else
                return true;
        }
        public  Boolean TwoPairs(int[] valorCartas)
        {
            int ContPrincipal = 0;
            foreach (var item in valorCartas)
            {
                int cont = 0;
                foreach (var itemverificado in valorCartas)
                {
                    if (item == itemverificado)
                    {
                        cont++;
                    }
                }
                if (cont == 2)
                {
                    ContPrincipal++;
                }
            }
            if (ContPrincipal == 4)
                return true;
            else
                return false;
        }
        public  Boolean OnePair(int[] valorCartas)
        {
            int cont = 0;
            for (int i = 0; i < valorCartas.Length - 1; i++)
            {
                if (valorCartas[i] == valorCartas[i + 1])
                    cont++;
            }
            if (cont == 1)
                return true;
            else
                return false;
        }
    }
}

