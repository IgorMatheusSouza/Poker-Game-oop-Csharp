using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JogoPokerMVC.Models
{
    public class JogoCombinacoes
    {
            public Boolean RoyalFlush(String[] naipesCartas, int[] valorCartas)
            {
                var item1 = naipesCartas.FirstOrDefault();
                if (naipesCartas.Skip(1).All(i => i == item1) && valorCartas.All(i => i >= 10))
                    return true;
                else
                    return false;
            }
            public Boolean StrightFlush(String[] naipesCartas, int[] valorCartas)
            {
                if (Straight(valorCartas) && Flush(naipesCartas))
                    return true;
                else
                    return false;
            }
            public Boolean FourOfAKind(int[] valorCartas)
            {
                int Cont = 0;
                foreach (var item in valorCartas)
                {
                    if (item == valorCartas[2])
                        Cont++;
                }
                if (Cont == 4)
                    return true;
                else
                    return false;
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
                if (naipesCartas.Skip(1).All(i => i == naipesCartas.FirstOrDefault()))
                    return true;
                else
                    return false;
            }
            public Boolean Straight(int[] valorCartas)
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
                        return valorCartas[i];
                }
                return 0;
            }
            public int TwoPairDesempate(int[] valorCartas)
            {
                for (int i = 4; i > 0; i--)
                {
                    int duplaAlta = valorCartas[i];
                    if (duplaAlta == valorCartas[i - 1])
                        return duplaAlta;
                }
                return 0;
            }
            public int ThreeOfAKindDesempate(int[] valorCartas)
            {
                int pesoDesempate = 0;
                for (int i = 0; i < valorCartas.Length - 1; i++)
                {
                    if (valorCartas[i] == valorCartas[i + 1])
                        pesoDesempate += valorCartas[i];
                }
                return pesoDesempate;
            }

            public int StraightEFlushDesempate(int[] valorCartas)
            {
                return valorCartas[4];
            }

            public int FullhouseDesempate(int[] valorCartas)
            {
                return valorCartas[2];
            }

            public int FourOfAKindDesempate(int[] valorCartas)
            {
                return valorCartas[2];
            }
            public int HighCardDesempateSegundaOcorrencia(int[] valorCartas)
            {
                return valorCartas[3];
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

