// =============== CONSTANTS ===============
#include "Ultrasonic.h"

Ultrasonic ultrasonic(4, 2); // Trig et Echo

// Énum des états (machine d’état)
enum State 
{
  UNDEFINED,        // Placeholder
  OBJECT_DETECTED, 	// Detection objet
  WAITING_MOVE,     // Robot en mouvement
  OBJECT_CAM,  	    // Objet devant la caméra
  VERDICT_OK,       // PC donne verdict OK
  VERDICT_NOK       // PC donne verdict NOK
};
static State workState = UNDEFINED;

// =============== PINS ===============
const int pinStartRobot = 8; // Pin détection objet
const int pinNOK = 12; // Pin objet nOK
const int pinOK = 13; // Pin objet OK
const int pinIsMoving = 7;  // Pin état de déplacement

// =============== INITIALISATION ===============
void setup() 
{
  Serial.begin(9600); // Init la com série
  
  pinMode(pinStartRobot, OUTPUT);  // Detection objet
  digitalWrite(pinStartRobot, LOW);
  pinMode(pinOK, OUTPUT); // Position déplacement objet
  digitalWrite(pinOK, LOW);
  pinMode(pinNOK,OUTPUT); // Position déplacement objet
  digitalWrite(pinNOK, LOW);

  pinMode(pinIsMoving, INPUT); // État déplacement
}

// =============== FUNCTIONS ===============
void updateState()
{
  long dist = ultrasonic.read();
  String data = Serial.readStringUntil('\n');
  bool isMoving = digitalRead(pinIsMoving) == HIGH;

  Serial.println(dist);

  switch (workState)
  {
    case UNDEFINED:
      if (dist > 0 && dist < 10) 
      {
        workState = OBJECT_DETECTED;
      }
      break;

    case OBJECT_DETECTED:
      if (isMoving)
      {
        workState = WAITING_MOVE;
      }
      break;

    case WAITING_MOVE:
      if (!isMoving)
      {
        workState = OBJECT_CAM;
      }
      break;

    case OBJECT_CAM:
      if (data == "OK")
      {
        workState = VERDICT_OK;
      }
      else if (data == "NOK")
      {
        workState = VERDICT_NOK;
      }
      break;

    case VERDICT_OK:
      workState = UNDEFINED; // Reset to initial state after verdict
      break;

    case VERDICT_NOK:
      workState = UNDEFINED; // Reset to initial state after verdict
      break;

    default:
      break;
  }
}

void startRobot()
{
  digitalWrite(pinStartRobot, HIGH);
  delay(1000);  // Attend une seconde
  digitalWrite(pinStartRobot, LOW);
}

void sendOK()
{
  digitalWrite(pinOK, HIGH);
  delay(1000);  // Attend une seconde
  digitalWrite(pinOK, LOW);
}

void sendNOK()
{
  digitalWrite(pinNOK, HIGH);
  delay(1000);  // Attend une seconde
  digitalWrite(pinNOK, LOW);
}

void notifyObjectCam()
{
  Serial.println("X");
}

// =============== MAIN ===============
void loop() 
{
  updateState();

  switch(workState)
  {
    case OBJECT_DETECTED:
      startRobot();
      break;

    case WAITING_MOVE:
      // Do nothing
      break;

    case OBJECT_CAM:
      notifyObjectCam();
      break;

    case VERDICT_OK:
      sendOK();
      break;
    
    case VERDICT_NOK:
      sendNOK();
      break;

    default:
      break;
  }
}
