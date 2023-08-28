using APIGuessBorderCountries.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace APIGuessBorderCountries.Pages
{
    public class IndexModel : PageModel
    {
        public static int flagNumber {  get; set; }
        public string correctAnswer { get; set; }
        public string wrongAnswer { get; set; }

        [BindProperty]
        public string inputUserGuess { get; set; }
        public string HelpingFlag { get; set; }
        public string HelpingName { get; set; }
        public static List<string> correctContries { get; set; }
        public List<correctData> correctFlags { get; set; }
        public List<string> contries { get; set; } = new List<string>();
        public static List<Root> Data { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            // Create an HttpClient instance
            using var httpClient = new HttpClient();

            try
            {
                // Make a GET request to the API endpoint
                var apiUrl = "https://restcountries.com/v3.1/all?fields=flags,name,borders,cca3,cioc"; // Replace with the actual API URL
                var response = await httpClient.GetStringAsync(apiUrl);

                // Deserialize the JSON response into C# objects
                Data = JsonConvert.DeserializeObject<List<Root>>(response);

                StartGame();

                return Page();
            }
            catch (Exception ex)
            {
                // Handle any errors, e.g., display an error message
                ViewData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return Page();
            }
        }

        public void StartGame()
        {
            List<string> tempList = new List<string>();
            Random rnd = new Random();
            flagNumber = rnd.Next(0, Data.Count - 1);
            int countryBorderAmount = Data[flagNumber].borders.Count;


            while (countryBorderAmount == 0)
            {
                flagNumber = rnd.Next(0, Data.Count - 1);
                countryBorderAmount = Data[flagNumber].borders.Count;
            }

            for (int i = 0; i < Data[flagNumber].borders.Count; i++)
            {
                tempList.Add(Data[flagNumber].borders[i].ToString());

            }
            correctContries = tempList;
            correctContries = ConvertCountryName(correctContries);
            
            contries = GetEveryCountry();
            correctFlags = new List<correctData>();
            correctFlags = GetCorrectFlags();

            HelpingName = Data[flagNumber].name.common;
            HelpingFlag = Data[flagNumber].flags.png;
        }

        public List<string> ConvertCountryName(List<string> flags)
        {
            for (int i = 0; i < Data.Count; i++)
            {
                for (int l = 0; l < flags.Count; l++)
                {
                    if (flags[l] == Data[i].cca3.ToString() || flags[l] == Data[i].cioc.ToString())
                    {
                        flags[l] = Data[i].name.common;
                    }
                }
            }
            return flags;
        }

        public List<correctData> GetCorrectFlags()
        {
            List<correctData> tempList = new List<correctData>();
            for (int i = 0; i < Data.Count; i++)
            {
                for (int l = 0; l < correctContries.Count; l++)
                {
                    if (correctContries[l] == Data[i].name.common)
                    {   correctData coda = new correctData();
                        coda.flag = Data[i].flags.png;
                        coda.name = Data[i].name.common;
                        tempList.Add(coda);
                    }
                }
            }
            return tempList;
        }

        public List<string> GetEveryCountry()
        {
            List<string> tempList = new List<string>();
            for (int i = 0; i < Data.Count; i++)
            {
                tempList.Add(Data[i].name.common);
            }
            return tempList;
        }

        public IActionResult OnPostUserGuess()
        {
            Console.WriteLine(inputUserGuess);

            correctFlags = GetCorrectFlags();
            HelpingName = Data[flagNumber].name.common;
            HelpingFlag = Data[flagNumber].flags.png;

            for (int i = 0; i < correctFlags.Count; i++)
            {
                if (inputUserGuess == correctFlags[i].name)
                {
                    wrongAnswer = "";
                    correctAnswer = $"Correct! {inputUserGuess} is a border country!";
                    correctFlags[i].isGuessed = true;
                    return Page();
                }
                else
                {
                    correctAnswer = "";
                    wrongAnswer = "Incorrect. Try again!";
                }
            }

            return Page();
        }

    }
}
