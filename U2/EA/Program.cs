using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaFiltrosImagenes;

/*
  sistema de filtros que modifique ciertos “pixeles” de una
  imagen representada como una cadena de texto numérica. Cada número representa un
  nivel de resolución o intensidad visual (escalas del 7 al 9).
*/
class Imagen(string nombre, string cadena)
{
  public string Nombre { get; set; } = nombre;
  public string Cadena { get; set; } = cadena;

  public override string ToString() => $"{Nombre}: {Cadena}";
}

// Clase base
abstract class Filtro(Imagen imagen)
{
  public string NombreImagen { get; set; } = imagen.Nombre;
  public string CadenaImagen { get; set; } = imagen.Cadena;
  public string ImagenOriginal { get; private set; } = imagen.Cadena;

  public virtual void AplicarFiltro();

  public virtual string RevertirFiltro()
  {
    CadenaImagen = ImagenOriginal;
    return CadenaImagen;
  }
}

// Filtro Pixelado: Convierte los pixeles seleccionados a 5 para simular pixelado.
class FiltroPixelado(Imagen imagen, List<int> posiciones) : Filtro(imagen)
{
  public List<int> PosicionesPixeladas { get; set; } = posiciones;

  public override void AplicarFiltro()
  {
    char[] chars = CadenaImagen.ToCharArray();
    foreach (int pos in PosicionesPixeladas)
    {
      if (pos >= 0 && pos < chars.Length)
        chars[pos] = '5';
    }
    CadenaImagen = new string(chars);
    Console.WriteLine($"Filtro Pixelado aplicado: {CadenaImagen}");
  }

    public override string RevertirFiltro()
    {
      Console.WriteLine("Filtro Pixelado revertido. Imagen restaurada.");
      return base.RevertirFiltro();
    }
}

// Filtro Desenfoque: Convierte los pixeles seleccionados a 2 para simular desenfoque.
class FiltroDesenfoque(Imagen imagen, List<int> posiciones, int nivel) : Filtro(imagen)
{
  public int NivelDesenfoque { get; set; } = nivel;
  public List<int> PosicionesDesenfoque { get; set; } = posiciones;

  public override void AplicarFiltro()
  {
    char[] chars = CadenaImagen.ToCharArray();
    foreach (int pos in PosicionesDesenfoque)
    {
      if (pos >= 0 && pos < chars.Length)
        chars[pos] = '2';
    }
    CadenaImagen = new string(chars);
    Console.WriteLine($"Filtro Desenfoque aplicado (Nivel {NivelDesenfoque}): {CadenaImagen}");
  }

  public override string RevertirFiltro()
  {
    Console.WriteLine("Filtro Desenfoque revertido. Imagen restaurada.");
    return base.RevertirFiltro();
  }
}

// Filtro IA: Aplica un filtro automático. Cambia las posiciones impares a 3.
class FiltroIA(Imagen imagen, int nivel, string nombreModeloIA) : FiltroDesenfoque(imagen, new List<int>(), nivel)
{
  public string NombreModeloIA { get; set; } = nombreModeloIA;

  public override void AplicarFiltro()
  {
    char[] chars = CadenaImagen.ToCharArray();
    for (int i = 0; i < chars.Length; i++)
    {
      if (i % 2 == 0)
        chars[i] = '3';
    }
    CadenaImagen = new string(chars);
    Console.WriteLine($"Filtro IA ({NombreModeloIA}) aplicado: {CadenaImagen}");
  }

  public override string RevertirFiltro()
  {
    Console.WriteLine($"Filtro IA ({NombreModeloIA}) revertido. Imagen restaurada.");
    return base.RevertirFiltro();
  }
}

// Filtro Híbrido genérico: Aplica doble transformación según el tipo de imagen.
class FiltroHibrido<T>(Imagen imagen, List<int> posiciones, int valorFiltro) : FiltroPixelado(imagen, posiciones)
{
  public int ValorFiltroGenerico { get; set; } = valorFiltro;

  public void AplicarFiltro(T imagen)
  {
    if (imagen is string cadena)
    {
      char[] chars = cadena.ToCharArray();
      foreach (int pos in PosicionesPixeladas)
      {
        if (pos >= 0 && pos < chars.Length)
          chars[pos] = '5';
      }
      CadenaImagen = new string(chars);
      Console.WriteLine($"Filtro Híbrido aplicado (string): {CadenaImagen}");
    }
    else if (imagen is char[] arreglo)
    {
      for (int i = 0; i < arreglo.Length; i++)
      {
        if (i % 2 == 0)
          arreglo[i] = $"{ValorFiltroGenerico}"[0]; // Convertir el número a char
      }
      CadenaImagen = new string(arreglo);
      Console.WriteLine($"Filtro Híbrido aplicado (char[]): {CadenaImagen}");
    }
  }

