using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace RCEExploit
{
    public class Main : MelonLoader.MelonMod
    {
        public override void OnApplicationStart()
        {
            try
            {
                MelonLoader.MelonLogger.Warning("Patching RCE Exploit");
                new HarmonyLib.Harmony("RCE").Patch(typeof(ExitGames.Client.Photon.IPhotonSocket).GetMethod("HandleReceivedDatagram"), prefix: new HarmonyMethod(typeof(Patched).GetMethod("HandleReceivedDatagram")));
                MelonLoader.MelonLogger.Msg("Successfully Patched!");
            }
            catch
            {
                MelonLoader.MelonLogger.Error("Coudnt Patch RCE");
                MelonLoader.MelonLogger.Error("Press any Key to close VRChat");
                System.Console.ReadKey();
                new Thread(() =>
               {
                   GameObject.Instantiate(new GameObject()); //Anti Proccess.kill patch
               }).Start();
            }
        }
    }
    public class Patched
    {
        public static bool HandleReceivedDatagram(UnhollowerBaseLib.Il2CppStructArray<byte> inBuffer, int length)
        {
            try
            {
                if (length > 1200 || inBuffer.Length > 1200)
                {
                    MelonLoader.MelonLogger.Error("RCE Blocked!");
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}