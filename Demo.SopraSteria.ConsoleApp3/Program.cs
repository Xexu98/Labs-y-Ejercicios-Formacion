using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.Net;

namespace Demo.SopraSteria.ConsoleApp3
{
    internal class Program
    {
        private static HttpClient http = new HttpClient();
        static void Main(string[] args)
        {
            GetToken();
        }

        static void GeoLocalizacionIp()
        {
            //Get ip
            var http = new HttpClient();

            // Definir la direcion Base(parte de la URL que se repite en todas las llamadas)
            http.BaseAddress = new Uri("http://ip-api.com/json/");

            //definir cabeceras

            //definir el cuerpo del mensaje

            //Llamada al mircro servicio (api reset o http-based) utilizando el metodo/verbo correspondiente
            //metodo o verbo : get
            HttpResponseMessage response = http.GetAsync("193.146.141.207").Result;
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Leer el contenido del body del mensaje(propiedad Conten del mensaje de respuesta)
                string content = response.Content.ReadAsStringAsync().Result;


                //Deserailizar convertir json a objeto

                var data= JsonConvert.DeserializeObject<InfoIP>(content);
            }
            else
            {
                Console.WriteLine($"Error:{response.StatusCode}");
            }
        }
        static void ZipInfo2()
        {
            //Get http://api.zippopotam.us/es/28013

        

            http.BaseAddress = new Uri("http://api.zippopotam.us/es/");

            string zip = Console.ReadLine();

            HttpResponseMessage response = http.GetAsync(zip).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Leer el contenido del body del mensaje(propiedad Conten del mensaje de respuesta)
                string content = response.Content.ReadAsStringAsync().Result;


                //Deserailizar convertir json a objeto

                var data = JsonConvert.DeserializeObject<dynamic>(content);

                Console.WriteLine($"Codigo Postal {data["post code"]}");
                Console.WriteLine($"Pais {data.country} {data["country abbreviation"]}");
                foreach(var ciudad in data.places)
                {
                    Console.WriteLine($" {ciudad["place name"]} {ciudad.state} {ciudad["state abbreviation"]}");
                    Console.WriteLine($" {ciudad.longitude} {ciudad.latitude}");

                }

            }
            else
            {
                Console.WriteLine($"Error:{response.StatusCode}");
            }
        }
        static void ZipInfo3()
        {
            //Get http://api.zippopotam.us/es/28013

     

            http.BaseAddress = new Uri("http://api.zippopotam.us/es/");

            var data = http.GetFromJsonAsync<ZipInfo>(Console.ReadLine()).Result;


            Console.WriteLine($"Codigo Postal {data.PostalCode}");
            Console.WriteLine($"Pais {data.Country} {data.CountryCode}");
            foreach (var ciudad in data.Places)
            {
                Console.WriteLine($" {ciudad.Name} {ciudad.State} {ciudad.StateCode}");
                Console.WriteLine($" {ciudad.Longitude} {ciudad.Latitude}");

            }

        }
        static void ZipInfo4()
        {
            //Get http://api.zippopotam.us/es/28013

          

            http.BaseAddress = new Uri("http://api.zippopotam.us/es/");

            string content = http.GetStringAsync(Console.ReadLine()).Result;
            var data = JsonConvert.DeserializeObject<ZipInfo>(content);

            Console.WriteLine($"Codigo Postal {data.PostalCode}");
            Console.WriteLine($"Pais {data.Country} {data.CountryCode}");
            foreach (var ciudad in data.Places)
            {
                Console.WriteLine($" {ciudad.Name} {ciudad.State} {ciudad.StateCode}");
                Console.WriteLine($" {ciudad.Longitude} {ciudad.Latitude}");

            }


        }
        static void Eco()
        {
            

            http.BaseAddress = new Uri("https://postman-echo.com/");

            http.DefaultRequestHeaders.Clear();

            http.DefaultRequestHeaders.Add("x-param-1", "D E M O");

            http.DefaultRequestHeaders.Add("User-Agent", "Ejercicio Demo");
            //http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            http.DefaultRequestHeaders.Add("Accept", "application/json");

            var zip = new ZipInfo()
            {
                PostalCode = "28014",
                Country = "Spain",
                CountryCode = "SP",
                Places = new List<PlaceZipInfo> { 
                    new PlaceZipInfo() { Name = "Madrid", State = "Madrid", StateCode = "M" }
                }
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(zip),Encoding.UTF8,"application/json");


            var zip2 = new
            {
                Code="28014",
                Pais="Spain",
                CodigoISO="SP",
                Referencia=521,
                Sitios= new dynamic[] {
                    new { Nombre = "Madrid", Comunidad = "Madrid", CodigoISO = "M" },
                    new { Nombre = "Alcala de Henares", Comunidad = "Madrid", CodigoISO = "M" } 
                }
            };

            var content2 = new StringContent(
               JsonConvert.SerializeObject(zip2), Encoding.UTF8, "application/json");

            var response = http.PostAsync("post?param1=demo&param2=Hola", content).Result;

            if(response.StatusCode==System.Net.HttpStatusCode.OK)
            {
                var responseBody=response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(responseBody);    
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
        static void GetStudent()
        {
           
            Console.Write("ID Estudiante: ");

            var response = http.GetAsync(Console.ReadLine()).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = JsonConvert.DeserializeObject<Student>(response.Content.ReadAsStringAsync().Result);

                Console.WriteLine($"Nombre: {data.Firstname} {data.Lastname}");
                Console.WriteLine($"Nacimiento: {data.DateOfBirth}");
                Console.WriteLine($"Clase: {data.ClassId}");
            }
            else Console.WriteLine($"Error: {response.StatusCode}.");
        }
        static void GetStudent2()
        {
            Console.Write("ID Estudiante: ");

            try
            {
                var data = http.GetFromJsonAsync<Student>(Console.ReadLine()).Result;
                if (data != null)
                {
                    Console.WriteLine($"Nombre: {data.Firstname} {data.Lastname}");
                    Console.WriteLine($"Nacimiento: {data.DateOfBirth}");
                    Console.WriteLine($"Clase: {data.ClassId}");
                }
                else Console.WriteLine($"Estudiante no encontrado.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Estudiante no encontrado.");
                Console.WriteLine($"Error: {e.Message}");
            }
        }
        static void PostStudent()
        {
            var http = new HttpClient();

            http.BaseAddress = new Uri("http://school.labs.com.es/api/students/");


            var student = new Student()
            {
                Id = 0,
                Firstname = "Juan",
                Lastname = "Garcia",
                DateOfBirth =new DateTime(1997-07-02)
                , ClassId =2
            };

            var studentJSON= JsonConvert.SerializeObject(student);
            var content = new StringContent(studentJSON, Encoding.UTF8,"application/json");
            var response = http.PostAsync("",content).Result;

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var data = JsonConvert.DeserializeObject<Student>(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine($"ID: {data.Id}");
                Console.WriteLine($"Nombre: {data.Firstname} {data.Lastname}");
                Console.WriteLine($"Nacimiento: {data.DateOfBirth}");
                Console.WriteLine($"Clase: {data.ClassId}");
            }
            else Console.WriteLine($"Error: {response.StatusCode}.");
        }
        static void PostStudent2()
        {
            var http = new HttpClient();

            http.BaseAddress = new Uri("http://school.labs.com.es/api/students/");

            var student = new Student()
            {
                Id = 0,
                Firstname = "Juan",
                Lastname = "Garcia",
                DateOfBirth = new DateTime(1997 - 07 - 02)
                ,
                ClassId = 2
            };
            
            var studentJSON = JsonConvert.SerializeObject(student);
            var content = new StringContent(studentJSON, Encoding.UTF8, "application/json");
            var response = http.PostAsync("", content).Result;
            if (response.StatusCode == HttpStatusCode.Created)
            {
                var data = JsonConvert.DeserializeObject<Student>(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine($"ID: {data.Id}");
                Console.WriteLine($"Nombre: {data.Firstname} {data.Lastname}");
                Console.WriteLine($"Nacimiento: {data.DateOfBirth}");
                Console.WriteLine($"Clase: {data.ClassId}");
            }
            else Console.WriteLine($"Error: {response.StatusCode}.");

        }
        static void UpdateStudent()
        {
            var http = new HttpClient();

            http.BaseAddress = new Uri("http://school.labs.com.es/api/students/");


            HttpResponseMessage response = http.GetAsync("").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string content1 = http.GetStringAsync(Console.ReadLine()).Result;
                var data = JsonConvert.DeserializeObject<Student>(content1);

                Console.WriteLine($"{data.Id} {data.Firstname} {data.Lastname} {data.DateOfBirth} {data.ClassId}");

                Console.WriteLine("Dame nombre:");
                data.Firstname = Console.ReadLine();

                Console.WriteLine("Dame apellidos:");
                data.Lastname = Console.ReadLine();

                Console.WriteLine("Dame fecha de nacimiento:");
                data.DateOfBirth = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Id de la clase:");
                data.ClassId = int.Parse(Console.ReadLine());

                var studentJSON = JsonConvert.SerializeObject(data);
                var content2 = new StringContent(studentJSON, Encoding.UTF8, "application/json");
                HttpResponseMessage response2 = http.PutAsync("", content2).Result;

                if (response2.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine(content2);
                }
                else
                {
                    Console.WriteLine($"Error:{response.StatusCode}");
                }
            }
            else
            {
                Console.WriteLine($"Error:{response.StatusCode}");
            }

        }
        static void DeleteStudent()
        {
            var http = new HttpClient();

            http.BaseAddress = new Uri("http://school.labs.com.es/api/students/");
            Student student= new Student();
            student.Id = 0;

            Console.WriteLine("Dame nombre:");

            Console.WriteLine("Dame apellidos:");

            Console.WriteLine("Dame fecha de nacimiento:");

            Console.WriteLine("Id de la clase:");

            var studentJSON = JsonConvert.SerializeObject(student);
            var content = new StringContent(studentJSON, Encoding.UTF8, "application/json");

            var response = http.DeleteAsync("8").Result;
            Console.WriteLine(Environment.NewLine);
            if (response.StatusCode == HttpStatusCode.OK) Console.WriteLine($"Eliminado");
            else Console.WriteLine($"Error: {response.StatusCode}.");
        }
        static void GetToken()
        {
            var http = new HttpClient();

            http.BaseAddress = new Uri("https://openapi.emtmadrid.es/v2/mobilitylabs/");

            http.DefaultRequestHeaders.Add("X-ClientId", "");
            http.DefaultRequestHeaders.Add("passKey", "");
            try
            {
                HttpResponseMessage response = http.GetAsync("user/login/").Result;
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    http.DefaultRequestHeaders.Clear();

                    var content = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<dynamic>(content);

                    Console.WriteLine($"Token: {data["data"][0]["accessToken"]}");
                    string token = data["data"][0]["accessToken"];
                    Parking(token);
                }
                else
                {
                    Console.WriteLine($"Error:{response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

        }
        static void BusStops(string token)
        {
            var http = new HttpClient();

            http.BaseAddress = new Uri("https://openapi.emtmadrid.es/v2/transport/busemtmad/stops/");

            http.DefaultRequestHeaders.Add("accessToken", token);


            var config = new 
            {
                cultureInfo = "ES",
                Text_StopRequired_YN = "Y",
                Text_EstimationsRequired_YN = "Y",
                Text_IncidencesRequired_YN = "N",
                DateTime_Referenced_Incidencies_YYYYMMDD = "20220405"
            };

            var configJSON = JsonConvert.SerializeObject(config);
            var content = new StringContent(configJSON, Encoding.UTF8, "application/json");

            Console.WriteLine("Dame numero de linea:");

            var response = http.PostAsync(Console.ReadLine()+"/arrives/", content).Result;

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content2 = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<dynamic>(content2);
        
                foreach(var linea in data["data"][0]["Arrive"])
                {
                    Console.WriteLine("==================================================");
                    Console.WriteLine($"Linea: {linea["line"]}");
                    Console.WriteLine($"Destino: {linea["destination"]}");
                    Console.WriteLine($"Distancia: {linea["DistanceBus"]} m");
                    Console.WriteLine($"Tiempo: {linea["estimateArrive"]} s");
                    Console.WriteLine("==================================================");

                }

            }
            else
            {
                Console.WriteLine($"Error:{response.StatusCode}");
            }

        }
        static void Parking(string token)
        {
            var http = new HttpClient();

            http.BaseAddress = new Uri("https://openapi.emtmadrid.es/v2/");

            http.DefaultRequestHeaders.Add("accessToken", token);

            HttpResponseMessage response = http.GetAsync("citymad/places/parkings/availability/").Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

               http.DefaultRequestHeaders.Clear();
               var content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<ParkingResponse>( response.Content.ReadAsStringAsync().Result);

                var numPlazas = data.Data.
                    Where(r=>r.FreeParking != null).
                    Sum(r=>r.FreeParking);
                Console.WriteLine("========================<      >==========================");
                Console.WriteLine($"Numero de plazas de parking totales: {numPlazas}");
                Console.WriteLine("========================<      >==========================");
                Console.WriteLine("");
                var parkings= data.Data.
                    Where(r => r.FreeParking != null)
                    .OrderByDescending(r => r.FreeParking)
                    .ToList();

                foreach(var parking in parkings)
                {
                    Console.WriteLine("==================================================");
                    Console.WriteLine($"Id: {parking.Id}");
                    Console.WriteLine($"Nombre: {parking.Name}");
                    Console.WriteLine($"Numero de plazas: {parking.FreeParking}");
                    Console.WriteLine("==================================================");
                }
                

            }
            else
            {
                Console.WriteLine($"Error:{response.StatusCode}");
            }
        }

    }
    public class ParkingResponse
    {
        public string Code { get; set; }
        public string Description { get; set; }

        [JsonProperty("datetime")]
        public DateTime DateTimeData { get; set; }
        public List<ParkingData> Data { get; set; }
    }
    public class ParkingData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FreeParking { get; set; }
    }
    public class InfoIP
    {
        public string status { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public string region { get; set; }
        public string regionName { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string timezone { get; set; }
        public string isp { get; set; }
        public string org { get; set; }
        public string query { get; set; }
        public decimal lat { get; set; }
        public decimal lon { get; set; }

        //public string as { get; set; }
    }
    public class InfoIP2
    {
        public string country { get; set; }
        public string city { get; set; }
        public string query { get; set; }

        //public string as { get; set; }
    }
    public class ZipInfo
    {
        [JsonProperty("post code")]
        public string PostalCode { get; set; }
        public string Country { get; set; }

        [JsonProperty("country abbreviation")]
        public string CountryCode { get; set; }
        public List<PlaceZipInfo> Places { get; set; }
    }
    public class PlaceZipInfo
    {
        [JsonProperty("place name")]
        public string Name { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string State { get; set; }

        [JsonProperty("state abbreviation")]
        public string StateCode { get; set; }
    }
    public class Student
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassId { get; set; }
    }

}