  public override string RevertirFiltro()
  {
    Console.WriteLine("Filtro Híbrido revertido. Imagen restaurada.");
    return base.RevertirFiltro();
  }
}

// Menú Interactivo
class MenuInteractivo
{

  private List<Filtro> filtrosAplicados = new List<Filtro>();
  private void AplicarFiltro(Filtro filtro)
  {
    filtro.AplicarFiltro();
    filtrosAplicados.Add(filtro);
  }



  private List<int> ObtenerPosiciones(string tipo)
  {
    Console.Write($"Posiciones {tipo} (separadas por espacios): ");
    return Console.ReadLine().Split().Select(int.Parse).ToList();
  }

  public void Iniciar()
  {
    bool nombreValido = false;
    string cadenaValida = string.Empty;
    string nombreImagen = string.Empty;

    do
    {

      Console.Write("Nombre de la imagen: ");
      nombreImagen = Console.ReadLine();
      Console.Write("Cadena representativa de la imagen (deben ser numeros entre 7 y 9 sin espacios): ");
      cadenaValida = Console.ReadLine();

      if (string.IsNullOrWhiteSpace(nombreImagen) || string.IsNullOrWhiteSpace(cadenaValida))
      {
        Console.WriteLine("Nombre o cadena de imagen no pueden estar vacíos.");
        continue;
      }
      nombreValido = true;
    } while (!nombreValido);

      var imagen = new Imagen(nombreImagen, cadenaValida);
    do
    {
      Console.WriteLine("""
              === Sistema de Filtros de Imagen ===
              1. Aplicar filtro pixelado
              2. Aplicar filtro desenfoque
              3. Aplicar filtro IA
              4. Aplicar filtro híbrido
              5. Revertir filtro actual
              6. Salir
            """);
      Console.Write("Selecciona una opción: ");
      string opcion = Console.ReadLine();

      if (opcion == "6") break;

      switch (opcion)
      {
        case "1":
          var pixelado = ObtenerPosiciones("pixelado");
          var filtroPixelado = new FiltroPixelado(imagen, pixelado);
          AplicarFiltro(filtroPixelado);
          imagen.Cadena = filtroPixelado.CadenaImagen;
          break;

        case "2":
          var desenfoque = ObtenerPosiciones("desenfoque");
          Console.Write("Nivel de desenfoque (1-10): ");
          int nivel = int.Parse(Console.ReadLine());
          var filtroDesenfoque = new FiltroDesenfoque(imagen, desenfoque, nivel);
          AplicarFiltro(filtroDesenfoque);
          imagen.Cadena = filtroDesenfoque.CadenaImagen;
          break;

        case "3":
          Console.Write("Nombre del modelo IA: ");
          string modeloIA = Console.ReadLine();
          var filtroIA = new FiltroIA(imagen, 5, modeloIA);
          AplicarFiltro(filtroIA);
          imagen.Cadena = filtroIA.CadenaImagen;
          break;

        case "4":
          var posicionesHibrido = ObtenerPosiciones("híbrido");

          Console.Write("Valor filtro generico (número): ");
          int valorFiltro = int.Parse(Console.ReadLine());
          var hibrido = new FiltroHibrido<char[]>(imagen, posicionesHibrido, valorFiltro);

          Console.Write("¿Tipo de imagen? (1=string, 2=char[]): ");
          int tipo = int.Parse(Console.ReadLine());
          if (tipo == 1)
          {
            hibrido.AplicarFiltro(cadenaValida.ToCharArray());
            imagen.Cadena = hibrido.CadenaImagen;
          }
          else
          {
            char[] arreglo = cadenaValida.ToCharArray();
            hibrido.AplicarFiltro(arreglo);
            imagen.Cadena = new string(arreglo);
          }
          break;

        case "5":
          if (filtrosAplicados.Count > 0)
          {
            imagen.Cadena = filtrosAplicados[^1].RevertirFiltro();
            filtrosAplicados.RemoveAt(filtrosAplicados.Count - 1);
            Console.WriteLine($"Imagen actual: {imagen.Cadena}");
          }
          else
          {
            Console.WriteLine("No hay filtros aplicados para revertir.");
          }
          break;

        default:
          Console.WriteLine("Opción no válida.");
          break;
      }
    } while (true);
  }
}

// Programa principal
class Program
{
    static void Main(string[] args)
    {
        new MenuInteractivo().Iniciar();
    }
}

