using System;


class Program {
  static void Main() {

  }
}

public class Microorganismo
{
    public string Nombre { get; set; }
    public string Tipo { get; set; } // Clasificación taxonómica completa
    public string FuncionBiologica { get; set; }
    public string SecuenciaGenetica { get; set; }
    public float Abundancia { get; set; } // Valor entre 0.0 y 1.0

    public Microorganismo(string nombre, string tipo, string funcionBiologica, string secuenciaGenetica, float abundancia)
    {
        Nombre = nombre;
        Tipo = tipo;
        FuncionBiologica = funcionBiologica;
        SecuenciaGenetica = secuenciaGenetica;
        Abundancia = abundancia;
    }

    public void CompararMicroorganismos(Microorganismo otroMicroorganismo)
    {
        float similitudGenetica = CalcularSimilitudGenetica(otroMicroorganismo);
        float diferenciaAbundancia = Math.Abs(this.Abundancia - otroMicroorganismo.Abundancia);

        Console.WriteLine($"""
          Similitud genética: {similitudGenetica:P2}
          Diferencia de abundancia: {diferenciaAbundancia:P2}
          """);
    }

    private float CalcularSimilitudGenetica(Microorganismo otro)
    {
        string secuencia1 = this.SecuenciaGenetica;
        string secuencia2 = otro.SecuenciaGenetica;

        int minLength = Math.Min(secuencia1.Length, secuencia2.Length);
        int matches = 0;

        for (int i = 0; i < minLength; i++)
        {
            if (secuencia1[i] == secuencia2[i]) matches++;
        }

        return (float)matches / minLength;
    }

    public void ModificarAbundancia(float nuevaAbundancia)
    {
        Abundancia = nuevaAbundancia;
         Console.WriteLine($"Abundancia actualizada a {Abundancia:P2}");
    }

    public void MostrarInformacionDetallada()
    {
        Console.WriteLine($"Nombre: {Nombre}");
        Console.WriteLine($"Clasificación Taxonómica: {Tipo}");
        Console.WriteLine($"Función Biológica: {FuncionBiologica}");
        Console.WriteLine($"Secuencia Genética: {SecuenciaGenetica}");
        Console.WriteLine($"Abundancia: {Abundancia:P2}");
    }

    public string SimularInteraccionMicrobiana(Microorganismo otro)
    {
        string filo1 = ObtenerFilo(this.Tipo);
        string filo2 = ObtenerFilo(otro.Tipo);

        bool mismaFilo = filo1 == filo2;
        bool funcionesComplementarias = FuncionesComplementarias(this.FuncionBiologica, otro.FuncionBiologica);

        float diferenciaAbundancia = Math.Abs(this.Abundancia - otro.Abundancia);
        bool efectoAbundancia = diferenciaAbundancia > 0.5f;

        string resultado = "Interacción: ";

        if (mismaFilo && funcionesComplementarias)
        {
            resultado += "Simbiosis (relación beneficiosa)";
        }
        else if (EsAntagonista(this, otro))
        {
            resultado += "Antagonismo (competencia o inhibición)";
        }
        else if (efectoAbundancia)
        {
            resultado += this.Abundancia > otro.Abundancia
                ? $"{this.Nombre} inhibe a {otro.Nombre} por mayor abundancia"
                : $"{otro.Nombre} inhibe a {this.Nombre} por mayor abundancia";
        }
        else
        {
            resultado += "Interacción neutra (sin relación significativa)";
        }

        return resultado;
    }

    private string ObtenerFilo(string clasificacion)
    {
        // Asume que el segundo nivel corresponde al filo (por ejemplo: "Bacteria, Firmicutes, Lactobacillales")
        var partes = clasificacion.Split(',');
        return partes.Length > 1 ? partes[1].Trim() : "Desconocido";
    }

    private bool FuncionesComplementarias(string funcion1, string funcion2)
    {
      string

        funcion1 = funcion1.ToLower();
        funcion2 = funcion2.ToLower();

        return (funcion1.Contains("inmunidad") && funcion2.Contains("ácido graso")) ||
               (funcion2.Contains("inmunidad") && funcion1.Contains("ácido graso"));
    }

    private bool EsAntagonista(Microorganismo a, Microorganismo b)
    {
        // Simulación básica: uno probiótico y otro patógeno
        bool aProbiotico = a.FuncionBiologica.ToLower().Contains("mejora");
        bool bPatogeno = b.FuncionBiologica.ToLower().Contains("patógeno");

        bool bProbiotico = b.FuncionBiologica.ToLower().Contains("mejora");
        bool aPatogeno = a.FuncionBiologica.ToLower().Contains("patógeno");

        return (aProbiotico && bPatogeno) || (bProbiotico && aPatogeno);
    }

    public Microorganismo ClonarMicroorganismo()
    {
        return new Microorganismo(Nombre, Tipo, FuncionBiologica, SecuenciaGenetica, Abundancia);
    }
}
