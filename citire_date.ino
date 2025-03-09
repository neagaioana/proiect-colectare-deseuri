#include <SPI.h>
#include <MFRC522.h>

#define SS_PIN D4   
#define RST_PIN D3  
#define LED_PIN D2  

MFRC522 mfrc522(SS_PIN, RST_PIN); 
unsigned long lastPrintTime = 0;

void setup() {
  Serial.begin(115200); 
  SPI.begin();          
  mfrc522.PCD_Init();   
  pinMode(LED_PIN, OUTPUT);
  Serial.println("Apropie tagul RFID.");
}

void loop() {
  if (millis() - lastPrintTime > 2000) {  
    Serial.println("Aștept tagul RFID.");
    lastPrintTime = millis();
  }

  if (!mfrc522.PICC_IsNewCardPresent()) {
    digitalWrite(LED_PIN, LOW); //stingere led cand nu e detectat tagul
    return;
  }
  
  if (!mfrc522.PICC_ReadCardSerial()) {
    Serial.println("Eroare la citirea tagului.");
    return;
  }

  //ledul se aprinde cand tagul e detectat
  digitalWrite(LED_PIN, HIGH);

  //convertire uid in string
  String uidString = "";
  Serial.print("Tag UID: ");
  for (byte i = 0; i < mfrc522.uid.size; i++) {
    Serial.print(mfrc522.uid.uidByte[i] < 0x10 ? " 0" : " ");
    Serial.print(mfrc522.uid.uidByte[i], HEX);
    uidString += String(mfrc522.uid.uidByte[i], HEX);
  }
  Serial.println();
  
  Serial.print("UID String: ");
  Serial.println(uidString); 

  mfrc522.PICC_HaltA(); 
}
