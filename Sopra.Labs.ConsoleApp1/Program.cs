using System;

namespace Sopra.Labs.ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CalcularLetraDNI();
        }

        /// <summary>
        /// Muestra la tabla de multiplicar del numero pasado 
        /// </summary>
        /// <param name="num">Número que se pasa para calcular su tabla de tipo int</param>
        static void MostrarTablaMultiplicar()
        {

            Console.WriteLine("Dame un valor");
            int numero = int.Parse(Console.ReadLine());
            Console.Clear();
            //FOR
            Console.WriteLine("Bucle for");
            for (int i = 0; i < 11; i++)
            {
                Console.WriteLine(numero * i);
            }

            //WHILE
            Console.WriteLine("Bucle while");
            int j = 0;
            while (j < 11)
            {
                Console.WriteLine(numero * j);
                j++;
            }

        }
        static void MostrarValores()
        {

            //DESDE EL VALOR DE INICIO AL VALOR FINAL
            //EN DIFERNTES SALTOS
            Console.WriteLine("Dame valor incial");
            int numeroInicial = int.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Dame valor final");
            int numeroFinal = int.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Dame valor saltos");
            int numeroDeSaltos = int.Parse(Console.ReadLine());
            Console.Clear();

            if(numeroInicial>numeroFinal)
            {
                while (numeroInicial >= numeroFinal)
                {
                    Console.WriteLine(numeroInicial);
                    numeroInicial -= numeroDeSaltos;
                }
            }
            else
            {
                while (numeroInicial <= numeroFinal)
                {
                    Console.WriteLine(numeroInicial);
                    numeroInicial += numeroDeSaltos;
                }
            }      
          
        }

        static void CalcularValores()
        {
            Console.WriteLine("Dame cantidad de valores");
            int valores = int.Parse(Console.ReadLine());
            Console.Clear();

            int[] valoresArray= new int[valores];
            for(int i=0; i<valores;i++)
            {
                Console.WriteLine("Dame nuevo valor:");
                int valor = int.Parse(Console.ReadLine());
                valoresArray[i] = valor;
                Console.Clear();
            }
            int max=valoresArray[0], min = valoresArray[0], media, suma= valoresArray[0];
            
            for(int i = 1; i < valores; i++)
            {
                if( max < valoresArray[i])
                {
                    max = valoresArray[i];
                }
                if (min > valoresArray[i])
                {
                    min = valoresArray[i];
                }
                suma += valoresArray[i];
            }
            media = suma / valores;
            Console.WriteLine("Maximo: "+max);
            Console.WriteLine("Minimo: " + min);
            Console.WriteLine("Media: " + media);
            Console.WriteLine("Suma: " + suma);
            //número de valores
            //almacenamos en un array
            //calculos, max, min, media, suma
        }

        static void CalcularLetraDNI()
        {
            Console.WriteLine("Dame el numero del DNI:");
            //numero % 23 

            int numeroDelDNI =  int.Parse(Console.ReadLine());
            int num=numeroDelDNI%23;
            //posicion del array de las letras
            char[] letras = { 'T', 'R', 'W', 'A', 'G', 'M', 'Y', 'F', 'P', 'D', 'X', 'B', 'N', 'J', 'Z', 'S', 'Q', 'V', 'H', 'L', 'C', 'K', 'E' };
            Console.Clear();
            Console.WriteLine(letras[num]); 
    
        }
    }
}
