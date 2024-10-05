#include <windows.h>
#include <process.h>

unsigned __stdcall ThreadFunc(void *arg) // Функция потока
{
  char **str = (char**) arg;
  MessageBox(0, str[0], str[1], 0);
  _endthreadex(0);
  return 0;
};

int main(int argc, char* argv[])
{
  char *InitStr1[2] = {"First thread running!","11111"};// строка для первого потока
  char *InitStr2[2] = {"Second thread running!","22222"};// строка для второго потока
  unsigned uThreadIDs[2];

  HANDLE hThreads[2];
  hThreads[0] = (HANDLE) _beginthreadex(NULL, 0, &ThreadFunc, InitStr1, 0, &uThreadIDs[0]);
  hThreads[1] = (HANDLE) _beginthreadex(NULL, 0, &ThreadFunc, InitStr2, 0, &uThreadIDs[1]);

  // Ждем, пока потоки не завершат свою работу
  WaitForMultipleObjects(2, hThreads, TRUE, INFINITE ); // Set no time-out
  // Закрываем дескрипторы
  CloseHandle(hThreads[0]);
  CloseHandle(hThreads[1]);
  return 0;
}