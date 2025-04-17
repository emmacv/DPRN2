using System;
using System.Collections.Specialized;

class Actividad
{



  static void Main(string[] args)
  {
    Console.WriteLine("Hola Mundo!");
  }
}

class GlimmerCard
{
  private int inkCost;
  private bool hasInkWellSymbol;
  private string color;
  private string name;
  private string version;
  private string[] classifications;
  private string ability;
  private int? strength;
  private int? willpower;
  private int? loreValue;

  public int InkCost
  {
    get { return inkCost; }
    set { inkCost = value; }
  }

  public bool HasInkWellSymbol
  {
    get { return hasInkWellSymbol; }
    set { hasInkWellSymbol = value; }
  }

  public string Color
  {
    get { return color; }
    set { color = value; }
  }

  public string Name
  {
    get { return name; }
    set { name = value; }
  }

  public string Version
  {
    get { return version; }
    set { version = value; }
  }

  public string[] Classifications
  {
    get { return classifications; }
    set { classifications = value; }
  }

  public string Ability
  {
    get { return ability; }
    set { ability = value; }
  }

  public int? Strength
  {
    get { return strength; }
    set { strength = value; }
  }

  public int? Willpower
  {
    get { return willpower; }
    set { willpower = value; }
  }

  public int? LoreValue
  {
    get { return loreValue; }
    set { loreValue = value; }
  }

  public GlimmerCard(int inkCost, bool hasInkWellSymbol, string color, string name, string version, string[] classifications, string ability, int? strength, int? willpower, int? loreValue)
  {
    this.inkCost = inkCost;
    this.hasInkWellSymbol = hasInkWellSymbol;
    this.color = color;
    this.name = name;
    this.version = version;
    this.classifications = classifications;
    this.ability = ability;
    this.strength = strength;
    this.willpower = willpower;
    this.loreValue = loreValue;
  }

  public GlimmerCard(string name, int inkCost)
  {
    this.name = name;
    this.inkCost = inkCost;
    hasInkWellSymbol = false;
    color = "Unknown";
    version = "1.0";
    classifications = new string[] { "Default" };
    ability = "None";
    strength = null;
    willpower = null;
    loreValue = null;
  }

  public void RecibirDaño(int cantidad)
  {
    if (willpower.HasValue)
    {
      willpower -= cantidad;
      if (willpower <= 0)
      {
        willpower = 0;
        Console.WriteLine($"El Glimmer '{name}' ha sido desterrado.");
      }
    }
    else
    {
      Console.WriteLine($"El Glimmer '{name}' no tiene voluntad definida.");
    }
  }

  public void ActivarHabilidad()
  {
    Console.WriteLine($"La habilidad del Glimmer '{name}' ha sido activada: {ability}");
  }

  public void Desafiar(GlimmerCard objetivo)
  {
    Console.WriteLine($"El Glimmer '{name}' desafía al Glimmer '{objetivo.Name}'.");
  }

  public void Ejercer()
  {
    Console.WriteLine($"El Glimmer '{name}' se ha ejercido.");
  }

  public void Listo()
  {
    Console.WriteLine($"El Glimmer '{name}' está listo para realizar otra acción.");
  }

  public void MostrarInformacion()
  {
    Console.WriteLine("Información del Glimmer:");
    Console.WriteLine($@"
  Nombre: {name}
  Costo de tinta: {inkCost}
  Símbolo de pozo de tinta: {hasInkWellSymbol}
  Color: {color}
  Versión: {version}
  Clasificaciones: {string.Join(", ", classifications)}
  Habilidad: {ability}
  Fuerza: {(strength.HasValue ? strength.ToString() : "N/A")}
  Voluntad: {(willpower.HasValue ? willpower.ToString() : "N/A")}
  Valor de sabiduría: {(loreValue.HasValue ? loreValue.ToString() : "N/A")}
  ");
  }
}
