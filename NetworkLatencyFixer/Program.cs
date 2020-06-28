using System;

namespace NetworkLatencyFixer
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                PrintUsage();
                return;
            }

            var manager = new RegistryManager();
            switch (args[0])
            {
                case "-l":
                    var networks = manager.GetAllNetworks();
                    foreach (var kv in networks)
                    {
                        Console.WriteLine($"{kv.Key} - {kv.Value}");
                    }
                    break;
                case "-f":
                    AddValuesToRegistry(manager, args[1]);
                    break;
                case "-d":
                    RemoveValuesFromRegistry(manager, args[1]);
                    break;
                default:
                    PrintUsage();
                    break;
            }
        }

        static void AddValuesToRegistry(RegistryManager manager, string interfaceId)
        {
            manager.SetValues(interfaceId);
        }

        static void RemoveValuesFromRegistry(RegistryManager manager, string interfaceId)
        {
            manager.DeleteValues(interfaceId);
        }

        static void PrintUsage()
        {
            Console.WriteLine("NetworkLatencyFixer -l - List all network interfaces with ids");
            Console.WriteLine("NetworkLatencyFixer -f <id> - Apply fix to interface with specified id");
            Console.WriteLine("NetworkLatencyFixer -d <id> - Remove fix from interface with speciefied id");
        }
    }
}
