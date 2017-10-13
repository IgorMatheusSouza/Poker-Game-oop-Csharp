using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JogoPokerMVC.Models;

namespace JogoPokerMVC.Controllers
{
    public class JogoViewController : Controller
    {
        public IActionResult Index()
        {
            ResultadoFront resultadoFront = new ResultadoFront();
            resultadoFront.Cartas = new String[10];
            for (int i = 0; i < resultadoFront.Cartas.Length; i++)
            {
                if (i < 5)
                    resultadoFront.Cartas[i] = "backcardRed";
                else
                    resultadoFront.Cartas[i] = "backcardBlue";
            }
            resultadoFront.CombinacoesString = null;
            return View(resultadoFront);
        }
        [HttpPost]
        public IActionResult Index(String estadoAtual)
        {

            if (estadoAtual == "Virar cartas")
            {
                Jogo pokerGame = new Jogo();

                ResultadoFront resultadoFront = new ResultadoFront();

                resultadoFront.Cartas = pokerGame.gerarCartasDosJogadores();

                int[] resultado = pokerGame.ResultadoDasCartas(resultadoFront.Cartas);

                if (resultado[0] > resultado[2])
                {
                    resultadoFront.ResultadoString = "O Jogador 1 ganhou com um " + pokerGame.RetornoStringFinal(resultado[0]);
                }
                if (resultado[0] < resultado[2])
                {
                    resultadoFront.ResultadoString = "O Jogador 2 ganhou com um " + pokerGame.RetornoStringFinal(resultado[2]);
                }
                if (resultado[0] == resultado[2])
                {
                           if (resultado[1] > resultado[3])
                           {
                               resultadoFront.ResultadoString = "O Jogador 1 ganhou com uma combição de " + pokerGame.RetornoStringFinal(resultado[2]) + " melhor";
                           }
                           else if (resultado[1] < resultado[3])
                           {
                               resultadoFront.ResultadoString = "O Jogador 2 ganhou com uma combição de " + pokerGame.RetornoStringFinal(resultado[2]) + " melhor";
                           }
                           else if (resultado[1] == resultado[3] && resultado[0] == 1)
                           {
                                    if (pokerGame.HighCardDesempateSegundaOcorrencia(pokerGame.valorCartasJog1) > pokerGame.HighCardDesempateSegundaOcorrencia(pokerGame.valorCartasJog1))
                                    {
                                        resultadoFront.ResultadoString = "O Jogador 1 ganhou com uma combição de " + pokerGame.RetornoStringFinal(resultado[2]) + " melhor";
                                    }
                                    else
                                        resultadoFront.ResultadoString = "O Jogador 2 ganhou com uma combição de " + pokerGame.RetornoStringFinal(resultado[2]) + " melhor";
                           }
                           else if (resultado[1] == resultado[3])
                           {
                               resultadoFront.ResultadoString = "Impatou";
                           }
                           else
                           {
                               resultadoFront.ResultadoString = "Algum erro aconteceu";
                           }
                }
                resultadoFront.CombinacoesString = pokerGame.RetornoStringFinal(resultado[0]) + "  Vs  " + pokerGame.RetornoStringFinal(resultado[2]);
                return View(resultadoFront);
            }
            else {
                ResultadoFront resultadoFront = new ResultadoFront();
                resultadoFront.Cartas = new String[10];
                for (int i = 0; i < resultadoFront.Cartas.Length; i++)
                {   
                    if(i<5)
                        resultadoFront.Cartas[i] = "backcardRed";
                    else
                        resultadoFront.Cartas[i] = "backcardBlue";
                }
                resultadoFront.CombinacoesString = null;
                return View(resultadoFront);
            }
          
        }
    }
}

//testando versionamento
//Codigo para verificar as combinações mais Raras de acontecer
/*
int[] resultado;
                while (true)
                {
                    for (int i = 0; i<pokerGame.cartas.Count;)
                    {
                        pokerGame.cartas.RemoveAt(i);
                    }
                   
                    resultadoFront.Cartas = pokerGame.gerarCartasDosJogadores();

                   resultado = pokerGame.ResultadoDasCartas(resultadoFront.Cartas);
                    if (resultado[0] == 10 || resultado[2] == 9)
                        break;
                    cont++;
                }
  */