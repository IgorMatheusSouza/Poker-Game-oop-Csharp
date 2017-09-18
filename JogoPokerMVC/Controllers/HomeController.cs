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
            String[] cartas10 = pokerGame.gerarCartasDosJogadores();
            int[] resultado = pokerGame.ResultadoDasCartas(cartas10);
            cartas10[9] += "/" + resultado[0] + " and " + resultado[1];
            return View(cartas10);
        }
    }
}
