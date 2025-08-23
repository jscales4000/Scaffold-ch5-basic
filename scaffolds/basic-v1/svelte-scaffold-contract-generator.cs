using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class SvelteScaffoldContractGenerator
{
    public static void Main()
    {
        Console.WriteLine("=== Svelte Scaffold Contract Generation ===");
        
        var contract = CreateSvelteScaffoldContract();
        
        // Generate .cce file
        GenerateCCEFile(contract);
        
        // Generate .chd file
        GenerateCHDFile(contract);
        
        // Generate .ccz file (ZIP archive)
        GenerateCCZFile(contract);
        
        Console.WriteLine("\n✅ Contract generation complete!");
        Console.WriteLine("Files created:");
        Console.WriteLine("  - SvelteScaffoldTouchPanel.cce");
        Console.WriteLine("  - SvelteScaffoldTouchPanel.chd");
        Console.WriteLine("  - SvelteScaffoldTouchPanel.ccz");
    }
    
    private static CrestronContract CreateSvelteScaffoldContract()
    {
        var contractId = GenerateId("contract");
        var componentId = GenerateId("component");
        
        return new CrestronContract
        {
            Id = contractId,
            Name = "SvelteScaffoldTouchPanel",
            Description = "Complete touch panel contract for Svelte scaffold with source selection, audio controls, and system functions",
            Version = "1.0.0.0",
            Components = new List<ContractComponent>
            {
                new ContractComponent
                {
                    Id = componentId,
                    Name = "TouchPanelController",
                    Description = "Main touch panel controller with all UI interactions",
                    Commands = new List<ComponentAttribute>
                    {
                        // Source Selection Commands (6 sources from App.svelte)
                        new ComponentAttribute { Name = "SelectPC", DataType = 1, Type = "command", Description = "Select PC source" },
                        new ComponentAttribute { Name = "SelectLaptop", DataType = 1, Type = "command", Description = "Select Laptop source" },
                        new ComponentAttribute { Name = "SelectAirmedia", DataType = 1, Type = "command", Description = "Select Airmedia source" },
                        new ComponentAttribute { Name = "SelectDocCam", DataType = 1, Type = "command", Description = "Select Document Camera source" },
                        new ComponentAttribute { Name = "SelectLecternHDMI", DataType = 1, Type = "command", Description = "Select Lectern HDMI source" },
                        new ComponentAttribute { Name = "SelectFloorPlate", DataType = 1, Type = "command", Description = "Select Floor Plate source" },
                        
                        // Audio Controls (from Footer.svelte)
                        new ComponentAttribute { Name = "VolumeUp", DataType = 1, Type = "command", Description = "Volume up momentary button press" },
                        new ComponentAttribute { Name = "VolumeDown", DataType = 1, Type = "command", Description = "Volume down momentary button press" },
                        new ComponentAttribute { Name = "MuteToggle", DataType = 1, Type = "command", Description = "Mute toggle button press" },
                        new ComponentAttribute { Name = "MicrophoneToggle", DataType = 1, Type = "command", Description = "Microphone mute toggle" },
                        
                        // System Controls (from Header.svelte and Footer.svelte)
                        new ComponentAttribute { Name = "PowerButton", DataType = 1, Type = "command", Description = "System power button press" },
                        new ComponentAttribute { Name = "HelpButton", DataType = 1, Type = "command", Description = "Help button press" },
                        new ComponentAttribute { Name = "SettingsButton", DataType = 1, Type = "command", Description = "Settings button press" },
                    },
                    Feedbacks = new List<ComponentAttribute>
                    {
                        // Source Selection Feedbacks
                        new ComponentAttribute { Name = "PCSelected", DataType = 1, Type = "feedback", Description = "PC source is selected feedback" },
                        new ComponentAttribute { Name = "LaptopSelected", DataType = 1, Type = "feedback", Description = "Laptop source is selected feedback" },
                        new ComponentAttribute { Name = "AirmediaSelected", DataType = 1, Type = "feedback", Description = "Airmedia source is selected feedback" },
                        new ComponentAttribute { Name = "DocCamSelected", DataType = 1, Type = "feedback", Description = "Document Camera source is selected feedback" },
                        new ComponentAttribute { Name = "LecternHDMISelected", DataType = 1, Type = "feedback", Description = "Lectern HDMI source is selected feedback" },
                        new ComponentAttribute { Name = "FloorPlateSelected", DataType = 1, Type = "feedback", Description = "Floor Plate source is selected feedback" },
                        
                        // Audio Feedbacks
                        new ComponentAttribute { Name = "IsMuted", DataType = 1, Type = "feedback", Description = "Audio is muted feedback (for red icon)" },
                        new ComponentAttribute { Name = "IsMicrophoneMuted", DataType = 1, Type = "feedback", Description = "Microphone is muted feedback (for red icon)" },
                        new ComponentAttribute { Name = "VolumeLevel", DataType = 2, Type = "feedback", Description = "Current volume level (0-65535) for gauge display" },
                        
                        // System Status Feedbacks
                        new ComponentAttribute { Name = "SystemPowered", DataType = 1, Type = "feedback", Description = "System power status feedback" },
                    }
                }
            },
            Specifications = new List<ComponentSpecification>
            {
                new ComponentSpecification
                {
                    Id = GenerateId("spec"),
                    ComponentId = componentId,
                    InstanceName = "TouchPanel",
                    ControlJoinId = 1
                }
            }
        };
    }
    
    private static void GenerateCCEFile(CrestronContract contract)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        var json = JsonSerializer.Serialize(contract, options);
        File.WriteAllText($"{contract.Name}.cce", json);
        Console.WriteLine($"✓ Generated {contract.Name}.cce");
    }
    
    private static void GenerateCHDFile(CrestronContract contract)
    {
        var component = contract.Components[0];
        var spec = contract.Specifications[0];
        
        var chd = $@"[
ObjTp=FSgntr
Sgntr=CHD
RelVrs=1
]
[
ObjTp=Hd
ProjectFile={contract.Name}
ContractID={contract.Id}
DateTimeUTC={DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff}
]
[
ObjTp=Symbol
Name={component.Name}
Code=1
ControlJoinId={spec.ControlJoinId}d
SmplCName={component.Name}.cs
CompCName={component.Name}
]
[
ObjTp=Sgn
H=1
Tp=1
SgnNm=SelectPC
]
[
ObjTp=Sgn
H=2
Tp=1
SgnNm=SelectLaptop
]
[
ObjTp=Sgn
H=3
Tp=1
SgnNm=SelectAirmedia
]
[
ObjTp=Sgn
H=4
Tp=1
SgnNm=SelectDocCam
]
[
ObjTp=Sgn
H=5
Tp=1
SgnNm=SelectLecternHDMI
]
[
ObjTp=Sgn
H=6
Tp=1
SgnNm=SelectFloorPlate
]
[
ObjTp=Sgn
H=7
Tp=1
SgnNm=VolumeUp
]
[
ObjTp=Sgn
H=8
Tp=1
SgnNm=VolumeDown
]
[
ObjTp=Sgn
H=9
Tp=1
SgnNm=MuteToggle
]
[
ObjTp=Sgn
H=10
Tp=1
SgnNm=MicrophoneToggle
]
[
ObjTp=Sgn
H=11
Tp=1
SgnNm=PowerButton
]
[
ObjTp=Sgn
H=12
Tp=1
SgnNm=HelpButton
]
[
ObjTp=Sgn
H=13
Tp=1
SgnNm=SettingsButton
]
[
ObjTp=Sgn
H=1
Tp=2
SgnNm=PCSelected
]
[
ObjTp=Sgn
H=2
Tp=2
SgnNm=LaptopSelected
]
[
ObjTp=Sgn
H=3
Tp=2
SgnNm=AirmediaSelected
]
[
ObjTp=Sgn
H=4
Tp=2
SgnNm=DocCamSelected
]
[
ObjTp=Sgn
H=5
Tp=2
SgnNm=LecternHDMISelected
]
[
ObjTp=Sgn
H=6
Tp=2
SgnNm=FloorPlateSelected
]
[
ObjTp=Sgn
H=7
Tp=2
SgnNm=IsMuted
]
[
ObjTp=Sgn
H=8
Tp=2
SgnNm=IsMicrophoneMuted
]
[
ObjTp=Sgn
H=9
Tp=2
SgnNm=SystemPowered
]
[
ObjTp=Sgn
H=1
Tp=3
SgnNm=VolumeLevel
]";
        
        File.WriteAllText($"{contract.Name}.chd", chd);
        Console.WriteLine($"✓ Generated {contract.Name}.chd");
    }
    
    private static void GenerateCCZFile(CrestronContract contract)
    {
        // For this demo, we'll just copy the .cce file as .ccz
        // In a real implementation, this would create a ZIP archive
        File.Copy($"{contract.Name}.cce", $"{contract.Name}.ccz", true);
        Console.WriteLine($"✓ Generated {contract.Name}.ccz");
    }
    
    private static readonly Random _random = new Random();
    private static readonly string _chars = "abcdefghijklmnopqrstuvwxyz0123456789";

    public static string GenerateId(string prefix = "", int length = 8)
    {
        var result = new char[length + 1];
        result[0] = '_';
        
        for (int i = 1; i <= length; i++)
        {
            result[i] = _chars[_random.Next(_chars.Length)];
        }
        
        return $"{prefix}{new string(result)}";
    }
}

// Data Models
public class CrestronContract
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Version { get; set; }
    public List<ContractComponent> Components { get; set; } = new List<ContractComponent>();
    public List<ComponentSpecification> Specifications { get; set; } = new List<ComponentSpecification>();
}

public class ContractComponent
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ComponentAttribute> Commands { get; set; } = new List<ComponentAttribute>();
    public List<ComponentAttribute> Feedbacks { get; set; } = new List<ComponentAttribute>();
}

public class ComponentAttribute
{
    public string Name { get; set; }
    public int DataType { get; set; } // 1=Digital, 2=Analog, 3=String
    public string Type { get; set; } // "command" or "feedback"
    public string Description { get; set; }
}

public class ComponentSpecification
{
    public string Id { get; set; }
    public string ComponentId { get; set; }
    public string InstanceName { get; set; }
    public int ControlJoinId { get; set; }
}