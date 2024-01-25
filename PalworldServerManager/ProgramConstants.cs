using System.Windows.Forms;

namespace PalworldServerManager
{
    public class ProgramConstants
    {
        public static string APPLICATION_DATA_PATH = Application.StartupPath + "\\Data\\";
        public static string KNOWN_SERVERS_FILENAME = "known_servers.csv";
        public static string USER_SETTINGS_FILENAME = "usersettings.csv";
        public static string SERVER_PROCESS_NAME = "PalServer-Win64-Test-Cmd";
        public static string SERVER_EXE_NAME = "\\PalServer.exe";
        public static string DEFAULT_PAL_SERVER_DIR_NAME = "\\PalServer";
        public static string PAL_SERVER_CONFIG_PATH = "\\Pal\\Saved\\Config\\WindowsServer\\PalWorldSettings.ini";
        public static string PAL_DEFAULT_CONFIG_PATH = "\\DefaultPalWorldSettings.ini";

        public static string PAL_SETTINGS_HEADER = "[/Script/Pal.PalGameWorldSettings]";
        public static string PAL_SETTINGS_OPTION_STR = "OptionSettings=(";
        public static char PAL_CONFIG_COMMENT_CHAR = ';';

        public static string PAL_SERVER_DIRECTORY_SUBSTRING = "PalServer - ";

        public static string KNOWN_SERVER_PATH = APPLICATION_DATA_PATH + KNOWN_SERVERS_FILENAME;
    }
}