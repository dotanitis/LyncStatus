// transmitterLync.pde
//
// This is implementetion for sending the status of Lync
// Presence for the led reciver
// Message format:
// <ID> <command> <arg1> <arg2> ... <argN>


#include <VirtualWire.h>

char msg[VW_MAX_MESSAGE_LEN];
uint8_t msgCounter = 0;


void setup()
{
    Serial.begin(9600);	  // Debugging only
    //Serial.println("setup");
    // Initialise the IO and ISR
    vw_set_ptt_inverted(false); // Required for DR3100
    vw_setup(2000);	 // Bits per sec
}

void loop()
{
    int num;
    boolean doTransmit = false;

    if (Serial.available() > 0)
    {
        digitalWrite(13, true); // Flash a light to show transmitting
        while(!doTransmit)
        {
            num = Serial.available();

            if (num == 0)
            {
                break;
            }
            else
            {
                msg[msgCounter] = Serial.read();
                if (msg[msgCounter] == 0x7c)
                {
                    Serial.println("");
                    doTransmit = true;
                    break;
                }
                else
                {
                    Serial.print(msg[msgCounter],HEX);
                    msgCounter++;
                }
            }
        }

        if (doTransmit == true)
        {
            vw_send((uint8_t *)msg, msgCounter);
            vw_wait_tx(); // Wait until the whole message is gone
            digitalWrite(13, false);
            msgCounter = 0; // prepare for new message
        }
    }
}
