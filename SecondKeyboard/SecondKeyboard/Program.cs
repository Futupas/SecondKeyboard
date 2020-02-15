using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;
using Newtonsoft.Json;
using System.IO;

namespace SecondKeyboard
{
    class Program
    {
        static
            InputSimulator sim = new InputSimulator();
        static JsonDataModel data;

        static void Main(string[] args)
        {

            JsonDataModel model = new JsonDataModel();

            model.buttons.Add(new JsonDataModel.WebsiteButtonModel());
            model.buttons.Add(new JsonDataModel.WebsiteButtonModel());

            model.virtualPhysicalPathMatches.Add(new JsonDataModel.VirtualPhysicalPathMatchModel());
            model.virtualPhysicalPathMatches.Add(new JsonDataModel.VirtualPhysicalPathMatchModel());

            string str = JsonConvert.SerializeObject(model);

            File.WriteAllText(@"C:\PROJECTS\SecondKeyboard\SecondKeyboard\SecondKeyboard\data2.json", str);

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            string dataString = "";
            //Chech does file exist
            if (File.Exists("data.json"))
            {
                dataString = File.ReadAllText("data.json");
                cw("data file found", ConsoleColor.Green);
            } else
            {
                Console.Write("Enter path to data file: ");
                string dataFileName = Console.ReadLine();
                while (! File.Exists(dataFileName))
                {
                    Console.Write("Enter valid path to data file: ");
                    dataFileName = Console.ReadLine();
                }
                dataString = File.ReadAllText(dataFileName);
                cw("data file found", ConsoleColor.Green);
            }

            data = (JsonDataModel)JsonConvert.DeserializeObject(dataString, typeof(JsonDataModel));
            cw("data from file built", ConsoleColor.Green);

            PanWebsite website = new PanWebsite(data.website.ipAddress, onRequest);
            cw("website created", ConsoleColor.Green);

            website.Start();

            cw("website started", ConsoleColor.Green);
            //sim.Keyboard.KeyPress(VirtualKeyCode.LWIN);
        }

        public static PanResponse onRequest (PanRequest request)
        {
            cw(request.Url, ConsoleColor.Green);
            if (request.Address.Length < 1)
            {
                return PanResponse.ReturnHtml(data.website.indexHtmlPath);
            }
            else if (request.Address[0].ToLowerInvariant() == "getbuttons")
            {
                return PanResponse.ReturnJson(data.buttons);
            }
            else if (request.Address[0].ToLowerInvariant() == "sendbtn")
            {
                try
                {
                    if (request.Address.Length != 2) return PanResponse.ReturnCode(403);
                    string[] btnCodes = request.Address[1].Split('+');

                    for (int i = 0; i < btnCodes.Length; i-=-1)
                    {
                        sim.Keyboard.KeyDown((VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), btnCodes[i]));
                    }
                    for (int i = btnCodes.Length-1; i >= 0; i--)
                    {
                        sim.Keyboard.KeyUp((VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), btnCodes[i]));
                    }

                    //sim.Keyboard.KeyPress((VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), btnCode));
                    return PanResponse.ReturnCode(200);
                } catch (Exception ex)
                {
                    cw(ex.Message, ConsoleColor.Red);
                    return PanResponse.ReturnCode(500);
                }
                
            }
            else
            {
                string virtAddress = request.Url;
                var matches = data.virtualPhysicalPathMatches.FindAll((e) => { return e.virtualPath == virtAddress; });
                if (matches.Count < 1) return PanResponse.ReturnCode(404);
                string physAddress = matches[0].physicalPath;
                if (!File.Exists(physAddress)) return PanResponse.ReturnCode(404);
                return PanResponse.ReturnFile(physAddress);
            }
            //sim.Keyboard.KeyPress(VirtualKeyCode.LWIN);
            //int value = (int)Enum.Parse(typeof(TestAppAreana.MovieList.Movies), KeyVal);
            return PanResponse.ReturnCode(200);
        }

        /// <summary>
        /// Console.WriteLine();
        /// </summary>
        private static void cw(string text, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor currentConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")}   {text}");
            Console.ForegroundColor = currentConsoleColor;
        }
    }
}
