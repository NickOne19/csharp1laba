using System;
using System.Collections.Generic;
using ConsoleApp3.DPL;

class Program
{
    static void Main(string[] args)
    {
        StudentDBStorage studentDBStorage = new StudentDBStorage(new StudentContext());
        studentDBStorage.mainMenu();
    }
}