// Decompiled with JetBrains decompiler
// Type: QPrawnUpgradeAccess.Patches.Exosuit_Update_Patch
// Assembly: QPrawnUpgradeAccess, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 17435F14-0935-4196-8FD9-FEF03EC4A1E7
// Assembly location: C:\Users\pred1\Desktop\QPrawnUpgradeAccess\QPrawnUpgradeAccess.dll

using Harmony;
using System.Collections.Generic;
using UnityEngine;

namespace QPrawnUpgradeAccess.Patches
{
  [HarmonyPatch(typeof (Exosuit))]
  [HarmonyPatch("Update")]
  internal class Exosuit_Update_Patch
  {
    public static void Postfix(Exosuit __instance)
    {
      if (!((Vehicle) __instance).GetPilotingMode())
        return;
      PDA pda = PDA_Awake_Patch.Pda;
      using (List<GameInput.Button>.Enumerator enumerator = QPatch.UpgradeButtons.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if (GameInput.GetButtonDown(enumerator.Current))
          {
            if (Object.op_Inequality((Object) pda, (Object) null) && pda.get_isOpen())
            {
              pda.Close();
              return;
            }
            ((VehicleUpgradeConsoleInput) ((Vehicle) __instance).upgradesInput).OpenFromExternal();
            return;
          }
        }
      }
      using (List<GameInput.Button>.Enumerator enumerator = QPatch.StorageButtons.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if (GameInput.GetButtonDown(enumerator.Current))
          {
            if (Object.op_Inequality((Object) pda, (Object) null) && pda.get_isOpen())
            {
              pda.Close();
              break;
            }
            ((StorageContainer) __instance.storageContainer).Open();
            break;
          }
        }
      }
    }
  }
}
