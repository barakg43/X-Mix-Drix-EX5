﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ex02.ConsoleUtils;
namespace X_Mix_Drix_UI
{
    class Program
    {
        public static void Main()
        {
            Menu menu = new Menu();
            menu.PrintMainMenu();
            menu.GetAndCheckUserInputForMenuItem();
        }
    }
}
