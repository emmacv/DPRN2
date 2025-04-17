using System;
using System.Collections.Specialized;

class Actividad {



  static void Main(string[] args) {
    Console.WriteLine("Hola Mundo!");
  }
}

class GlimmerCard {
  int? strenght;
  int? willPower;
  int? loreValue;
  int cost;
  bool hasInkWellIcon;
  string name;
  string version;
  Ink ink;
  int classification;
  string ability;
  string effects;
  int lore;

  class Ink {
    string name;
  }
}
