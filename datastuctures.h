//datastructure.h
//data structures for Lync serial tester

#define MAX_NUM_OF_ARGS 0x04


struct tLyncCommandStruct
{
	unsigned char command;
	unsigned char numOfArgs;
	unsigned char args[MAX_NUM_OF_ARGS];
};

struct tLyncCommandArgs
{
	unsigned char commands;
	unsigned char numOfArgs;
};


struct tLyncCommandHelp
{
	unsigned char command;
	const char* commandName;
	const char* args[MAX_NUM_OF_ARGS];
};

enum eCOMMAND_LIST
{
	COMMAND_LIST_START  = 0x00,

	COMMAND_KEEP_ALIVE  = COMMAND_LIST_START,
	COMMAND_SET_COLOR,
	COMMAND_CLEAR_COLOR,
	COMMAND_DEBUG_MODE,
	COMMAND_INPUT_MODE,

	COMMAND_MAX_NUM_OF_LIST
};

enum eINPUT_MODE
{
	INPUT_MODE_START  = 0x00,

	INPUT_MODE_SEIRAL  = INPUT_MODE_START,
	INPUT_MODE_RF,

	INPUT_MODE_MAX_NUM
};
