//serialLyncTester.ino
//Make to test the LyncStatus module.
//Author: Dotan Hofling (dotanitis@gmail.com)

//This section needed to test the whole protocol for lync status
//indication which is format in the folloing structure:
// <ID>       : byte, id of the reciever
// <Command>  : byte, number of commnad
// <NumOfArgs>: byte, number of arguments (is needed?)
// <arg1.argn>: byte(s), arguments as arguments

// We need to remember that the goal of this code is to communicate with the main board and make it test the leds and serial interface;
// This program will be a skelaton for the actual trasmitter which will get the siganls from the HOST computer and send it via RF(VirtualWire lib)

// Pin    I/O    wired_to
// -----    -----   --------------
// 3         O        right_Red
// 5         O        right_Green
// 6         O        right_blue
// 9         O        left_Red
// 10       O        left_Green
// 11       O        left_blue
// 12       I        data_RF

#include <Arduino.h>
#include <VirtualWire.h>
#include "datastuctures.h"

#define MAX_CHARS_IN_NUMBER 4
int incomingByte = 0;
byte currentID = 0x9;
byte MIN_BYTE=0x00;
byte MAX_BYTE=0x7f;
boolean isDebugMode = true;
eINPUT_MODE input_mode = INPUT_MODE_RF;
uint8_t buf[VW_MAX_MESSAGE_LEN];
uint8_t buflen = VW_MAX_MESSAGE_LEN;
int bufLocation = 0;



int l_b = 11;
int l_g = 10;
int l_r = 9;

int r_b = 6;
int r_g = 5;
int r_r = 3;


tLyncCommandArgs commandsArgs[] =
{
	{COMMAND_KEEP_ALIVE, 0},
	{COMMAND_SET_COLOR, 4},
	{COMMAND_CLEAR_COLOR, 1},
	{COMMAND_DEBUG_MODE,1},
	{COMMAND_INPUT_MODE,1}
};

tLyncCommandHelp commandHelp[] =
{
	{COMMAND_KEEP_ALIVE, "COMMAND_KEEP_ALIVE",   {"","","",""}},
	{COMMAND_SET_COLOR, "COMMAND_SET_COLOR",     {"ORIENTATION","R","G","B"}},
	{COMMAND_CLEAR_COLOR, "COMMAND_CLEAR_COLOR", {"ORIENTATION","","",""}},
	{COMMAND_DEBUG_MODE, "COMMAND_DEBUG_MODE", {"ENA_DIS","","",""}},
	{COMMAND_INPUT_MODE, "COMMAND_INPUT_MODE", {"SER_RF","","",""}}
};

boolean validateVariable(byte variable, byte minVal, byte maxVal)
{
    return ((variable >= minVal) && (variable < maxVal));
}

void echoCommand(tLyncCommandStruct* command)
{
    byte argsCounter;
    byte argsNumber;

    Serial.print("ID:");
    Serial.println(currentID,HEX);
    Serial.print("Command:");
    Serial.println(command->command,HEX);

    //Serial.println("Command:%s",commandHelp[command->command].commandName);
    //argsNumber = command->numOfArgs;
    for (argsCounter=0; argsCounter<command->numOfArgs; argsCounter++)
    {
        Serial.print(argsCounter,DEC);
        Serial.print(":");
        Serial.println((byte)command->args[argsCounter],DEC);
    }
}

int readInput()
{
    int ans = 0;
    char number[] = {0x00,0x00,0x00,0x00};//max is 255_ ascii
    byte numberLocation = 0;
    boolean foundNumber = false;
    switch(input_mode)
    {
    case INPUT_MODE_SEIRAL:
        if (Serial.available() > 0)
        {
            ans = Serial.parseInt();
        }

        break;
    case INPUT_MODE_RF:
        for (numberLocation = 0; !(foundNumber) && numberLocation<MAX_CHARS_IN_NUMBER;numberLocation++)
        {
            number[numberLocation] = buf[bufLocation];
            foundNumber = (buf[bufLocation] == 0x5f) ? true : false;
            if (isDebugMode)
            {
                Serial.print(bufLocation,DEC);
                Serial.print(',');
                Serial.print(numberLocation,DEC);
                Serial.print(',');
                Serial.print(number[numberLocation],HEX);
                Serial.print(',');
                Serial.print(validateVariable(number[numberLocation],'0','9'),DEC);
                Serial.print(',');
                Serial.println(foundNumber,DEC);
            }
            bufLocation++;
        }
        ans = atoi(number);

        if (isDebugMode)
        {
            Serial.print("Found:");
            Serial.println(ans,DEC);
        }

        break;
    default:
        break;

    }
    return ans;
}

