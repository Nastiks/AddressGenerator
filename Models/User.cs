namespace AddressGenerator.Models
{
    public class User
    {
        private static SimpleRandom? random;

        private string[] Gender = { "male", "female" };
        
        public int Id { get; set; }
        public int Number { get; set; }
        public string? FullName { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }  
        public double NumberErrors { get; set; }

        public User(int seed, string country, double numberErrors)
        {
            random = new SimpleRandom(seed);
            Id = seed;
            NumberErrors = numberErrors;
            Country = country;
            FullName = GenerateName(Country);
            Address = GenerateAddress(Country);
            Phone = GeneratePhone(Country);
        }

        public User GenerateUser(User user)
        {
            return DefineErrorField(user, user.NumberErrors);
        }

        public string GenerateName(string country)
        {
            string gender = random!.Next(2) == 0 ? Gender[0] : Gender[1];
            string[] firstNames = File.ReadAllLines($"C:\\sources\\Task5Work\\AddressGenerator\\wwwroot\\{country}\\{gender}_firstName.txt");
            string[] lastNames = File.ReadAllLines($"C:\\sources\\Task5Work\\AddressGenerator\\wwwroot\\{country}\\{gender}_lastName.txt");
            string fullName = $"{firstNames[random.Next(firstNames.Length)]} {lastNames[random.Next(lastNames.Length)]}";
            return fullName;
        }

        public string GenerateAddress(string country)
        {
            string[] regions = File.ReadAllLines($"C:\\sources\\Task5Work\\AddressGenerator\\wwwroot\\{country}\\region.txt");
            string[] cities = File.ReadAllLines($"C:\\sources\\Task5Work\\AddressGenerator\\wwwroot\\{country}\\city.txt");
            string[] streets = File.ReadAllLines($"C:\\sources\\Task5Work\\AddressGenerator\\wwwroot\\{country}\\street.txt");
            string address = $"{country} {regions[random!.Next(regions.Length)]} {cities[random!.Next(cities.Length)]} {streets[random!.Next(streets.Length)]} {random.Next(150)}";
            return address;
        }

        public string GeneratePhone(string country)
        {
            string[] codeCountry = File.ReadAllLines($"C:\\sources\\Task5Work\\AddressGenerator\\wwwroot\\{country}\\phone_codeCountry.txt");
            string[] regionCodes = File.ReadAllLines($"C:\\sources\\Task5Work\\AddressGenerator\\wwwroot\\{country}\\phone_regionCode.txt");
            string phone = $"{codeCountry[0]} {regionCodes[random!.Next(regionCodes.Length)]} {random.Next(00000, 99999)}";
            return phone;
        }

        public static User DefineErrorField(User user, double numberErrors)
        {
            for (int i = 1; i <= numberErrors; i++)
            {
                switch (random!.Next(1, 4))
                {
                    case 1:
                        user.FullName = DistributeErrors(user.FullName!, user.Country!);
                        break;
                    case 2:
                        user.Address = DistributeErrors(user.Address!, user.Country!);
                        break;
                    default:
                        user.Phone = DistributeErrors(user.Phone!, user.Country!);
                        break;
                }
            }
            return user;            
        }

        public static string DistributeErrors(string value, string country)
        {
            switch (random!.Next(1, 3))
            {
                case 1:
                    return DeletingSymbol(value);
                    case 2:
                    return AddingSymbol(value, country);
                default:
                    return RearrangeSymbol(value);
            }
        }

        public static string DeletingSymbol(string value) => value.Remove(random!.Next(value.Length), 1);
        
        public static string AddingSymbol(string value, string country)
        {
            string[] letters = File.ReadAllLines($"C:\\sources\\Task5Work\\AddressGenerator\\wwwroot\\{country}\\letter.txt");
            string alphabet = letters[0];
            string result = value.Insert(random!.Next(value.Length), (alphabet[random!.Next(random!.Next(value.Length))]).ToString());
            return result;
        }

        public static string RearrangeSymbol(string value) => value[1] + value[0] + value.Substring(2);        
    }
}
