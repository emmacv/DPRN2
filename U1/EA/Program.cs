using System;
using System.Text.RegularExpressions;

namespace U1.EA;
    public class MenuInteractivo
    {
        private List<Microorganismo> microorganismos;
        public MenuInteractivo(List<Microorganismo> microorganismos)
        {
            this.microorganismos = microorganismos;
        }
        public void MostrarMenu()
        {
            int opcion;
            do
            {
                Console.WriteLine("\n--- Gestión de Microorganismos ---");
                Console.WriteLine("1. Agregar Microorganismo");
                Console.WriteLine("2. Mostrar Información Detallada");
                Console.WriteLine("3. Modificar Abundancia");
                Console.WriteLine("4. Comparar dos Microorganismos");
                Console.WriteLine("5. Simular Interacción");
                Console.WriteLine("6. Clonar un Microorganismo");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AgregarMicroorganismo();
                        break;
                    case 2:
                        MostrarMicroorganismos();
                        break;
                    case 3:
                        ModificarAbundancia();
                        break;
                    case 4:
                        CompararMicroorganismos();
                        break;
                    case 5:
                        SimularInteraccion();
                        break;
                    case 6:
                        ClonarMicroorganismo();
                        break;
                    case 0:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            } while (opcion != 0);
        }

        private void AgregarMicroorganismo()
        {
            Console.Write("Nombre científico: ");
            string nombre = Console.ReadLine();
            Console.Write("Clasificación taxonómica: ");
            string tipo = Console.ReadLine();
            Console.Write("Función biológica: ");
            string funcion = Console.ReadLine();
            Console.Write("Secuencia genética: ");
            string secuencia = Console.ReadLine();
            Console.Write("Abundancia (0.0 - 1.0): ");
            float abundancia = float.Parse(Console.ReadLine());

            microorganismos.Add(new Microorganismo(nombre, tipo, funcion, secuencia, abundancia));
            Console.WriteLine("Microorganismo agregado exitosamente.");
        }

        private void MostrarMicroorganismos()
        {
            if (microorganismos.Count == 0)
            {
                Console.WriteLine("No hay microorganismos registrados.");
                return;
            }

            foreach (var micro in microorganismos)
            {
                micro.MostrarInformacionDetallada();
            }
        }

        private void ModificarAbundancia()
        {
            if (microorganismos.Count == 0)
            {
                Console.WriteLine("No hay microorganismos disponibles.");
                return;
            }

            Console.Write("Seleccione el índice del microorganismo (empezando desde 0): ");
            int indice = int.Parse(Console.ReadLine());

            if (indice >= 0 && indice < microorganismos.Count)
            {
                Console.Write("Nueva abundancia (0.0 - 1.0): ");
                float nuevaAbundancia = float.Parse(Console.ReadLine());
                microorganismos[indice].ModificarAbundancia(nuevaAbundancia);
            }
            else
            {
                Console.WriteLine("Índice inválido.");
            }
        }

        private void CompararMicroorganismos()
        {
            if (microorganismos.Count < 2)
            {
                Console.WriteLine("Se requieren al menos dos microorganismos para comparar.");
                return;
            }
            Console.Write("Índice del primer microorganismo: ");
            int indice1 = int.Parse(Console.ReadLine());
            Console.Write("Índice del segundo microorganismo: ");
            int indice2 = int.Parse(Console.ReadLine());

            if (indice1 >= 0 && indice1 < microorganismos.Count && indice2 >= 0 && indice2 < microorganismos.Count)
            {
                microorganismos[indice1].CompararMicroorganismos(microorganismos[indice2]);
            }
            else
            {
                Console.WriteLine("Índices inválidos.");
            }
        }

        private void SimularInteraccion()
        {
            if (microorganismos.Count < 2)
            {
                Console.WriteLine("Se requieren al menos dos microorganismos para simular interacciones.");
                return;
            }

            Console.Write("Índice del primer microorganismo: ");
            int indice1 = int.Parse(Console.ReadLine());
            Console.Write("Índice del segundo microorganismo: ");
            int indice2 = int.Parse(Console.ReadLine());

            if (indice1 >= 0 && indice1 < microorganismos.Count && indice2 >= 0 && indice2 < microorganismos.Count)
            {
                microorganismos[indice1].SimularInteraccionMicrobiana(microorganismos[indice2]);
            }
            else
            {
                Console.WriteLine("Índices inválidos.");
            }
        }

        private void ClonarMicroorganismo()
        {
            if (microorganismos.Count == 0)
            {
                Console.WriteLine("No hay microorganismos para clonar.");
                return;
            }

            Console.Write("Índice del microorganismo a clonar: ");
            int indice = int.Parse(Console.ReadLine());

            if (indice >= 0 && indice < microorganismos.Count)
            {
                var clon = microorganismos[indice].ClonarMicroorganismo();
                microorganismos.Add(clon);
                Console.WriteLine("Microorganismo clonado exitosamente.");
            }
            else
            {
                Console.WriteLine("Índice inválido.");
            }
        }
    }



