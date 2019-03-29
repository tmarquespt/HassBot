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
                "Usage";
        public static readonly string USAGE_AFK =
                "`~afk <message>` ou `~away <message>` ou `~seen <username>`";
        public static readonly string USAGE_SEEN =
                "`~seen <username>`";
        public static readonly string USAGE_EIGHTBALL =
                "`~8ball <a tua questão>`";
        public static readonly string USAGE_AS =
                "`~as @user #channel <a tua mensagem>`";
        public static readonly string USAGE_BASE64 =
                "`~base64_encode <string to encode>` ou `~base64_decode <string to decode>`";
        public static readonly string USAGE_BREAKINGCHANGES =
                "`~breaking_changes 82` mostra as breaking changes para a versão 0.82.0 e as suas releases";
        public static readonly string USAGE_COMMAND =
                "`~command add` ou `~command refresh` para refrescar os comandos";
        public static readonly string USAGE_C2F =
                "`~c2f <valor numérico de temperatura em graus Celcios>`";
        public static readonly string USAGE_F2C =
                "`~f2c <valor numérico de temperatura em graus fahrenheit>`";
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
                "`~lookup <palavra> <@ optional user>`";
        public static readonly string USAGE_DEEPSEARCH =
                "`~deepsearch <keyword>`";
        public static readonly string USAGE_LMGTFY =
                "`~lmgtfy <google search string>`";
        public static readonly string USAGE_YAML2JSON =
                "`~yaml2json <yaml code>`";
        public static readonly string USAGE_JSON2YAML =
                "`~json2yaml <json code>`";
        public static readonly string USAGE_SUBSCRIBE =
                "`~inscreva-se<tag> `para assinar \n`~lista de inscrição` para ver todas as tags inscritas.";
        public static readonly string USAGE_UNSUBSCRIBE =
                "`~cancele a inscrição <tag> `para cancelar a assinatura \n`~ cancelar a assinatura all` para limpar tudo";
        public static readonly string USAGE_YAML =
                "Tente o seguinte:\n~yaml? \\`\\`\\`yaml\ncode\n\\`\\`\\`";

        /// <summary>
        /// Command Specific Messages
        /// </summary>
        public static readonly string SEEN_MESSAGE_FORMAT =
                "**{0} está ausente** for {1} com uma mensagem :point_right: {2}";
        public static readonly string AWAY_MESSAGE_FORMAT =
                "{0} está ausente! {1} :wave:";
        public static readonly string ACCESS_DENIED =
                "Acesso negado";
        public static readonly string ACCESS_DENIED_MESSAGE =
                "Não tens permissões para executar este comando.Por favor, entra em contato com @Manager para ter acesso.";
        public static readonly string COMMAND_TOTAL =
                "Existem `{0}` comando (s) personalizado (s) disponíveis.";
        public static readonly string SUCCESS =
                "Successo!";
        public static readonly string COMMAND_SUCCESS_MESSAGE =
                "Vá em frente e execute o comando usando `~{0}`";
        public static readonly string TITLE_SUBSCRIBE =
                "Subscreve";
        public static readonly string TITLE_UNSUBSCRIBE =
                "Cancelar subscrição";
        public static readonly string ERROR_NO_SUBSCRIPTIONS =
                "Não tens nenhuma inscrição atualmente.";
        public static readonly string INFO_TAG_EXISTS =
                "A tag '{0}' já está na tua lista de inscrição.";
        public static readonly string INFO_SUBSCRIPTION_SUCCESS =
                "Subscreveste a tag '{0}' com sucesso.";
        public static readonly string INFO_CURRENT_SUBSCRIPTIONS =
                "As tuas assinaturas atuais são: {0}";
        public static readonly string INFO_UNSUBSCRIBE_ALL_SUCCESS =
                "Não foi possível cancelar a inscrição em todas as tag.";
        public static readonly string INFO_NOT_SUBSCRIBED =
                "Tu não te inscreveste em'{0}'.";
        public static readonly string INFO_UNSUBSCRIBE_SUCCESS =
                "Não inscrito com sucesso em '{0}'.";

        public static readonly string MAXLINELIMITMESSAGE =
            "Atenção !: Por favor, usa <https://www.hastebin.com> para partilhares o teu código se tiver mais que 10 linhas. Foste avisado, {0}!;";

        public static readonly string COMMAND_REFRESH_SUCCESSFUL =
                "Comandos, Sitemap e domínios bloqueados são recarregados e prontos para serem usados!";
        public static readonly string COMMAND_REFRESH_FAILED =
                "Falha ao atualizar os dados de pesquisa! contato @Manager";

        public static readonly string COMMAND_MESSAGE =
                "Ger os dados online do Hassbot em <https://github.com/awesome-automations/hassbot-data>. Garante que corres o comando run `~command refresh` sempre que reinicias";
        public static readonly string GOOD_YAML =
                "Isso é código YAML perfeitamente válido!";
        public static readonly string INVALID_YAML =
                "Invalido YAML! Erro: {1}";
        public static readonly string WELCOME_TITLE =
                "Bem-vindo";
        public static readonly string WELCOME_MESSAGE =
                "Bem-vindo ao {0} Discord Chanal!";
        public static readonly string WELCOME_RULES_MESSAGE =
                "Por favor lê {0} \n";
        public static readonly string CODE_SHARING_MESSAGE =
                "Para partilhares código, por favor usa <https://www.hastebin.com>\nSe tiver menos de 10 linhas de código, ** certifica-te, ** que é formatado usando o formato abaixo:\n\\`\\`\\`yaml\ncode\n\\`\\`\\`\n";
        public static readonly string FORMAT_CODE =
                "Código de formato";
        public static readonly string LET_ME_GOOGLE =
                "Vamos procurar no Google por ti...";
        public static readonly string BAN_MESSAGE =
                "Utilizador **{0}** foi banido permanentemente por publicar {1}.";
        public static readonly string ERROR_BLOCKED_URL =
                "{0} A tua mensagem foi eliminada porque contém um link ou um nome de domínio '{1}' que está na lista de bloqueados devido a: '** {2} **'.\nPor favor, posta de novo removendo / alterando o nome de domínio / link.A tua mensagem original foi enviada para ti.";
        public static readonly string USER_MESSAGE_BLOCKED_URL =
                "Aqui está a tua mensagem original que usaste anteriormente e que foi bloqueada.Tu podes publicar novamente removendo / alterando o nome de domínio / link: { 0} \nRazão para eliminação: ** {1} ** \nA tua mensagem: {2} ";
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
                ":pray:";
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
