using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JogoPokerMVC.Models
{
    public class Jogo: JogoCombinacoes
    {
        public List<String> cartas = new List<String>();
        public String[] maos10Cartas = new String[10];
        //distribuir as cartas e naipes separados
        public int[] valorCartasJog1 = new int[5];
        public string[] naipeCartasJog1 = new String[5];
        public int[] valorCartasJog2 = new int[5];
        public string[] naipeCartasJog2 = new String[5];

        

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

            //vetor com os resultados de combicoes e desempate dos dois jogadores
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
            //retorna um vetor de int com 4 posicoes sendo posicao[0]=combinação jogador1, posicao[1]= desempate jog1, posicao[2]= combinação jog2 e posicao[3] =desempate jog2
            return resultado2Jogadores;
        }





        //Verificar qual é a combinação das mãos
        public int[] ContarPontos(int[] valorJog, String[] naipe)
        {
            //Primeira  posição no vetor resultadoJogador equivale ao peso de cada combinacao exemplo RoyalFlush=10, StraightFlush=9 etc.
            //Segunda posição no vetor resultadoJogado equivale ao peso para desempatar exemplo Dupla maior = 9, quadra = 12(dama);

            int[] resultadoJogador = new int[] { 0, 0 };

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
            if (Straight(valorJog))
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


    }
}
        