# 🤖 C# Gemini AI Chatbot

Bu proje, Google'ın güçlü yapay zeka modeli **Gemini (2.5 Flash / 2.0)** ile iletişim kuran, C# ve Windows Forms (WinForms) kullanılarak geliştirilmiş modern bir masaüstü chatbot uygulamasıdır.

Harici bir SDK kullanmadan, doğrudan `HttpClient` ve `REST API` yapısı ile Google sunucularıyla haberleşir.

## 🚀 Özellikler

* **Güncel Yapay Zeka:** Google Gemini 2.5 Flash / 2.0 modelleriyle entegre çalışır.
* **Bağlam Hafızası (Context):** Bot, önceki mesajları hatırlar ve sohbetin akışına göre cevap verir.
* **Hızlı ve Hafif:** Sadece `System.Text.Json` ve standart .NET kütüphaneleri kullanır.
* **Kullanıcı Dostu Arayüz:** Mesajlaşma geçmişi, kolay gönderim ve şık tasarım.
* **Hata Yönetimi:** API bağlantı sorunlarını veya geçersiz anahtarları algılar ve kullanıcıya bildirir.

## 🛠 Kullanılan Teknolojiler

* **Dil:** C#
* **Platform:** .NET Framework / .NET Core (Windows Forms)
* **API:** Google Gemini API (REST)
* **Veri Formatı:** JSON (System.Text.Json)
* **IDE:** Visual Studio 2022

## ⚙️ Kurulum ve Çalıştırma

Projeyi kendi bilgisayarınızda çalıştırmak için aşağıdaki adımları izleyin:

### 1. Gereksinimler
* Visual Studio (2019 veya üzeri)
* .NET Framework veya .NET Core yüklü olmalıdır.

### 2. API Anahtarı Alma
Bu projenin çalışması için Google'dan ücretsiz bir API anahtarı almanız gerekir:
1.  [Google AI Studio](https://aistudio.google.com/) adresine gidin.
2.  Sol menüden **"Get API key"** butonuna tıklayın.
3.  **"Create API key"** diyerek anahtarınızı oluşturun ve kopyalayın.

### 3. Projeyi Ayarlama
1.  Projeyi indirin veya kopyalayın.
2.  Visual Studio ile `LLM_Chatbot.sln` dosyasını açın.
3.  **NuGet Paket Yöneticisi** üzerinden `System.Text.Json` kütüphanesini yükleyin (Eğer yüklü değilse).
4.  `Form1.cs` dosyasını açın ve aşağıdaki satırı bulun:

```csharp
string apiKey = "BURAYA_GEMINI_API_KEYINI_YAZ";
