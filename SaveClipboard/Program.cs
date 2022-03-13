using System.Text.Json;

namespace SaveClipboard
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            string fileName = Directory.GetCurrentDirectory() + "\\config.json";
            Config C;
            if (!File.Exists(fileName))
            {
                C = GetDefaultConfig();
                string str = JsonSerializer.Serialize(
                    C, new JsonSerializerOptions { WriteIndented = true }
                );

                File.WriteAllText(fileName, str);
            } else
            {
                Config? temp = JsonSerializer.Deserialize<Config>(File.ReadAllText(fileName));
                if (temp == null)
                {
                    temp = GetDefaultConfig();
                }
                C = temp;
            }

            SaveClipboard Instance = new(C.PathToSave);

            Instance.save();
        }

        private static Config GetDefaultConfig()
        {
            return new() { PathToSave = Directory.GetCurrentDirectory() + "\\saves\\" };
        }
    }

    public class Config 
    { 
        
        public string PathToSave { get => _PathToSave; set => _PathToSave = value ?? Directory.GetCurrentDirectory() + "\\saves\\"; }
        private string _PathToSave = Directory.GetCurrentDirectory() + "\\saves\\";
    }
}