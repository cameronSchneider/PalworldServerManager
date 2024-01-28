using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static PalworldServerManager.ProgramConstants;

namespace PalworldServerManager.SteamCmdUtils
{
    public class SteamCmd
    {
        private static SteamCmd instance = null;

        private static readonly HttpClient httpClient = new HttpClient(); // HttpClient only supposed to be instantiated once

        private string steamCmdPath = "";
        private bool isSteamCmdInstalled = false;

        Process steamCmdProc = null;

        public SteamCmd(string pathToSteamCmd) 
        {
            if(instance == null)
            {
                instance = this;
            }
            else 
            {
                return;
            }

            steamCmdPath = pathToSteamCmd;
            isSteamCmdInstalled = Directory.Exists(steamCmdPath) && File.Exists(steamCmdPath + STEAM_CMD_EXE_NAME) && File.Exists(steamCmdPath + "\\steamclient.dll");
        }

        public bool IsSteamCmdInstalled()
        {
            return isSteamCmdInstalled;
        }

        // Download & install steamcmd from Valve.
        public async Task InstallSteamCmd(ProgressBarForm progress)
        {
            string downloadZipPath = steamCmdPath + STEAM_CMD_ZIP_NAME;

            progress.SetDescriptionTextSafe("Downloading SteamCmd...");
            if (!File.Exists(downloadZipPath))
            {
                await DownloadFileAsync(STEAM_CMD_INSTALL_LINK, downloadZipPath);
            }

            progress.SetProgressSafe(25);

            progress.SetDescriptionTextSafe("Extracting SteamCmd...");
            await Task.Run(() => ZipFile.ExtractToDirectory(downloadZipPath, steamCmdPath));
            File.Delete(downloadZipPath);

            progress.SetProgressSafe(50);

            steamCmdProc = new Process();
            steamCmdProc.StartInfo.FileName = steamCmdPath + STEAM_CMD_EXE_NAME;
            steamCmdProc.StartInfo.Arguments = "+quit";
            steamCmdProc.StartInfo.UseShellExecute = false;
            steamCmdProc.StartInfo.CreateNoWindow = true;
            steamCmdProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            steamCmdProc.EnableRaisingEvents = true;

            progress.SetDescriptionTextSafe("Installing SteamCmd...");

            steamCmdProc.Start();
            await steamCmdProc.WaitForExitAsync();

            progress.SetProgressSafe(100);
        }

        private async Task DownloadFileAsync(string uri, string outputPath)
        {
            Uri result;
            if (!Uri.TryCreate(uri, UriKind.Absolute, out result))
            {
                throw new InvalidOperationException("URI is invalid.");
            }

            if (File.Exists(outputPath))
            {
                throw new InvalidOperationException("File already exists.");
            }

            using (var response = await httpClient.GetAsync(result, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                using (FileStream fileStream = File.Create(outputPath))
                {
                    using (var httpStream = await response.Content.ReadAsStreamAsync())
                    {
                        await httpStream.CopyToAsync(fileStream);
                    }
                }
            }
        }
    }
}
