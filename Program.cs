using System.Management;

ManagementScope ms = new ManagementScope("\\\\.\\root\\HP\\InstrumentedBIOS");

ms.Connect();
ManagementObjectSearcher searcher =new ManagementObjectSearcher("SELECT * FROM HP_BIOSPassword");
searcher.Scope = ms;

foreach (ManagementObject obj in searcher.Get())
{
    foreach (var prop in obj.Properties) {
        
        Console.WriteLine($"{prop.Name}:{prop.Value}");    
    }
    Console.WriteLine("\n");
    // show the service
    //Console.WriteLine(service.ToString());
}


//string query = "SELECT * FROM Win32_OperatingSystem";
//ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

//foreach (ManagementObject obj in searcher.Get())
//{
//    Console.WriteLine("OS Name: " + obj["Caption"]);
//    Console.WriteLine("Version: " + obj["Version"]);
//    Console.WriteLine("Architecture: " + obj["OSArchitecture"]);
//}
return;

var charset = Enumerable.Range(0, 127)
    .Select(x => (char)x)
    .Where(x => char.IsLetterOrDigit(x) || char.IsPunctuation(x))
    .ToList();

var max = 8;
var min = 4;
var combos = new List<string>() { "" };
for (var depth = 1; depth <= max; depth++)
{
    var newcombos = new List<string>();
    foreach (var combo in combos)
    {
        foreach (var c in charset)
        {
            var token = $"{combo}{c}";
            if (token.Length >= min)
            {
                Console.WriteLine(token);
            }
            newcombos.Add(token);
        }
    }
    combos = newcombos;
}


