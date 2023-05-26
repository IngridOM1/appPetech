using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using appPetech.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

//using System.ComponentModel.DataAnnotations.Schema;

namespace appPetech.Integration
{
    public class DogBreedIntegration
    {
        public int Id { get; set; }

        private readonly ILogger<DogBreedIntegration> _logger;

        private const string API_URL = "https://dog-breeds2.p.rapidapi.com/dog_breeds";
        private const string API_KEY = "b091a8b5f0msh3db7936ea08f65ep1676d7jsnd6e6aa3888ec";
        private const string API_HEADER_KEY = "X-RapidAPI-Key";

        private readonly HttpClient httpClient;

        public DogBreedIntegration(ILogger<DogBreedIntegration> logger){
            _logger=logger;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add(API_HEADER_KEY,API_KEY);

        }

        public async Task<List<RazaPerros>> GetDogBreedsAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync(API_URL);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<RazaPerros> dogBreeds = await JsonSerializer.DeserializeAsync<List<RazaPerros>>(responseStream, jsonOptions);
                return dogBreeds;
            }
            else
            {
                throw new Exception("Error al obtener los datos de la API.");
            }
        }




        /*
        public async Task<List<RazaPerros>> GetDogBreedsAsync(){
            HttpResponseMessage response = await httpClient.GetAsync(API_URL);
            if (response.IsSuccessStatusCode)
            {
                List<RazaPerros> dogBreeds = await response.Content.ReadFromJsonAsync<List<RazaPerros>>();
                return dogBreeds;
            }
            else
            {
                throw new Exception("Error al obtener los datos de la API.");
            }
        }

        */

        //GET https://dog-breeds2.p.rapidapi.com/dog_breeds HTTP/1.1
        //Accept: application/json
        //X-RapidAPI-Key: b091a8b5f0msh3db7936ea08f65ep1676d7jsnd6e6aa3888ec









    }

}