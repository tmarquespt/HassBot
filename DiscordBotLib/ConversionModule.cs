///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : HassBot.cs
//  DESCRIPTION     : A class that implements various conversion commands
//                  :   c2f -> converts celsius to fahrenheit
//                  :   f2c -> converts fahrenheit to celsius
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;
using System.Web;

namespace DiscordBotLib
{

    public class ConversionModule : BaseModule {

        [Command("c2f")]
        public async Task CelsiusToFahrenheit() {
            await base.DisplayUsage(Constants.USAGE_C2F);
        }

        [Command("f2c")]
        public async Task FahrenheitToCelsius() {
            await base.DisplayUsage(Constants.USAGE_F2C);
        }

        [Command("hex2dec")]
        public async Task HexToDec() {
            await base.DisplayUsage(Constants.USAGE_HEX2DEC);
        }

        [Command("dec2hex")]
        public async Task DecToHex() {
            await base.DisplayUsage(Constants.USAGE_DEC2HEX);
        }

        [Command("dec2bin")]
        public async Task Dec2Bin() {
            await base.DisplayUsage(Constants.USAGE_DEC2BIN);
        }

        [Command("bin2dec")]
        public async Task Bin2Dec() {
            await base.DisplayUsage(Constants.USAGE_BIN2DEC);
        }

        [Command("c2f")]
        public async Task CelsiusToFahrenheit([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(Constants.EMOJI_THERMOMETER);
            embed.WithColor(Helper.GetRandomColor());

            // mention users if any
            string mentionedUsers = base.MentionedUsers();

            double temp_c = 0.0;
            try {
                temp_c = double.Parse(cmd.Trim());
            }
            catch {
                temp_c = 0.0;
            }

            double temp_f = ConvertCelsiusToFahrenheit(temp_c);
            embed.AddInlineField("Conversão de Centígrados para Fahrenheit",
                string.Format("{0} {1} graus Centígrados = {2} graus Fahrenheit!", mentionedUsers, temp_c, temp_f.ToString("#.##")));
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("f2c")]
        public async Task FahrenheitToCelsius([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":thermometer:");
            embed.WithColor(Helper.GetRandomColor());

            // mention users if any
            string mentionedUsers = base.MentionedUsers();

            double temp_f = 0.0;
            try {
                temp_f = double.Parse(cmd.Trim());
            }
            catch {
                temp_f = 0.0;
            }

            double temp_c = ConvertFahrenheitToCelsius(temp_f);
            embed.AddInlineField("Conversão de Fahrenheit para Centígrados",
                string.Format("{0} {1} graus Fahrenheit = {2} graus Centígrados!", mentionedUsers, temp_f, temp_c.ToString("#.##")));
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("hex2dec")]
        public async Task Hex2Dec([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":1234:");
            embed.WithColor(Helper.GetRandomColor());

            // mention users if any
            string mentionedUsers = base.MentionedUsers();

            int decValue = Hex2Decimal(cmd.Trim());
            embed.AddInlineField("Conversão de Hexadecimal para Decimal",
                string.Format("{0} '{1}' em Hexadecimal = '{2}' em Decimal", mentionedUsers, cmd.Trim(), decValue));
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("dec2hex")]
        public async Task Dec2Hex([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":1234:");
            embed.WithColor(Helper.GetRandomColor());

            // mention users if any
            string mentionedUsers = base.MentionedUsers();
            int decValue = 0;
            try {
                decValue = int.Parse(cmd.Trim());
            }
            catch {
                decValue = 0;
            }

            string hexValue = Decimal2Hex(decValue);
            embed.AddInlineField("Conversão de Decimal para Hexadecimal",
                string.Format("{0} '{1}' em Decimal = '{2}' em Hexadecimal", mentionedUsers, cmd.Trim(), hexValue));
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("dec2bin")]
        public async Task Dec2Bin([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":1234:");
            embed.WithColor(Helper.GetRandomColor());

            // mention users if any
            string mentionedUsers = base.MentionedUsers();
            int decValue = 0;
            try {
                decValue = int.Parse(cmd.Trim());
            }
            catch {
                decValue = 0;
            }

            string binValue = Decimal2Binary(decValue);
            embed.AddInlineField("Conversão de Decimal para Binário",
                string.Format("{0} '{1}' em Decimal = '{2}' em Binário", mentionedUsers, cmd.Trim(), binValue));
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("bin2dec")]
        public async Task Bin2Dec([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(":1234:");
            embed.WithColor(Helper.GetRandomColor());

            // mention users if any
            string mentionedUsers = base.MentionedUsers();
            int decValue = Binary2Decimal(cmd.Trim());
            embed.AddInlineField("Conversão de Binário em Decimal",
                string.Format("{0} '{1}' em Binário = '{2}' em Decimal", mentionedUsers, cmd.Trim(), decValue));
            await ReplyAsync(string.Empty, false, embed);
        }

        private static double ConvertCelsiusToFahrenheit(double c) {
            return ((9.0 / 5.0) * c) + 32;
        }

        private static double ConvertFahrenheitToCelsius(double f) {
            return (5.0 / 9.0) * (f - 32);
        }

        private static int Hex2Decimal(string hexValue) {
            int decValue = Convert.ToInt32(hexValue, 16);
            return decValue;
        }

        private static string Decimal2Hex(int decValue) {
            string hexValue = decValue.ToString("X");
            return hexValue;
        }

        private static string Decimal2Binary(int decValue) {
            try {
                string binary = Convert.ToString(decValue, 2);
                return binary;

            }
            catch {
                return "Erro na conversão ou impossível de converter!";
            }
        }

        private static int Binary2Decimal(string binValue) {
            try {
                return Convert.ToInt32(binValue, 2);
            }
            catch {
                return 0;
            }
        }
    }
}
