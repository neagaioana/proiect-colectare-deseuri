#include <ESP8266WiFi.h>
 #include <ESP8266HTTPClient.h>
 #include <SPI.h>
 #include <MFRC522.h>
 #include <time.h>  // Bibliotecă pentru sincronizarea timpului
 
 // Configurare pinii RC522
 #define SS_PIN D4
 #define RST_PIN D3
 
 // Configurare WiFi
 #define STASSID "OMiLAB"
 #define STAPSK "digifofulbs"
 #define SERVER_IP "10.14.11.136"  // IP-ul serverului tău
 #define SERVER_PORT 5270
 
 MFRC522 mfrc522(SS_PIN, RST_PIN);
 
 void setup() {
   Serial.begin(115200);
   WiFi.begin(STASSID, STAPSK);
 
   Serial.print("Connecting to WiFi");
   while (WiFi.status() != WL_CONNECTED) {
     delay(500);
     Serial.print(".");
   }
 
   Serial.println("\nConnected to WiFi!");
   Serial.print("IP Address: ");
   Serial.println(WiFi.localIP());
 
   // Configurare NTP (GMT+2 România)
   configTime(7200, 0, "pool.ntp.org", "time.nist.gov");
 
   Serial.println("Aștept sincronizarea cu NTP...");
   time_t now = time(nullptr);
   while (now < 100000) { // Așteaptă până primește un timp valid (> 1970-01-01)
     Serial.print(".");
     delay(500);
     now = time(nullptr);
   }
   Serial.println("\nSincronizare completă!");
 
   SPI.begin();
   mfrc522.PCD_Init();
   Serial.println("Apropie un tag RFID...");
 }
 
 
 void loop() {
   Serial.println("Aștept tag RFID...");
 
   if (!mfrc522.PICC_IsNewCardPresent() || !mfrc522.PICC_ReadCardSerial()) {
     delay(1000);
     return;
   }
 
   // Citește UID-ul cardului
   String uid = "";
   for (byte i = 0; i < mfrc522.uid.size; i++) {
     uid += String(mfrc522.uid.uidByte[i], HEX);
   }
   uid.toUpperCase();
 
   Serial.print("Tag UID: ");
   Serial.println(uid);
 
   // Obține data curentă
   String currentDateTime = getCurrentDateTime();
 
   // Trimite UID-ul și data la server
   sendDataToServer(uid, currentDateTime);
 
   mfrc522.PICC_HaltA();
   delay(5000);  // Pauză între citiri
 }
 
 
 String getCurrentDateTime() {
   time_t now = time(nullptr);
   if (now < 100000) { // Dacă ora nu este validă, returnează un mesaj de eroare
     return "NTP ERROR";
   }
   
   struct tm *timeinfo = localtime(&now);
   char buffer[25];
   strftime(buffer, sizeof(buffer), "%Y-%m-%dT%H:%M:%S", timeinfo);
   
   return String(buffer);
 }
 
 
 // Funcție pentru trimiterea datelor la server
 void sendDataToServer(String uid, String collectionTime) {
   if (WiFi.status() != WL_CONNECTED) {
     Serial.println("WiFi not connected!");
     return;
   }
 
   WiFiClient client;
   HTTPClient http;
 
   // Construire URL API
   String url = "http://" + String(SERVER_IP) + ":" + String(SERVER_PORT) + "/api/Date";
   Serial.print("Connecting to: ");
   Serial.println(url);
 
   http.begin(client, url);
   http.addHeader("Content-Type", "application/json");
 
   // Construire payload JSON
   String jsonPayload = "{\"IdPubela\": \"" + uid + "\", \"CollectionTime\": \"" + collectionTime + "\"}";
   Serial.print("Sending payload: ");
   Serial.println(jsonPayload);
 
   // Trimite cererea POST
   int httpCode = http.POST(jsonPayload);
 
   if (httpCode > 0) {
     Serial.printf("[HTTP] Response code: %d\n", httpCode);
     Serial.println("Server response: " + http.getString());
   } else {
     Serial.printf("[HTTP] POST failed, error: %s\n", http.errorToString(httpCode).c_str());
   }
 
   http.end();
 }