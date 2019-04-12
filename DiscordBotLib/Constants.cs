using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotLib
{
    public sealed class Constants
    {

        /// <summary>
        /// Usage Specific Messages
        /// </summary>
        public static readonly string TITLE_HASSBOT =
                "CPHAbot";
        public static readonly string USAGE_TITLE =
                "Utilização";
        public static readonly string USAGE_AFK =
                "`~afk <a tua mensagem>` ou `~away <a tua mensagem>`";
        public static readonly string USAGE_SEEN =
                "`~seen <username>`";
        public static readonly string USAGE_EIGHTBALL =
                "`~8ball <a tua questão>`";
        public static readonly string USAGE_AS =
                "`~as @user #channel <a tua mensagem>`";
        public static readonly string USAGE_BASE64 =
                "`~base64_encode <string para codificar>` ou `~base64_decode <string para descodificar>`";
        public static readonly string USAGE_BREAKINGCHANGES =
                "`~breaking_changes 90` mostra as breaking changes para a versão 0.90 e as suas releases";
        public static readonly string USAGE_COMMAND =
                "`~command add` ou `~command refresh` para refrescar os comandos";
        public static readonly string USAGE_C2F =
                "`~c2f <temperatura em graus centígrados>`";
        public static readonly string USAGE_F2C =
                "`~f2c <temperatura em graus fahrenheit>`";
        public static readonly string USAGE_VIOLATION =
                "`~violation pardon @username` para perdoar uma violação às regras de um utilizador.\n`~violation add @user <descrição da violação>` para adicionar uma nova violação às regras";
        public static readonly string USAGE_HEX2DEC =
                "`~hex2dec <valor decimal>`";
        public static readonly string USAGE_DEC2HEX =
                "`~dec2hex <valor hexadecimal>`";
        public static readonly string USAGE_BIN2DEC =
                "`~bin2dec <valor binário>`";
        public static readonly string USAGE_DEC2BIN =
                "`~dec2bin <valor decimal>`";
        public static readonly string USAGE_LOOKUP =
                "`~lookup <palavra> <-opcional utilizador->`";
        public static readonly string USAGE_DEEPSEARCH =
                "`~deepsearch <keyword>`";
        public static readonly string USAGE_LMGTFY =
                "`~lmgtfy <palavra a pesquisar no google>`";
        public static readonly string USAGE_YAML2JSON =
                "`~yaml2json <yaml code>`";
        public static readonly string USAGE_JSON2YAML =
                "`~json2yaml <json code>`";
        public static readonly string USAGE_SUBSCRIBE =
                "`~subscribe <tag>` para subscrever\n`~subscribe list` para ver todas as tags subscritas.";
        public static readonly string USAGE_UNSUBSCRIBE =
                "`~unsubscribe <tag>` para remover a subscrição\n`~unsubscribe all` to clear everything!";
        public static readonly string USAGE_YAML =
                "Tenta o seguinte:\n~yaml? \\`\\`\\`yaml\ncódigo\n\\`\\`\\`";

        /// <summary>
        /// Command Specific Messages
        /// </summary>
        public static readonly string SEEN_MESSAGE_FORMAT =
                "**{0} está ausente** por {1} com a mensagem :point_right: {2}";
        public static readonly string AWAY_MESSAGE_FORMAT =
                "{0} está ausente! {1} :wave:";
        public static readonly string ACCESS_DENIED =
                "Acesso negado";
        public static readonly string ACCESS_DENIED_MESSAGE =
                "Não tens permissões para executar este comando. Por favor, entra em contato com @Manager para reportar a situação.";
        public static readonly string COMMAND_TOTAL =
                "Existem `{0}` comando(s) personalizado(s) disponível(eis).";
        public static readonly string SUCCESS =
                "Successo!";
        public static readonly string COMMAND_SUCCESS_MESSAGE =
                "Executa o comando usando `~{0}`";
        public static readonly string TITLE_SUBSCRIBE =
                "Subscreve";
        public static readonly string TITLE_UNSUBSCRIBE =
                "Cancela a subscrição";
        public static readonly string ERROR_NO_SUBSCRIPTIONS =
                "Não tens nenhuma subscrição actualmente.";
        public static readonly string INFO_TAG_EXISTS =
                "A tag '{0}' já está na tua lista de subscrição.";
        public static readonly string INFO_SUBSCRIPTION_SUCCESS =
                "Subscreveste a tag '{0}' com sucesso.";
        public static readonly string INFO_CURRENT_SUBSCRIPTIONS =
                "As tuas subscrições atuais são: {0}";
        public static readonly string INFO_UNSUBSCRIBE_ALL_SUCCESS =
                "Cancelamento da subscrição em todas as tag com sucesso.";
        public static readonly string INFO_NOT_SUBSCRIBED =
                "Não subscreveste '{0}'.";
        public static readonly string INFO_UNSUBSCRIBE_SUCCESS =
                "Cancelamento da subscrição com sucesso em '{0}'.";

        public static readonly string MAXLINELIMITMESSAGE =
            "Atenção! Por favor, usa <https://www.hastebin.com> ou outro similar para partilhares o teu código se tiver mais que 10 linhas. Foste avisado, {0}!;";

        public static readonly string COMMAND_REFRESH_SUCCESSFUL =
                "Commands, Sitemap e Blocked Domains foram recarregados com sucesso e estão prontos a ser utilizados!";
        public static readonly string COMMAND_REFRESH_FAILED =
                "Falha ao atualizar os dados de pesquisa! Entra em contacto com o @Manager para reportar a situação.";

        public static readonly string COMMAND_MESSAGE =
                "Manage Hassbot data online at <https://github.com/awesome-automations/hassbot-data>. Make sure you run `~command refresh` after updating data online.";
        public static readonly string GOOD_YAML =
                ":cpha_yes: Código YAML válido!";
        public static readonly string INVALID_YAML =
                ":cpha_no: Código YAML inválido! Erro: {1}";
        public static readonly string WELCOME_TITLE =
                "Bem vindo ao servidor de Discord da CPHA!";
        public static readonly string WELCOME_MESSAGE =
                "Todos os Termos do Serviço/Regras do site e fórum se aplicam aqui.\n\nCaso tenhas alguma dúvida contacta um dos nossos Administradores ou Moderadores.";
        public static readonly string WELCOME_RULES_MESSAGE =
                " Lê um resumo das Regras no {0} \n";
        public static readonly string CODE_SHARING_MESSAGE =
                "Para partilhares código, por favor usa <https://www.hastebin.com> ou outro similar.\nSe tiver menos de 10 linhas de código, certifica-te que é formatado do seguinte modo:\n\\`\\`\\`yaml\ncódigo\n\\`\\`\\`\n";
        public static readonly string FORMAT_CODE =
                "Formatar código";
        public static readonly string LET_ME_GOOGLE =
                "Vou procurar no Google por ti...";
        public static readonly string BAN_MESSAGE =
                "O utilizador **{0}** foi banido permanentemente por publicar {1}.";
        public static readonly string ERROR_BLOCKED_URL =
                "{0} A tua mensagem foi eliminada porque contém um link ou um nome de domínio '{1}' que está na lista de bloqueados devido a: '** {2} **'.\nPor favor, pública de novo removendo/alterando o nome de domínio/link. A tua mensagem original foi enviada para ti por MP.";
        public static readonly string USER_MESSAGE_BLOCKED_URL =
                "Esta foi a mensagem que publicaste e que foi bloqueada. Podes publicar novamente removendo/alterando o nome de domínio/link: { 0} \nMotivo para eliminação: ** {1} ** \nA tua mensagem: {2} ";
        /// <summary>
        /// Emojis
        /// </summary>
        public static readonly string EMOJI_INFORMATION =
                ":information_source:";
        public static readonly string EMOJI_STOPSIGN =
                ":octagonal_sign:";
        public static readonly string EMOJI_THUMBSUP =
                ":thumbsup:";
        public static readonly string EMOJI_THUMBSDOWN =
                ":thumbsdown:";
        public static readonly string EMOJI_THERMOMETER =
                ":thermometer:";
        public static readonly string EMOJI_NAMASTE =
                ":round_pushpin:";
        public static readonly string EMOJI_POINT_UP =
                ":point_up:";
        public static readonly string EMOJI_POINT_DOWN =
                ":point_down:";
        public static readonly string EMOJI_FAIL =
                ":cold_sweat:";
        public static readonly string EMOJI_GO =
                ":checkered_flag:";
        public static readonly string EMOJI_PING_PONG =
                ":ping_pong:";
    }
}
