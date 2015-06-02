#define GRAPH_FB "graph.facebook.com"
typedef struct FbStatus{
	char* message;
	char* Id;
} FbStatus;

int String2Char(string s,char a[]);
char * String2CharPointer(string s);
FbStatus GetFBStatuses(string id, string field, string token);
int UpStatusFb(string paramId, string paramMessage, string paramToken);
void DecodeMessage(char* fdMessage, string  &decodedMessage);
void DebugChar();