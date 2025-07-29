using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.Management.Infrastructure;


// USB Legacy Port Charging | Enable

var cimNamspace = "//./root/HP/InstrumentedBIOS";
var cimClass = "HP_BIOSSettingInterface";
var cimMethod = "SetBIOSSetting";
var paramCollection = new CimMethodParametersCollection
{
    CimMethodParameter.Create("Name", "Setup Password", CimFlags.None),
    CimMethodParameter.Create("Password", "<utf-16/>", CimFlags.None),
    CimMethodParameter.Create("Value", "<utf-16/>", CimFlags.None)
};

paramCollection["Password"].Value = "<utf-16/>Abcd";

var session = CimSession.Create(null);
var instance = session.EnumerateInstances(cimNamspace, cimClass).First();

var charset = Enumerable.Range(0, 127)
    .Select(x => (char)x)
    .Where(x => char.IsLetterOrDigit(x))
    .ToList();

var max = 8;
var min = 4;
var combos = new List<string>() { "" };
int counter = 0;
Stopwatch sw = Stopwatch.StartNew();
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
                if (counter % 1000 == 0)
                {
                    Console.WriteLine($"{counter} in {sw.Elapsed.ToString()}");
                    sw.Restart();
                }
                counter++;
                paramCollection["Password"].Value = $"<utf-16/>{token}";
                var go = session.InvokeMethod(instance, cimMethod, paramCollection);
                if (go.OutParameters["Return"].Value.ToString() == "6")
                {
                }
                else
                {
                    Console.WriteLine($"Valid {token} {go.OutParameters["Return"].Value.ToString()}");
                    return;
                }
            }
            newcombos.Add(token);
        }
    }
    combos = newcombos;
}


