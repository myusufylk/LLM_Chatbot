using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LLM_Chatbot
{
    public partial class FrmChatbot : Form
    {
        // 🔹 Chat hafızası (dynamic yaptık ki verilere kolay erişelim)
        List<dynamic> messages = new List<dynamic>();

        public FrmChatbot()
        {
            InitializeComponent();
        }

        // 🔹 Gönder Butonu
        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
                return;

            string userMessage = txtMessage.Text;

            // Ekrana yaz
            rtbChat.AppendText("🧑 Sen: " + userMessage + "\n\n");
            txtMessage.Clear();

            // Hafızaya ekle
            messages.Add(new { role = "user", content = userMessage });

            try
            {
                // Gemini'ye gönder
                string botReply = await SendToGemini();

                // Cevabı ekrana yaz
                rtbChat.AppendText("🤖 Bot: " + botReply + "\n\n");

                // Cevabı hafızaya ekle (Gemini'de asistan rolü 'model'dir)
                messages.Add(new { role = "model", content = botReply });

                // Otomatik aşağı kaydır
                rtbChat.SelectionStart = rtbChat.Text.Length;
                rtbChat.ScrollToCaret();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // 🔹 ENTER tuşu kontrolü
        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                btnSend.PerformClick();
            }
        }

        // 🔹 Google Gemini API İsteği (DÜZELTİLMİŞ KISIM)
        private async Task<string> SendToGemini()
        {
            // NOT: Kodunuzda paylaştığınız API Key'i buraya ekledim.
            string apiKey = "API KEY";

            
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                // ❌ DİKKAT: 'Authorization' satırı BURADAN SİLİNDİ. 
                // Gemini anahtarı URL'de alır, header'da istemez.

                // 1. Mesajları Gemini'nin anlayacağı formata çeviriyoruz
                var geminiContents = new List<object>();

                foreach (var msg in messages)
                {
                    // "system" rolü gelirse "user" gibi davranması için basit bir kontrol
                    string role = msg.role == "assistant" ? "model" : "user";
                    if (msg.role == "system") role = "user";

                    geminiContents.Add(new
                    {
                        role = role,
                        parts = new[]
                        {
                            new { text = msg.content }
                        }
                    });
                }

                // 2. Gemini JSON yapısını oluştur
                var requestBody = new
                {
                    contents = geminiContents
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // 3. İsteği Gönder
                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();

                // 4. Cevabı Parçala (Gemini Formatına Göre)
                using (JsonDocument doc = JsonDocument.Parse(result))
                {
                    // API Hata kontrolü
                    if (doc.RootElement.TryGetProperty("error", out JsonElement errorElement))
                    {
                        return "API Hatası: " + errorElement.GetProperty("message").GetString();
                    }

                    // Doğru veri yolu: candidates -> content -> parts -> text
                    return doc.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();
                }
            }
        }
    }
}
