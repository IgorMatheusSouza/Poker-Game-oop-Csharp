using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JogoPokerMVC.Models
{
    public class Jogo
    {
        public List<String> cartas = new List<String>();

        public String[] maos10Cartas = new String[10];

        public String[] gerarCartasDosJogadores()
        {
            //Criar cartas
            String[] naipes = new String[] { "o", "c", "p", "s" };
            foreach (var naipe in naipes)
            {
                for (int i = 2; i <= 14; i++)
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
                int randnumero = rand.Next(0, cartas.Count());
                maos10Cartas[i] = cartas[randnumero];
                cartas.RemoveAt(randnumero);
            }
            return maos10Cartas;
        }
        public int[] ResultadoDasCartas(String[] maos10Cartas)
        {
            //distribuir as cartas e naipes separados 
            int[] valorCartasJog1 = new int[5];
            string[] naipeCartasJog1 = new String[5];
            int[] valorCartasJog2 = new int[5];
            string[] naipeCartasJog2 = new String[5];

            int[] resultado2Jogadores = new int[4];

            for (int i = 0; i < 5; i++)
            {
                valorCartasJog1[i] = Convert.ToInt32(maos10Cartas[i][0] + "" + maos10Cartas[i][1] + "");
                naipeCartasJog1[i] = "" + maos10Cartas[i][2];
            }
            for (int i = 5; i < 10; i++)
            {
                valorCartasJog2[i - 5] = Convert.ToInt16(maos10Cartas[i][0] + "" + maos10Cartas[i][1] + "");
                naipeCartasJog2[i - 5] = "" + maos10Cartas[i][2];
            }
            Array.Sort(valorCartasJog1);
            Array.Sort(valorCartasJog2);

            //Armazenar o resultado das combinações e o peso para desempate
            int[] resultadoComPeso = ContarPontos(valorCartasJog1, naipeCartasJog1);
                  resultado2Jogadores[0] = resultadoComPeso[0];
                  resultado2Jogadores[1] = resultadoComPeso[1];
            resultadoComPeso = ContarPontos(valorCartasJog2, naipeCartasJog2);
                  resultado2Jogadores[2] = resultadoComPeso[0];
                  resultado2Jogadores[3] = resultadoComPeso[1];
            //retorna um vetor de int com 4 posicoes sendo 0 combinação jogador1, 1 desempate jog1, 2 combinação jog2 e 3 desempate jog2
            return resultado2Jogadores;
        }

        //Verificar qual é a combinação das mãos
        public int[] ContarPontos(int[] valorJog, String[] naipe)
        {
            //Primeira  posição no vetor resultadoJogador equivale ao peso de cada combinacao exemplo RoyalFlush=10, StraightFlush=9 etc.
            //Segunda posição no vetor resultadoJogado equivale ao peso para desempatar
            int[] resultadoJogador = new int[] {0,0};

            if (RoyalFlush(naipe, valorJog))
            {
                resultadoJogador[0] = 10;
                return resultadoJogador;
            }
            if (StrightFlush(naipe, valorJog))
            {
                resultadoJogador[0] = 9;
                resultadoJogador[1] = StraightEFlushDesempate(valorJog);
                return resultadoJogador;
            }
            if (FourOfAKind(valorJog))
            {
                resultadoJogador[0] = 8;
                resultadoJogador[1] = FourOfAKindDesempate(valorJog);
                return resultadoJogador;
            }
            if (FullHouse(valorJog))
            {
                resultadoJogador[0] = 7;
                resultadoJogador[1] = FullhouseDesempate(valorJog);
                return resultadoJogador;
            }
            if (Flush(naipe))
            {
                resultadoJogador[0] = 6;
                resultadoJogador[1] = StraightEFlushDesempate(valorJog);
                return resultadoJogador;
            }
            if (Stright(valorJog))
            {
                resultadoJogador[0] = 5;
                resultadoJogador[1] = StraightEFlushDesempate(valorJog);
                return resultadoJogador;
            }
            if (ThreeOfAKind(valorJog))
            {
                resultadoJogador[0] = 4;
                resultadoJogador[1] = ThreeOfAKindDesempate(valorJog);
                return resultadoJogador;
            }
            if (TwoPairs(valorJog))
            {
                resultadoJogador[0] = 3;
                resultadoJogador[1] = TwoPairDesempate(valorJog);
                return resultadoJogador;
            }
            if (OnePair(valorJog))
            {
                resultadoJogador[0] = 2;
                resultadoJogador[1] = OnePairDesempate(valorJog);
                return resultadoJogador;
            }
            else
            {
                resultadoJogador[0] = 1;
                resultadoJogador[1] = HighCardDesempate(valorJog);
                return resultadoJogador;
            }
        }



        //verificar combinações

        public Boolean RoyalFlush(String[] naipesCartas, int[] valorCartas)
        {
            var item1 = naipesCartas.FirstOrDefault();
            if (naipesCartas.Skip(1).All(i => i == item1)  && valorCartas.Skip(1).All(i => i >= 10))
                return true;
            else
                return false;
        }
        public Boolean StrightFlush(String[] naipesCartas, int[] valorCartas)
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
        public Boolean FourOfAKind(int[] valorCartas)
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
        public Boolean FullHouse(int[] valorCartas)
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
        public Boolean Flush(String[] naipesCartas)
        {
            if (!naipesCartas.Skip(1).All(i => i == naipesCartas.FirstOrDefault()))
                return false;
            else
                return true;
        }
        public Boolean Stright(int[] valorCartas)
        {
            for (int i = 0; i < valorCartas.Length - 1; i++)
            {
                if (!(valorCartas[i] + 1 == valorCartas[i + 1]))
                    return false;
            }
            return true;
        }
        public Boolean ThreeOfAKind(int[] valorCartas)
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
        public Boolean TwoPairs(int[] valorCartas)
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
        public Boolean OnePair(int[] valorCartas)
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


        //Peso das cartas para Desempate
        public int HighCardDesempate(int[] valorCartas)
        {
            return valorCartas.Max(i => i);
        }
        public int OnePairDesempate(int[] valorCartas)
        {
            for (int i = 0; i < valorCartas.Length - 1; i++)
            {
                if (valorCartas[i] == valorCartas[i + 1])
                   return valorCartas[i] + valorCartas[i + 1];
            }
            return 0;
        }
        public int TwoPairDesempate(int[] valorCartas)
        {
            int pesoDesempate = 0;
                for (int i = 0; i < valorCartas.Length - 1; i++)
                {
                    if (valorCartas[i] == valorCartas[i + 1])
                        pesoDesempate += valorCartas[i] + valorCartas[i + 1];
                }
            return pesoDesempate;
        }
        public int ThreeOfAKindDesempate(int[] valorCartas)
        {
            int pesoDesempate = 0;
            for (int i = 0; i < valorCartas.Length - 1; i++)
            {
                if(valorCartas[i] == valorCartas[i + 1])
                    pesoDesempate += valorCartas[i] + valorCartas[i + 1];
            }
            return pesoDesempate;
        }

        public int StraightEFlushDesempate(int[] valorCartas)
        {
            return valorCartas.Sum(i => i);
        }

        public int FullhouseDesempate(int[] valorCartas)
        {
            int pesoDesempate = 0;
               for (int i = 0; i < valorCartas.Length - 1; i++)
               {
                   if(valorCartas[i] == valorCartas[i + 1])
                       pesoDesempate += valorCartas[i] + valorCartas[i + 1];
               }
            return pesoDesempate;
        }

        public int FourOfAKindDesempate(int[] valorCartas)
        {
            int pesoDesempate = 0;
            for (int i = 0; i < valorCartas.Length - 1; i++)
            {
                if (valorCartas[i] == valorCartas[i + 1])
                    pesoDesempate += valorCartas[i] + valorCartas[i + 1];
            }
            return pesoDesempate;
        }

        //Retornar String Com a combinação vitoriosa
        public String RetornoStringFinal(int valorCarta)
        {
            switch (valorCarta)
            {
                case 1:
                    return "High Card";
                case 2:
                    return "One Pair";
                case 3:
                    return "Two Pair";
                case 4:
                    return "Three of a Kind";
                case 5:
                    return "Straight";
                case 6:
                    return "Flush";
                case 7:
                    return "Full House";
                case 8:
                    return "Four Of A Kind";
                case 9:
                    return "Straight Flush";
                case 10:
                    return "Royal Flush";
                default:
                    return "Algo saiu errado.";
                    
            }
        }
    }
}