boolean validateCommand(tLyncCommandStruct* command)
{
    int argsCounter;

	boolean ans = true;

	if (command==0) return false;

	command->command = readInput(); //<COMMAND>
	ans &= validateVariable(command->command, COMMAND_LIST_START, COMMAND_MAX_NUM_OF_LIST);

	if (ans)
	{
		command->numOfArgs = readInput(); //<NumberOfArgs>

		if (ans)
        {
            for (argsCounter=0; argsCounter<command->numOfArgs; argsCounter++)
            {
                command->args[argsCounter] = readInput();
            }
        }
	}

	return ans;
}

void setColor(int ledDir, int red, int green, int blue)
{
  if (ledDir == 0)
  {
    analogWrite(l_r,red);
    analogWrite(l_g,green);
    analogWrite(l_b,blue);
  }
  else
  {
    analogWrite(r_r,red);
    analogWrite(r_g,green);
    analogWrite(r_b,blue);
  }
}

//This exceute function will be reused in the reciver section which will actually
//turn on the leds,
//In the Transmitter the execute will transform the command into bitstream which
//will be sent over RF
void excecuteCommand(tLyncCommandStruct* command)
{
    if (command==0) return;

    switch (command->command)
    {
    case COMMAND_KEEP_ALIVE:
            setColor(0,0,255,0);
            setColor(1,0,255,0);
            delay(100);
            setColor(0,0,0,0);
            setColor(1,0,0,0);
            break;
    case COMMAND_SET_COLOR:
            setColor(0,command->args[1],
                       command->args[2],
                       command->args[3]);

            setColor(1,command->args[1],
                       command->args[2],
                       command->args[3]);
            break;
    case COMMAND_CLEAR_COLOR:
            setColor(0,0,0,0);
            setColor(1,0,0,0);
        break;
    case COMMAND_DEBUG_MODE:
            isDebugMode = (command->args[1]==0x01) ? true : false;
        break;
    case COMMAND_INPUT_MODE:
            input_mode = (command->args[1] == 0x00) ? INPUT_MODE_SEIRAL : INPUT_MODE_RF;
        break;
    default:
        break;
    }
}



void setup()
{
	Serial.begin(9600);

	pinMode(l_r, OUTPUT);
    pinMode(l_g, OUTPUT);
    pinMode(l_b, OUTPUT);
    pinMode(r_r, OUTPUT);
    pinMode(r_g, OUTPUT);
    pinMode(r_b, OUTPUT);

    // Initialise the IO and ISR
    vw_set_ptt_inverted(false);
    vw_set_rx_pin(12);
    vw_setup(2000);	 // Bits per sec

    vw_rx_start(); // Start the receiver PLL running
}

void loop()
{

    if (((input_mode == INPUT_MODE_SEIRAL) && (Serial.available() > 0)) ||
        ((input_mode == INPUT_MODE_RF) && (vw_get_message(buf, &buflen))))
    {
        boolean isCommandValid = true;
        bufLocation = 0;
        int i = 0;

        tLyncCommandStruct command = {0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
        Serial.println("/nNew Trasnmittion:");
        Serial.print("Buffer Length:");
        Serial.print(buflen,DEC);

        for ( i=0; i<buflen; i++)
        {
            Serial.print(buf[i],HEX);
            Serial.print(',');
        }
        Serial.println("/n");

        incomingByte = readInput(); // <ID>;

        //1. Check if the adress (ID) is correct
        isCommandValid &= (currentID == incomingByte);

        if (isDebugMode)
        {
            Serial.println(incomingByte,DEC);
        }


        //2. Validate the commands
        if (isCommandValid)
        {
            isCommandValid &= validateCommand(&command);
        }
        else
        {
            if (isDebugMode)
            {
                Serial.println("ID Is Invalid");
            }
        }

        //3. Execute command
        if (isCommandValid)
        {
            if (isDebugMode)
            {
                //echoCommand(&command);
            }
            excecuteCommand(&command);
        }
    }


/*
    uint8_t buf[VW_MAX_MESSAGE_LEN];
    uint8_t buflen = VW_MAX_MESSAGE_LEN;

    if (vw_get_message(buf, &buflen)) // Non-blocking
    {
	int i;

        digitalWrite(10, true); // Flash a light to show received good message
	// Message with a good checksum received, dump it.
        digitalWrite(5, true);
	Serial.print("Got: ");

	for (i = 0; i < buflen; i++)
	{
	    Serial.print(buf[i], HEX);
	    Serial.print(" ");
	}
	Serial.println("");
        digitalWrite(10, false);
                digitalWrite(5, false);
    }
*/

}
