using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JogoPokerMVC.Models;

namespace JogoPokerMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Jogo pokerGame = new Jogo();
            ResultadoFront resultadoFront = new ResultadoFront();

            resultadoFront.Cartas = pokerGame.gerarCartasDosJogadores();
            int[] resultado = pokerGame.ResultadoDasCartas(resultadoFront.Cartas);

            if (resultado[0] > resultado[2])
            {
                resultadoFront.ResultadoString = "O Jogador 1 ganhou com um "+pokerGame.RetornoStringFinal(resultado[0])+" contra um "+ pokerGame.RetornoStringFinal(resultado[2]);
            }
            if(resultado[0] < resultado[2])
            {
                resultadoFront.ResultadoString = "O Jogador 2 ganhou com um " + pokerGame.RetornoStringFinal(resultado[2]) + " contra um " + pokerGame.RetornoStringFinal(resultado[0]);
            }
            if (resultado[0] == resultado[2])
            {
                    if(resultado[1] > resultado[3])
                    {
                        resultadoFront.ResultadoString = "O Jogador 1 ganhou com uma combição de " + pokerGame.RetornoStringFinal(resultado[2]) + " melhor do que a combinação de " + pokerGame.RetornoStringFinal(resultado[0]) + " do jogador 2";
                    }
                    else if(resultado[1] < resultado[3])
                    {
                        resultadoFront.ResultadoString = "O Jogador 2 ganhou com uma combição de " + pokerGame.RetornoStringFinal(resultado[2]) + " melhor do que a combinação de " + pokerGame.RetornoStringFinal(resultado[0]) + " do jogador 1";
                    }
                    else if(resultado[1] == resultado[3])
                    {
                        resultadoFront.ResultadoString = "Impatou com " + pokerGame.RetornoStringFinal(resultado[0]);
                    }
                    else
                    {
                    resultadoFront.ResultadoString = "Algum erro aconteceu";
                    }
            }

            return View(resultadoFront);
        }
    }
}
