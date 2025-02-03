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
  VERDICT_1,      // PC donne verdict 1
  VERDICT_2,      // PC donne verdict 2
  VERDICT_3       // PC donne verdict 3
};
static State workState = UNDEFINED;

// =============== PINS ===============
const int pinStartRobot = 8; // Pin détection objet (DI3)
const int pin2 = 12; // Pin objet 2 (DI2)
const int pin1 = 13; // Pin objet 1 (DI1)
const int pinIsMoving = 7;  // Pin état de déplacement (DO1)

// =============== INITIALISATION ===============
void setup() 
{
  Serial.begin(9600); // Init la com série
  
  pinMode(pinStartRobot, OUTPUT);  // Detection objet
  digitalWrite(pinStartRobot, LOW);
  pinMode(pin1, OUTPUT); // Position déplacement objet
  digitalWrite(pin1, LOW);
  pinMode(pin2,OUTPUT); // Position déplacement objet
  digitalWrite(pin2, LOW);

  pinMode(pinIsMoving, INPUT); // État déplacement
}

// =============== FUNCTIONS ===============
void updateState()
{
  long dist = ultrasonic.read();
  String data = Serial.readStringUntil('\n');
  bool isMoving = digitalRead(pinIsMoving) == HIGH;

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
      if (data == "1")
      {
        workState = VERDICT_1;
      }
      else if (data == "2")
      {
        workState = VERDICT_2;
      }
      else if (data == "3")
      {
        workState = VERDICT_3;
      }
      break;

    case VERDICT_1:
      workState = UNDEFINED; // Reset to initial state after verdict
      break;

    case VERDICT_2:
      workState = UNDEFINED; // Reset to initial state after verdict
      break;

    case VERDICT_3:
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

void send1()
{
  digitalWrite(pin1, HIGH);
  digitalWrite(pin2, LOW);
  delay(1000);  // Attend une seconde
  digitalWrite(pin1, LOW);
}

void send2()
{
  digitalWrite(pin1, LOW);
  digitalWrite(pin2, HIGH);
  delay(1000);  // Attend une seconde
  digitalWrite(pin2, LOW);
}

void send3()
{
  digitalWrite(pin1, HIGH);
  digitalWrite(pin2, HIGH);
  delay(1000);  // Attend une seconde
  digitalWrite(pin1, LOW);
  digitalWrite(pin2, LOW);
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

    case VERDICT_1:
      send3();
      break;
    
    case VERDICT_2:
      send2();
      break;

    case VERDICT_3 :
      send3();
      break;

    default:
      break;
  }
}
