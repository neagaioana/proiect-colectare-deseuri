#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <SPI.h>
#include <MFRC522.h>

// Configurare pinii RC522
#define SS_PIN D4  // SS (SDA) conectat la D4 (GPIO2)
#define RST_PIN D3 // RST conectat la D3 (GPIO0)

// Configurare WiFi
#define STASSID "OMiLAB"      //  Introdu numele rețelei WiFi
#define STAPSK "digifofulbs"   // Introdu parola WiFi
#define SERVER_IP "10.14.10.113" // IP-ul serverului API (verifică dacă este corect)
#define SERVER_PORT 3000         // Portul serverului API

MFRC522 mfrc522(SS_PIN, RST_PIN); // Creare obiect MFRC522

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

  SPI.begin();          // Inițializare SPI
  mfrc522.PCD_Init();   // Inițializare RC522
  Serial.println("Apropie un tag RFID...");
}

void loop() {
  Serial.println("Aștept tag RFID...");

  // Verifică dacă există un card RFID nou
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

  // Trimite UID-ul către server
  sendDataToServer(uid);

  mfrc522.PICC_HaltA();
  delay(5000); // Pauză între citiri
}

// Funcție pentru trimiterea datelor către API
void sendDataToServer(String uid) {
  if (WiFi.status() != WL_CONNECTED) {
    Serial.println("WiFi not connected!");
    return;
  }

  WiFiClient client;
  HTTPClient http;

  // Construire URL API
  String url = "http://" + String(SERVER_IP) + ":" + String(SERVER_PORT) + "/api/data";
  Serial.print("Connecting to: ");
  Serial.println(url);

  http.begin(client, url);
  http.addHeader("Content-Type", "application/json");

  // Construire payload JSON
  String jsonPayload = "{\"id\": \"" + uid + "\", \"name\": \"Popa Daniel\"}";
  
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
