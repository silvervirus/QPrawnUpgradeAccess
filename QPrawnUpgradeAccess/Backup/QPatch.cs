// Decompiled with JetBrains decompiler
// Type: QPrawnUpgradeAccess.QPatch
// Assembly: QPrawnUpgradeAccess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 17435F14-0935-4196-8FD9-FEF03EC4A1E7
// Assembly location: C:\Users\pred1\Desktop\QPrawnUpgradeAccess\QPrawnUpgradeAccess.dll

using Harmony;
using Oculus.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace QPrawnUpgradeAccess
{
  internal class QPatch
  {
    public static List<GameInput.Button> UpgradeButtons;
    public static List<GameInput.Button> StorageButtons;
    private static string Path;

    public static void Patch()
    {
      HarmonyInstance.Create("qprawn_upgrade_access.mod").PatchAll(Assembly.GetExecutingAssembly());
      QPatch.Path = QPatch.Path ?? "QMods\\QPrawnUpgradeAccess";
      QPatch.LoadConfig();
      Console.WriteLine("[QPrawnUpgradeAccess] Patched");
    }

    private static string GetModInfoPath()
    {
      return Environment.CurrentDirectory + "\\" + QPatch.Path + "\\mod.json";
    }

    private static void LoadConfig()
    {
      string modInfoPath = QPatch.GetModInfoPath();
      if (!File.Exists(modInfoPath))
      {
        Console.WriteLine("[QPrawnUpgradeAccess] Couldn't load: " + modInfoPath);
      }
      else
      {
        QPatch.ModConfig modConfig = (QPatch.ModConfig) null;
        try
        {
          modConfig = (QPatch.ModConfig) JsonConvert.DeserializeObject<QPatch.ModConfig>(File.ReadAllText(modInfoPath));
        }
        catch
        {
        }
        if (modConfig == null)
        {
          Console.WriteLine("[QPrawnUpgradeAccess] Invalid mod.json format");
        }
        else
        {
          List<GameInput.Button> buttonList1 = new List<GameInput.Button>();
          List<GameInput.Button> buttonList2 = new List<GameInput.Button>();
          string[] strArray1 = new string[2]
          {
            modConfig.Config.Upgrade0,
            modConfig.Config.Upgrade1
          };
          foreach (string str in strArray1)
          {
            GameInput.Button? nullable = QPatch.ToEnum(str);
            if (nullable.HasValue)
              buttonList1.Add(nullable.Value);
            else if (str != "")
              Console.WriteLine("[QPrawnUpgradeAccess]: Invalid buton value: " + str);
          }
          string[] strArray2 = new string[2]
          {
            modConfig.Config.Storage0,
            modConfig.Config.Storage1
          };
          foreach (string str in strArray2)
          {
            GameInput.Button? nullable = QPatch.ToEnum(str);
            if (nullable.HasValue)
              buttonList2.Add(nullable.Value);
            else if (str != "")
              Console.WriteLine("[QPrawnUpgradeAccess]: Invalid buton value: " + str);
          }
          QPatch.UpgradeButtons = buttonList1;
          QPatch.StorageButtons = buttonList2;
        }
      }
    }

    public static GameInput.Button? ToEnum(string value)
    {
      if (string.IsNullOrEmpty(value))
        return new GameInput.Button?();
      try
      {
        return new GameInput.Button?((GameInput.Button) Enum.Parse(typeof (GameInput.Button), value, true));
      }
      catch
      {
        return new GameInput.Button?();
      }
    }

    static QPatch()
    {
      List<GameInput.Button> buttonList1 = new List<GameInput.Button>();
      buttonList1.Add((GameInput.Button) 8);
      buttonList1.Add((GameInput.Button) 9);
      QPatch.UpgradeButtons = buttonList1;
      List<GameInput.Button> buttonList2 = new List<GameInput.Button>();
      buttonList2.Add((GameInput.Button) 15);
      QPatch.StorageButtons = buttonList2;
      QPatch.Path = (string) null;
    }

    [Serializable]
    public class ModConfig
    {
      public string Id;
      public string DisplayName;
      public string Author;
      public List<string> Requires;
      public bool Enable;
      public string AssemblyName;
      public string EntryMethod;
      public QPatch.LocalConfig Config;
    }

    [Serializable]
    public class LocalConfig
    {
      public string Upgrade0 = "Slot1";
      public string Upgrade1 = "Slot2";
      public string Storage0 = "Reload";
      public string Storage1 = "";
    }
  }
}
