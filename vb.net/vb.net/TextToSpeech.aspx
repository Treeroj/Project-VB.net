<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TextToSpeech.aspx.vb" Inherits="TextToSpeech" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function speakText() {
            var textToSpeak = document.getElementById('<%= TextBox1.ClientID %>').value;
            var utterance = new SpeechSynthesisUtterance(textToSpeak);
            window.speechSynthesis.onvoiceschanged = function () {
                var voices = window.speechSynthesis.getVoices();
                console.log(voices);
                for (var i = 0; i < voices.length; i++) {
                    console.log('Voice ' + i + ': ' + voices[i].name + ' (' + voices[i].lang + ')');
                }
                var thaiVoices = voices.filter(function (voice) {
                    return voice.lang === 'th-TH';
                });

                console.log('Thai Voices:');
                thaiVoices.forEach(function (voice, index) {
                    console.log('Voice ' + index + ': ' + voice.name + ' (' + voice.lang + ')');
                });
            };
            utterance.voice = window.speechSynthesis.getVoices().find(function (voice) {
                return voice.name === 'Microsoft Premwadee Online (Natural) - Thai (Thailand)';
            });
            window.speechSynthesis.speak(utterance);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="btnSpeak" runat="server" Text="Speak" OnClientClick="speakText(); return false;" />
        </div>
    </form>
</body>
</html>