class Principal {
    private List<Microorganismo> microorganismos = new List<Microorganismo>();
    private MenuInteractivo menuInteractivo;

    public Principal() {
        menuInteractivo = new MenuInteractivo(microorganismos);
    }

    static void Main() {
        Principal P = new();

        P.menuInteractivo.MostrarMenu();
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

    public void SimularInteraccionMicrobiana(Microorganismo otro)
    {
        string filo1 = ObtenerFilo(this.Tipo);
        string filo2 = ObtenerFilo(otro.Tipo);

        bool mismaFilo = filo1 == filo2;
        bool esSimbiosis = EsSimbiosis(otro);

        float diferenciaAbundancia = Math.Abs(this.Abundancia - otro.Abundancia);
        bool efectoAbundancia = diferenciaAbundancia > 0.5f;

        string resultado = """Resultado de interacción:
            Relación entre microorganismos:
            """;

        if (mismaFilo || esSimbiosis)
        {
            resultado += "Simbiosis (relación beneficiosa)";
        }
        else if (EsCompetencia(otro) || !mismaFilo)
        {
            resultado += "Antagonismo (competencia o inhibición)";
        }
        else
        {
            resultado += "Interacción neutra (sin relación significativa)";
        }

        var (masAbundante, menosAbundante) = this.Abundancia > otro.Abundancia ? (this, otro) : (otro, this);
        var diferencia = Math.Abs(masAbundante.Abundancia - menosAbundante.Abundancia);

        if (diferencia > 0.5f)
        {
            resultado += $"\nEfecto significativo en la abundancia por parte de {masAbundante.Nombre} sobre {menosAbundante.Nombre}";
        }

        Console.WriteLine(resultado);
    }

    private string ObtenerFilo(string clasificacion)
    {
        // Asume que el segundo nivel corresponde al filo (por ejemplo: "Bacteria, Firmicutes, Lactobacillales")
        var partes = clasificacion.Split(',');
        return partes.Length > 1 ? partes[1].Trim() : "Desconocido";
    }

  	private bool FuncionesComplementarias(string funcion1, string funcion2)
    {
        funcion1 = funcion1.ToLower();
        funcion2 = funcion2.ToLower();

        return (funcion1.Contains("inmunidad") && funcion2.Contains("ácido graso")) ||
               (funcion2.Contains("inmunidad") && funcion1.Contains("ácido graso"));
    }

		private bool EsBenefico(Microorganismo microorganismo)
		{
            const string efectos = "mejora|producción";
            const string funciones = "inmunidad intestinal|digestión|ácidos grasos|lactosa";
            Regex efectosRegex = new Regex(efectos, RegexOptions.IgnoreCase);
            Regex funcionesRegex = new Regex(funciones, RegexOptions.IgnoreCase);
            bool isEfectoBenefico = efectosRegex.IsMatch(microorganismo.FuncionBiologica);
            bool isBeneficio = efectosRegex.IsMatch(microorganismo.FuncionBiologica);

            return isEfectoBenefico && isBeneficio;
		}

		private bool EsPatogeno(Microorganismo microorganismo)
		{
            const string efectos = "infecciones|toxinas|patógeno";
            Regex efectosRegex = new Regex(efectos, RegexOptions.IgnoreCase);

            return efectosRegex.IsMatch(microorganismo.FuncionBiologica);
		}

    private bool EsSimbiosis(Microorganismo externo)
    {
        bool aBenefico = EsBenefico(this);
        bool bBenefico = EsBenefico(externo);

        return aBenefico && bBenefico;
    }

    private bool EsCompetencia(Microorganismo b)
    {
        bool aPatogeno = EsPatogeno(this);
        bool bPatogeno = EsPatogeno(b);

        bool aBenefico = EsBenefico(this);
        bool bBenefico = EsBenefico(b);

        return (aPatogeno && bBenefico) || (bPatogeno && aBenefico);

    }

    public Microorganismo ClonarMicroorganismo() => new Microorganismo(this.Nombre, this.Tipo, this.FuncionBiologica, this.SecuenciaGenetica, Abundancia);
}

