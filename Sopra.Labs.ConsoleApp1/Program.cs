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
        /// Muestra la tabla de multiplicar del numero pasado por consola dos veces, una por bucle for
        /// y otra por bucle while.
        /// </summary>
        static void MostrarTablaMultiplicar()
        {

            Console.WriteLine("Dame un valor");
            int numero;

            do
            {
                Console.Clear();
                Console.WriteLine("Dame un valor");      
            }
            while (!int.TryParse(Console.ReadLine(), out numero));

            //Bucle FOR
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

        /// <summary>
        /// Muestra los valores, entre dos numeros pasados por consolas, dado un salto pasado por consola.
        /// </summary>
        static void MostrarValores()
        {

            //DESDE EL VALOR DE INICIO AL VALOR FINAL
            //EN DIFERNTES SALTOS
            int numeroInicial;
            do
            {
                Console.Clear();
                Console.WriteLine("Dame valor incial");
          
            }
            while (!int.TryParse(Console.ReadLine(), out numeroInicial));

            int numeroFinal;

            do
            {
                Console.Clear();
                Console.WriteLine("Dame valor final");
            }
            while (!int.TryParse(Console.ReadLine(), out numeroFinal));

            int numeroDeSaltos;

            do
            {
                Console.Clear();
                Console.WriteLine("Dame valor saltos");
            }
            while (!int.TryParse(Console.ReadLine(), out numeroDeSaltos));

            if (numeroInicial>numeroFinal)
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

        /// <summary>
        /// Muestra el maximo, minimo, media y suma de un cantidad de valores pasados por consola
        /// </summary>
        static void CalcularValores()
        {
            //número de valores
            //almacenamos en un array
            //calculos, max, min, media, suma
            int valores;
            do
            {
                Console.Clear();
                Console.WriteLine("Dame cantidad de valores");
            }
            while (!int.TryParse(Console.ReadLine(), out valores));
     

            int[] valoresArray= new int[valores];
            for(int i=0; i<valores;i++)
            {
                int valor;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Dame nuevo valor:");
                }
                while (!int.TryParse(Console.ReadLine(), out valor));
                valoresArray[i] = valor;
            }

            //Calculos del maximo, minimo, suma y media
            int max =valoresArray[0], min = valoresArray[0], media, suma= valoresArray[0];           
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
            
        }

        /// <summary>
        /// Muestra la letra del DNI pasado por consola 
        /// </summary>
        static void CalcularLetraDNI()
        {
            //numero % 23 

            int numeroDelDNI;
            do
            {
                Console.Clear();
                Console.WriteLine("Dame el numero del DNI:");
            }
            while (!int.TryParse(Console.ReadLine(), out numeroDelDNI));            
            int num=numeroDelDNI%23;

            //posicion del array de las letras
            Console.WriteLine("Numero del DNI completo:");
            char[] letras = { 'T', 'R', 'W', 'A', 'G', 'M', 'Y', 'F', 'P', 'D', 'X', 'B', 'N', 'J', 'Z', 'S', 'Q', 'V', 'H', 'L', 'C', 'K', 'E' };      
            Console.WriteLine(numeroDelDNI+""+letras[num]); 
    
        }
    }
}
