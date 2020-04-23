// Decompiled with JetBrains decompiler
// Type: QPrawnUpgradeAccess.Patches.PDA_Awake_Patch
// Assembly: QPrawnUpgradeAccess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 17435F14-0935-4196-8FD9-FEF03EC4A1E7
// Assembly location: C:\Users\pred1\Desktop\QPrawnUpgradeAccess\QPrawnUpgradeAccess.dll

using Harmony;

namespace QPrawnUpgradeAccess.Patches
{
  [HarmonyPatch(typeof (PDA))]
  [HarmonyPatch("Update")]
  internal class PDA_Awake_Patch
  {
    public static PDA Pda;

    public static void Postfix(PDA __instance)
    {
      PDA_Awake_Patch.Pda = __instance;
    }
  }
}
