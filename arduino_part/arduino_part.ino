// CONSTANTES
// Énum des états (machine d’état)
enum State 
{
  onWaitButton, 	// En attente d'appui sur le bouton
  onWaitRobot,  	// En attente de la réponse du robot
  onWaitProcess,	// En attente du traitement d’image
};

//—------- PINS —--------
const int pinPriseObject = 3;  // Pin bouton
const int pinPosObject = 9;  // Pin AN1

//—------ AUTRES --------
State robotState = onWaitButton;


// INITIALISATION
void setup() 
{
  Serial.begin(9600); // Init la com série
  pinMode(pinStartRobot, OUTPUT);  // Démarrage robot = sortie
  pinMode(robotStatePin, INPUT); // État robot = entrée

  digitalWrite(pinStartRobot, LOW);
}

// MAIN
void loop() 
{
  switch(workState)
  {
  case objectDetect:
    if (Serial.available() > 0) {  // Vérifier si des données sont disponibles
      String data = Serial.readStringUntil('\n');  // Lire la chaîne envoyée
      if (data=="XXX"){
        digitalWrite(pinStartRobot, HIGH);
        delay(1000);  // Attend une seconde
        digitalWrite(pinStartRobot, LOW);
      }
      else {
      Serial.println("Error !!!");
      }
    }
    else {
      Serial.println("Error !!!");
    }
  case objectCam:
    if (Serial.available() > 0) {  // Vérifier si des données sont disponibles
      Serial.println("XXX");
    }
    else {
      Serial.println("Error !!!");
    }
  case objectVerdict:
    if (data=="XXX1"){
      digitalWrite(pinStartRobot, HIGH);
      delay(1000);  // Attend une seconde
      digitalWrite(pinStartRobot, LOW);
    }
    else if (data=="XXX2"){
      digitalWrite(pinStartRobot, HIGH);
      delay(1000);  // Attend une seconde
      digitalWrite(pinStartRobot, LOW);
    }
  }
}
