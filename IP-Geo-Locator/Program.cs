﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace IP_Geo_Locator
{

        internal class Program
        {
            static async Task Main(string[] args)
            {
                Console.Title = "Geolocator";
                Console.Write("Enter IP Address: ");
                string ip = Console.ReadLine();
                string url = $"https://ipinfo.io/{ip}/json";

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        Console.WriteLine("[+] Request Successfully Made");

                        string responseData= await response.Content.ReadAsStringAsync();
                        Data ipInfo = JsonConvert.DeserializeObject<Data>(responseData);

                        Console.Clear();
                        Console.WriteLine($"Country: {ipInfo.country}"); 
                        Console.WriteLine($"City: {ipInfo.city}");
                        Console.WriteLine($"Coordinates: {ipInfo.loc}");
                        Console.WriteLine($"Postal Code: {ipInfo.postal}"); 
                        Console.WriteLine($"Region: {ipInfo.region}");
                        Console.WriteLine($"ASN: {ipInfo.org}");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    // Catches any exceptions that may occur
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
