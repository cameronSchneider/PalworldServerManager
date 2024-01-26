using System;
using System.Windows.Forms;

namespace PalworldServerManager
{
    public class ProgramConstants
    {
        public static readonly string APPLICATION_USER_DATA_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PalWorldServerManager\\Data\\";
        public static readonly string APPLICATION_DATA_PATH = Application.StartupPath + "\\Data\\";
        public static readonly string KNOWN_SERVERS_FILENAME = "known_servers.csv";
        public static readonly string USER_SETTINGS_FILENAME = "usersettings.csv";
        public static readonly string SERVER_PROCESS_NAME = "PalServer-Win64-Test-Cmd";
        public static readonly string SERVER_EXE_NAME = "\\PalServer.exe";
        public static readonly string DEFAULT_PAL_SERVER_DIR_NAME = "\\PalServer";
        public static readonly string DEFAULT_PAL_SERVER_WORLD_PATH = "\\PalServer\\Pal\\Saved";
        public static readonly string PAL_SERVER_CONFIG_PATH = "\\Pal\\Saved\\Config\\WindowsServer\\PalWorldSettings.ini";
        public static readonly string PAL_DEFAULT_CONFIG_PATH = "\\DefaultPalWorldSettings.ini";

        public static readonly string PAL_SETTINGS_HEADER = "[/Script/Pal.PalGameWorldSettings]";
        public static readonly string PAL_SETTINGS_OPTION_STR = "OptionSettings=(";
        public static readonly char PAL_CONFIG_COMMENT_CHAR = ';';

        public static readonly string PAL_SERVER_DIRECTORY_SUBSTRING = "PalServer - ";

        public static readonly string KNOWN_SERVER_PATH = APPLICATION_USER_DATA_PATH + KNOWN_SERVERS_FILENAME;

        public static readonly string PAL_GAME_SETTING_OPTIONS_FILE = "game_settings_options.csv";
        public static readonly string PAL_GAME_SETTING_DESCRIPTIONS_FILE = "game_settings_descriptions.csv";
    }
}